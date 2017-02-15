using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class LevelDescription : MonoBehaviour {

	public int ThisLevel;
	public CSVLeveling levelData;
	public Text RankText;
	public Text UnlockTitle;
	public string textToParse;
	private string BonusType;
	private string BonusValue;
	int parsedValue;
	private string[] splitString;
	private bool levelUnlocked;
	private bool bonusUnlocked;

	void Start ()
	{
		levelData = CSVLeveling.Instance;
		GetComponent<Image>().color = Color.grey;
		GetComponent<Button>().enabled = false;
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
	}

	void NetworkDisconnect()
	{
		if(transform.name != "LevelTemplate")
			Destroy(gameObject);
	}

	void Update () {
		if (ThisLevel <= LevelManager.Instance.CurrentRank  && !levelUnlocked)
		{
			GetComponent<Button>().enabled = true;
			GetComponent<Image>().color = Color.white;
			if (BonusType == "Event" && !bonusUnlocked)
			{
				//EventManager.Instance.TriggerEvent(BonusValue);
			}
			if (BonusType == "Budget" && !bonusUnlocked)
			{
				CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Budget", ConvertString(BonusValue));
			}
			if (BonusType == "Project")
			{
				int projectId = 0;
				switch (LevelManager.Instance.RoleType)
				{
					case "Environment":
						projectId = ConvertString(levelData.GetEnvironmentPlayer(ThisLevel));
						break;
					case "Social":
						projectId = ConvertString(levelData.GetSocialPlayer(ThisLevel));
						break;
					case "Finance":
						projectId = ConvertString(levelData.GetFinancePlayer(ThisLevel));
						break;
				}
				ProjectManager.Instance.UnlockProject(projectId);
			}
			levelUnlocked = true;
		}
	}

	public void SetupLayout(int i)
	{

		if (LevelManager.Instance.CurrentRank > 0)
		{
			bonusUnlocked = true;
		}
		ThisLevel = i;
		RankText.text = "" + ThisLevel;
		textToParse = levelData.GetUnlock(i);
		ExtractData();
	}

	public void ExtractData()
	{
		splitString = textToParse.Split('/');
		//SaveStateManager.Instance.LogEvent("PLAYER: " + LevelManager.Instance.RoleType + " QUEST: " + Title + " CHOICE: " + Choice1 + " RESULT:" + Result1 + " EFFECT: " + Effect1);

		for (int i = 0; i < splitString.Length; i++)
		{
			//even members are the names. save them and get corresponding values
			if (i % 2 == 0)
			{
				UnlockTitle.text = splitString[i];
				BonusType = splitString[i];
			}
			//odd members are values. parse the value and act depending on the already saved name
			if (i % 2 != 0)
			{
				int.TryParse(splitString[i], NumberStyles.AllowLeadingSign, null, out parsedValue);
				UnlockTitle.text += " " + splitString[i];
				BonusValue = splitString[i];
			}
		}
	}

	private int ConvertString(string input)
	{
		int parsedInt = 0;
		int.TryParse(input, NumberStyles.Any, null, out parsedInt);
		return parsedInt;
	}
}
