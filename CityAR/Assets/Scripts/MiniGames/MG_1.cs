using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Vuforia;

public class MG_1 : AManager<MG_1>
{
	public float TimeLimit;
	public int CollectedDocs;
	public int DocsNeeded = 5;
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
	private float spawnTime;
	public Vector3 StartingPos;
	public float zLayer = 100f;

	//CSV Rows
	public class Row
	{
		public string type;
        public string english;
        public string german;
        public string french;
        public string dutch;
    }

    public TextAsset WordFile;
	public List<Row> rowList = new List<Row>();
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

		Background.transform.localScale = new Vector3(Width, Height, 0);
		Background.transform.localPosition = new Vector3(0, 0, zLayer + 25);
		StartingPos = new Vector3(0, -Height / 4, 0);
	}

	void Update()
	{

	}

	public void SpawnWord()
	{
		GameObject word = ObjectPool.Spawn(WordPrefab, manager.MG_1_GO.transform);

		int random = Utilities.RandomInt(0, rowList.Count - 1);
	    string title = "";
        switch (TextManager.Instance.CurrentLanguage)
	    {
            case "english":
                title = GetRow(random).english;
	            break;
            case "german":
                title = GetRow(random).german;
                break;
            case "french":
                title = GetRow(random).french;
                break;
            case "dutch":
                title = GetRow(random).dutch;
                break;
        }
        word.GetComponent<Word>().SetVars(StartingPos, title, GetRow(random).type);
		WordList.Add(word);
		word.transform.name = zLayer + " " + title;
		word.transform.SetAsFirstSibling();
	}

	public void InitGame()
	{
		InvokeRepeating("SpawnWord", 0f, 3.5f);
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
			row.english = grid[i][1];
            row.german = grid[i][2];
            row.french = grid[i][3];
            row.dutch = grid[i][4];
            rowList.Add(row);
		}
		isLoaded = true;
	}
	public Row GetRow(int find)
	{
		return rowList[find];
	}
	#endregion
}