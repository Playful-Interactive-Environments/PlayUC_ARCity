using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Networking;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance = null;
	public CSVManager CSVQuests;
	public CSVManagerProjects CSVProjects;
	public UIManager UI;
	private int questnum;

	public GameObject QuestPrefab;
	public int MaxQuests = 5;
	public int CurrentQuests;
	public float SpawnRate;
	public List<int> QuestIDs = new List<int>();
	public List<GameObject> QuestList = new List<GameObject>(); 
	public string[] Meanings = {"Catastrophal", "Very Bad", "Bad", "Moderate", "Good", "Very Good", "Excellent"};
	private int randomId;
	private float _questTime;
	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		CSVQuests = CSVManager.Instance;
		UI = UIManager.Instance;
		CSVProjects = CSVManagerProjects.Instance;
		Invoke("PopulateIds", .1f);
		randomId = GetRandomQuest();

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
		_questTime += Time.deltaTime;
		GenerateQuests();
	}

	void OnDestroy()
	{
		RemoveActiveQuests();
	}

	public void RemoveActiveQuests()
	{
		foreach (GameObject quest in QuestList)
		{
			Destroy(quest);
		}
		QuestList.Clear();
	}

	public void RemoveQuest(int id)
	{
	    GameObject questdestroy = null;
		foreach (GameObject obj in QuestList)
		{
			if (obj.GetComponent<Quest>().ID == id)
			{
			    questdestroy = obj;
			}
		}
	    QuestList.Remove(questdestroy);
		Destroy(questdestroy);
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
		if (CurrentQuests < MaxQuests && _questTime > SpawnRate)
		{
			HexCell cell = HexGrid.Instance.GetRandomCell();

			GameObject obj = Instantiate(QuestPrefab, cell.transform.position, Quaternion.identity) as GameObject;
			obj.transform.parent = CellManager.Instance.ImageTarget.transform;
			obj.transform.name = "Quest";
			QuestList.Add(obj);
			Quest _quest = obj.GetComponent<Quest>();
			_quest.ID = randomId;

			_quest.Title = CSVQuests.Find_ID(randomId).title;
			_quest.Content = CSVQuests.Find_ID(randomId).content;

			_quest.Choice1 = CSVQuests.Find_ID(randomId).choice_1;
			_quest.Choice2 = CSVQuests.Find_ID(randomId).choice_2;

			_quest.Result1 = CSVQuests.Find_ID(randomId).result_1;
			_quest.Result2 = CSVQuests.Find_ID(randomId).result_2;

			_quest.Effect1 = CSVQuests.Find_ID(randomId).effect_1;
			_quest.Effect2 = CSVQuests.Find_ID(randomId).effect_2;

			_quest.SetCell(cell);

			randomId = GetRandomQuest();

			CurrentQuests++;
			_questTime = 0;

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
