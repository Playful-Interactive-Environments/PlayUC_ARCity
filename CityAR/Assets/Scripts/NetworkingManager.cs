using System;
using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking.NetworkSystem;

public class NetworkingManager : NetworkManager
{

    //Network Connection
    public string ConnectionIP;
    public int ConnectionPort = 7777;
    public bool ClientConnected = false;
    public static NetworkingManager Instance = null;             
    public bool isServer;
    public bool isClient;
    public InputField IPInput;
    public Button HostButton;
    public Button ConnectButton;
    public Button AutoConnectButton;
    public Text DebugText;
    public GameObject GameManager;
    public ServerBroadcast broadcast;
    public ClientListen listen;
    bool AutoConnectEnabled;
    private int _autoConnectAttempts;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (isServer || isClient)
        {
            ConnectButton.interactable = false;
        }
        else
        {
            ConnectButton.interactable = true;
        }
        if (isServer)
            HostButton.interactable = false;
        if (!isServer && !isClient)
        {
            HostButton.interactable = true;
        }
    }

    public void StartupHost()
    {
        SetPort();
        StartHost();
        GameObject gamemng = Instantiate(GameManager, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        NetworkServer.Spawn(gamemng);
        isServer = true;
        broadcast.StartServerBroadcast();
        if (listen.listenStarted)
        {
            listen.StopListenning();
            AutoConnectButton.GetComponentInChildren<Text>().text = "Find";
        }
    }

    public void StopHosting()
    {
        if (isServer)
        {
            StopHost();
            NetworkServer.ClearLocalObjects();
            NetworkServer.ClearSpawners();
        }
        if (isClient)
        {
            StopClient();
        }
        NetworkServer.Reset();
        UIManager.Instance.ResetMenus();

    }

    public void SetIPAddress()
    {
        ConnectionIP = IPInput.text;
        System.Net.IPAddress aIP;
        
        if (!System.Net.IPAddress.TryParse(ConnectionIP, out aIP))
        {
            DebugText.text = "INVALID IP ADDRESS";
            return;
        }
        else
        {
            networkAddress = ConnectionIP;
        }
    }

    void SetPort()
    {
        networkPort = ConnectionPort;
    }

    #region Server 
    public override void OnStartHost()
    {
        base.OnStartServer();
        Debug.Log("Server IP: " + Network.player.ipAddress);
        DebugText.text = "Server IP: " + Network.player.ipAddress;

    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        Debug.Log("Server Stopped");
        isServer = false;
        broadcast.StopBroadcasting();
        if (listen.listenStarted)
        {
            listen.StopListenning();
            AutoConnectButton.GetComponentInChildren<Text>().text = "Find";
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("OnServerConnect " + conn.connectionId);
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
        NetworkServer.DestroyPlayersForConnection(conn);
        Debug.Log("OnServerRemovePlayer " + player);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        DebugText.text = "Server Disconnected" + conn.lastError;
        GlobalManager.Instance.SetTaken(conn.connectionId, false);
    }
    #endregion

    #region Client
    public void JoinGame()
    {
        SetPort();
        SetIPAddress();
        StartClient();
    }

    public void AutoConnect()
    {
        listen.StartClientListen();
        InvokeRepeating("TryConnect", 0f, 1f);
    }

    public void TryConnect()
    {
        if (listen.serverIP != "")
        {
            ConnectionIP = listen.serverIP;
            IPInput.text = listen.serverIP;
            AutoConnectButton.GetComponentInChildren<Text>().text = "Found!";
            CancelInvoke("TryConnect");
        }
        else
        {
            AutoConnectButton.GetComponentInChildren<Text>().text = "Wait...";
        }

    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        UIManager.Instance.RoleUI();
        if (!isServer)
        {
            DebugText.text = "Connected. IP: " + Network.player.ipAddress;
            isClient = true;
            if (listen.listenStarted)
            {
                listen.StopListenning();
                AutoConnectButton.GetComponentInChildren<Text>().text = "Find";
            }
            Debug.Log("OnClientConnect " + conn);
        }
    }
    
    public override void OnStartClient(NetworkClient client)
    {
       base.OnStartClient(client);
        Debug.Log("OnStartClient");
        UIManager.Instance.RoleUI();
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        ClientScene.DestroyAllClientObjects();
        ClientScene.ClearSpawners();
        isClient = false;
        EventManager.TriggerEvent("NetworkDisconnect");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        DebugText.text = "Client Disconnected" + conn.lastError;
        ReconnectClient();
    }

    public void ReconnectClient()
    {
        StopClient();
        //StartCoroutine("Reconnect");
    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {
        base.OnServerError(conn, errorCode);
        Debug.Log(errorCode);
    }

    IEnumerator Reconnect()
    {
        yield return new WaitForSeconds(.2f);
        StartClient();
        yield return new WaitForSeconds(.2f);
        switch (LocalManager.Instance.RoleType)
        {
            case "Environment":
                UIManager.Instance.ChooseEnvironment();
                break;
            case "Finance":
                UIManager.Instance.ChooseFinance();
                break;
            case "Social":
                UIManager.Instance.ChooseSocial();
                break;
        }
    }
    #endregion
}