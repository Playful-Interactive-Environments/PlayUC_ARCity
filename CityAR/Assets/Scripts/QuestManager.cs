using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance = null;
	public CSVManager Quests;
	public CSVManagerProjects CSVProjects;
	public UIManager UI;
	private int questnum;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		Quests = CSVManager.Instance;
		UI = UIManager.Instance;
		CSVProjects = CSVManagerProjects.Instance;

	}

	public string GetProjectDescription(int projectnum)
	{
		string description = "";
		return description = CSVProjects.Find_ID(projectnum).title
			+ "\n" + CSVProjects.Find_ID(projectnum).content
			+ "\nRadius: " + CSVProjects.Find_ID(projectnum).radius
			+ "\nSocial: " + CSVProjects.Find_ID(projectnum).social
			+ "\nEnvironment: " + CSVProjects.Find_ID(projectnum).environment
			+ "\nFinance: " + CSVProjects.Find_ID(projectnum).finance;
	}

	void Update ()
	{

	}

	public void GetQuest()
	{
		questnum = (int)Random.Range(1, Quests.GetRowList().Count + 1);
		//Debug.Log(questnum);
		UI.Title.text = Quests.Find_ID(questnum).title;
		UI.Content.text = Quests.Find_ID(questnum).content;
		UI.Choice1.text = Quests.Find_ID(questnum).choice_1;
		UI.Choice2.text = Quests.Find_ID(questnum).choice_2;
		UI.Choice3.text = Quests.Find_ID(questnum).choice_3;
		//Debug.Log("" + Quests.Find_ID(questnum).title);
	}

	public string GetTitle(int num)
	{
		return CSVProjects.Find_ID(num).title;
	}
	public string GetContent(int num)
	{
		return CSVProjects.Find_ID(num).content;
	}

	public int GetSocial(int num)
	{
		string social = CSVProjects.Find_ID(num).social;
		//Debug.Log(ConvertString(social));
		return ConvertString(social);
	}
	public int GetFinance(int num)
	{
		string finance = CSVProjects.Find_ID(num).finance;
		//Debug.Log(ConvertString(finance));
		return ConvertString(finance);
	}
	public int GetRadius(int num)
	{
		string radius = CSVProjects.Find_ID(num).radius;
		//Debug.Log(ConvertString(radius));
		return ConvertString(radius);
	}
	public int GetEnvironment(int num)
	{
		string environment = CSVProjects.Find_ID(num).environment;
		//Debug.Log(ConvertString(environment));
		return ConvertString(environment);
	}

	private int ConvertString(string input)
	{
		int parsedInt = 0;
		int.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
		return parsedInt;

	}
	public void GetResult(int result)
	{
		switch (result)
		{
			case 1:
				UI.Result.text = Quests.Find_ID(questnum).result_1_id;
				break;
			case 2:
				UI.Result.text = Quests.Find_ID(questnum).result_2_id;
				break;
			case 3:
				UI.Result.text = Quests.Find_ID(questnum).result_3_id;
				break;
		}

	}
}
