using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Vuforia;

public class MG_1 : AManager<MG_1>
{
	public float TimeLimit;
	public int CollectedDocs;
	public int DocsNeeded = 10;
	public GameObject WordPrefab;
	public GameObject DropPrefab;
	public GameObject Background;
	public List<GameObject> WordList = new List<GameObject>();
	public GameObject TargetFinance;
	public GameObject TargetSocial;
	public GameObject TargetEnvironment;
	private MGManager manager;
	private float Height;
	private float Width;
	private float threshold = 2f;
	private float spawnTime;
	public Vector3 StartingPos;


	//CSV Rows
	public class Row
	{
		public string title;
		public string type;
	}

	public TextAsset WordFile;
	public List<Row> rowList = new List<Row>();
	private bool isLoaded = false; 

	void Start ()
	{	
		ObjectPool.CreatePool(WordPrefab, 5);
		ObjectPool.CreatePool(DropPrefab, 3);
		manager = MGManager.Instance;
		Load(WordFile);
		Invoke("SetSize", 0.1f);
	}

	void SetSize()
	{
		Height = manager.Height;
		Width = manager.Width;
		//float scaleX =  1 * 9f / 16f * Width / Height;
		float scaleX = 1;
		Vector3 finDropPos = new Vector3(-Width / 3, Height / 4, 0);

		TargetFinance = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, finDropPos, Quaternion.identity);
		TargetFinance.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Finance;
		TargetFinance.transform.name = "finance";
		TargetFinance.transform.GetChild(0).GetComponent<TextMesh>().text = "Finance";
		TargetFinance.transform.localScale = new Vector3(scaleX, 1, 0);

		Vector3 socDropPos = new Vector3(0, Height / 4, 0);
		TargetSocial = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, socDropPos, Quaternion.identity);
		TargetSocial.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Social;
		TargetSocial.transform.name = "social";
		TargetSocial.transform.GetChild(0).GetComponent<TextMesh>().text = "Social";
		TargetSocial.transform.localScale = new Vector3(scaleX, 1, 0);

		Vector3 envDropBos = new Vector3(Width / 3, Height / 4, 0);
		TargetEnvironment = ObjectPool.Spawn(DropPrefab, manager.MG_1_GO.transform, envDropBos, Quaternion.identity);
		TargetEnvironment.GetComponent<DropArea>().CurrentType = DropArea.DragZoneType.Environment;
		TargetEnvironment.transform.name = "environment";
		TargetEnvironment.transform.GetChild(0).GetComponent<TextMesh>().text = "Environment";
		TargetEnvironment.transform.localScale = new Vector3(scaleX, 1, 0);

		Background.transform.localScale = new Vector3(Width, Height, 0);
		Background.transform.localPosition = new Vector3(0,0,1);
		StartingPos = new Vector3(0, -Height / 4, 0);
	}

	void Update()
	{

	}

	public void SpawnWord()
	{
		GameObject word = ObjectPool.Spawn(WordPrefab, manager.MG_1_GO.transform);
		int random = Utilities.RandomInt(0, rowList.Count - 1);
		word.GetComponent<Word>().SetVars(StartingPos, GetRow(random).title, GetRow(random).type);
		WordList.Add(word);
	}

	public void InitGame()
	{
		InvokeRepeating("SpawnWord", 0f, 2.5f);
	}

	public void ResetGame()
	{
		ObjectPool.RecycleAll(WordPrefab);
		WordList.Clear();
		CollectedDocs = 0;
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
			row.title = grid[i][1];
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