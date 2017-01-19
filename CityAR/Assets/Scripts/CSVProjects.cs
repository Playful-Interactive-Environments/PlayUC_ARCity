﻿// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSVProjects : AManager<CSVProjects>
{
    
    public class Row
    {
        public string id;
        public string title;
        public string content;
        public string influence;
        public string social;
        public string environment;
        public string finance;
        public string cost;

    }
    public TextAsset File;
    public List<Row> rowList = new List<Row>();

    bool isLoaded = false;

    void Start()
    {
        Load(File);
    }

    #region CSV Commands
    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void AddNew(int id, string title, string content, int influence, int social, int environment, int finance, int cost)
    {
        Row row = new Row();
        row.id = "" + id;
        row.title = title;
        row.content = content;
        row.influence = "" + influence;
        row.social = "" + social;
        row.environment = "" + environment;
        row.finance = "" + finance;
        row.cost = "" + cost;
        rowList.Add(row);
    }
    public bool IsLoaded()
    {
        return isLoaded;
    }
    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 0; i < grid.Length; i++)
        {
            Row row = new Row();
            row.id = grid[i][0];
            row.title = grid[i][1];
            row.content = grid[i][2];
            row.influence = grid[i][3];
            row.social = grid[i][4];
            row.environment = grid[i][5];
            row.finance = grid[i][6];
            row.cost = grid[i][7];
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
    public Row Find_Title(string find)
    {
        return rowList.Find(x => x.title == find);
    }

    #endregion
}