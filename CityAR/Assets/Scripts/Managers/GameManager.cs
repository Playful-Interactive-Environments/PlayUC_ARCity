using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{

	//states: "Game", "MiniGame", "NewStart", "DiscussionStart", "DiscussionEnd", "Occupied"} ;
	[SyncVar]
	public string EnvironmentState;
	[SyncVar]
	public string FinanceState;
	[SyncVar]
	public string SocialState;

	private string MyState;
	private int currentEvent;
	public static GameManager Instance;
	private float currentTime;
	public float eventTime;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
		EventDispatcher.StartListening("ClientDisconnect", ClientDisconnect);

    }
    void NetworkDisconnect()
	{
		StopAllCoroutines();
		StartCoroutine(EndDiscussion());
	}

	void ClientDisconnect()
	{
		StopAllCoroutines();
		StartCoroutine(EndDiscussion());
	}

    void Update ()
	{

	}

	void CheckMyState()
	{
		if (LevelManager.Instance.RoleType == "Environment")
			MyState = EnvironmentState;
		if (LevelManager.Instance.RoleType == "Social")
			MyState = SocialState;
		if (LevelManager.Instance.RoleType == "Finance")
			MyState = FinanceState;
	}

	IEnumerator StartDiscussion()
	{
		//if user is occupied wait until they finish
		CheckMyState();
		while (MyState == "MiniGame")
		{
			CheckMyState();
			yield return new WaitForSeconds(1f);
		}
		EventDispatcher.TriggerEvent("StartDiscussion");
		yield return new WaitForSeconds(1f);
		UIManager.Instance.ShowProjectDisplay();
		UIManager.Instance.ShowDiscussionPanel();
        UIManager.Instance.ShowInfoScreen();
    }

    IEnumerator EndDiscussion()
	{
		UIManager.Instance.HideProjectDisplay();
		UIManager.Instance.HideDiscussionPanel();
        yield return new WaitForSeconds(1f);
		UIManager.Instance.GameUI();
	}

	public void SetState(string player, string state)
	{
		if (isServer)
		{
			switch (player)
			{
				case "Finance":
					FinanceState = state;
					break;
				case "Social":
					SocialState = state;
					break;
				case "Environment":
					EnvironmentState = state;
					break;
			}
		}
		if (isClient)
		{
			switch (state)
			{
				case "DiscussionStart":
					StartCoroutine(StartDiscussion());
					break;
				case "DiscussionEnd":
					StartCoroutine(EndDiscussion());
					break;
			}
		}
	}
}
