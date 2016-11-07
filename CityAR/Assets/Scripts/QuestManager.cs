using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Networking;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance = null;
	public CSVManager Quests;
	public CSVManagerProjects CSVProjects;
	public UIManager UI;
	private int questnum;

	public GameObject QuestPrefab;
	public int MaxQuests = 5;
	public int CurrentQuests;
	public float SpawnRate;
	public List<int> QuestIDs = new List<int>();
	public string[] Meanings = {"Catastrophal", "Very Bad", "Bad", "Moderate", "Good", "Very Good", "Excellent"};


	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		Quests = CSVManager.Instance;
		UI = UIManager.Instance;
		CSVProjects = CSVManagerProjects.Instance;
		InvokeRepeating("GenerateQuests", 5f, SpawnRate);
		Invoke("PopulateIds", .1f);
	}

	void PopulateIds()
	{
		for (int i = 1; i <= MaxQuests; i++)
		{
			QuestIDs.Add(i);
		}
	}
	public string GetProjectDescription(int projectnum)
	{
		string description = "";
		return description = GetTitle(projectnum)
							 + "\n" + GetContent(projectnum)
							 + "\nRating: " + GetRating(projectnum)
							 + "\nCost: " + GetCost(projectnum)
							 +"\nSocial: " + TranslateMeaning(GetSocial(projectnum))
							 + "\nEnvironment: " + TranslateMeaning(GetEnvironment(projectnum))
							 + "\nFinance: " + TranslateMeaning(GetFinance(projectnum));
	}

	void Update ()
	{

	}

	public string TranslateMeaning(int number)
	{
		string meaning = "";
		switch (number)
		{
			case -3:
				meaning = Meanings[0];
				break;
			case -2:
				meaning = Meanings[1];
				break;
			case -1:
				meaning = Meanings[2];
				break;
			case 0:
				meaning = Meanings[3];
				break;
			case 1:
				meaning = Meanings[4];
				break;
			case 2:
				meaning = Meanings[5];
				break;
			case 3:
				meaning = Meanings[6];
				break;
		}

		return meaning;
	}
	public void GenerateQuests()
	{
		if (CurrentQuests < MaxQuests)
		{
			Vector3 pos = HexGrid.Instance.GetRandomPos();
			GameObject obj = Instantiate(QuestPrefab, pos, Quaternion.identity) as GameObject;
			obj.transform.parent = CellManager.Instance.ImageTarget.transform;
			obj.transform.name = "Quest";
			Quest _quest = obj.GetComponent<Quest>();

			_quest.ID = GetRandomQuest();
			_quest.Title = Quests.Find_ID(_quest.ID).title;
			_quest.Content = Quests.Find_ID(_quest.ID).content;

			_quest.Choice1 = Quests.Find_ID(_quest.ID).choice_1;
			_quest.Choice2 = Quests.Find_ID(_quest.ID).choice_2;

			_quest.Result1 = Quests.Find_ID(_quest.ID).result_1;
			_quest.Result2 = Quests.Find_ID(_quest.ID).result_2;

			_quest.Effect1 = Quests.Find_ID(_quest.ID).effect_1;
			_quest.Effect2 = Quests.Find_ID(_quest.ID).effect_2;

			CurrentQuests++;
		}
	}

	int GetRandomQuest()
	{
		if (QuestIDs.Count == 0)
			PopulateIds();
		int randomQuest = Random.Range(0, QuestIDs.Count);

		int returnId = QuestIDs[randomQuest];
		QuestIDs.RemoveAt(randomQuest);
		return returnId;
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
	public int GetRating(int num)
	{
		string rating = CSVProjects.Find_ID(num).rating;
		//Debug.Log(ConvertString(radius));
		return ConvertString(rating);
	}
	public int GetEnvironment(int num)
	{
		string environment = CSVProjects.Find_ID(num).environment;
		//Debug.Log(ConvertString(environment));
		return ConvertString(environment);
	}
	public int GetCost(int num)
	{
		string cost = CSVProjects.Find_ID(num).cost;
		//Debug.Log(ConvertString(environment));
		return ConvertString(cost);
	}

	private int ConvertString(string input)
	{
		int parsedInt = 0;
		int.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
		return parsedInt;

	}
}
