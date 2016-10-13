using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkCommunicator : NetworkBehaviour
{

	public override void OnStartLocalPlayer()
	{
		base.OnStartLocalPlayer();
		if (CellManager.Instance != null && isLocalPlayer)
			CellManager.Instance.NetworkCommunicator = this;
	}
	void Start () {
	
	}
	
	void Update () {
		if (CellManager.Instance != null && isLocalPlayer)
			CellManager.Instance.NetworkCommunicator = this;
	}
	public void SpawnObject(Vector3 pos)
	{
		if (isServer)
		{
			GameObject gobj = Instantiate(CellManager.Instance.SpawnPrefab, pos, Quaternion.identity) as GameObject;
			NetworkServer.Spawn(gobj);
		}
		if (isClient && !isServer)
		{
			CmdSpawnObject(pos);
		}
	}

	public void TakeRole(string role)
	{
		if (isServer)
		{
			switch (role)
			{
				case "Environment":
					RoleManager.Instance.Environment = true;
					break;
				case "Social":
					RoleManager.Instance.Social = true;
					break;
				case "Finance":
					RoleManager.Instance.Finance = true;
					break;
			}
		}
		if (isClient && !isServer)
		{
			CmdTakeRole(role);
		}
	}
	[Command]
	public void CmdSpawnObject(Vector3 pos)
	{
		GameObject gobj = Instantiate(CellManager.Instance.SpawnPrefab, pos, Quaternion.identity) as GameObject;
		NetworkServer.Spawn(gobj);
	}

	[Command]
	public void CmdTakeRole(string role)
	{
		switch (role)
		{
			case "Environment":
				RoleManager.Instance.Environment = true;
				break;
			case "Social":
				RoleManager.Instance.Social = true;
				break;
			case "Finance":
				RoleManager.Instance.Finance = true;
				break;
		}
	}
}
