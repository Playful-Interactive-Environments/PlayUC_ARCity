﻿// This code automatically generated by TableCodeGen

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CSVQuests: AManager<CSVQuests>
{
    
    public class Row
    {
        public string id;
        public string title;
        public string content;
        public string choice_1;
        public string choice_2;
        public string result_1;
        public string result_2;
        public string effect_1;
        public string effect_2;
    }
    public TextAsset EnglishQuests;
    public TextAsset GermanQuests;
    private string EnglishText;
    private string GermanText;
    public List<Row> rowList = new List<Row>();

    bool isLoaded = false;
    public void LoadLanguage(string language)
    {
        rowList.Clear();
        switch (language)
        {
            case "english":
                Load(EnglishText);
                break;
            case "german":
                Load(GermanText);
                break;
        }
    }
    void Start()
    {
        LoadExternalFile();
        LoadLanguage("english");
    }

    void LoadExternalFile()
    {
        try
        {
            string _englishPath = Path.Combine(Application.persistentDataPath, "EnglishQuests.csv");
            string _germanPath = Path.Combine(Application.persistentDataPath, "GermanQuests.csv");
            EnglishText = File.ReadAllText(_englishPath, Encoding.UTF8);
            GermanText = File.ReadAllText(_germanPath, Encoding.UTF8);
            Debug.Log("File found.");
            NetworkingManager.Instance.DebugText.text = "found";
        }
        catch (Exception c)
        {
            Debug.Log("No file found. Loading defaults.");
            EnglishText = EnglishQuests.text;
            GermanText = GermanQuests.text;
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
            row.id = grid[i][0];
            row.title = grid[i][1];
            row.content = grid[i][2];
            row.choice_1 = grid[i][3];
            row.choice_2 = grid[i][4];
            row.result_1 = grid[i][5];
            row.result_2 = grid[i][6];
            row.effect_1 = grid[i][7];
            row.effect_2 = grid[i][8];
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

    public Row Find_ID(int find)
    {
        return rowList[find];
    }
    public List<Row> Find_ID_All(string find)
    {
        return rowList.FindAll(x => x.id == find);
    }
    public Row Find_Title(string find)
    {
        return rowList.Find(x => x.title == find);
    }
    public List<Row> Find_Title_All(string find)
    {
        return rowList.FindAll(x => x.title == find);
    }
    public Row Find_Content(string find)
    {
        return rowList.Find(x => x.content == find);
    }
    public List<Row> Find_Content_All(string find)
    {
        return rowList.FindAll(x => x.content == find);
    }
    public Row Find_Choice_1(string find)
    {
        return rowList.Find(x => x.choice_1 == find);
    }
    public List<Row> Find_Choice_1_All(string find)
    {
        return rowList.FindAll(x => x.choice_1 == find);
    }
    public Row Find_Choice_2(string find)
    {
        return rowList.Find(x => x.choice_2 == find);
    }
    public List<Row> Find_Choice_2_All(string find)
    {
        return rowList.FindAll(x => x.choice_2 == find);
    }

    public Row Find_Result_1(string find)
    {
        return rowList.Find(x => x.result_1 == find);
    }
    public List<Row> Find_Result_1_All(string find)
    {
        return rowList.FindAll(x => x.result_1 == find);
    }
    public Row Find_Result_2(string find)
    {
        return rowList.Find(x => x.result_2 == find);
    }
    public List<Row> Find_Result_2_All(string find)
    {
        return rowList.FindAll(x => x.result_2 == find);
    }
    public Row Find_Result_3(string find)
    {
        return rowList.Find(x => x.effect_1 == find);
    }
    public List<Row> Find_Result_3_All(string find)
    {
        return rowList.FindAll(x => x.effect_1 == find);
    }
    public Row Find_Effect(string find)
    {
        return rowList.Find(x => x.effect_2 == find);
    }
    public List<Row> Find_Effect_All(string find)
    {
        return rowList.FindAll(x => x.effect_2 == find);
    }
    #endregion
}