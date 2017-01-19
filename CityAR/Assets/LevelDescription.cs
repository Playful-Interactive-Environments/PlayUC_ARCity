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
	public string Unlock;
	string savestring;
	int parsedValue;
	private string[] splitString;

	void Start ()
	{
		csvLeveling = CSVLeveling.Instance;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetupLayout(int i)
	{
		LevelNumber = i;
		RankText.text = "" + LevelNumber;
		Unlock = csvLeveling.GetUnlock(i);
		SetUnlockType();
	}

	public void SetUnlockType()
	{
		splitString = Unlock.Split('/');
		//SaveStateManager.Instance.LogEvent("PLAYER: " + LevelManager.Instance.RoleType + " QUEST: " + Title + " CHOICE: " + Choice1 + " RESULT:" + Result1 + " EFFECT: " + Effect1);

		for (int i = 0; i < splitString.Length; i++)
		{
			//even members are the names. save them and get corresponding values
			if (i % 2 == 0)
			{
				savestring = splitString[i];
				UnlockTitle.text = splitString[i];

			}
			//odd members are values. parse the value and act depending on the already saved name
			if (i % 2 != 0)
			{
				int.TryParse(splitString[i], NumberStyles.AllowLeadingSign, null, out parsedValue);
				UnlockTitle.text += " " + splitString[i];
				if (savestring == "Event")
				{

				}
				if (savestring == "Budget")
				{

				}
				if (savestring == "Project")
				{

				}

			}
		}
	}
}
