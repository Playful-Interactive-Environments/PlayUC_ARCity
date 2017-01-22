using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine.Networking;
using UnityEngine.UI;
public class SaveStateManager : NetworkBehaviour
{
	public static SaveStateManager Instance = null;
	//GameDuration
	[SyncVar]
	public float GameDuration; //game end 
	[SyncVar]
	private int CurrentMonth = 0;
	[SyncVar]
	private int CurrentYear = 2017;
	private float _currentTime;
	private string[] _months = {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"};
	public int MonthDuration = 10; //in seconds - time for month change
	public float CellMaxValue = 50; //5 heatmap steps!
	public int StartingBudget;
	private int defaultConnId = -1;
	public float TimeStamp;
	//rec events & occupied cells
	public SyncListInt OccupiedList = new SyncListInt();
	public class PlayerData : SyncListStruct<PlayerDataSave> { }
	public PlayerData Players = new PlayerData();
	public EventSave GlobalSave;
	private string savestatepath;

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
			//init player save
			Players.Add(new PlayerDataSave("Environment", false, 0, StartingBudget, defaultConnId,0,1));
			Players.Add(new PlayerDataSave("Social", false, 0, StartingBudget, defaultConnId,0,1));
			Players.Add(new PlayerDataSave("Finance", false, 0, StartingBudget, defaultConnId,0,1));
			//init event logging
			GlobalSave = new EventSave();
			//init occupied cell save
			InitOccupiedList();
		}
		//create new json file
		savestatepath = Path.Combine(Application.persistentDataPath, "jsonTest.json");
		File.WriteAllText(savestatepath, "New Game");
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			RecordData();
		}
		if (isServer)
		{
			TimeStamp += Time.time;
			_currentTime += Time.deltaTime;
			if (Mathf.RoundToInt(_currentTime) == MonthDuration)
			{
				CurrentMonth++;
				_currentTime = 0;
			}
			if (CurrentMonth == 11)
			{
				CurrentYear++;
				CurrentMonth = 0;
			}
		}
		UIManager.Instance.TimeText.text = _months[CurrentMonth] + "." + CurrentYear;
	}
	
	#region Saving Player Data
	public struct PlayerDataSave
	{
		public string RoleType;
		public bool Taken;
		public int Influence;
		public int Budget;
		public int ConnectionId;
		public int Quests;
		public int Rank;

		public PlayerDataSave(string roletype, bool taken, int influence, int budget, int connectionid, int quests, int rank)
		{
			RoleType = roletype;
			Taken = taken;
			Influence = influence;
			Budget = budget;
			ConnectionId = connectionid;
			Quests = quests;
			Rank = rank;
		}
	}

	public class EventSave
	{
		public string TimeStamp;
		public string Event;
	}

	public void RecordData()
	{
		savestatepath = Path.Combine(Application.persistentDataPath, "jsonTest.json");
		File.AppendAllText(savestatepath, JsonUtility.ToJson(GlobalSave, true));
		foreach (PlayerDataSave player in Players)
		{
			File.AppendAllText(savestatepath, JsonUtility.ToJson(player, true));
		}
	}

	public void LogEvent(string content)
	{
		if (NetworkingManager.Instance.isServer)
		{
			GlobalSave.TimeStamp = Utilities.DisplayTime(TimeStamp);
			GlobalSave.Event = content;
			RecordData();
		}
	}

	public void SetTaken(int connectionId, bool taken)
	{
		PlayerDataSave dataOld = new PlayerDataSave();
		PlayerDataSave dataNew = new PlayerDataSave();
		foreach (PlayerDataSave playerdata in Players)
		{
			if (playerdata.ConnectionId == connectionId)
			{
				dataNew = new PlayerDataSave(playerdata.RoleType, taken, playerdata.Influence, playerdata.Budget, defaultConnId, playerdata.Quests, playerdata.Rank);
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
				dataNew = new PlayerDataSave(playerdata.RoleType, taken, playerdata.Influence, playerdata.Budget, connectionid, playerdata.Quests, playerdata.Rank);
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
				dataNew = new PlayerDataSave(playerdata.RoleType, playerdata.Taken, playerdata.Influence, playerdata.Budget + budget, playerdata.ConnectionId, playerdata.Quests, playerdata.Rank);
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
	public void SetRank(string roletype, int rank)
	{
		PlayerDataSave dataOld = new PlayerDataSave();
		PlayerDataSave dataNew = new PlayerDataSave();
		foreach (PlayerDataSave playerdata in Players)
		{
			if (playerdata.RoleType == roletype)
			{
				dataNew = new PlayerDataSave(playerdata.RoleType, playerdata.Taken, playerdata.Influence, playerdata.Budget, playerdata.ConnectionId, playerdata.Quests, playerdata.Rank + rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public int GetRank(string roletype)
	{
		int returnVar = 0;
		foreach (PlayerDataSave playerdata in Players)
		{
			if (playerdata.RoleType == roletype)
			{
				returnVar = playerdata.Rank;
			}
		}
		return returnVar;
	}
	public void SetInfluence(string roletype, int influence)
	{
		PlayerDataSave dataOld = new PlayerDataSave();
		PlayerDataSave dataNew = new PlayerDataSave();
		foreach (PlayerDataSave playerdata in Players)
		{
			if (playerdata.RoleType == roletype)
			{
				dataNew = new PlayerDataSave(playerdata.RoleType, playerdata.Taken, playerdata.Influence + influence, playerdata.Budget, playerdata.ConnectionId, playerdata.Quests, playerdata.Rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public int GetInfluence(string roletype)
	{
		int returnVar = 0;
		foreach (PlayerDataSave playerdata in Players)
		{
			if (playerdata.RoleType == roletype)
			{
				returnVar = playerdata.Influence;
			}
		}
		return returnVar;
	}
	public void SetQuests(string roletype, int quest)
	{
		PlayerDataSave dataOld = new PlayerDataSave();
		PlayerDataSave dataNew = new PlayerDataSave();
		foreach (PlayerDataSave playerdata in Players)
		{
			if (playerdata.RoleType == roletype)
			{
				dataNew = new PlayerDataSave(playerdata.RoleType, playerdata.Taken, playerdata.Influence, playerdata.Budget, playerdata.ConnectionId, playerdata.Quests + quest, playerdata.Rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public int GetQuests(string roletype)
	{
		int returnVar = 0;
		foreach (PlayerDataSave playerdata in Players)
		{
			if (playerdata.RoleType == roletype)
			{
				returnVar = playerdata.Quests;
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
			case "Influence":
				SetInfluence(roletype, amount);
				break;
			case "Quest":
				SetQuests(roletype, amount);
				break;
			case "Rank":
				SetRank(roletype, amount);
				break;
		}
	}

	#endregion

	public int GetAllBudget()
	{
		int returnval = 0;
		foreach (PlayerDataSave playerdata in Players)
		{
			returnval += playerdata.Budget;
		}
		return returnval;
	}

	public int GetAllQuests()
	{
		int returnval = 0;
		foreach (PlayerDataSave playerdata in Players)
		{
			returnval += playerdata.Quests;
		}
		return returnval;
	}

	public void AddOccupied(int id)
	{
		OccupiedList[id] += 1;
	}

	public void RemoveOccupied(int id)
	{
		OccupiedList[id] -= 1;
	}

	public void InitOccupiedList()
	{
		foreach (GameObject cell in CellGrid.Instance.GridCells)
		{
			OccupiedList.Add(0);
		}
	}
}