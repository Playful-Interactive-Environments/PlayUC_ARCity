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
	public GameObject Cell;
	public CellLogic CellLogic;
	public GameObject[] RepresentationSets;
	public GameObject RepresentationParent;

	//result logic
	string savestring;
	int parsedValue;
	private string[] splitString;

	void Start () {
		Invoke("CreateRepresentation", .1f);
	}

	void CreateRepresentation()
	{
		GameObject representation = Instantiate(RepresentationSets[Utilities.RandomInt(0, RepresentationSets.Length - 1)], transform.position, Quaternion.identity) as GameObject;
		representation.transform.parent = RepresentationParent.transform;
		representation.transform.localScale = new Vector3(3, 3, 3);
		representation.transform.localEulerAngles += new Vector3(0, 180, 0);
		transform.position += CellLogic.GetPositionOffset();    
	}

	void Update () {
	
	}

	public void SetCell(GameObject cell)
	{
		Cell = cell;
		CellLogic = Cell.GetComponent<CellLogic>();
		CellManager.Instance.NetworkCommunicator.CellOccupiedStatus("add", CellLogic.CellId);

	}
	public void Choose(int effect)
	{
		if (effect == 1)
		{
			splitString = Effect1.Split('/');
			UIManager.Instance.UpdateResult(Result1);
			SaveStateManager.Instance.LogEvent("PLAYER: " + LevelManager.Instance.RoleType + " QUEST: " + Title + " CHOICE: " + Choice1 + " RESULT:" + Result1 + " EFFECT: " + Effect1);

		}

		if (effect == 2)
		{
			splitString = Effect2.Split('/');
			UIManager.Instance.UpdateResult(Result2);
			SaveStateManager.Instance.LogEvent("PLAYER: " + LevelManager.Instance.RoleType + " QUEST: " + Title + " CHOICE: " + Choice2 + " RESULT:" + Result2 + " EFFECT: " + Effect2);

		}

		for (int i = 0; i < splitString.Length; i++)
		{
			//even members are the names. save them and get corresponding values
			if (i % 2 == 0)
			{
				savestring = splitString[i];
			}
			//odd members are values. parse the value and act depending on the already saved name
			if (i % 2 != 0)
			{
				int.TryParse(splitString[i], NumberStyles.AllowLeadingSign, null, out parsedValue);
				if (savestring == "Influence")
				{
					CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Influence", parsedValue);
					UIManager.Instance.UpdateResult("Influence", splitString[i]);
				}
				if (savestring == "Budget")
				{
					CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Budget", parsedValue);
					UIManager.Instance.UpdateResult("Budget", splitString[i]);
				}
				if (savestring == "Project")
				{
					UIManager.Instance.UpdateResult("Project", "new");
				}
				if (savestring == "Environment")
				{
					CellManager.Instance.NetworkCommunicator.UpdateCellValue(savestring, CellLogic.CellId, parsedValue);
					UIManager.Instance.UpdateResult("Environment", splitString[i]);
				}
				if (savestring == "Finance")
				{
					CellManager.Instance.NetworkCommunicator.UpdateCellValue(savestring, CellLogic.CellId, parsedValue);
					UIManager.Instance.UpdateResult("Finance", splitString[i]);
				}
				if (savestring == "Social")
				{
					CellManager.Instance.NetworkCommunicator.UpdateCellValue(savestring, CellLogic.CellId, parsedValue);
					UIManager.Instance.UpdateResult("Social", splitString[i]);
				}
			}
		}
		RemoveQuest();
	}

	public void RemoveQuest()
	{
		QuestManager.Instance.RemoveQuest(ID);
		QuestManager.Instance.CurrentQuests -= 1;
		CellManager.Instance.NetworkCommunicator.CellOccupiedStatus("remove", CellLogic.CellId);
		CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Quest", 1);
	}
}
