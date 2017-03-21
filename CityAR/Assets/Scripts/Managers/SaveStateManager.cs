﻿using System;
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

	private int defaultConnId = -1;
	//rec events & occupied cells
	public class PlayerData : SyncListStruct<PlayerStats> { }
	public class ProjectData : SyncListStruct<ProjectStats> { }
	public class MiniGamesData : SyncListStruct<MiniGameStats> { }

	public PlayerData Players = new PlayerData();
	public ProjectData PlayerProjects = new ProjectData();
	public MiniGamesData PlayerMiniGames = new MiniGamesData();

	public EventSave GlobalSave;
	private string _savePath;



	public class EventSave
	{
		public string TimeStamp;
		public string Event;
	}

	public struct PlayerStats
	{
		public string Player;
		public bool Taken;
		public int Influence;
		public int Budget;
		public int ConnectionId;
		public int Quests;
		public int Rank;

		public PlayerStats(string player, bool taken, int influence, int budget, int connectionid, int quests, int rank)
		{
			Player = player;
			Taken = taken;
			Influence = influence;
			Budget = budget;
			ConnectionId = connectionid;
			Quests = quests;
			Rank = rank;
		}
	}

	public struct ProjectStats
	{
		public string Player;
		public int Proposed;
		public int Approved;
		public int Denied;
		public int Accepted;
		public int Failed;
		public int MoneySpent;
		public ProjectStats(string player, int proposed, int approved, int denied, int accepted, int failed, int money)
		{
			Player = player;
			Proposed = proposed;
			Approved = approved;
			Denied = denied;
			Accepted = accepted;
			Failed = failed;
			MoneySpent = money;
		}
	}
	public struct MiniGameStats
	{
		public string Player;
		public int TimeSpent;
		public int Mg1Wins;
		public int Mg2Wins;
		public int Mg3Wins;
		public int Fails;

		public MiniGameStats(string player, int timeSpent, int mg1, int mg2, int mg3, int fails)
		{
			Player = player;
			TimeSpent = timeSpent;
			Mg1Wins = mg1;
			Mg2Wins = mg2;
			Mg3Wins = mg3;
			Fails = fails;
		}
	}
	public void AddMiniGameStats(string owner, string action)
	{
		MiniGameStats dataOld = new MiniGameStats();
		MiniGameStats dataNew = new MiniGameStats();
		foreach (MiniGameStats playerdata in PlayerMiniGames)
		{
			if (playerdata.Player == owner)
			{
				switch (action)
				{
					case "Mg1Win":
						dataNew = new MiniGameStats(playerdata.Player, playerdata.TimeSpent, playerdata.Mg1Wins + 1, playerdata.Mg2Wins, playerdata.Mg3Wins, playerdata.Fails);
						break;
					case "Mg2Win":
						dataNew = new MiniGameStats(playerdata.Player, playerdata.TimeSpent, playerdata.Mg1Wins, playerdata.Mg2Wins + 1, playerdata.Mg3Wins, playerdata.Fails);
						break;
					case "Mg3Win":
						dataNew = new MiniGameStats(playerdata.Player, playerdata.TimeSpent, playerdata.Mg1Wins, playerdata.Mg2Wins, playerdata.Mg3Wins + 1, playerdata.Fails);
						break;
					case "MgFail":
						dataNew = new MiniGameStats(playerdata.Player, playerdata.TimeSpent, playerdata.Mg1Wins, playerdata.Mg2Wins, playerdata.Mg3Wins, playerdata.Fails + 1);
						break;
				}
				dataOld = playerdata;
			}
		}
		PlayerMiniGames.Remove(dataOld);
		PlayerMiniGames.Add(dataNew);
	}
	public void AddMiniGameTime(string owner, int time)
	{
		MiniGameStats dataOld = new MiniGameStats();
		MiniGameStats dataNew = new MiniGameStats();
		foreach (MiniGameStats playerdata in PlayerMiniGames)
		{
			if (playerdata.Player == owner)
			{
				dataNew = new MiniGameStats(playerdata.Player, playerdata.TimeSpent + time, playerdata.Mg1Wins, playerdata.Mg2Wins, playerdata.Mg3Wins, playerdata.Fails);
				dataOld = playerdata;
			}
		}
		PlayerMiniGames.Remove(dataOld);
		PlayerMiniGames.Add(dataNew);
	}

	public void AddProjectAction(string owner, string action)
	{
		ProjectStats dataOld = new ProjectStats();
		ProjectStats dataNew = new ProjectStats();
		foreach (ProjectStats playerdata in PlayerProjects)
		{
			if (playerdata.Player == owner)
			{
				switch (action)
				{
					case "Propose":
						dataNew = new ProjectStats(playerdata.Player, playerdata.Proposed + 1, playerdata.Approved, playerdata.Denied, playerdata.Accepted, playerdata.Failed, playerdata.MoneySpent);
						break;
					case "Approve":
						dataNew = new ProjectStats(playerdata.Player, playerdata.Proposed, playerdata.Approved + 1, playerdata.Denied, playerdata.Accepted, playerdata.Failed, playerdata.MoneySpent);
						break;
					case "Deny":
						dataNew = new ProjectStats(playerdata.Player, playerdata.Proposed, playerdata.Approved, playerdata.Denied + 1, playerdata.Accepted, playerdata.Failed, playerdata.MoneySpent);
						break;
					case "Accepted":
						dataNew = new ProjectStats(playerdata.Player, playerdata.Proposed, playerdata.Approved, playerdata.Denied, playerdata.Accepted + 1, playerdata.Failed, playerdata.MoneySpent);
						break;
					case "Failed":
						dataNew = new ProjectStats(playerdata.Player, playerdata.Proposed, playerdata.Approved, playerdata.Denied, playerdata.Accepted, playerdata.Failed + 1, playerdata.MoneySpent);
						break;
				}
				dataOld = playerdata;
			}
		}
		PlayerProjects.Remove(dataOld);
		PlayerProjects.Add(dataNew);
	}

	public void AddMoneySpent(string owner, int amount)
	{
		ProjectStats dataOld = new ProjectStats();
		ProjectStats dataNew = new ProjectStats();
		foreach (ProjectStats playerdata in PlayerProjects)
		{
			if (playerdata.Player == owner)
			{
				dataNew = new ProjectStats(playerdata.Player, playerdata.Proposed, playerdata.Approved, playerdata.Denied, playerdata.Accepted, playerdata.Failed, playerdata.MoneySpent + amount);
				dataOld = playerdata;
			}
		}
		PlayerProjects.Remove(dataOld);
		PlayerProjects.Add(dataNew);
	}

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
			Players.Add(new PlayerStats("Finance", false, 0, Vars.Instance.StartingBudget, defaultConnId,0,0));
			Players.Add(new PlayerStats("Social", false, 0, Vars.Instance.StartingBudget, defaultConnId,0,0));
			Players.Add(new PlayerStats("Environment", false, 0, Vars.Instance.StartingBudget, defaultConnId,0,0));
			//init project data save
			PlayerProjects.Add(new ProjectStats("Finance", 0, 0, 0, 0, 0, 0));
			PlayerProjects.Add(new ProjectStats("Social", 0, 0, 0, 0, 0, 0));
			PlayerProjects.Add(new ProjectStats("Environment", 0, 0, 0, 0, 0, 0));
			//init mini game data save
			PlayerMiniGames.Add(new MiniGameStats("Finance", 0, 0, 0, 0, 0));
			PlayerMiniGames.Add(new MiniGameStats("Social", 0, 0, 0, 0, 0));
			PlayerMiniGames.Add(new MiniGameStats("Environment", 0, 0, 0, 0, 0));
			//init event logging
			GlobalSave = new EventSave();          
		}
		//create new json file
		_savePath = Path.Combine(Application.persistentDataPath, "jsonTest.json");
		File.WriteAllText(_savePath, "New Game");
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			RecordData();
		}


	}

	#region Saving Player Data

	public void RecordData()
	{
		_savePath = Path.Combine(Application.persistentDataPath, "jsonTest.json");
		File.AppendAllText(_savePath, JsonUtility.ToJson(GlobalSave, true));
		foreach (PlayerStats player in Players)
		{
			File.AppendAllText(_savePath, JsonUtility.ToJson(player, true));
		}
	}

	public void LogEvent(string content)
	{
		if (NetworkingManager.Instance.isServer)
		{
			GlobalSave.TimeStamp = Utilities.DisplayTime(GameManager.Instance.CurrentTime);
			GlobalSave.Event = content;
			RecordData();
		}
	}

	public void SetTaken(int connectionId, bool taken)
	{
		PlayerStats dataOld = new PlayerStats();
		PlayerStats dataNew = new PlayerStats();
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.ConnectionId == connectionId)
			{
				dataNew = new PlayerStats(playerdata.Player, taken, playerdata.Influence, playerdata.Budget, defaultConnId, playerdata.Quests, playerdata.Rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public void SetTaken(string roletype, bool taken, int connectionid)
	{
		PlayerStats dataOld = new PlayerStats();
		PlayerStats dataNew = new PlayerStats();
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				dataNew = new PlayerStats(playerdata.Player, taken, playerdata.Influence, playerdata.Budget, connectionid, playerdata.Quests, playerdata.Rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public bool GetTaken(string roletype)
	{
		bool returnVar = false;
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				returnVar = playerdata.Taken;
			}
		}
		return returnVar;
	}

	public void SetBudget(string roletype, int budget)
	{
		PlayerStats dataOld = new PlayerStats();
		PlayerStats dataNew = new PlayerStats();
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				dataNew = new PlayerStats(playerdata.Player, playerdata.Taken, playerdata.Influence, playerdata.Budget + budget, playerdata.ConnectionId, playerdata.Quests, playerdata.Rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public int GetBudget(string roletype)
	{
		int returnVar = 0;
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				returnVar = playerdata.Budget;
			}
		}
		return returnVar;
	}

	public void SetRank(string roletype, int rank)
	{
		PlayerStats dataOld = new PlayerStats();
		PlayerStats dataNew = new PlayerStats();
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				dataNew = new PlayerStats(playerdata.Player, playerdata.Taken, playerdata.Influence, playerdata.Budget, playerdata.ConnectionId, playerdata.Quests, playerdata.Rank + rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public int GetRank(string roletype)
	{
		int returnVar = 0;
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				returnVar = playerdata.Rank;
			}
		}
		return returnVar;
	}

	public void SetInfluence(string roletype, int influence)
	{
		PlayerStats dataOld = new PlayerStats();
		PlayerStats dataNew = new PlayerStats();
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				dataNew = new PlayerStats(playerdata.Player, playerdata.Taken, playerdata.Influence + influence, playerdata.Budget, playerdata.ConnectionId, playerdata.Quests, playerdata.Rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public int GetInfluence(string roletype)
	{
		int returnVar = 0;
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				returnVar = playerdata.Influence;
			}
		}
		return returnVar;
	}

	public void SetQuests(string roletype, int quest)
	{
		PlayerStats dataOld = new PlayerStats();
		PlayerStats dataNew = new PlayerStats();
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				dataNew = new PlayerStats(playerdata.Player, playerdata.Taken, playerdata.Influence, playerdata.Budget, playerdata.ConnectionId, playerdata.Quests + quest, playerdata.Rank);
				dataOld = playerdata;
			}
		}
		Players.Remove(dataOld);
		Players.Add(dataNew);
	}

	public int GetQuests(string roletype)
	{
		int returnVar = 0;
		foreach (PlayerStats playerdata in Players)
		{
			if (playerdata.Player == roletype)
			{
				returnVar = playerdata.Quests;
			}
		}
		return returnVar;
	}
	
	public int GetAllBudget()
	{
		int returnval = 0;
		foreach (PlayerStats playerdata in Players)
		{
			returnval += playerdata.Budget;
		}
		return returnval;
	}

	public int GetAllQuests()
	{
		int returnval = 0;
		foreach (PlayerStats playerdata in Players)
		{
			returnval += playerdata.Quests;
		}
		return returnval;
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
			case "MoneySpent":
				AddMoneySpent(roletype, amount);
				break;
			case "Deny":
				AddProjectAction(roletype, datatype);
				break;
			case "Approve":
				AddProjectAction(roletype, datatype);
				break;
			case "Accepted":
				AddProjectAction(roletype, datatype);
				break;
			case "Failed":
				AddProjectAction(roletype, datatype);
				break;
			case "Mg1Win":
				AddMiniGameStats(roletype, datatype);
				break;
			case "Mg2Win":
				AddMiniGameStats(roletype, datatype);
				break;
			case "Mg3Win":
				AddMiniGameStats(roletype, datatype);
				break;
			case "MgFail":
				AddMiniGameStats(roletype, datatype);
				break;
			case "MgTime":
				AddMiniGameTime(roletype, amount);
				break;
		}
	}
	#endregion


}