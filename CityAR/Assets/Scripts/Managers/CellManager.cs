using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
[NetworkSettings(channel = 2, sendInterval = .5f)]

public class CellManager : NetworkBehaviour
{
	public GameObject ImageTarget;
	public NetworkCommunicator NetworkCommunicator;
	public static CellManager Instance = null;
	public SyncListInt SocialRates = new SyncListInt();
	public SyncListInt EnvironmentRates = new SyncListInt();
	public SyncListInt FinanceRates = new SyncListInt();

	private int _maxValue;
	private int maxTotalValue;
	int totalStartingSocial;
	int totalStartingEnvironment;
	int totalStartingFinance;
	[SyncVar]
	public int CurrentSocialGlobal;
	[SyncVar]
	public int CurrentEnvironmentGlobal;
	[SyncVar]
	public int CurrentFinanceGlobal;
	private CellGrid _cellGrid;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		_cellGrid = CellGrid.Instance;
	}

	void Start ()
	{
		Invoke("GenerateValues", .1f);
		InvokeRepeating("UpdateGridVariables", 1f, .5f);
		ImageTarget = GameObject.Find("ImageTarget");
		if (isServer)
		{
			InvokeRepeating("CurrentGridVariables", 1f, .5f);
		}
	}
	
	void Update ()
	{

	}
	
	void GenerateValues()
	{
		_maxValue = (int)Vars.Instance.SingleCellMaxVal;
		maxTotalValue = Vars.Instance.CellTotalVal;
		randSum(_cellGrid.Count, maxTotalValue, "Social");
		randSum(_cellGrid.Count, maxTotalValue, "Environment");
		randSum(_cellGrid.Count, maxTotalValue, "Finance");
	}

	private float[] randSum(int n, int m, string type)
	{
		float sum = 0;
		float[] randNums = new float[n];

		for (int i = 0; (i < randNums.Length); i++)
		{
			//limit generated number to a 10% value from total cell sum
			randNums[i] = Utilities.RandomFloat(Mathf.RoundToInt((float)m/10), m);
			sum += randNums[i];
		}

		for (int i = 0; (i < randNums.Length); i++)
		{
			randNums[i] = randNums[i] / sum * m;
			int value = Mathf.RoundToInt(randNums[i]);
			//Debug.Log(randNums[i] + " " + randNums[i] / sum + " " + sum);

			switch (type)
			{
				case "Social":
					SocialRates.Add(value);
					CurrentSocialGlobal += value;
					break;
				case "Environment":
					EnvironmentRates.Add(value);
					CurrentEnvironmentGlobal += value;
					break;
				case "Finance":
					FinanceRates.Add(value);
					CurrentFinanceGlobal += value;
					break;
			}
		}
		return randNums;
	}

	void CurrentGridVariables()
	{
		CurrentEnvironmentGlobal = 0;
		CurrentSocialGlobal = 0;
		CurrentFinanceGlobal = 0;
		for (int i = 0; i < _cellGrid.Count; i++)
		{
			CurrentEnvironmentGlobal += _cellGrid.GetCell(i).GetComponent<CellLogic>().EnvironmentRate;
			CurrentSocialGlobal += _cellGrid.GetCell(i).GetComponent<CellLogic>().SocialRate;
			CurrentFinanceGlobal += _cellGrid.GetCell(i).GetComponent<CellLogic>().FinanceRate;
		}
	}

	void UpdateGridVariables()
	{
		UpdateCellVars(SocialRates, EnvironmentRates, FinanceRates);
	}

	public void UpdateFinance(int grid, int value)
	{
		if(FinanceRates[grid] + value > _maxValue)
			FinanceRates[grid] = _maxValue;
		else if (FinanceRates[grid] + value < 0)
			FinanceRates[grid] = 0;
		else FinanceRates[grid] += value;
	}
	public void UpdateSocial(int grid, int value)
	{
		if (SocialRates[grid] + value > _maxValue)
			SocialRates[grid] = _maxValue;
		else if (SocialRates[grid] + value < 0)
			SocialRates[grid] = 0;
		else SocialRates[grid] += value;
	}
	public void UpdateEnvironment(int grid, int value)
	{
		if (EnvironmentRates[grid] + value > _maxValue)
			EnvironmentRates[grid] = _maxValue;
		else if (EnvironmentRates[grid] + value < 0)
			EnvironmentRates[grid] = 0;
		else EnvironmentRates[grid] += value;
	}
	public void UpdateCellVars(SyncListInt social, SyncListInt environment, SyncListInt finance)
	{
		for (int i = 0; i < _cellGrid.Count; i++)
		{
			_cellGrid.GetCell(i).GetComponent<CellLogic>().SocialRate = social[i];
			_cellGrid.GetCell(i).GetComponent<CellLogic>().EnvironmentRate = environment[i];
			_cellGrid.GetCell(i).GetComponent<CellLogic>().FinanceRate = finance[i];
		}
	}
}

