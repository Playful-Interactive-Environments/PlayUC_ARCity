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
	public float eventTime;
	public static GameManager Instance;
	private UIManager UiM;
	private SaveStateManager SaveData;

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
		UiM = UIManager.Instance;
		SaveData = SaveStateManager.Instance;
	}

	void NetworkDisconnect()
	{
	    if (MyState != "GameEnd")
	    {
            StopAllCoroutines();
            StartCoroutine(EndDiscussion());
        }
	}

	void ClientDisconnect()
    {
        if (MyState != "GameEnd")
        {
            StopAllCoroutines();
            StartCoroutine(EndDiscussion());
        }
    }

	void Update ()
	{
		if (isServer)
		{
			CurrentTime += Time.deltaTime;
		}
		UiM.TimeText.text = Utilities.DisplayTime(CurrentTime);
	}

	#region GameEnd
	void CheckWinState()
	{
		//TIME END
		if (CurrentTime >= Vars.Instance.GameEndTime)
		{
			UiM.GameEndResult.text = TextManager.Instance.TimeWinText;
			UiM.GameEndResultImage.sprite = UiM.YouWin;
			CalculateAchievements();
		}
		//UTOPIA END
		if (CellManager.Instance.CurrentSocialGlobal >= Vars.Instance.UtopiaRate &&
			CellManager.Instance.CurrentEnvironmentGlobal >= Vars.Instance.UtopiaRate &&
			CellManager.Instance.CurrentFinanceGlobal >= Vars.Instance.UtopiaRate)
		{
			UiM.GameEndResult.text = TextManager.Instance.UtopiaWinText;
			UiM.GameEndResultImage.sprite = UiM.YouWin;
			CalculateAchievements();
		}
		//MAYOR END
		foreach (SaveStateManager.PlayerStats player in SaveData.Players)
		{
			if (player.Rank == Vars.Instance.MayorLevel)
			{
				UiM.GameEndResult.text = TextManager.Instance.MayorWinText;
				UiM.GameEndExtraText.text = TextManager.Instance.MayorAnnounceText;

				if (player.Player == LevelManager.Instance.RoleType)
				{
					UiM.GameEndResultImage.sprite = UiM.YouWin;
				}
				else
				{
					switch (player.Player)
					{
						case Vars.Player1:
							UiM.GameEndResultImage.sprite = UiM.Player1_winSprite;
							break;
						case Vars.Player2:
							UiM.GameEndResultImage.sprite = UiM.Player2_winSprite;
							break;
						case Vars.Player3:
							UiM.GameEndResultImage.sprite = UiM.Player3_winSprite;
							break;
					}
				}
				CalculateAchievements();
			}
		}
	}

	void CalculateAchievements()
	{
		//End Game 
		CellManager.Instance.NetworkCommunicator.SetPlayerState(LevelManager.Instance.RoleType, "GameEnd");
		EventDispatcher.TriggerEvent("GameEnd");
		CancelInvoke("CheckWinState");
		StopAllCoroutines();
		UiM.DisplayEndStates("EndStateStart");
		//StartCoroutine(DisplayGameEndStates());
		UiM.Change(UIManager.UiState.GameEnd);
		//Calculate Global End State Vars
		UiM.TimePlayedN.text = Utilities.DisplayTime(CurrentTime);
		UiM.SuccessfulProjectN.text = "" + SaveData.GetAllSucessful("SuccessfulProjectN");
		UiM.TotalAddedValueN.text = "" + CellManager.Instance.GetTotalAddedValue();
		UiM.MostImprovedFieldN.text = "" + CellManager.Instance.GetMostImprovedValue();
		UiM.LeastImprovedFieldN.text = "" + CellManager.Instance.GetLeastImprovedValue();
		CellManager.Instance.GetValueImages();
        //Calculate Player Achievements
        SaveData.CalculateAchievement("MostSuccessfulProjects");
        SaveData.CalculateAchievement("MostMoneySpent");
        SaveData.CalculateAchievement("HighestInfluence");
        SaveData.CalculateAchievement("MostWinMiniGames");
        SaveData.CalculateAchievement("LeastTimeMiniGames");
        //Calculate Personal Stats
        SaveData.CalculatePersonalStats();
	}

	IEnumerator DisplayGameEndStates()
	{
		UiM.DisplayEndStates("EndStateStart");
		yield return new WaitForSeconds(5f);
		UiM.DisplayEndStates("GlobalEndState");
		yield return new WaitForSeconds(5f);
		UiM.DisplayEndStates("PlayerAchievements");
		yield return new WaitForSeconds(5f);
		UiM.DisplayEndStates("PlayerStats");
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
        if (MyState == "GameEnd")
            StopAllCoroutines();
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
		UiM.ShowProjectDisplay();
		UiM.ShowDiscussionPanel();
		UiM.ShowInfoScreen();
	}

	IEnumerator EndDiscussion()
	{
		UiM.HideProjectDisplay();
		UiM.HideDiscussionPanel();
		yield return new WaitForSeconds(1f);
		UiM.GameUI();
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
