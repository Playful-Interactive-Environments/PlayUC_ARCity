using System.Collections;
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
    public string effect1;
    [SyncVar]
    public string effect2;
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
		EventManager.Instance.CurrentEventScript = GetComponent<EventScript>();
	}

	void TriggerEvent()
	{
		UIManager.Instance.SetEventText(title, content, choice1, choice2);
        NotificationManager.Instance.AddNotification("Event", title, content);
		switch (type)
		{
			case "Crisis":
				EventDispatcher.TriggerEvent("Crisis");
				break;
			case "DesignProject":
				EventDispatcher.TriggerEvent("DesignProject");
				break;
			case "Environment":
				_storedValue = CellManager.Instance.CurrentEnvironmentGlobal;
				EventDispatcher.TriggerEvent("HelpMayor");
				break;
			case "Finance":
				_storedValue = CellManager.Instance.CurrentFinanceGlobal;
				EventDispatcher.TriggerEvent("HelpMayor");
				break;
			case "Social":
				_storedValue = CellManager.Instance.CurrentSocialGlobal;
				EventDispatcher.TriggerEvent("HelpMayor");
				break;
			case "Budget":
				_storedValue = SaveStateManager.Instance.GetAllBudget();
				EventDispatcher.TriggerEvent("HelpMayor");
				break;
			case "Quest":
                _storedValue = SaveStateManager.Instance.GetAllQuests();
                EventDispatcher.TriggerEvent("HelpMayor");
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
                    UIManager.Instance.EventResultText.text = "Event Failed!\n " + effect1;
                    RemoveEvent();
                }
                HelpMayorEvent = false;
			}
			if (CurrentProgress >= CurrentGoal)
			{
				HelpMayorEvent = false;
                UIManager.Instance.EventResultText.text = "Event Successful!\n " + effect2;
                RemoveEvent();
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
				CurrentProgress = SaveStateManager.Instance.GetAllBudget() - _storedValue;
				break;
			case "Quest":
                CurrentProgress = SaveStateManager.Instance.GetAllQuests() - _storedValue;
                break;
		}
		UIManager.Instance.Event_CurrentProgress.text = "" + CurrentProgress + "/" + CurrentGoal;
		UIManager.Instance.Event_TimeLeft.text = "" + Utilities.DisplayTime(TimeLeft);
	}

    public void RemoveEvent()
    {
        UIManager.Instance.ShowEventResult();
        UIManager.Instance.EventVars.SetActive(false);
        Destroy(this);
    }
	#endregion
}
