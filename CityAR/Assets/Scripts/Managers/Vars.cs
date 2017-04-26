using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

public class Vars : AManager<Vars>
{
    [Header("Cell Starting Values")]
    public int CellTotalVal;
    public float SingleCellMaxVal; //4 heatmap steps!

    [Header("Player Variables")]
    public int StartingBudget;
    public int MinPlayers;

    [Header("Game End Goals")]
    public float GameEndTime;
    public int UtopiaRate;
    public int MayorLevel;

    [Header("Mini Game Settings")]
    public float MiniGameTime = 30;
    public float[] Mg1_SpawnTimes;
    public int[] Mg1_DocsNeeded;
    public int[] Mg2_VotersNeeded;
    public int[] Mg2_VotersSpawned;
    public int[] Mg3_AgentNum;
    public int[] Mg3_AreaNeeded;
    [Header("Player Roles")]
    public const string Player1 = "Finance";
    public const string Player2 = "Social";
    public const string Player3 = "Environment";
    public const string MainValue1 = "Budget";
    public const string MainValue2 = "Influence";

    [Header("Voting Strings")]
    public const string Approved = "Approved";
    public const string Denied = "Denied";
    public const string Choice1 = "Choice1";
    public const string Choice2 = "Choice2";
    public const string ResultChoice1 = "Result_Choice1";
    public const string ResultChoice2 = "Result_Choice2";

    [Header("MgStrings")]
    public const string Mg1 = "Sort";
    public const string Mg2 = "Advertise";
    public const string Mg3 = "Area";
    public const string NoMg = "None";

    [Header("Event Messages")]
    public const string LocalClientDisconnect = "LocalClientDisconnect";
    public const string ServerHandleDisconnect = "ServerHandleDisconnect";



    #region CSV
    public class Row
    {
        public string startingbudget;
        public string minplayers;
        public string gameendtime;
        public string utopiarate;
        public string mayorlevel;
        public string minigametime;
    }
    public TextAsset VarsAsset;
    private string VarsText;
    public List<Row> rowList = new List<Row>();

    bool isLoaded = false;

    void Start()
    {
        LoadExternalFile();
        Load(VarsText);
        LoadVariables();
    }

    void LoadExternalFile()
    {
        try
        {
            string _varsPath = Path.Combine(Application.persistentDataPath, "GlobalVariables.csv");
            VarsText = File.ReadAllText(_varsPath, Encoding.UTF8);
            Debug.Log("File found.");
            NetworkingManager.Instance.DebugText.text = "found";
        }
        catch (Exception c)
        {
            Debug.Log("No file found. Loading defaults.");
            VarsText = VarsAsset.text;
        }
    }

    public void Load(string text)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(text);
        for (int i = 0; i < grid.Length; i++)
        {
            Row row = new Row();
            row.startingbudget = grid[i][0];
            row.minplayers = grid[i][1];
            row.gameendtime = grid[i][2];
            row.utopiarate = grid[i][3];
            row.mayorlevel = grid[i][4];
            row.minigametime = grid[i][5];
            rowList.Add(row);
        }
        isLoaded = true;
    }

    private void LoadVariables()
    {
        int i = 1; //numbers are on second line
        StartingBudget = ConvertToInt(rowList[i].startingbudget);
        MinPlayers = ConvertToInt(rowList[i].minplayers);
        GameEndTime = ConvertToFloat(rowList[i].gameendtime);
        UtopiaRate = ConvertToInt(rowList[i].utopiarate);
        MayorLevel = ConvertToInt(rowList[i].mayorlevel);
        MiniGameTime = ConvertToFloat(rowList[i].minigametime);
    }
    public List<Row> GetRowList()
    {
        return rowList;
    }

    public bool IsLoaded()
    {
        return isLoaded;
    }

    private int ConvertToInt(string input)
    {
        int parsedInt = 0;
        int.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
        return parsedInt;
    }
    private float ConvertToFloat(string input)
    {
        float parsedInt = 0;
        float.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
        return parsedInt;
    }
    #endregion
}
