using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Vuforia;

public class MG_1 : AManager<MG_1>
{
	public int CollectedDocs;
	public GameObject WordPrefab;
	//public GameObject DropPrefab;
	public GameObject Background;
	public List<GameObject> WordList = new List<GameObject>();
	public GameObject TargetFinance;
	public GameObject TargetSocial;
	public GameObject TargetEnvironment;
	private MGManager manager;
	private float Height;
	private float Width;
	public Vector3 StartingPos;
	public float zLayer = 100f;
    //balancing
    public int DocsNeeded = 5;
    private float spawnTime = 3.5f;
    private int timesPlayed;
    private string currentDifficulty;
    //CSV Rows
    public class Row
	{
		public string type;
	    public string difficulty;
        public string english;
        public string german;
        public string french;
        public string dutch;
    }

    public TextAsset WordFile;
	public List<Row> rowList = new List<Row>();
    private List<Row> wordsEasy = new List<Row>();
    private List<Row> wordsMedium = new List<Row>();
    private List<Row> wordsHard = new List<Row>();

    private bool isLoaded = false; 

	void Start ()
	{	
		ObjectPool.CreatePool(WordPrefab, 5);
		//ObjectPool.CreatePool(DropPrefab, 3);
		manager = MGManager.Instance;
		Load(WordFile);
        Invoke("SetSize", 0.1f);
	}

	void SetSize()
	{
		Height = manager.Height;
		Width = manager.Width;
		Vector3 finDropPos = new Vector3(-Width / 3, Height / 4, 0);

		//TargetFinance = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, finDropPos, Quaternion.identity);
		TargetFinance.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Finance;
		TargetFinance.transform.name = Vars.Player1;
		TargetFinance.transform.GetComponentInChildren<TextMesh>().text = Vars.Player1;
		TargetFinance.transform.position = finDropPos;
		TargetFinance.transform.localScale = new Vector3(1, 1, 1);

		Vector3 socDropPos = new Vector3(0, Height / 4, 0);
		//TargetSocial = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, socDropPos, Quaternion.identity);
		TargetSocial.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Social;
		TargetSocial.transform.name = Vars.Player2;
		TargetSocial.transform.GetComponentInChildren<TextMesh>().text = Vars.Player2;
		TargetSocial.transform.localScale = new Vector3(1, 1, 1);
		TargetSocial.transform.position = socDropPos;

		Vector3 envDropBos = new Vector3(Width / 3, Height / 4, 0);
		//TargetEnvironment = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, envDropBos, Quaternion.identity);
		TargetEnvironment.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Environment;
		TargetEnvironment.transform.name = Vars.Player3;
		TargetEnvironment.transform.GetComponentInChildren<TextMesh>().text = Vars.Player3;
		TargetEnvironment.transform.localScale = new Vector3(1, 1, 1);
		TargetEnvironment.transform.position = envDropBos;

		Background.transform.localScale = new Vector3(60, 60, 0);
		Background.transform.localPosition = new Vector3(0, 0, zLayer + 25);
		StartingPos = new Vector3(0, -Height / 4, 0);
	}

	void Update()
	{

	}

    void GetWord()
    {
        
    }
	public void SpawnWord()
	{
		GameObject word = ObjectPool.Spawn(WordPrefab, manager.MG_1_GO.transform);
	    int wordN = 0;
        string title = "";
        switch (TextManager.Instance.CurrentLanguage)
	    {
            case "english":
	            switch (currentDifficulty)
	            {
                    case "easy":
                        wordN = Utilities.RandomInt(0, wordsEasy.Count - 1);
                        title = GetEasy(wordN).english;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetEasy(wordN).type);
                        break;
                    case "medium":
                        wordN = Utilities.RandomInt(0, wordsMedium.Count - 1);
                        title = GetMedium(wordN).english;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetMedium(wordN).type);

                        break;
                    case "hard":
                        wordN = Utilities.RandomInt(0, wordsHard.Count - 1);
                        title = GetHard(wordN).english;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetHard(wordN).type);

