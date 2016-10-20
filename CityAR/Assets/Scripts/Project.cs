using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Project : NetworkBehaviour
{
	[SyncVar]
	public int Radius;
	[SyncVar]
	public int Social;
	[SyncVar]
	public int Environment;
	[SyncVar]
	public int Finance;

	public HexCell cell;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaceProject(Vector3 pos)
	{
		cell= HexGrid.Instance.GetCell(pos);
	}
}
/*
		NetworkServer.Spawn(ProjectManager.Instance.ChosenProject);
			ProjectManager.Instance.ChosenProject.transform.position = pos;
			*/
