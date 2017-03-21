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
	[SyncVar]
	public float CurrentTime;
	private string MyState;
	private int currentEvent;
	public static GameManager Instance;
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
		InvokeRepeating("CheckWinState", 0, 1f);
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
		if (isServer)
		{
			CurrentTime += Time.deltaTime;
		}
		UIManager.Instance.TimeText.text = Utilities.DisplayTime(CurrentTime);
	}
	#region GameEnd
	void CheckWinState()
	{
		if (CurrentTime >= Vars.Instance.GameEndTime)
		{
			UIManager.Instance.GameEndResult.text = TextManager.Instance.TimeWinText;
            UIManager.Instance.GameEndResultImage.sprite = UIManager.Instance.YouWin;
            CalculateAchievements();
		}
		if (CellManager.Instance.CurrentSocialGlobal >= Vars.Instance.UtopiaRate &&
			CellManager.Instance.CurrentEnvironmentGlobal >= Vars.Instance.UtopiaRate &&
			CellManager.Instance.CurrentFinanceGlobal >= Vars.Instance.UtopiaRate)
		{
			UIManager.Instance.GameEndResult.text = TextManager.Instance.UtopiaWinText;
            UIManager.Instance.GameEndResultImage.sprite = UIManager.Instance.YouWin;
            CalculateAchievements();
		}

		foreach (SaveStateManager.PlayerStats player in SaveStateManager.Instance.Players)
		{
			if (player.Rank == Vars.Instance.MayorLevel)
			{
				UIManager.Instance.GameEndResult.text = TextManager.Instance.MayorWinText;
				UIManager.Instance.GameEndExtraText.text = TextManager.Instance.MayorAnnounceText;

				if (player.Player == LevelManager.Instance.RoleType)
				{
					UIManager.Instance.GameEndResultImage.sprite = UIManager.Instance.YouWin;
				}
				else
				{
					switch (player.Player)
					{
						case Vars.Player1:
							UIManager.Instance.GameEndResultImage.sprite = UIManager.Instance.FinanceWin;

							break;
						case Vars.Player2:
							UIManager.Instance.GameEndResultImage.sprite = UIManager.Instance.SocialWin;

							break;
						case Vars.Player3:
							UIManager.Instance.GameEndResultImage.sprite = UIManager.Instance.EnvironmentWin;

							break;
					}
				}
				CalculateAchievements();
			}
		}
	}

	void CalculateAchievements()
	{
		EventDispatcher.TriggerEvent("GameEnd");
		CancelInvoke("CheckWinState");
	    StartCoroutine(DisplayGameEndStates());
		UIManager.Instance.Change(UIManager.UiState.GameEnd);

	}

    IEnumerator DisplayGameEndStates()
    {
        UIManager.Instance.DisplayEndStates("EndStateStart");
        yield return new WaitForSeconds(5f);
        UIManager.Instance.DisplayEndStates("GlobalEndState");
        yield return new WaitForSeconds(5f);
        UIManager.Instance.DisplayEndStates("PlayerAchievements1");
        yield return new WaitForSeconds(5f);
        UIManager.Instance.DisplayEndStates("PlayerAchievements2");
        yield return new WaitForSeconds(5f);
        UIManager.Instance.DisplayEndStates("PlayerStats");
    }
    #endregion

    #region Discussion State
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
	#endregion
}
