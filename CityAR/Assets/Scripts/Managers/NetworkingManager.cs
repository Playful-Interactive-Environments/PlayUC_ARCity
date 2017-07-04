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
    public ConnectionConfig connConf;

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
        IpText.text = "My IP: " + Network.player.ipAddress;
        connectionConfig.AcksType = ConnectionAcksType.Acks128;
        connectionConfig.InitialBandwidth = 3000;
        connectionConfig.PingTimeout = 500;
        connectionConfig.DisconnectTimeout = 5000;
        connectionConfig.PacketSize = 1470;
        connectionConfig.InitialBandwidth = 5000;
        connectionConfig.NetworkDropThreshold = 50;
        connectionConfig.OverflowDropThreshold = 50;
    }

    void Update()
    {
    }

    public void StartupHost()
    {
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
        ResetBroadcasting();
        StopAllCoroutines();
        NetworkServer.Reset();
        AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Search;
        AutoConnectButton.interactable = true;
        HostButton.interactable = true;
        UIManager.Instance.ResetMenus();
    }

    #region Server 
    public override void OnStartServer()
    {
        base.OnStartServer();
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
        DebugText.text = "OnServerConnect";
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        // IpText.text = "Server Disconnected" + conn.lastError;
        //SaveStateManager.Instance.SetTaken(conn.connectionId, false);
        NetworkServer.SetClientNotReady(conn);
        NetworkServer.DestroyPlayersForConnection(conn);
        EventDispatcher.TriggerEvent(Vars.ServerHandleDisconnect);
        DebugText.text = "OnServerDisconnect" + conn.lastError;
    }
    #endregion

    #region Client
    public void AutoConnect()
    {
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

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        AutoConnectButton.interactable = true;
        HostButton.interactable = true;
        LocalManager.Instance.GameRunning = false;
        UIManager.Instance.ResetMenus();
        Debug.Log("OnClientDisconnect");
        DebugText.text = "Disconnected! Try Again! Code: 2" + conn.lastError;
        EventDispatcher.TriggerEvent(Vars.LocalClientDisconnect);
        Invoke("AutoConnect", 2f);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        isClient = false;
        if (IsClientConnected())
        {
            AutoConnectButton.interactable = true;
            HostButton.interactable = true;
            EventDispatcher.TriggerEvent(Vars.LocalClientDisconnect);
            DebugText.text = "Disconnected! Try Again! Code: 1";
        }
        //Debug.Log("OnStopClient");
    }
    public void ResetBroadcasting()
    {
        if (listen.listenStarted)
            listen.StopListenning();
        if (broadcast.broadcastStarted)
            broadcast.StopBroadcasting();
    }
    #endregion
}