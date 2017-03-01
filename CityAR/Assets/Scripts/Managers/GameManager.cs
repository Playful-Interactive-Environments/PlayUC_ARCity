using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{

	public string[] States = new[] {"Grey", "MiniGame", "Event"};
	public string EnvironmentState;
	public string FinanceState;
	public string SocialState;

	public static GameManager Instance;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start () {

	}
	
	void Update () {
		if (isServer)
		{
			StartEvent();
		}
	}

	void StartEvent()
	{
		
	}
}
