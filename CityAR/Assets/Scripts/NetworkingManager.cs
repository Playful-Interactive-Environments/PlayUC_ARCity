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
    public Text DebugText;
    public GameObject GameManager;
    
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
            HostButton.interactable = true;

    }
    public void StartupHost()
    {
        SetPort();
        StartHost();
        GameObject gamemng = Instantiate(GameManager, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        NetworkServer.Spawn(gamemng);
        isServer = true;
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
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("OnServerConnect" + conn.connectionId);
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
        Debug.Log("OnServerDisconnect " + conn.connectionId);
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


    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        if (!isServer)
        {
            DebugText.text = "Connected. IP: " + Network.player.ipAddress;
            isClient = true;
            Debug.Log("OnClientConnect " + conn);
        }
            UIManager.Instance.RoleUI();
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        Debug.Log("OnStartClient");
        client.RegisterHandler(MsgType.Error, OnError);
    }

    public override void OnStopClient()
    {

        base.OnStopClient();
        ClientScene.DestroyAllClientObjects();
        ClientScene.ClearSpawners();
        isClient = false;

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("OnClientDisconnect " + conn.connectionId);
        ReconnectClient();
    }

    public void ReconnectClient()
    {
        StopClient();
        StartCoroutine("Reconnect");
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);

        Debug.Log(errorCode);
    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {
        base.OnServerError(conn, errorCode);
        Debug.Log(errorCode);
    }
    void OnError(NetworkMessage netMsg)
    {
        var errorMsg = netMsg.ReadMessage<ErrorMessage>();
        Debug.Log(errorMsg);
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

/*
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);

            GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            Debug.Log("OnServerAddPlayer " + playerControllerId);
    }

    }
*/
