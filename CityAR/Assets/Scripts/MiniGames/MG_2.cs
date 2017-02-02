using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_2 : AManager<MG_2> {

	public List<Vector3> Waypoints = new List<Vector3>();
	public List<Vector3> StartPoints = new List<Vector3>();
	public GameObject Advertisement;
	public GameObject TargetStage;
	public GameObject VoterPrefab;
	public int VotersNeeded = 20;
	public int VotersCollected;
	private float Height;
	private float Width;
	public float TimeLimit = 20f;
	void Start ()
	{
		ObjectPool.CreatePool(VoterPrefab, VotersNeeded);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Height = MGManager.Instance.Height;
		Width = MGManager.Instance.Width;
	}
	#region AdGame
	public void InitGame()
	{
		//create voter start points; position advertisement & stage
		float xEast = Width;
		float xWest = -Width/2;
		float yNorth = -Height/2;
		float ySouth = Height;

		for (int i = 0; i <= 20; i++)
		{
			StartPoints.Add(new Vector3(Utilities.RandomFloat(xWest, xEast), Utilities.RandomFloat(yNorth, ySouth), 0));

		}
		
		Advertisement.transform.position = new Vector3(0, Height / 4, 0);
		TargetStage.transform.position = new Vector3(0, -Height / 4, 0);

		for (int i = 0; i < VotersNeeded * 3; i++)
		{
			Vector3 startingPos = StartPoints[Utilities.RandomInt(0, StartPoints.Count)];
			GameObject voter = ObjectPool.Spawn(VoterPrefab, this.transform, startingPos, Quaternion.identity);
		}
	}
	#endregion

	public void ResetGame()
	{
		ObjectPool.RecycleAll(VoterPrefab);
		VotersCollected = 0;
		StartPoints.Clear();
		Waypoints.Clear();
		Advertisement.GetComponent<Advertisement>().Reset();
	}
}

/*
StartPoints.Add(new Vector3(0, 0, 0));
StartPoints.Add(new Vector3(-Height, 0, 0));
StartPoints.Add(new Vector3(Height, 0, 0));
StartPoints.Add(new Vector3(-Height, -Width, 0));
StartPoints.Add(new Vector3(0, Width, 0));
StartPoints.Add(new Vector3(0, -Width, 0));
		int waypoints = 50;
for (int i = 0; i <= waypoints; i++)
{
	Waypoints.Add(new Vector3(Utilities.RandomFloat(-Width, Width), Utilities.RandomFloat(-Height, Height), 0));
}
*/
