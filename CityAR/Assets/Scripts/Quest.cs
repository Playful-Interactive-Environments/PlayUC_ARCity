using System;
using UnityEngine;
using System.Collections;
using System.Globalization;

public class Quest : MonoBehaviour
{

	public int ID;
	public string Title;
	public string Content;
	public string Choice1;
	public string Choice2;
	public string Result1;
	public string Result2;
	public string Effect1;
	public string Effect2;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void ChooseEffect1()
	{
		if (GetString(Effect1) == "Rating")
		{
			CellManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Rating", GetInt(Effect1));
		}
		if (GetString(Effect1) == "Budget")
		{
			CellManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Budget", GetInt(Effect1));
		}
		RemoveQuest();
	}
	public void ChooseEffect2()
	{

		if (GetString(Effect2) == "Rating")
		{
			CellManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Rating", GetInt(Effect2));
		}
		if (GetString(Effect2) == "Budget")
		{
			CellManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Budget", GetInt(Effect2));
		}
		RemoveQuest();
	}

	public string GetString(string tosplit)
	{
		string[] splitString = tosplit.Split(' ');
		string type = splitString[0];
		return type;
	}

	public int GetInt(string tosplit)
	{
		string[] splitString = tosplit.Split(' ');
		int parsedInt;
		int.TryParse(splitString[1], NumberStyles.AllowLeadingSign, null, out parsedInt);
		return parsedInt;
	}

	public void RemoveQuest()
	{
		QuestManager.Instance.CurrentQuests -= 1;
		Destroy(gameObject);
	}
}
