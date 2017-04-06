using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Reflection;
using UnityEngine.Networking;

public class CellGrid : AManager<CellGrid>
{
	public static GameObject[][] Grid;
	public List<GameObject> GridCells = new List<GameObject>(); 
	public GameObject PrefabCube;
	public int Columns = 4;
	public int Rows = 3;
	public int Count;
	private Vector3 StartPosition;
	public float Height = 0f;

	void Start()
	{
		float _cubeX = ValueManager.Instance.MapWidth/Columns;
		float _cubeZ = ValueManager.Instance.MapHeight/Rows;
		//PrefabCube.transform.localScale = new Vector3(_cubeX, 1, _cubeZ);
		// init start position for the cube grid, based on size of the map and size of the cube.
		StartPosition = new Vector3(-ValueManager.Instance.MapWidth / 2, 0, -ValueManager.Instance.MapHeight / 2);
		Grid = new GameObject[Columns][];
		for (int i = 0; i < Columns; i++)
		{
			Grid[i] = new GameObject[Rows];

			for (int j = 0; j < Rows; j++)
			{
				Grid[i][j] = Instantiate(PrefabCube, new Vector3((i * _cubeX ) + _cubeX / 2, Height, (j * _cubeZ) + _cubeZ / 2), Quaternion.identity);
				Grid[i][j].transform.parent = this.transform;
				Grid[i][j].GetComponent<CellLogic>().CellId = Count;
				Grid[i][j].transform.name = "" + Count;
				GridCells.Add(Grid[i][j]);
				Count ++;
			}
		}
		this.transform.position = StartPosition;
	}
	
	void Update () {
		
	}

	public GameObject GetCell(int cellid)
	{
		return GridCells[cellid];
	}

	public GameObject GetRandomCell()
	{
		return GridCells[Random.Range(0, Count)];
	}

    public void DefaultState()
    {
        foreach (GameObject cell in GridCells)
        {
            cell.GetComponent<CellLogic>().Default();
        }
    }
}
