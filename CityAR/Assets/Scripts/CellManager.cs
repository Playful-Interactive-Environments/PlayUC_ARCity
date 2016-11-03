using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CellManager : NetworkBehaviour
{
	public GameObject SpawnPrefab;
	public GameObject ImageTarget;
	public NetworkCommunicator NetworkCommunicator;
	public static CellManager Instance = null;
	public SyncListInt SocialRates = new SyncListInt();
	public SyncListInt EnvironmentRates = new SyncListInt();
	public SyncListInt FinanceRates = new SyncListInt();

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
					SocialRates.Add(Random.Range(0, 100));
					EnvironmentRates.Add(Random.Range(0, 100));
					FinanceRates.Add(Random.Range(0, 100));
			}
		}
		InvokeRepeating("UpdateGridVariables", 0f, 0.5f);
		ImageTarget = GameObject.Find("ImageTarget");
	}
	
	void Update () {
	
	}
	void UpdateGridVariables()
	{
		UpdateCellVars(SocialRates, EnvironmentRates, FinanceRates);
	}
	public void UpdateFinance(int grid, int value)
	{
		FinanceRates[grid] += value;
	}
	public void UpdateSocial(int grid, int value)
	{
		SocialRates[grid] += value;
	}
	public void UpdateEnvironment(int grid, int value)
	{
		EnvironmentRates[grid] += value;
	}
	public void UpdateCellVars(SyncListInt social, SyncListInt environment, SyncListInt finance)
	{
		for (int i = 0; i < HexGrid.Instance.cells.Length; i++)
		{
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().SocialRate = social[i];
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().EnvironmentRate = environment[i];
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().FinanceRate = finance[i];
		}
	}
}
