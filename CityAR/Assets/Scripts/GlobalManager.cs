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
	public struct PlayerDataSave
	{
		public string RoleType;
		public bool Taken;
		public int Rating;
		public int Budget;
        public int ConnectionId;
        public PlayerDataSave(string roletype, bool taken, int rating, int budget, int connectionid)
        {
            RoleType = roletype;
            Taken = taken;
            Rating = rating;
            Budget = budget;
            ConnectionId = connectionid;
        }
    }
    public class PlayerData: SyncListStruct<PlayerDataSave> { }
    public PlayerData Players = new PlayerData();
    private int defaultConnId = -1;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
    }
    void Start()
    {
        if (isServer)
        {
            Players.Add(new PlayerDataSave("Environment", false, StartingRating, StartingBudget, defaultConnId));
            Players.Add(new PlayerDataSave("Social", false, StartingRating, StartingBudget, defaultConnId));
            Players.Add(new PlayerDataSave("Finance", false, StartingRating, StartingBudget, defaultConnId));
        }
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

    public void SetTaken(int connectionId, bool taken)
    {
        PlayerDataSave dataOld = new PlayerDataSave();
        PlayerDataSave dataNew = new PlayerDataSave();
        foreach (PlayerDataSave playerdata in Players)
        {
            if (playerdata.ConnectionId == connectionId)
            {
                dataNew = new PlayerDataSave(playerdata.RoleType, taken, playerdata.Rating, playerdata.Budget, defaultConnId);
                dataOld = playerdata;
            }
        }
        Players.Remove(dataOld);
        Players.Add(dataNew);
    }

    public void SetTaken(string roletype, bool taken, int connectionid)
    {
        PlayerDataSave dataOld = new PlayerDataSave();
        PlayerDataSave dataNew = new PlayerDataSave();
        foreach (PlayerDataSave playerdata in Players)
        {
            if (playerdata.RoleType == roletype)
            {
                dataNew = new PlayerDataSave(playerdata.RoleType, taken, playerdata.Rating, playerdata.Budget, connectionid);
                dataOld = playerdata;
            }
        }
        Players.Remove(dataOld);
        Players.Add(dataNew);
    }
    public bool GetTaken(string roletype)
    {
        bool returnVar = false;
        foreach (PlayerDataSave playerdata in Players)
        {
            if (playerdata.RoleType == roletype)
            {
                returnVar = playerdata.Taken;
            }
        }
        return returnVar;
    }

    public void SetBudget(string roletype, int budget)
    {
        PlayerDataSave dataOld = new PlayerDataSave();
        PlayerDataSave dataNew = new PlayerDataSave();
        foreach (PlayerDataSave playerdata in Players)
        {
            if (playerdata.RoleType == roletype)
            {
                dataNew = new PlayerDataSave(playerdata.RoleType, playerdata.Taken, playerdata.Rating, playerdata.Budget + budget, playerdata.ConnectionId);
                dataOld = playerdata;
            }
        }
        Players.Remove(dataOld);
        Players.Add(dataNew);
    }

    public int GetBudget(string roletype)
    {
        int returnVar = 0;
        foreach (PlayerDataSave playerdata in Players)
        {
            if (playerdata.RoleType == roletype)
            {
                returnVar = playerdata.Budget;
            }
        }
        return returnVar;
    }

    public void SetRating(string roletype, int rating)
    {
        PlayerDataSave dataOld = new PlayerDataSave();
        PlayerDataSave dataNew = new PlayerDataSave();
        foreach (PlayerDataSave playerdata in Players)
        {
            if (playerdata.RoleType == roletype)
            {
                dataNew = new PlayerDataSave(playerdata.RoleType, playerdata.Taken, playerdata.Rating + rating, playerdata.Budget, playerdata.ConnectionId);
                dataOld = playerdata;
            }
        }
        Players.Remove(dataOld);
        Players.Add(dataNew);
    }
    public int GetRating(string roletype)
    {
        int returnVar = 0;
        foreach (PlayerDataSave playerdata in Players)
        {
            if (playerdata.RoleType == roletype)
            {
                returnVar = playerdata.Rating;
            }
        }
        return returnVar;
    }

    public void UpdateData(string roletype, string datatype, int amount)
    {
        switch (datatype)
        {
            case "Budget":
                SetBudget(roletype, amount);
                break;
            case "Rating":
                SetRating(roletype, amount);
                break;
		}
	}
}


