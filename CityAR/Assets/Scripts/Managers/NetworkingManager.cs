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
    private bool tryConnect;
    public InputField IPInput;
    public Button HostButton;
    public Button AutoConnectButton;
    public Text DebugText;
    public Text IpText;
    public GameObject GameManagerPrefab;
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
        networkPort = ConnectionPort;
    }

    void Start()
    {
        connectionConfig.AcksType = ConnectionAcksType.Acks128;
        connectionConfig.MaxSentMessageQueueSize = 1024;
        connectionConfig.MaxCombinedReliableMessageCount = 1;
        IpText.text = "My IP: " + Network.player.ipAddress;

    }

    void Update()
    {

    }

    public void StartupHost()
    {
        StopHosting();
        StartHost();
        GameObject gamemng = Instantiate(GameManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(gamemng);
        isServer = true;
        broadcast.StartServerBroadcast();
        if (listen.listenStarted)
        {
            listen.StopListenning();
            AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Search;
        }
        GameManager.Instance.ClientsConnected += 1;
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
            StopHost();
            StopClient();
        }
        if (listen.listenStarted)
            listen.StopListenning();
        if (broadcast.broadcastStarted)
            broadcast.StopBroadcasting();
        StopAllCoroutines();
        NetworkServer.Reset();
        AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Search;
        AutoConnectButton.interactable = true;
        HostButton.interactable = true;
        UIManager.Instance.ResetMenus();
    }

    #region Server 
    public override void OnStartHost()
    {
        base.OnStartServer();
        //Debug.Log("Server IP: " + Network.player.ipAddress);
        IpText.text = "My IP: " + Network.player.ipAddress;
        UIManager.Instance.RoleUI();
        AutoConnectButton.interactable = false;
        HostButton.interactable = false;
        DebugText.text = "OnStartHost";

    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        isServer = false;
        LocalManager.Instance.GameRunning = false;
        broadcast.StopBroadcasting();
        AutoConnectButton.interactable = true;
        HostButton.interactable = true;
        if (listen.listenStarted)
        {
            listen.StopListenning();
        }
        Debug.Log("Server Stopped");
        DebugText.text = "OnStopServer";


    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("OnServerConnect " + conn.connectionId);
        if (GameManager.Instance != null)
            GameManager.Instance.ClientsConnected += 1;
        DebugText.text = "OnServerConnect";

    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        base.OnServerAddPlayer(conn, playerControllerId);
        Debug.Log("OnAddPlayer");
        DebugText.text = "OnServerAddPlayer" + conn.connectionId;

    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
        NetworkServer.DestroyPlayersForConnection(conn);
        Debug.Log("OnServerRemovePlayer " + player);
        EventDispatcher.TriggerEvent(Vars.ServerHandleDisconnect);

        DebugText.text = "OnServerRemovePlayer" + conn.lastError;

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        // IpText.text = "Server Disconnected" + conn.lastError;
        SaveStateManager.Instance.SetTaken(conn.connectionId, false);
        EventDispatcher.TriggerEvent(Vars.ServerHandleDisconnect);
        GameManager.Instance.ClientsConnected -= 1;
        DebugText.text = "OnServerDisconnect" + conn.lastError;

    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {
        base.OnServerError(conn, errorCode);
        Debug.Log(errorCode);
        EventDispatcher.TriggerEvent(Vars.ServerHandleDisconnect);

        DebugText.text = "OnServerError" + conn.lastError;

    }
    #endregion

    #region Client

    public void AutoConnect()
    {
        if (isNetworkActive)
            StopHosting();
        StopAllCoroutines();
        StartCoroutine(TryConnect());
    }

    IEnumerator TryConnect()
    {
        AutoConnectButton.interactable = false;
        HostButton.interactable = false;
        listen.StartClientListen();

        int loopT = 3;
        for (int i = 0; i <= loopT; i++)
        {
            yield return new WaitForSeconds(Mathf.Round(loopT / 3));
            AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Searching + ".";
            yield return new WaitForSeconds(Mathf.Round(loopT / 3));
            AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Searching + "..";
            yield return new WaitForSeconds(Mathf.Round(loopT / 3));
            AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Searching + "...";
            if (listen.serverIP != "" && !tryConnect)
            {
                ConnectionIP = listen.serverIP;
                IPInput.text = listen.serverIP;
                AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Connecting + ".";
                yield return new WaitForSeconds(.3f);
                AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Connecting + "..";
                yield return new WaitForSeconds(.3f);
                AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Connecting + "...";
                yield return new WaitForSeconds(.3f);
                UIManager.Instance.RoleUI();
                StartClient();
                listen.StopListenning();
                StopAllCoroutines();
            }
            if (SetIPAddress() && !tryConnect)  
            {
                StartClient();
                tryConnect = true;
                yield return new WaitForSeconds(.5f);
            }
            if (tryConnect)
            {
                if (IsClientConnected())
                {
                    AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.ServerFound;
                    tryConnect = false;
                    yield return new WaitForSeconds(2f);
                    UIManager.Instance.RoleUI();
                    DebugText.text = "manual successful";
                    listen.StopListenning();
                    StopAllCoroutines();
                }
                if (!IsClientConnected())
                {
                    tryConnect = false;
                    StopClient();
                    DebugText.text = "manual fail";
                }
            }
        }
        if (!IsClientConnected())
        {
            AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.NoServer;
            yield return new WaitForSeconds(2f);
            AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Search;
            AutoConnectButton.interactable = true;
            HostButton.interactable = true;
            listen.StopListenning();
        }
    }

    public bool SetIPAddress()
    {
        ConnectionIP = IPInput.text;
        System.Net.IPAddress aIP;

        if (!System.Net.IPAddress.TryParse(ConnectionIP, out aIP))
        {
            IPInput.text = "INVALID IP!";
            return false;
        }
        else
        {
            networkAddress = ConnectionIP;
            return true;
        }
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        if (!isServer)
        {

            IpText.text = "My IP: " + Network.player.ipAddress;
            isClient = true;
            AutoConnectButton.interactable = false;
            HostButton.interactable = false;
            AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Search;
            Debug.Log("OnClientConnect " + conn);
            DebugText.text = "Client Connected";

        }
    }

    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        //Debug.Log("OnStartClient");
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        ClientScene.DestroyAllClientObjects();
        ClientScene.ClearSpawners();
        isClient = false;
        if (IsClientConnected())
        {
            AutoConnectButton.interactable = true;
            HostButton.interactable = true;
            EventDispatcher.TriggerEvent(Vars.LocalClientDisconnect);
            DebugText.text = "Disconnected! Try Again! Code:1" ;

        }
        //Debug.Log("OnStopClient");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        //IpText.text = "Client Disconnected" + conn.lastError + conn.lastMessageTime;
       // StartCoroutine(Reconnect());
        AutoConnectButton.interactable = true;
        HostButton.interactable = true;
        LocalManager.Instance.GameRunning = false;
        UIManager.Instance.ResetMenus();
        Debug.Log("OnClientDisconnect");
        DebugText.text = "Disconnected! Try Again! Code:2" + conn.lastError;
        EventDispatcher.TriggerEvent(Vars.LocalClientDisconnect);
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
        Debug.Log("OnClientError");
        AutoConnectButton.interactable = true;
        HostButton.interactable = true;
        DebugText.text = "Disconnected! Try Again! Code:3" + conn.lastError;
        EventDispatcher.TriggerEvent(Vars.LocalClientDisconnect);

    }
    #endregion
}