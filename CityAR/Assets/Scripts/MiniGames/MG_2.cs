using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_2 : AManager<MG_2> {

	public float TimeLimit;
	public float TimeSpent;
	public int VotersNeeded = 10;
	public int VotersCollected;
	public GameObject Advertisement;
	public GameObject TargetStage;
	public GameObject VoterPrefab;

	private float Height;
	private float Width;

	private MGManager manager;
	public List<GameObject> Agents = new List<GameObject>();

	void Start ()
	{
		ObjectPool.CreatePool(VoterPrefab, VotersNeeded);
		manager = MGManager.Instance;
	}

	public void SetVars(int agents, int needed, float time)
	{
		VotersNeeded = needed;
		TimeLimit = time;
	}

	void Update ()
	{
		Height = manager.Height;
		Width = manager.Width;
	}
	#region AdGame
	public IEnumerator InitGame()
	{
		Advertisement.transform.position = new Vector3(0, Height / 4, 0);
		TargetStage.transform.position = new Vector3(0, -Height / 2 + 20, 50);
		for (int i = 0; i < 10; i++)
		{
			GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
			agent.layer = LayerMask.NameToLayer("MG_2");
			agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.Random);
			Agents.Add(agent);
		}
		for (int i = 0; i < 10; i++)
		{
			GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
			agent.layer = LayerMask.NameToLayer("MG_2");
			agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.Circle);
			Agents.Add(agent);
		}
		for (int i = 0; i < 5; i++)
		{
			GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
			agent.layer = LayerMask.NameToLayer("MG_2");
			agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.RightLeft);
			Agents.Add(agent); Agents.Add(agent);
			yield return new WaitForSeconds(.2f);
		}
		for (int i = 0; i < 5; i++)
		{
			GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
			agent.layer = LayerMask.NameToLayer("MG_2");
			agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.LeftRight);
			Agents.Add(agent); Agents.Add(agent);
			yield return new WaitForSeconds(.2f);
		}
		for (int i = 0; i < 5; i++)
		{
			GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
			agent.layer = LayerMask.NameToLayer("MG_2");
			agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.Downtop);
			Agents.Add(agent); Agents.Add(agent);
			yield return new WaitForSeconds(.2f);
		}
		for (int i = 0; i < 5; i++)
		{
			GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
			agent.layer = LayerMask.NameToLayer("MG_2");
			agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.TopDown);
			Agents.Add(agent);
			yield return new WaitForSeconds(.3f);
		}

	}
	#endregion

	public void ResetGame()
	{
		ObjectPool.RecycleAll(VoterPrefab);
		VotersCollected = 0;
	    TimeSpent = 0;
		Advertisement.GetComponent<Advertisement>().Reset();
	}
}

