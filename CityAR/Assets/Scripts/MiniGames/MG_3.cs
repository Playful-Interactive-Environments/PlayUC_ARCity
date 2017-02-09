using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class MG_3 : AManager<MG_3>
{
	public struct Area
	{
		float X;
		float Y;
		Vector3 centerPoint;

		public Area(float x, float y, Vector3 center)
		{
			X = x;
			Y = y;
			centerPoint = center;
		}

		public bool Contains(Vector3 pos)
		{
			bool contains = false;
			float x = pos.x;
			float y = pos.y;
			if (GetWest().x <= x && GetEast().x >= x && GetSouth().y <= y && GetNorth().y >= y)
				contains = true;
			return contains;
		}

		public void ResetArea(float x, float y, Vector3 center)
		{
			X = x;
			Y = y;
			centerPoint = center;
		}

		public float GetX()
		{
			return X;
		}

		public float GetY()
		{
			return Y;
		}
		public void SetX(float x)
		{
			X = x;
		}

		public void SetY(float y)
		{
			Y = y;
		}
		public float AreaSize()
		{
			return X * Y;
		}
		public Vector3 GetCenter()
		{
			return centerPoint;
		}

		public void SetCenter(Vector3 center)
		{
			centerPoint = center;
		}
		public Vector3 GetSouth()
		{
			return new Vector3(centerPoint.x, centerPoint.y - (Y /2), 0);
		}
		public Vector3 GetNorth()
		{
			return new Vector3(centerPoint.x, centerPoint.y + (Y / 2), 0);
		}
		public Vector3 GetEast()
		{
			return new Vector3(centerPoint.x + (X/ 2), centerPoint.y, 0);
		}
		public Vector3 GetWest()
		{
			return new Vector3(centerPoint.x - (X / 2), centerPoint.y, 0);
		}

		public Vector3 GetSW()
		{
			return new Vector3(centerPoint.x - (X / 2), centerPoint.y - (Y / 2), 0);
		}
		public Vector3 GetSE()
		{
			return new Vector3(centerPoint.x + (X / 2), centerPoint.y - (Y / 2), 0);
			
		}
		public Vector3 GetNW()
		{
			return new Vector3(centerPoint.x - (X / 2), centerPoint.y + (Y / 2), 0);
		}
		public Vector3 GetNE()
		{
			return new Vector3(centerPoint.x + (X / 2), centerPoint.y + (Y / 2), 0);
		}
	}

	public float TimeLimit;
	public float CurrentPercent;
	public float PercentNeeded = 80f;
	private float Height;
	private float Width;
	public GameObject DrawPrefab;
	public GameObject WallPrefab;
	public GameObject AreaPrefab;
	//starting area
	private Area freeArea;
	private Area coverArea;
	private Area newFreeArea;
	private GameObject southWall;
	private GameObject northWall;
	private GameObject eastWall;
	private GameObject westWall;
	private float thickness = 3f;
	private float scaleoffset = 10f;
	private float posoffset = 9.5f;
	private float CoveredSize;
	private float StartSize;
	//agents
	public GameObject AgentPrefab;
	private int agentNum = 10;
	private int waypoints = 20;
	private List<GameObject> Agents = new List<GameObject>();
	public List<Vector3> AgentWaypoints = new List<Vector3>();
	//dragging
	private GameObject dragStartWall;
	private GameObject dragObject;
	private LineRenderer dragLine;
	private Vector3 lineStart;
	private Vector3 lineGoal;
	private Vector3 dragStart;
	private float minLength;
	public Material[] LineMats;
	private bool droppable;
	public int agentsContained;
	private float distanceSnap;
	private float currentLength;

	void Start () {
		
		ObjectPool.CreatePool(WallPrefab, 5);
		ObjectPool.CreatePool(AreaPrefab, 10);
		ObjectPool.CreatePool(DrawPrefab, 1);
		ObjectPool.CreatePool(AgentPrefab, 20);
	}
	
	void Update () {
		CurrentPercent = Mathf.Round(CoveredSize/StartSize*100f);
		if (dragLine != null)
			ContainsPos();
	}

	public void BeginDrag(Vector3 startPos, GameObject wall)
	{
		dragStartWall = wall;
		dragStart = new Vector3(startPos.x, startPos.y, 0);
		//create dragged object and assign its line renderer
		dragObject = ObjectPool.Spawn(DrawPrefab, dragStart, Quaternion.identity);
		dragObject.layer = LayerMask.NameToLayer("MG_3");
		dragObject.transform.parent = MGManager.Instance.MG_3_GO.transform;
		dragLine = dragObject.GetComponent<LineRenderer>();
		dragLine.numPositions = 2;
		dragLine.material = LineMats[1];
		//goal of dragged line is always symmetric of the starting point
		switch (dragStartWall.transform.name)
		{
			case "south":
				lineStart = new Vector3(dragStart.x, freeArea.GetSouth().y, 0);
				lineGoal = new Vector3(lineStart.x, lineStart.y + freeArea.GetY(), 0);
				break;
			case "north":
				lineStart = new Vector3(dragStart.x, freeArea.GetNorth().y, 0);
				lineGoal = new Vector3(lineStart.x, lineStart.y - freeArea.GetY(), 0);
				break;
			case "west":
				lineStart = new Vector3(freeArea.GetWest().x, dragStart.y, 0);
				lineGoal = new Vector3(lineStart.x + freeArea.GetX(), lineStart.y, 0);
				break;
			case "east":
				lineStart = new Vector3(freeArea.GetEast().x, dragStart.y, 0);
				lineGoal = new Vector3(lineStart.x - freeArea.GetX(), lineStart.y, 0);
				break;
		}
		minLength = Vector3.Distance(lineStart, lineGoal);
		CalculateAreas();
	}

	public void Drag(Vector3 dragEnd)
	{
		dragLine.SetPosition(0, dragStart);
		dragLine.SetPosition(1, dragEnd);
		currentLength = Vector3.Distance(dragStart, dragEnd);
		switch (dragStartWall.transform.name)
		{
			case "south":
				distanceSnap = Vector3.Distance(dragEnd, new Vector3(lineGoal.x, dragEnd.y, 0));
				break;
			case "north":
				goto case "south";
				break;
			case "west":
				distanceSnap = Vector3.Distance(dragEnd, new Vector3(dragEnd.x,lineGoal.y, 0));
				break;
			case "east":
				goto case "west";
				break;
		}
	}

	void ContainsPos()
	{
		agentsContained = 0;
		foreach (GameObject agent in Agents)
		{
			if (coverArea.Contains(agent.transform.position))
			{
				agentsContained += 1;
				agent.GetComponent<SpriteRenderer>().color = Color.red;
			}
			else
			{
				agent.GetComponent<SpriteRenderer>().color = Color.black;
			}
		}
		if (distanceSnap <= 5f && agentsContained == 0 && currentLength > minLength)
		{
			dragLine.material = LineMats[0];
			droppable = true;
		}
		else
		{
			dragLine.material = LineMats[1];
			droppable = false;
		}
	}

	void CalculateAreas()
	{
		float val1;
		float val2;
		switch (dragStartWall.transform.name)
		{
			case "south":
				val1 = Vector3.Distance(freeArea.GetSW(), lineStart);
				val2 = Vector3.Distance(freeArea.GetSE(), lineStart);
				//fill area west of division
				if (val1 < val2)
				{
					//update cover area size
					coverArea.ResetArea(val1, minLength, new Vector3(freeArea.GetSW().x + val1 / 2, freeArea.GetCenter().y, 0));
					//update possible new free area
					newFreeArea.ResetArea(val2, minLength,
						new Vector3(freeArea.GetSE().x - val2 / 2, freeArea.GetCenter().y, 0));
				}
				//fill area east of division
				if (val2 < val1)
				{
					coverArea.ResetArea(val2, minLength, new Vector3(freeArea.GetSE().x - val2 / 2, freeArea.GetCenter().y, 0));
					newFreeArea.ResetArea(val1, minLength,
						new Vector3(freeArea.GetSW().x + val1 / 2, freeArea.GetCenter().y, 0));
				}
				break;
			case "north":
				lineStart = lineGoal;
				goto case "south";
				break;
			case "west":
				val1 = Vector3.Distance(freeArea.GetNW(), lineStart);
				val2 = Vector3.Distance(freeArea.GetSW(), lineStart);
				//fill area west of division
				if (val1 < val2)
				{
					coverArea.SetX(minLength);
					coverArea.SetY(val1);
					coverArea.SetCenter(new Vector3(freeArea.GetCenter().x, freeArea.GetNW().y - val1 / 2, 0));
					newFreeArea.ResetArea(minLength, val2,
						new Vector3(freeArea.GetCenter().x, freeArea.GetSW().y + val2 / 2, 0));
				}
				if (val2 < val1)
				{
					coverArea.SetX(minLength);
					coverArea.SetY(val2);
					coverArea.SetCenter(new Vector3(freeArea.GetCenter().x, freeArea.GetSW().y + val2 / 2, 0));
					newFreeArea.ResetArea(minLength, val1,
						new Vector3(freeArea.GetCenter().x, freeArea.GetNW().y - val1 / 2, 0));
				}
				break;
			case "east":
				lineStart = lineGoal;
				goto case "west";
				break;
		}
	}

	void CreateCoveredArea()
	{
		float val1;
		float val2;
		GameObject occupyArea = ObjectPool.Spawn(AreaPrefab, MGManager.Instance.MG_3_GO.transform);
		occupyArea.transform.localScale = new Vector3(coverArea.GetX(), coverArea.GetY(), 1);
		occupyArea.transform.position = coverArea.GetCenter();
		CoveredSize += (coverArea.AreaSize());
		//adjust walls
		switch (dragStartWall.transform.name)
		{
			case "south":
				val1 = Vector3.Distance(freeArea.GetSW(), lineStart);
				val2 = Vector3.Distance(freeArea.GetSE(), lineStart);
				if (val1 < val2)
				{
					freeArea = newFreeArea;
					westWall.transform.localScale = new Vector3(thickness, minLength + thickness, 1);
					westWall.transform.position = freeArea.GetWest();
					northWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
					northWall.transform.position = freeArea.GetNorth();
					southWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
					southWall.transform.position = freeArea.GetSouth();
				}
				if (val2 < val1)
				{
					freeArea = newFreeArea;
					eastWall.transform.localScale = new Vector3(thickness, minLength + thickness, 1);
					eastWall.transform.position = freeArea.GetEast();

					northWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
					northWall.transform.position = freeArea.GetNorth();
					southWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
					southWall.transform.position = freeArea.GetSouth();
				}
				break;
			case "north":
				lineStart = lineGoal;
				goto case "south";
				break;
			case "west":
				val1 = Vector3.Distance(freeArea.GetNW(), lineStart);
				val2 = Vector3.Distance(freeArea.GetSW(), lineStart);
				if (val1 < val2)
				{
					freeArea = newFreeArea;
					northWall.transform.localScale = new Vector3(minLength + thickness, thickness, 1);
					northWall.transform.position = freeArea.GetNorth();

					westWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
					westWall.transform.position = freeArea.GetWest();
					eastWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
					eastWall.transform.position = freeArea.GetEast();
				}
				if (val2 < val1)
				{
					freeArea = newFreeArea;
					southWall.transform.localScale = new Vector3(minLength + thickness, thickness, 1);
					southWall.transform.position = freeArea.GetSouth();

					westWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
					westWall.transform.position = freeArea.GetWest();
					eastWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
					eastWall.transform.position = freeArea.GetEast();
				}
				break;
			case "east":
				lineStart = lineGoal;
				goto case "west";
				break;
		}
	}

	public void EndDrag()
	{
		if (droppable)
		{
			CreateCoveredArea();
			UpdateAgentBorders();
		}
		dragLine.numPositions = 0;
		dragLine = null;
		ObjectPool.Recycle(dragObject);
	}

	void UpdateAgentBorders()
	{
		float xEast = freeArea.GetEast().x - posoffset;
		float xWest = freeArea.GetWest().x + posoffset;
		float yNorth = freeArea.GetNorth().y - posoffset;
		float ySouth = freeArea.GetSouth().y + posoffset;

		foreach (GameObject agent in Agents)
		{
			agent.GetComponent<Agent>().SetWaypoints(xEast, xWest, yNorth, ySouth);
		}
	}

	public void SpawnAgents()
	{
		float xEast = freeArea.GetEast().x - posoffset;
		float xWest = freeArea.GetWest().x + posoffset;
		float yNorth = freeArea.GetNorth().y - posoffset;
		float ySouth = freeArea.GetSouth().y + posoffset;
		for (int i = 0; i < agentNum; i++)
		{
			Vector3 waypoint = new Vector3(Utilities.RandomFloat(xWest, xEast), Utilities.RandomFloat(yNorth, ySouth),0);
			GameObject agent = ObjectPool.Spawn(AgentPrefab, MGManager.Instance.MG_3_GO.transform, waypoint, Quaternion.identity);
			agent.layer = LayerMask.NameToLayer("MG_3");
			Agents.Add(agent);
		}
		UpdateAgentBorders();
	}

	public void InitGame()
	{
		Height = MGManager.Instance.Height;
		Width = MGManager.Instance.Width;
		freeArea = new Area(Width / 2, Height / 2, new Vector3(0, 0, 0));
		coverArea = new Area(0, 0, new Vector3(0, 0, 0));
		newFreeArea = new Area(0, 0, new Vector3(0, 0, 0));
		StartSize = freeArea.AreaSize();

		//Create SouthBorder
		southWall = ObjectPool.Spawn(WallPrefab, MGManager.Instance.MG_3_GO.transform, freeArea.GetSouth());
		southWall.layer = LayerMask.NameToLayer("MG_3");
		southWall.GetComponent<BoxCollider>().center = new Vector3(0, -posoffset, 0);
		southWall.GetComponent<BoxCollider>().size = new Vector3(1, scaleoffset*2, 0);
		southWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
		southWall.transform.name = "south";

		//Create NorthBorder
		northWall = ObjectPool.Spawn(WallPrefab, MGManager.Instance.MG_3_GO.transform, freeArea.GetNorth());
		northWall.layer = LayerMask.NameToLayer("MG_3");
		northWall.GetComponent<BoxCollider>().center = new Vector3(0, posoffset, 0);
		northWall.GetComponent<BoxCollider>().size = new Vector3(1, scaleoffset*2, 0);
		northWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
		northWall.transform.name = "north";

		//Create EastBorder
		eastWall = ObjectPool.Spawn(WallPrefab, MGManager.Instance.MG_3_GO.transform, freeArea.GetEast());
		eastWall.layer = LayerMask.NameToLayer("MG_3");
		eastWall.GetComponent<BoxCollider>().center = new Vector3(posoffset, 0, 0);
		eastWall.GetComponent<BoxCollider>().size = new Vector3(scaleoffset*2, 1, 0);
		eastWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
		eastWall.transform.name = "east";

		//Create WestBorder
		westWall = ObjectPool.Spawn(WallPrefab, MGManager.Instance.MG_3_GO.transform, freeArea.GetWest());
		westWall.layer = LayerMask.NameToLayer("MG_3");
		westWall.GetComponent<BoxCollider>().center = new Vector3(-posoffset, 0, 0);
		westWall.GetComponent<BoxCollider>().size = new Vector3(scaleoffset*2, 1, 0);
		westWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
		westWall.transform.name = "west";

		//create agents based on set borders
		SpawnAgents();
	}

	public void ResetGame()
	{
		ObjectPool.RecycleAll(AreaPrefab);
		ObjectPool.RecycleAll(WallPrefab);
		ObjectPool.RecycleAll(DrawPrefab);
		ObjectPool.RecycleAll(AgentPrefab);
		Agents.Clear();
		CoveredSize = 0;
		StartSize = 0;
		agentsContained = 0;
		AgentWaypoints.Clear();
	}
}


