using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GlobalManager : NetworkBehaviour
{
	public static GlobalManager Instance = null;
	public class PlayerData
	{
		[SyncVar]
		public string RoleType;
		[SyncVar]
		public int Rating;
		[SyncVar]
		public int Budget;

		public PlayerData(string roletype, int rating, int budget)
		{
		    RoleType = roletype;
		    Rating = rating;
		    Budget = budget;
		}
	}

	[SyncVar]
	public float GameDuration;
	[SyncVar]
	private int CurrentMonth;
	[SyncVar]
	public int CurrentYear;
	private float _currentTime;
	private string[] _months = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
	public int MonthDuration = 10;
	public float CellMaxValue = 50;
	public PlayerData EnvironmentPlayer;
	public PlayerData SocialPlayer;
	public PlayerData FinancePlayer;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		//InvokeRepeating("SendData", 1f, 1f);
	}

	void Update ()
	{
		if (isServer)
		{
			_currentTime += Time.deltaTime;
			if (Mathf.RoundToInt(_currentTime) == MonthDuration)
			{
				CurrentMonth++;
				_currentTime = 0;
			}
			if (CurrentMonth == 12)
			{
				CurrentYear++;
				CurrentMonth = 0;
			}
		}
		UIManager.Instance.TimeText.text = "Year " + CurrentYear + ": " + _months[CurrentMonth];
		
	}

	void Start()
	{
		EnvironmentPlayer = new PlayerData("Environment", RoleManager.Instance.Budget, RoleManager.Instance.Rating);
		SocialPlayer = new PlayerData("Social", RoleManager.Instance.Budget, RoleManager.Instance.Rating);
		FinancePlayer = new PlayerData("Finance", RoleManager.Instance.Budget, RoleManager.Instance.Rating);
	}

	void SendData()
	{
			if (RoleManager.Instance.Environment)
			{
                string roletype = "Environment";
				int budget = RoleManager.Instance.Budget;
                int rating = RoleManager.Instance.Rating;
                CellManager.Instance.NetworkCommunicator.SavePlayerData(roletype, rating, budget);
            }
			if (RoleManager.Instance.Social)
			{
                string roletype = "Social";
                int budget = RoleManager.Instance.Budget;
                int rating = RoleManager.Instance.Rating;
                CellManager.Instance.NetworkCommunicator.SavePlayerData(roletype, rating, budget);
            }
            if (RoleManager.Instance.Finance)
			{
                string roletype = "Finance";
                int budget = RoleManager.Instance.Budget;
                int rating = RoleManager.Instance.Rating;
                CellManager.Instance.NetworkCommunicator.SavePlayerData(roletype, rating, budget);
        }
	}
    public void SavePlayerData(string roletype, int rating, int budget)
    {
        //Debug.Log(roletype + rating + budget);
        switch (roletype)
        {
            case "Environment":
                EnvironmentPlayer.RoleType = roletype;
                EnvironmentPlayer.Rating = rating;
                EnvironmentPlayer.Budget = budget;
                break;
            case "Social":
                SocialPlayer.RoleType = roletype;
                SocialPlayer.Rating = rating;
                SocialPlayer.Budget = budget;
                break;
            case "Finance":
                FinancePlayer.RoleType = roletype;
                FinancePlayer.Rating = rating;
                FinancePlayer.Budget = budget;
                break;
        }
    }

    public void LoadPlayerData(string roletype)
	{
        UIManager.Instance.DebugText.text = roletype;
		switch (roletype)
		{
			case "Environment":
				RoleManager.Instance.Budget = EnvironmentPlayer.Budget;
				RoleManager.Instance.Rating = EnvironmentPlayer.Rating;
				break;
			case "Social":
				RoleManager.Instance.Budget = SocialPlayer.Budget;
				RoleManager.Instance.Rating = SocialPlayer.Rating;
				break;
			case "Finance":
				RoleManager.Instance.Budget = FinancePlayer.Budget;
				RoleManager.Instance.Rating = FinancePlayer.Rating;
				break;
		}
	}
}

/*
	public void SavePlayerData(string roletype, int rating, int budget)
	{
		switch (roletype)
		{
			case "Environment":
				EnvironmentPlayer.RoleType = roletype;
				EnvironmentPlayer.Rating = rating;
				EnvironmentPlayer.Budget = budget;

				break;
			case "Social":
				SocialPlayer.RoleType = roletype;
				SocialPlayer.Rating = rating;
				SocialPlayer.Budget = budget;
				break;
			case "Finance":
				FinancePlayer.RoleType = roletype;
				FinancePlayer.Rating = rating;
				FinancePlayer.Budget = budget;
				break;

		}
	}
		public void SavePlayerData(string roletype, int rating, int budget)
	{
		if (isServer)
		{
			GlobalManager.Instance.SavePlayerData(roletype, rating, budget);
		}
		if (isClient && !isServer)
		{
			ProjectManager.Instance.ResetUI();
			CmdSavePlayerData(roletype, rating, budget);
		}
	}

	[Command]
	void CmdSavePlayerData(string roletype, int rating, int budget)
	{
		SavePlayerData(roletype, rating, budget);
	}
*/
