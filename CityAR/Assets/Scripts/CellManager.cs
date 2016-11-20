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

    private int _maxValue;
    int totalAllowedPerCell;
    int totalStartingSocial;
    int totalStartingEnvironment;
    int totalStartingFinance;
    public int TotalEndSocial;
    public int TotalEndEnvironment;
    public int TotalEndFinance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }

    void GenerateValues()
    {
        _maxValue = (int)GlobalManager.Instance.CellMaxValue; //gets the max value per cell
        totalAllowedPerCell = _maxValue * 2; //all three areas have a total limit of twice the max value per cell
       

        if (isServer)
        {
            for (int i = 0; i < HexGrid.Instance.cells.Length; i++)
            {
                int socialrate = Random.Range(0, _maxValue); //generate social var
                int environmentrate = Random.Range(0, _maxValue); // generate environment var
                int financerate = totalAllowedPerCell - (socialrate + environmentrate); //leftover is finance var
                SocialRates.Add(socialrate);
                EnvironmentRates.Add(environmentrate);
                FinanceRates.Add(financerate);
                totalStartingEnvironment += environmentrate;
                totalStartingFinance += financerate;
                totalStartingSocial += socialrate;
            }
        }
    }

    void CalculateEndState()
    {

    }

	void Start ()
    {


        GenerateValues();
		InvokeRepeating("UpdateGridVariables", 0f, 0.5f);
		ImageTarget = GameObject.Find("ImageTarget");
	}
	
	void Update ()
    {
	
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
		for (int i = 0; i < HexGrid.Instance.cells.Length; i++)
		{
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().SocialRate = social[i];
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().EnvironmentRate = environment[i];
			HexGrid.Instance.cells[i].GetComponent<CellLogic>().FinanceRate = finance[i];
		}
	}
}
