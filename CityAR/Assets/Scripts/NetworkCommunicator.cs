using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkCommunicator : NetworkBehaviour
{

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //transform.name = "" + connectionToClient;
        if (CellManager.Instance != null && isLocalPlayer)
            CellManager.Instance.NetworkCommunicator = this;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
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
    [Command]
    public void CmdSpawnObject(Vector3 pos)
    {
        GameObject gobj = Instantiate(CellManager.Instance.SpawnPrefab, pos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(gobj);

    }
}
