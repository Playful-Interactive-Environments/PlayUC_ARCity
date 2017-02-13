using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class LevelDescription : MonoBehaviour {

	public int LevelNumber;
	public CSVLeveling csvLeveling;
	public Text RankText;
	public Text UnlockTitle;
	public string textToParse;
	private string UnlockType;
	private string UnlockValue;
	int parsedValue;
	private string[] splitString;
	private bool LevelReached;

	void Start ()
	{
		csvLeveling = CSVLeveling.Instance;
		GetComponent<Image>().color = Color.grey;
		GetComponent<Button>().enabled = false;
	}

	void Update () {
		if (LevelManager.Instance.CurrentRank == LevelNumber && !LevelReached)
		{
			GetComponent<Button>().enabled = true;
			GetComponent<Image>().color = Color.white;
			if (UnlockType == "Event")
			{
				EventManager.Instance.TriggerEvent(UnlockValue);
			}
			if (UnlockType == "Budget")
			{
				CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Budget", ConvertString(UnlockValue));
			}
			if (UnlockType == "Project")
			{
			    int projectId = 0;
                switch (LevelManager.Instance.RoleType)
                {
                    case "Environment":
                        projectId = ConvertString(csvLeveling.GetEnvironmentPlayer(LevelNumber));
                        break;
                    case "Social":
                        projectId = ConvertString(csvLeveling.GetSocialPlayer(LevelNumber));
                        break;
                    case "Finance":
                        projectId = ConvertString(csvLeveling.GetFinancePlayer(LevelNumber));
                        break;
                }
                ProjectManager.Instance.UnlockProject(projectId);
			}
			LevelReached = true;
		}
	}

	public void SetupLayout(int i)
	{
		LevelNumber = i;
		RankText.text = "" + LevelNumber;
		textToParse = csvLeveling.GetUnlock(i);
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
				UnlockType = splitString[i];
			}
			//odd members are values. parse the value and act depending on the already saved name
			if (i % 2 != 0)
			{
				int.TryParse(splitString[i], NumberStyles.AllowLeadingSign, null, out parsedValue);
				UnlockTitle.text += " " + splitString[i];
				UnlockValue = splitString[i];
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