                        break;
	            }
	            break;
            case "german":
                switch (currentDifficulty)
                {
                    case "easy":
                        wordN = Utilities.RandomInt(0, wordsEasy.Count - 1);
                        title = GetEasy(wordN).german;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetEasy(wordN).type);

                        break;
                    case "medium":
                        wordN = Utilities.RandomInt(0, wordsMedium.Count - 1);
                        title = GetMedium(wordN).german;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetMedium(wordN).type);

                        break;
                    case "hard":
                        wordN = Utilities.RandomInt(0, wordsHard.Count - 1);
                        title = GetHard(wordN).german;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetHard(wordN).type);

                        break;
                }
                break;
            case "french":
                switch (currentDifficulty)
                {
                    case "easy":
                        wordN = Utilities.RandomInt(0, wordsEasy.Count - 1);
                        title = GetEasy(wordN).french;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetEasy(wordN).type);

                        break;
                    case "medium":
                        wordN = Utilities.RandomInt(0, wordsMedium.Count - 1);
                        title = GetMedium(wordN).french;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetMedium(wordN).type);

                        break;
                    case "hard":
                        wordN = Utilities.RandomInt(0, wordsHard.Count - 1);
                        title = GetHard(wordN).german;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetHard(wordN).type);

                        break;
                }
                break;
            case "dutch":
                switch (currentDifficulty)
                {
                    case "easy":
                        wordN = Utilities.RandomInt(0, wordsEasy.Count - 1);
                        title = GetEasy(wordN).dutch;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetEasy(wordN).type);

                        break;
                    case "medium":
                        wordN = Utilities.RandomInt(0, wordsMedium.Count - 1);
                        title = GetMedium(wordN).dutch;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetMedium(wordN).type);

                        break;
                    case "hard":
                        wordN = Utilities.RandomInt(0, wordsHard.Count - 1);
                        title = GetHard(wordN).dutch;
                        word.GetComponent<Word>().SetVars(StartingPos, title, GetHard(wordN).type);

                        break;
                }
                break;
        }
		WordList.Add(word);
		word.transform.name = zLayer + " " + title;
		word.transform.SetAsFirstSibling();
	}

	public void InitGame()
	{
	    if (timesPlayed == 0 || timesPlayed == 1)
	    {
	        spawnTime = Vars.Instance.Mg1_SpawnTimes[0];
	        DocsNeeded = Vars.Instance.Mg1_DocsNeeded[0];
	        currentDifficulty = "easy";
	    }
	    if (timesPlayed == 2 || timesPlayed == 3)
	    {
            spawnTime = Vars.Instance.Mg1_SpawnTimes[1];
            DocsNeeded = Vars.Instance.Mg1_DocsNeeded[1];
            currentDifficulty = "medium";
        }
        if (timesPlayed > 3)
        {
            spawnTime = Vars.Instance.Mg1_SpawnTimes[2];
            DocsNeeded = Vars.Instance.Mg1_DocsNeeded[2];
            currentDifficulty = "hard";
        }
        InvokeRepeating("SpawnWord", 0f, spawnTime);
	}

    public void IncreaseDifficulty()
    {
        timesPlayed += 1;
    }

	public void ResetGame()
	{
		ObjectPool.RecycleAll(WordPrefab);
		WordList.Clear();
		CollectedDocs = 0;
		zLayer = 100f;
		CancelInvoke("SpawnWord");
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
	public void Load(TextAsset asset)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(asset.text);
		for (int i = 1; i < grid.Length; i++)
		{
			Row row = new Row();
			row.type = grid[i][0];
            row.difficulty = grid[i][1];
            row.english = grid[i][2];
            row.german = grid[i][3];
            row.french = grid[i][4];
            row.dutch = grid[i][5];
            rowList.Add(row);
		}
        foreach (Row row in rowList)
        {
            if (row.difficulty == "easy")
            {
                wordsEasy.Add(row);
            }
            if (row.difficulty == "medium")
            {
                wordsMedium.Add(row);
            }
            if (row.difficulty == "hard")
            {
                wordsHard.Add(row);
            }
        }
        isLoaded = true;
	}
	public Row GetRow(int find)
	{
		return rowList[find];
	}
    public Row GetEasy(int find)
    {
        return wordsEasy[find];
    }
    public Row GetMedium(int find)
    {
        return wordsMedium[find];
    }
    public Row GetHard(int find)
    {
        return wordsHard[find];
    }
    #endregion
}