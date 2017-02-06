using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_2 : AManager<MG_2> {

	public GameObject Advertisement;
	public GameObject TargetStage;
	public GameObject VoterPrefab;
	public int VotersNeeded = 20;
    public int VotersCollected;
    private int agentNum;
    private int waypoints = 40;
	private float Height;
	private float Width;
	public float TimeLimit = 20f;
    private MGManager manager;
    public List<GameObject> Agents = new List<GameObject>();

	void Start ()
	{
		ObjectPool.CreatePool(VoterPrefab, VotersNeeded);
        manager = MGManager.Instance;

    }

    public void SetVars(int agents, int needed, float time)
    {
        agentNum = agents;
        VotersNeeded = needed;
        TimeLimit = time;
    }

    void Update ()
	{
		Height = manager.Height;
		Width = manager.Width;
	}
	#region AdGame
	public void InitGame()
	{
		//create voter start points; position advertisement & stage
		float xEast = Width;
		float xWest = -Width/2;
		float yNorth = -Height/2;
		float ySouth = Height;
		Advertisement.transform.position = new Vector3(0, Height / 4, 0);
		TargetStage.transform.position = new Vector3(0, -Height / 4, 0);

        for (int i = 0; i < agentNum; i++)
        {
            Vector3 waypoint = new Vector3(Utilities.RandomFloat(xWest, xEast), Utilities.RandomFloat(yNorth, ySouth), 0);
            GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform, waypoint, Quaternion.identity);
            agent.layer = LayerMask.NameToLayer("MG_2");
            Agents.Add(agent);
        }
        foreach (GameObject agent in Agents)
        {
            agent.GetComponent<Agent>().ResetWaypoints();
            for (int i = 0; i <= waypoints; i++)
            {
                Vector3 waypoint = new Vector3(Utilities.RandomFloat(xWest, xEast), Utilities.RandomFloat(yNorth, ySouth), 0);
                agent.GetComponent<Agent>().AddWaypoint(waypoint);
            }
        }
    }
	#endregion

	public void ResetGame()
	{
		ObjectPool.RecycleAll(VoterPrefab);
		VotersCollected = 0;
		Advertisement.GetComponent<Advertisement>().Reset();
	}
}

