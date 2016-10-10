using System;
using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;

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
    public GameObject CellManager;
    public GameObject RoleManager;

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
        GameObject cellmng = Instantiate(CellManager, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        GameObject rolemng = Instantiate(RoleManager, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        NetworkServer.Spawn(cellmng);
        NetworkServer.Spawn(rolemng);
        isServer = true;
    }
    public void StopHosting()
    {
        if (isServer)
        {
            StopHost();
            NetworkServer.ClearLocalObjects();
            NetworkServer.ClearSpawners();
            NetworkServer.Reset();
            UIManager.Instance.ResetMenus();
        }
        if (isClient)
        {
            StopClient();
            NetworkServer.Reset();
            UIManager.Instance.ResetMenus();
        }
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
        Debug.Log("OnServerConnect");
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
        base.OnServerRemovePlayer(conn, player);
        NetworkServer.DestroyPlayersForConnection(conn);
        Debug.Log("OnServerRemovePlayer " + player);
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
        //debugTextClient.text = "Client Started";
        Debug.Log("OnStartClient");
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        DebugText.text = "Client Stopped";
        ClientScene.DestroyAllClientObjects();
        ClientScene.ClearSpawners();
        Debug.Log("OnStopClient");
        isClient = false;

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("OnClientDisconnect");
    }

    public void ReconnectClient()
    {
        StopClient();
        StartCoroutine("Reconnect");
    }

    IEnumerator Reconnect()
    {
        yield return new WaitForSeconds(1f);
        StartClient();

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
