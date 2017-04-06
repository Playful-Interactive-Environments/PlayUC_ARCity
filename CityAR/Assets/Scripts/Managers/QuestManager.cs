using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Networking;
using Vuforia;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance = null;
	public CSVQuests CSVQuests;
	public UIManager UI;
	private int questnum;
	public GameObject QuestPrefab;
	public int MaxQuests = 5;
	public int CurrentQuests;
	public float SpawnRate;
	public List<int> QuestIDs = new List<int>();
	public List<GameObject> QuestList = new List<GameObject>(); 
	public string[] Meanings = {"Disaster", "Very Bad", "Bad", "Moderate", "Good", "Very Good", "Excellent"};
	private int randomId;
	private float _questTime;
	private List<Vector3> startPoints = new List<Vector3>();
	void Start () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		CSVQuests = CSVQuests.Instance;
		UI = UIManager.Instance;
		Invoke("PopulateIds", .1f);
		randomId = GetRandomQuest();
		for (int i = 0; i < 5; i++)
		{
			startPoints.Add(new Vector3(Utilities.RandomFloat(ValueManager.xEast, ValueManager.xWest), 0, ValueManager.yNorth));
			startPoints.Add(new Vector3(Utilities.RandomFloat(ValueManager.xEast, ValueManager.xWest), 0, ValueManager.ySouth));
			startPoints.Add(new Vector3(ValueManager.xEast, 0, Utilities.RandomFloat(ValueManager.ySouth, ValueManager.yNorth)));
			startPoints.Add(new Vector3(ValueManager.xWest, 0, Utilities.RandomFloat(ValueManager.ySouth, ValueManager.yNorth)));
		}
		ObjectPool.CreatePool(QuestPrefab,6);
	}

	void PopulateIds()
	{
		for (int i = 1; i <= MaxQuests; i++)
		{
			QuestIDs.Add(i);
		}
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
		ObjectPool.Recycle(questdestroy);
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
            //TODO:Fix Random Cell
            GameObject obj = ObjectPool.Spawn(QuestPrefab, startPoints[Utilities.RandomInt(0, startPoints.Count)], Quaternion.identity);
			obj.transform.parent = LocalManager.Instance.ImageTarget.transform;
			obj.transform.name = "Quest";
			QuestList.Add(obj);
			Quest _quest = obj.GetComponent<Quest>();
		    _quest.SetQuest(randomId);
			randomId = GetRandomQuest();
			CurrentQuests++;
			_questTime = 0;
		}
	}

    public void LoadLanguage()
    {
        foreach (GameObject quest in QuestList)
        {
           quest.GetComponent<Quest>().SetQuest(quest.GetComponent<Quest>().ID);
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
}
