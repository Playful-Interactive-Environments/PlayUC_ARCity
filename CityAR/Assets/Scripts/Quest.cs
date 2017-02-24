using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using UnityEngine.AI;

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
	public GameObject Logo;
	private float yRot;
	public GameObject[] RepresentationSets;
	public GameObject RepresentationParent;
	private GameObject representation;
	//result logic
	string savestring;
	int parsedValue;
	private string[] splitString;
	private List<Vector3> Waypoints = new List<Vector3>();
	private Vector3 nextWaypoint;
	private int nextPoint;
	public bool canMove;
	public float distance;
	//Agent Movement
	private NavMeshAgent agent;

	void Awake ()
	{
		agent = GetComponent<NavMeshAgent>();
		for (int i = 0; i <= 5f; i++)
		{
			Vector3 waypoint = new Vector3(Utilities.RandomFloat(ValueManager.xWest, ValueManager.xEast), 0, Utilities.RandomFloat(ValueManager.yNorth, ValueManager.ySouth));
			Waypoints.Add(waypoint);
		}
		Waypoints.Add(new Vector3(Utilities.RandomFloat(ValueManager.xEast, ValueManager.xWest), 0, ValueManager.yNorth));
		Waypoints.Add(new Vector3(Utilities.RandomFloat(ValueManager.xEast, ValueManager.xWest), 0, ValueManager.ySouth));
		Waypoints.Add(new Vector3(ValueManager.xEast, 0, Utilities.RandomFloat(ValueManager.ySouth, ValueManager.yNorth)));
		Waypoints.Add(new Vector3(ValueManager.xWest, 0, Utilities.RandomFloat(ValueManager.ySouth, ValueManager.yNorth)));
		Waypoints.Add(new Vector3(ValueManager.Instance.MapWidth/2, 0, ValueManager.Instance.MapHeight / 2));
	}

	void OnEnable()
	{
		agent.speed = Utilities.RandomFloat(10, 20);
		canMove = true;
		nextPoint = Utilities.RandomInt(0, Waypoints.Count);
		nextWaypoint = Waypoints[nextPoint];
		agent.SetDestination(nextWaypoint);
	}

	void Start()
	{
		CreateRepresentation();
	}

	void CreateRepresentation()
	{
		representation = Instantiate(RepresentationSets[Utilities.RandomInt(0, RepresentationSets.Length - 1)], transform.position, Quaternion.identity);
		representation.transform.parent = RepresentationParent.transform;
		representation.transform.localScale = new Vector3(75, 75, 75);
	}

	void Update ()
	{
		distance = Vector3.Distance(transform.position, nextWaypoint);
		yRot += Time.deltaTime * 50f;
		Logo.transform.localEulerAngles = new Vector3(0, yRot, 0);
		if (distance <= 10f && canMove)
		{
			StartCoroutine(GetTarget());
		}
	}


	IEnumerator GetTarget()
	{
		agent.Stop();
		agent.velocity = new Vector3(0,0,0);
		representation.GetComponentInChildren<Animator>().SetBool("wave", true);
		canMove = false;
		yield return new WaitForSeconds(Utilities.RandomFloat(2,5));
		agent.speed = Utilities.RandomFloat(5, 15);
		representation.GetComponentInChildren<Animator>().SetBool("wave", false);
		canMove = true;
		nextPoint = Utilities.RandomInt(0, Waypoints.Count);
		nextWaypoint = Waypoints[nextPoint];
		agent.SetDestination(nextWaypoint);
		agent.Resume();
	}
	public void SetCell(GameObject cell)
	{
		Cell = cell;
		CellLogic = Cell.GetComponent<CellLogic>();

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
		CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Quest", 1);
	}
}