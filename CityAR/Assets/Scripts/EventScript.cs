﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EventScript : NetworkBehaviour {

	[SyncVar]
	public int id;
	[SyncVar]
	public string type;
	[SyncVar]
	public string title;
	[SyncVar]
	public string content;
	[SyncVar]
	public string choice1;
	[SyncVar]
	public string choice2;
	[SyncVar]
	public float TimeLeft;
	[SyncVar]
	public int CurrentGoal;
	[SyncVar]
	public int CurrentProgress;
	[SyncVar]
	public bool HelpMayorEvent;
	[SyncVar]
	private int _storedValue;

	void Start ()
	{
		Invoke("TriggerEvent", .5f);
		GlobalEvents.Instance.CurrentEventScript = GetComponent<EventScript>();
	}

	void TriggerEvent()
	{
		UIManager.Instance.SetEventText(title, content, choice1, choice2);

		switch (type)
		{
			case "Crisis":
				EventManager.TriggerEvent("Crisis");
				break;
			case "DesignProject":
				EventManager.TriggerEvent("DesignProject");
				break;
			case "Environment":
				_storedValue = CellManager.Instance.CurrentEnvironmentGlobal;
				EventManager.TriggerEvent("HelpMayor");
				break;

			case "Finance":
				_storedValue = CellManager.Instance.CurrentFinanceGlobal;
				EventManager.TriggerEvent("HelpMayor");
				break;
			case "Social":
				_storedValue = CellManager.Instance.CurrentSocialGlobal;
				EventManager.TriggerEvent("HelpMayor");
				break;
			case "Budget":
				_storedValue = GlobalManager.Instance.GetAllBudget();
				EventManager.TriggerEvent("HelpMayor");
				break;
			case "Quest":
                _storedValue = GlobalManager.Instance.GetAllQuests();
                EventManager.TriggerEvent("HelpMayor");
				break;
		}
	}

	void Update ()
	{
		if (HelpMayorEvent)
		{
			TimeLeft -= Time.deltaTime;
			if (TimeLeft <= 0)
			{
				if (CurrentProgress <= CurrentGoal)
				{
					//EventFail!
				}
				HelpMayorEvent = false;
			}
			if (CurrentProgress >= CurrentGoal)
			{
				HelpMayorEvent = false;
				//EventSuccessful!
			}
			HelpMayorProgress();
		}
	}
	#region EventLogic

	public void HelpMayorProgress()
	{
		switch (type)
		{
			case "Environment":
				CurrentProgress = CellManager.Instance.CurrentEnvironmentGlobal - _storedValue;
				break;
			case "Finance":
				CurrentProgress = CellManager.Instance.CurrentFinanceGlobal - _storedValue;
				break;
			case "Social":
				CurrentProgress = CellManager.Instance.CurrentSocialGlobal - _storedValue;
				break;
			case "Budget":
				CurrentProgress = GlobalManager.Instance.GetAllBudget() - _storedValue;
				break;
			case "Quest":
                CurrentProgress = GlobalManager.Instance.GetAllQuests() - _storedValue;
                break;
		}
		UIManager.Instance.Event_CurrentProgress.text = "" + CurrentProgress + "/" + CurrentGoal;
		UIManager.Instance.Event_TimeLeft.text = "" + Utilities.DisplayTime(TimeLeft);
	}
	#endregion
}
