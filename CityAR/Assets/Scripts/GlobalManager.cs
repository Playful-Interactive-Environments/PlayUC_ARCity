using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GlobalManager : NetworkBehaviour
{
	public static GlobalManager Instance = null;
	//GameDuration
	[SyncVar]
	public float GameDuration; //game end 
	[SyncVar]
	private int CurrentMonth;
	[SyncVar]
	public int CurrentYear;
	private float _currentTime;
	private string[] _months = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
	public int MonthDuration = 10; //in seconds - time for month change
	public float CellMaxValue = 50; //5 heatmap steps!
	public int StartingBudget;
	public int StartingRating;
	//Storing Player Data
	public struct PlayerData
	{
		public string RoleType;
		public bool Taken;
		public int Rating;
		public int Budget;
	}
	[SyncVar] public PlayerData EnvironmentPlayer;
	[SyncVar] public PlayerData SocialPlayer;
	[SyncVar] public PlayerData FinancePlayer;
	public NetworkCommunicator EnvironmentCommunicator;
	public NetworkCommunicator SocialCommunicator;
	public NetworkCommunicator FinanceCommunicator;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		//InvokeRepeating("SendData", 1f, 1f);
		InvokeRepeating("Refresh", 0f, .1f);
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
		EnvironmentPlayer.Budget = StartingBudget;
		EnvironmentPlayer.Rating = StartingRating;
		SocialPlayer.Budget = StartingBudget;
		SocialPlayer.Rating = StartingRating;
		FinancePlayer.Budget = StartingBudget;
		FinancePlayer.Rating = StartingRating;
	}
	void Refresh()
	{
		if (isServer)
		{
			if (EnvironmentCommunicator == null)
				EnvironmentPlayer.Taken = false;
			if (SocialCommunicator == null)
				SocialPlayer.Taken = false;
			if (FinanceCommunicator == null)
				FinancePlayer.Taken = false;
		}
	}

	public void UpdateData(string roletype, string datatype, int amount)
	{
		switch (roletype)
		{
			case "Environment":
				switch (datatype)
				{
					case "Budget":
						EnvironmentPlayer.Budget += amount;
						break;
					case "Rating":
						EnvironmentPlayer.Rating += amount;
						break;
				}
				break;
			case "Social":
				switch (datatype)
				{
					case "Budget":
						SocialPlayer.Budget += amount;

						break;
					case "Rating":
						SocialPlayer.Rating += amount;

						break;
				}
				break;
			case "Finance":
				switch (datatype)
				{
					case "Budget":
						FinancePlayer.Budget += amount;
						break;
					case "Rating":
						FinancePlayer.Rating += amount;
						break;
				}
				break;
		}
		Debug.Log(roletype + datatype + amount);
	}
}


