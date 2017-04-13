using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 1, sendInterval = .1f)]

public class CellManager : NetworkBehaviour
{
    public static CellManager Instance = null;
    public SyncListInt SocialRates = new SyncListInt();
    public SyncListInt EnvironmentRates = new SyncListInt();
    public SyncListInt FinanceRates = new SyncListInt();

    private int _maxValue;
    private int maxTotalValue;
    [SyncVar] int totalStartingSocial;
    [SyncVar] int totalStartingEnvironment;
    [SyncVar] int totalStartingFinance;
    [SyncVar] public int CurrentSocialGlobal;
    [SyncVar] public int CurrentEnvironmentGlobal;
    [SyncVar] public int CurrentFinanceGlobal;
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

    void Start()
    {            InvokeRepeating("UpdateGridVariables", 1f, .5f);

        if (isServer)
        {
            Invoke("GenerateValues", .1f);
            InvokeRepeating("CurrentGridVariables", 1f, .5f);
        }
    }

    void Update()
    {

    }

    void GenerateValues()
    {
        _maxValue = (int) Vars.Instance.SingleCellMaxVal;
        maxTotalValue = Vars.Instance.CellTotalVal;
        randSum(_cellGrid.Count, maxTotalValue, Vars.Player2);
        randSum(_cellGrid.Count, maxTotalValue, Vars.Player3);
        randSum(_cellGrid.Count, maxTotalValue, Vars.Player1);
        totalStartingSocial = CurrentSocialGlobal;
        totalStartingEnvironment = CurrentEnvironmentGlobal;
        totalStartingFinance = CurrentFinanceGlobal;
    }

    private float[] randSum(int n, int m, string type)
    {
        float sum = 0;
        float[] randNums = new float[n];

        for (int i = 0; (i < randNums.Length); i++)
        {
            //limit generated number to a 10% value from total cell sum
            randNums[i] = Utilities.RandomFloat(Mathf.RoundToInt((float) m/10), m);
            sum += randNums[i];
        }

        for (int i = 0; (i < randNums.Length); i++)
        {
            randNums[i] = randNums[i]/sum*m;
            int value = Mathf.RoundToInt(randNums[i]);
            //Debug.Log(randNums[i] + " " + randNums[i] / sum + " " + sum);

            switch (type)
            {
                case Vars.Player2:
                    SocialRates.Add(value);
                    CurrentSocialGlobal += value;
                    break;
                case Vars.Player3:
                    EnvironmentRates.Add(value);
                    CurrentEnvironmentGlobal += value;
                    break;
                case Vars.Player1:
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
            CurrentEnvironmentGlobal += _cellGrid.GetCellLogic(i).EnvironmentRate;
            CurrentSocialGlobal += _cellGrid.GetCellLogic(i).SocialRate;
            CurrentFinanceGlobal += _cellGrid.GetCellLogic(i).FinanceRate;
        }
    }

    void UpdateGridVariables()
    {
        UpdateCellVars(SocialRates, EnvironmentRates, FinanceRates);
    }

    public void UpdateFinance(int grid, int value)
    {
        if (FinanceRates[grid] + value > _maxValue)
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
            _cellGrid.GetCellLogic(i).SocialRate = social[i];
            _cellGrid.GetCellLogic(i).EnvironmentRate = environment[i];
            _cellGrid.GetCellLogic(i).FinanceRate = finance[i];
        }
    }

    #region End Game Stats

    public int GetTotalAddedValue()
    {
        return (CurrentEnvironmentGlobal + CurrentFinanceGlobal + CurrentSocialGlobal) -
               (totalStartingFinance + totalStartingEnvironment + totalStartingSocial);
    }

    public int GetMostImprovedValue()
    {
        return Utilities.GetHighestVal(CurrentSocialGlobal - totalStartingSocial,
            CurrentEnvironmentGlobal - totalStartingEnvironment, CurrentFinanceGlobal - totalStartingFinance);
    }

    public int GetLeastImprovedValue()
    {
        return Utilities.GetLowestVal(CurrentSocialGlobal - totalStartingSocial,
            CurrentEnvironmentGlobal - totalStartingEnvironment, CurrentFinanceGlobal - totalStartingFinance);
    }

    public void GetValueImages()
    {
        int highestVal = Utilities.GetHighestVal(CurrentSocialGlobal, CurrentEnvironmentGlobal, CurrentFinanceGlobal);
        if (highestVal == CurrentEnvironmentGlobal)
            UIManager.Instance.MostImprovedFieldImage.sprite = UIManager.Instance.Player3_winSprite;
        if (highestVal == CurrentSocialGlobal)
            UIManager.Instance.MostImprovedFieldImage.sprite = UIManager.Instance.Player2_winSprite;
        if (highestVal == CurrentFinanceGlobal)
            UIManager.Instance.MostImprovedFieldImage.sprite = UIManager.Instance.Player1_winSprite;

        int lowestVal = Utilities.GetLowestVal(CurrentSocialGlobal, CurrentEnvironmentGlobal, CurrentFinanceGlobal);
        if (lowestVal == CurrentEnvironmentGlobal)
            UIManager.Instance.LeastImprovedFieldImage.sprite = UIManager.Instance.Player3_winSprite;
        if (lowestVal == CurrentSocialGlobal)
            UIManager.Instance.LeastImprovedFieldImage.sprite = UIManager.Instance.Player2_winSprite;
        if (lowestVal == CurrentFinanceGlobal)
            UIManager.Instance.LeastImprovedFieldImage.sprite = UIManager.Instance.Player1_winSprite;
    }
#endregion

}

