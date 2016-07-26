using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MessageCommunicator : NetworkBehaviour
{

    public override void OnStartLocalPlayer() 
    {
        base.OnStartLocalPlayer();
        //transform.name = "" + connectionToClient;
        if (ObjectManager.Instance != null && isLocalPlayer)
            ObjectManager.Instance.LocalPlayerObject = gameObject;
    }

    void Update()
    {
        if (ObjectManager.Instance != null && isLocalPlayer)
            ObjectManager.Instance.LocalPlayerObject = gameObject;
    }

    public void ObjectTaken(GameObject constrobj)
    {
        if (isServer)
            constrobj.GetComponent<ConstructionObject>().ObjectTaken = true;
        if (isClient && !isServer)
            CmdSendObjectTaken(constrobj);
    }

    public void ObjectPlaced(GameObject constrobj, GameObject gridpiece)
    {
        if (isServer)
        {
            ObjectManager.Instance.UpdateOccupiedVar(gridpiece.GetComponent<GridPieceLogic>().Count, true);
            constrobj.GetComponent<ConstructionObject>().PlaceObject(gridpiece);
        }

        if (isClient && !isServer)
        {
            constrobj.GetComponent<ConstructionObject>().PlaceObject(gridpiece);
            CmdSendObjectPlaced(constrobj, gridpiece.transform.name);
        }

    }
    public void SpawnObject()
    {
        if (isServer)
        {
            GameObject gobj = Instantiate(ObjectManager.Instance.House, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            NetworkServer.SpawnWithClientAuthority(gobj, connectionToClient);
        }
        if (isClient && !isServer)
        {
            CmdSpawnObject();
        }
    }

    #region Commands to Server
    [Command]
    public void CmdSendObjectPlaced(GameObject constrobj, string gridpiece)
    {
        GameObject gridobj = GameObject.Find(gridpiece);
        ObjectManager.Instance.UpdateOccupiedVar(gridobj.GetComponent<GridPieceLogic>().Count, true);
        constrobj.GetComponent<ConstructionObject>().PlaceObject(gridobj);
    }

    [Command]
    public void CmdSendObjectTaken(GameObject constrobj)
    {
        constrobj.GetComponent<ConstructionObject>().ObjectTaken = true;
    }

    [Command]
    public void CmdSpawnObject()
    {
        GameObject gobj = Instantiate(ObjectManager.Instance.House, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        NetworkServer.SpawnWithClientAuthority(gobj, connectionToClient);

    }
    #endregion


}
