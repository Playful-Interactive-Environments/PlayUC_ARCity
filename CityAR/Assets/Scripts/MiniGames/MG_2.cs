using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_2 : AManager<MG_2>
{

    public float TimeSpent;
    public int VotersCollected;
    public GameObject Advertisement;
    public GameObject TargetStage;
    public GameObject VoterPrefab;
    public GameObject Background;
    private float Height;
    private float Width;
    private MGManager manager;
    public List<GameObject> Agents = new List<GameObject>();

    //balancing
    private int timesPlayed;
    private int votersSpawned;
    public int VotersNeeded;

    void Start()
    {
        ObjectPool.CreatePool(VoterPrefab, VotersNeeded);
        manager = MGManager.Instance;
    }

    void Update()
    {
        Height = manager.Height;
        Width = manager.Width;
    }
    #region AdGame
    public void IncreaseDifficulty()
    {
        timesPlayed += 1;
    }

    public IEnumerator InitGame()
    {
        //load models
        Advertisement.transform.position = new Vector3(0, Height / 4, 0);
        TargetStage.transform.position = new Vector3(0, -Height / 2 + 40, 50);
        Background.transform.localScale = new Vector3(60, 60, 0);
        Background.transform.localPosition = new Vector3(0, 0, 100);
        //balance
        if (timesPlayed == 0 || timesPlayed == 1)
        {
            votersSpawned = Mathf.RoundToInt((float)Vars.Instance.Mg2_VotersSpawned[0] / 6);
            VotersNeeded = Vars.Instance.Mg2_VotersNeeded[0];
        }
        if (timesPlayed == 2 || timesPlayed == 3)
        {
            votersSpawned = Mathf.RoundToInt((float)Vars.Instance.Mg2_VotersSpawned[1] / 6);
            VotersNeeded = Vars.Instance.Mg2_VotersNeeded[1];
        }
        if (timesPlayed > 3)
        {
            votersSpawned = Mathf.RoundToInt((float)Vars.Instance.Mg2_VotersSpawned[2] / 6);
            VotersNeeded = Vars.Instance.Mg2_VotersNeeded[2];
        }
        //spawn agents 6 different patterns
        for (int i = 0; i < votersSpawned; i++)
        {
            GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
            agent.layer = LayerMask.NameToLayer("MG_2");
            agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.Random);
            Agents.Add(agent);
        }
        for (int i = 0; i < votersSpawned; i++)
        {
            GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
            agent.layer = LayerMask.NameToLayer("MG_2");
            agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.Circle);
            Agents.Add(agent);
        }
        for (int i = 0; i < votersSpawned; i++)
        {
            GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
            agent.layer = LayerMask.NameToLayer("MG_2");
            agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.RightLeft);
            Agents.Add(agent); Agents.Add(agent);
            yield return new WaitForSeconds(.2f);
        }
        for (int i = 0; i < votersSpawned; i++)
        {
            GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
            agent.layer = LayerMask.NameToLayer("MG_2");
            agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.LeftRight);
            Agents.Add(agent); Agents.Add(agent);
            yield return new WaitForSeconds(.2f);
        }
        for (int i = 0; i < votersSpawned; i++)
        {
            GameObject agent = ObjectPool.Spawn(VoterPrefab, manager.MG_2_GO.transform);
            agent.layer = LayerMask.NameToLayer("MG_2");
            agent.GetComponent<Agent>().SetWaypoints(Width, Height, Agent.MovementPattern.Downtop);
            Agents.Add(agent); Agents.Add(agent);
            yield return new WaitForSeconds(.2f);
        }
        for (int i = 0; i < votersSpawned; i++)
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

