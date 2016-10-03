using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CellManager : NetworkBehaviour
{
    public GameObject SpawnPrefab;
    public GameObject ImageTarget;
    public NetworkCommunicator NetworkCommunicator;
    public static CellManager Instance = null;
    public SyncListInt UnemploymentRates = new SyncListInt();
	public SyncListInt PollutionRates = new SyncListInt();
	public SyncListBool OccupiedGrid = new SyncListBool();

	void Start () {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        if (isServer)
		{
			for (int i = 0; i < HexGrid.Instance.cells.Length; i++)
			{
				    UnemploymentRates.Add(Random.Range(0, 100));
					PollutionRates.Add(Random.Range(0, 100));
					OccupiedGrid.Add(false);
			}
		}
		InvokeRepeating("UpdateGridVariables", 0f, 0.5f);
        ImageTarget = GameObject.Find("ImageTarget");
    }
	
	void Update () {
	
	}
	void UpdateGridVariables()
	{
		UpdateCellVars(UnemploymentRates, PollutionRates, OccupiedGrid);
	}
	public void UpdateOccupiedVar(int grid, bool value)
	{
		OccupiedGrid[grid] = value;
	}
	public void UpdateUnemploymentVar(int grid, int value)
	{
		UnemploymentRates[grid] += value;
	}
	public void UpdatePollutionVar(int grid, int value)
	{
		PollutionRates[grid] += value;
	}
	public void UpdateCellVars(SyncListInt unemployment, SyncListInt pollution, SyncListBool occupied)
	{
		for (int i = 0; i < HexGrid.Instance.cells.Length; i++)
		{
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().JobsRate = unemployment[i];
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().PollutionRate = pollution[i];
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().Occupied = occupied[i];
		}
	}
}
