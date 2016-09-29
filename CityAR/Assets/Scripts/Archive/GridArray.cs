using UnityEngine;
using System.Collections;
using System.Deployment.Internal;
using System.Reflection;
using UnityEngine.Networking;

public class GridArray : AManager<GridArray>
{
	public static GameObject[][] Grid;
	public GameObject PrefabCube;
	public int Rows = 15;
	public int Pieces = 10;
	public int Count = 0;
	private Vector3 StartPosition;
	private float _cubeSize;
	public float Height = 5f;
	private bool GridCreated;

	void Start()
	{
	
		_cubeSize = PrefabCube.transform.localScale.x;
		// init start position for the cube grid, based on size of the map and size of the cube.
		StartPosition = new Vector3(-ValueManager.Instance.MapWidth / 2, 0, -ValueManager.Instance.MapHeight / 2);
		Grid = new GameObject[Rows][];
		for (int i = 0; i < Rows; i++)
		{
			Grid[i] = new GameObject[Pieces];

			for (int j = 0; j < Pieces; j++)
			{
				Grid[i][j] = Instantiate(PrefabCube, new Vector3((i * _cubeSize ) + _cubeSize / 2, Height, (j * _cubeSize) + _cubeSize / 2), Quaternion.identity) as GameObject;
				Grid[i][j].transform.parent = this.transform;
				Grid[i][j].GetComponent<GridPieceLogic>().Count = Count;
				Grid[i][j].transform.name = Count + ": " + "Row:" + j + " Piece:" + i;
				Count ++;
			}
		}
		this.transform.position = StartPosition;
	}
	
	void Update () {
		
	}

	public void UpdateGridVars(SyncListInt unemployment, SyncListInt pollution, SyncListBool occupied)
	{
		int count = 0;
		for (int i = 0; i < Rows; i++)
		{
			for (int j = 0; j < Pieces; j++)
			{
				
				Grid[i][j].GetComponent<GridPieceLogic>().JobsRate = unemployment[count];
				Grid[i][j].GetComponent<GridPieceLogic>().PollutionRate = pollution[count];
				Grid[i][j].GetComponent<GridPieceLogic>().Occupied = occupied[count];
				count++;
			}
		}
	}
}
