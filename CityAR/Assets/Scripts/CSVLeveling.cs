using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CSVLeveling : AManager<CSVLeveling>
{
    public class Row
    {
        public string rank;
        public string unlock;
        public string influencegoal;
        public string environmentplayer;
        public string socialplayer;
        public string financeplayer;

    }
    public TextAsset LevelFile;
    private string LevelingText;
    public List<Row> rowList = new List<Row>();

    bool isLoaded = false;

    void Start()
    {
        LoadExternalFile();
        Load(LevelingText);
    }
    void LoadExternalFile()
    {
        try
        {
            string _levelPath = Path.Combine(Application.persistentDataPath, "LevelingSystem.csv");
            LevelingText = File.ReadAllText(_levelPath, Encoding.UTF8);
            Debug.Log("File found.");
            NetworkingManager.Instance.DebugText.text = "found";
        }
        catch (Exception c)
        {
            Debug.Log("No file found. Loading defaults.");
            LevelingText = LevelFile.text;
        }
    }

    void Update()
    {

    }

    #region CSV Commands
    public List<Row> GetRowList()
    {
        return rowList;
    }

    public bool IsLoaded()
    {
        return isLoaded;
    }
    public void Load(string text)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(text);
        for (int i = 0; i < grid.Length; i++)
        {
            Row row = new Row();
            row.rank = grid[i][0];
            row.unlock = grid[i][1];
            row.influencegoal = grid[i][2];
            row.environmentplayer = grid[i][3];
            row.socialplayer = grid[i][4];
            row.financeplayer = grid[i][5];
            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

    public Row Find_Rank(int find)
    {
        return rowList[find];
    }
    public string GetUnlock(int num)
    {
        return Find_Rank(num).unlock;
    }
    public string GetInfluenceNeeded(int num)
    {
        return Find_Rank(num).influencegoal;
    }
    public string GetEnvironmentPlayer(int num)
    {
        return Find_Rank(num).environmentplayer;
    }
    public string GetSocialPlayer(int num)
    {
        return Find_Rank(num).socialplayer;
    }
    public string GetFinancePlayer(int num)
    {
        return Find_Rank(num).financeplayer;
    }
    
    #endregion
}
