using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVEvents : AManager<CSVEvents> {


    public class Row
    {
        public string id;
        public string type;
        public string title;
        public string content;
        public string choice_1;
        public string choice_2;
        public string result_1;
        public string result_2;
        public string time;
        public string goal;
    }
    public TextAsset File;
    public List<Row> rowList = new List<Row>();

    bool isLoaded = false;

    void Start()
    {
        Load(File);
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
    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.id = grid[i][0];
            row.type = grid[i][1];
            row.title = grid[i][2];
            row.content = grid[i][3];
            row.choice_1 = grid[i][4];
            row.choice_2 = grid[i][5];
            row.result_1 = grid[i][6];
            row.result_2 = grid[i][7];
            row.time = grid[i][8];
            row.goal = grid[i][9];
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
        return rowList.Find(x => x.id == find.ToString());
    }
    public string GetType(int num)
    {
        return Find_ID(num).type;
    }
    public string GetTitle(int num)
    {
        return Find_ID(num).title;
    }
    public string GetContent(int num)
    {
        return Find_ID(num).content;
    }
    public string GetChoice1(int num)
    {
        return Find_ID(num).choice_1;
    }
    public string GetChoice2(int num)
    {
        return Find_ID(num).choice_2;
    }
    public string GetResult1(int num)
    {
        return Find_ID(num).result_1;
    }
    public string GetResult2(int num)
    {
        return Find_ID(num).result_2;
    }
    public string GetTime(int num)
    {
        return Find_ID(num).time;
    }
    public string GetGoal(int num)
    {
        return Find_ID(num).goal;
    }
    #endregion
}
