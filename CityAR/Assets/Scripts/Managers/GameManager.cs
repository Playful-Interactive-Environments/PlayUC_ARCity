using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{

    //states: "Game", "MiniGame", "Event", "DiscussionStart", "Occupied"} ;
    [SyncVar]
    public string EnvironmentState;
	[SyncVar]
	public string FinanceState;
    [SyncVar]
    public string SocialState;

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
		currentEvent = EventManager.Instance.RandomEvent();
	}

    void Update ()
	{

	}

	IEnumerator StartDiscussion()
	{
		UIManager.Instance.GameUI();
		yield return new WaitForSeconds(1f);
		UIManager.Instance.ShowProjectInfo();
		UIManager.Instance.ShowDiscussionPanel();
	}

	IEnumerator EndDiscussion()
	{
		UIManager.Instance.HideProjectInfo();
		UIManager.Instance.HideDiscussionPanel();
		yield return new WaitForSeconds(1f);
		UIManager.Instance.GameUI();
	}

	public void StartEvent()
	{
		//TODO: Check all players, use only 1 for debug
		if (EnvironmentState.Equals("Game"))
		{
			//event triggered - send to clients and reset event trigger time
			CellManager.Instance.NetworkCommunicator.HandleEvent("StartEvent", currentEvent);
			currentTime = 0;
			eventTime = Utilities.RandomFloat(5, 15);
		}
	}

    public void SetState(string player, string state)
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
