using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class ServerManager : NetworkManager
{

	public string ConnectionIP;
	public int ConnectionPort = 7777;
	public bool ClientConnected = false;
	public TextMesh debugTextClient;
	public static ServerManager Instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
	public bool isServer;
	public bool isClient;
	public Text debugTextServer;
	public GameObject _monumentRef;
	private GameObject _scalemanagerRef;
	private GameObject _soundmanagerRef;
	public GameObject SoundManager;
	public GameObject ScaleManager;
	public GameObject Monument; 


	void Start()
	{
		//Check if instance already exists
		if (Instance == null)

			//if not, set instance to this
			Instance = this;

		//If instance already exists and it's not this:
		else if (Instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}

	public void StartupHost()
	{

		_monumentRef = Instantiate(Monument, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		_scalemanagerRef = Instantiate(ScaleManager, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_soundmanagerRef = Instantiate(SoundManager, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		SetPort();
		StartServer();
		isServer = true;
		NetworkServer.Spawn(_monumentRef);
		NetworkServer.Spawn(_scalemanagerRef);
		NetworkServer.Spawn(_soundmanagerRef);

	}

	public void StopHosting()
	{
		StopServer();
		NetworkServer.Reset();
		Destroy(_monumentRef);
		Destroy(_scalemanagerRef);
		Destroy(_soundmanagerRef);
	}

	void SetIPAddress()
	{
		networkAddress = ConnectionIP;
	}
	void SetPort()
	{
		networkPort = ConnectionPort;
	}
	#region Server 
	public override void OnStartServer()
	{
		base.OnStartServer();
		debugTextServer.text = "Server Started";
	}

	public override void OnStopServer()
	{
		base.OnStopServer();
		debugTextServer.text = "Server Stopped";
	}
	public override void OnServerConnect(NetworkConnection conn)
	{

		debugTextServer.text = "Client " + conn.connectionId + " connected.";
		
	}

	public override void OnServerDisconnect(NetworkConnection conn)
	{
		base.OnServerDisconnect(conn);

		debugTextServer.text = "Client " + conn.connectionId + " disconnected.";
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer(conn, playerControllerId);
		
	}

	public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
	{
		
		base.OnServerRemovePlayer(conn, player);
	}

	#endregion

	#region Client
	public void JoinGame()
	{
		SetIPAddress();
		SetPort();
		StartClient();
		isClient = true;

	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		base.OnClientConnect(conn);
		//debugTextClient.text = "ClientConnected";
	}

	public override void OnStartClient(NetworkClient client)
	{
		base.OnStartClient(client);
		//debugTextClient.text = "Client Started";
	}

	public override void OnStopClient()
	{
		base.OnStopClient();
		//debugTextClient.text = "Server Stopped";
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
