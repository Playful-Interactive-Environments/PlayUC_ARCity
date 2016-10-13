using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance = null;
	public CSVManager Quests;
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

	}
	
	void Update () {

	}

	public void GetQuest()
	{
		questnum = (int)Random.Range(1, Quests.GetRowList().Count+1);
		Debug.Log(questnum);
		UI.Title.text = Quests.Find_ID(questnum).title;
		UI.Content.text = Quests.Find_ID(questnum).content;
		UI.Choice1.text = Quests.Find_ID(questnum).choice_1;
		UI.Choice2.text = Quests.Find_ID(questnum).choice_2;
		UI.Choice3.text = Quests.Find_ID(questnum).choice_3;
		Debug.Log("" + Quests.Find_ID(questnum).title);
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