/*
	void CalculateAreas()
	{
		float val1;
		float val2;
		float drawnDistance = Vector3.Distance(lineStart, lineGoal);
		GameObject coveredArea = ObjectPool.Spawn(AreaPrefab, MGManager.Instance.MG_3_GO.transform);
		switch (dragStartWall.transform.name)
		{
			case "south":
				val1 = Vector3.Distance(freeArea.GetSW(), lineStart);
				val2 = Vector3.Distance(freeArea.GetSE(), lineStart);
				//fill area west of division
				if (val1 < val2)
				{
					//create cover area
					coveredArea.transform.localScale = new Vector3(val1, drawnDistance, 1);
						//define size of covered area
					coveredArea.transform.position = new Vector3(freeArea.GetSW().x + val1/2, freeArea.GetCenter().y, 0);
						//define center point of covered area
					//update free area
					freeArea.ResetArea(val2, drawnDistance,
						new Vector3(freeArea.GetSE().x - val2/2, freeArea.GetCenter().y, 0));
						//reset size and center point of free area
					//adjust west wall to new surface area
					westWall.transform.localScale = new Vector3(thickness, drawnDistance + thickness, 1);
					westWall.transform.position = freeArea.GetWest();
					//adjust other walls to fit the new free area
					northWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
					northWall.transform.position = freeArea.GetNorth();
					southWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
					southWall.transform.position = freeArea.GetSouth();
					coveredSize += (val1*drawnDistance); //adjust already occupied area
				}
				//fill area east of division
				if (val2 < val1)
				{
					coveredArea.transform.localScale = new Vector3(val2, drawnDistance, 1);
					coveredArea.transform.position = new Vector3(freeArea.GetSE().x - val2/2, freeArea.GetCenter().y, 0);
					freeArea.ResetArea(val1, drawnDistance,
						new Vector3(freeArea.GetSW().x + val1/2, freeArea.GetCenter().y, 0));

					eastWall.transform.localScale = new Vector3(thickness, drawnDistance + thickness, 1);
					eastWall.transform.position = freeArea.GetEast();

					northWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
					northWall.transform.position = freeArea.GetNorth();
					southWall.transform.localScale = new Vector3(freeArea.GetX() + thickness, thickness, 1);
					southWall.transform.position = freeArea.GetSouth();
					coveredSize += (val2*drawnDistance);
				}
				break;
			case "north":
				lineStart = lineGoal;
				goto case "south";
				break;
			case "west":
				val1 = Vector3.Distance(freeArea.GetNW(), lineStart);
				val2 = Vector3.Distance(freeArea.GetSW(), lineStart);
				//fill area west of division
				if (val1 < val2)
				{
					coveredArea.transform.localScale = new Vector3(drawnDistance, val1, 1);
					coveredArea.transform.position = new Vector3(freeArea.GetCenter().x, freeArea.GetNW().y - val1/2, 0);
					freeArea.ResetArea(drawnDistance, val2,
						new Vector3(freeArea.GetCenter().x, freeArea.GetSW().y + val2/2, 0));

					northWall.transform.localScale = new Vector3(drawnDistance + thickness, thickness, 1);
					northWall.transform.position = freeArea.GetNorth();

					westWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
					westWall.transform.position = freeArea.GetWest();
					eastWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
					eastWall.transform.position = freeArea.GetEast();
					coveredSize += (val1*drawnDistance);
				}
				if (val2 < val1)
				{
					coveredArea.transform.localScale = new Vector3(drawnDistance, val2, 1);
					coveredArea.transform.position = new Vector3(freeArea.GetCenter().x, freeArea.GetSW().y + val2/2, 0);
					freeArea.ResetArea(drawnDistance, val1,
						new Vector3(freeArea.GetCenter().x, freeArea.GetNW().y - val1/2, 0));

					southWall.transform.localScale = new Vector3(drawnDistance + thickness, thickness, 1);
					southWall.transform.position = freeArea.GetSouth();

					westWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
					westWall.transform.position = freeArea.GetWest();
					eastWall.transform.localScale = new Vector3(thickness, freeArea.GetY() + thickness, 1);
					eastWall.transform.position = freeArea.GetEast();
					coveredSize += (val2*drawnDistance);
				}
				break;
			case "east":
				lineStart = lineGoal;
				goto case "west";
				break;
		}
	}


			test1 = ObjectPool.Spawn(AreaPrefab, MGManager.Instance.MG_3_GO.transform);
		test2 = ObjectPool.Spawn(AreaPrefab,MGManager.Instance.MG_3_GO.transform);
		test2.transform.name = "test2";
		test1.transform.name= "test1";
		test1.layer = LayerMask.NameToLayer("MG_3");
		test2.layer = LayerMask.NameToLayer("MG_3");
			test1.transform.position = coverArea.GetCenter();
		test1.transform.localScale = new Vector3(coverArea.GetX(), coverArea.GetY(), 1);
		test2.transform.position = newFreeArea.GetCenter();
		test2.transform.localScale = new Vector3(newFreeArea.GetX(), newFreeArea.GetY(), 1);
	 */
