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
        Cell = CellGrid.Instance.GetRandomCell();
        CellLogic = Cell.GetComponent<CellLogic>();
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
        representation.GetComponentInChildren<Animator>().SetBool("walk", true);
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
	    agent.isStopped = true;
		agent.velocity = new Vector3(0,0,0);
        representation.GetComponentInChildren<Animator>().SetBool("walk", false);
        yield return new WaitForSeconds(.5f);
        representation.GetComponentInChildren<Animator>().SetBool("wave", true);
		canMove = false;
		yield return new WaitForSeconds(7f);
		agent.speed = Utilities.RandomFloat(5, 15);
		representation.GetComponentInChildren<Animator>().SetBool("wave", false);
        representation.GetComponentInChildren<Animator>().SetBool("walk", true);
        canMove = true;
		nextPoint = Utilities.RandomInt(0, Waypoints.Count);
		nextWaypoint = Waypoints[nextPoint];
		agent.SetDestination(nextWaypoint);
	    agent.isStopped = false;
	}

	public void SetQuest(int randomId)
	{
        ID = randomId;

        Title = CSVQuests.Instance.Find_ID(randomId).title;
        Content = CSVQuests.Instance.Find_ID(randomId).content;

        Choice1 = CSVQuests.Instance.Find_ID(randomId).choice_1;
        Choice2 = CSVQuests.Instance.Find_ID(randomId).choice_2;

        Result1 = CSVQuests.Instance.Find_ID(randomId).result_1;
        Result2 = CSVQuests.Instance.Find_ID(randomId).result_2;

        Effect1 = CSVQuests.Instance.Find_ID(randomId).effect_1;
        Effect2 = CSVQuests.Instance.Find_ID(randomId).effect_2;
    }

	public void Choose(int effect)
	{
		if (effect == 1)
		{
			splitString = Effect1.Split('/');
			UIManager.Instance.UpdateResult(Result1);
			SaveStateManager.Instance.LogEvent("PLAYER: " + LocalManager.Instance.RoleType + " QUEST: " + Title + " CHOICE: " + Choice1 + " RESULT:" + Result1 + " EFFECT: " + Effect1);
		}

		if (effect == 2)
		{
			splitString = Effect2.Split('/');
			UIManager.Instance.UpdateResult(Result2);
			SaveStateManager.Instance.LogEvent("PLAYER: " + LocalManager.Instance.RoleType + " QUEST: " + Title + " CHOICE: " + Choice2 + " RESULT:" + Result2 + " EFFECT: " + Effect2);
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
				if (savestring == Vars.MainValue2)
				{
                    LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue2, parsedValue);
					UIManager.Instance.UpdateResult(Vars.MainValue2, splitString[i]);
				}
				if (savestring == Vars.MainValue1)
				{
                    LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue1, parsedValue);
					UIManager.Instance.UpdateResult(Vars.MainValue1, splitString[i]);
				}
				if (savestring == Vars.Player3)
				{
                    LocalManager.Instance.NetworkCommunicator.UpdateCellValue(savestring, CellLogic.CellId, parsedValue);
					UIManager.Instance.UpdateResult(Vars.Player3, splitString[i]);
				}
				if (savestring == Vars.Player1)
				{
                    LocalManager.Instance.NetworkCommunicator.UpdateCellValue(savestring, CellLogic.CellId, parsedValue);
					UIManager.Instance.UpdateResult(Vars.Player1, splitString[i]);
				}
				if (savestring == Vars.Player2)
				{
					LocalManager.Instance.NetworkCommunicator.UpdateCellValue(savestring, CellLogic.CellId, parsedValue);
					UIManager.Instance.UpdateResult(Vars.Player2, splitString[i]);
				}
			}
		}
		RemoveQuest();
	}

	public void RemoveQuest()
	{
		QuestManager.Instance.RemoveQuest(ID);
		QuestManager.Instance.CurrentQuests -= 1;
        LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Quest", 1);
	}
}