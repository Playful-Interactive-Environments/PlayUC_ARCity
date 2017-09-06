using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class NetMng : NetworkManager
{
    public static NetMng Instance;
    public NetworkDiscovery Discovery;
    public GameObject GameManagerPrefab;
    public InputField IPInput;
    public Text DebugText;
    public Text IpText;
    public Button HostButton;
    public Button AutoConnectButton;
    public bool isServer;
    public bool isClient;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Update()
    {

    }

    public void StartGame()
    {
        isServer = true;
        StartHost();
        GameObject gamemng = Instantiate(GameManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(gamemng);
        Discovery.Initialize();
        Discovery.StartAsServer();
    }

    public void SearchGame()
    {
        StartCoroutine(CheckConnection());

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
        // ResetBroadcasting();
        StopAllCoroutines();
        NetworkServer.Reset();
        AutoConnectButton.GetComponentInChildren<Text>().text = TextManager.Instance.Search;
        EnableButtons();
        UIManager.Instance.ResetMenus();
    }

    IEnumerator CheckConnection()
    {
        Discovery.Initialize();
        DisableButtons();
        yield return new WaitForSeconds(1f);
        Discovery.StartAsClient();

        yield return new WaitForSeconds(1f);
        if (IsClientConnected())
        {
            isClient = true;
            DebugText.text = "Connected";
        }
        else
        {
            DebugText.text = "Nothing found. Trying input";
            if (IPInput.text != null)
            {
                StopClient();
                Discovery.StopBroadcast();
                yield return new WaitForSeconds(.1f);
                networkAddress = IPInput.text;
                StartClient();
                yield return new WaitForSeconds(3f);
                if (IsClientConnected())
                {
                    isClient = true;
                    DebugText.text = "Connected";
                }
                else
                {
                    DebugText.text = "Nothing found. Try again";
                    StopClient();
                    EnableButtons();
                }
            }
        }
    }

    void EnableButtons()
    {
        AutoConnectButton.interactable = true;
        HostButton.interactable = true;
    }

    void DisableButtons()
    {
        AutoConnectButton.interactable = false;
        HostButton.interactable = false;
    }
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        IpText.text = "My IP: " + Network.player.ipAddress;
        UIManager.Instance.RoleUI();
        DisableButtons();
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        LocalManager.Instance.GameRunning = false;
        isServer = false;
        EnableButtons();
        Discovery.StopBroadcast();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        EventDispatcher.TriggerEvent(Vars.ServerHandleDisconnect);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        DisableButtons();
        UIManager.Instance.RoleUI();
        IpText.text = "My IP: " + Network.player.ipAddress;
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        LocalManager.Instance.GameRunning = false;
        UIManager.Instance.ResetMenus();
        EnableButtons();
        isClient = false;
        DebugText.text = "Disconnected! Try Again! Code: 2" + conn.lastError;
        EventDispatcher.TriggerEvent(Vars.LocalClientDisconnect);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        if (IsClientConnected())
        {
            isClient = false;
            EnableButtons();
            EventDispatcher.TriggerEvent(Vars.LocalClientDisconnect);
            DebugText.text = "Disconnected! Try Again! Code: 1";
        }
    }
}
