using System;
using UnityEngine;
using System.Collections;
using System.Globalization;
using System.Threading;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = .1f)]
public class NetworkCommunicator : NetworkBehaviour
{

    public int ConnectionId;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (CellManager.Instance != null && isLocalPlayer)
            CellManager.Instance.NetworkCommunicator = this;
    }

    void Start () {
        if(isServer)
            ConnectionId = connectionToClient.connectionId;
        InvokeRepeating("Stats",.1f,1f);

    }

    void Stats()
    {
        Debug.Log(NetworkClient.GetTotalConnectionStats());
    }
    void Update () {
        if (CellManager.Instance != null && isLocalPlayer)
            CellManager.Instance.NetworkCommunicator = this;

    }

    public void CellOccupiedStatus(string action, Vector3 pos)
    {
        if (isServer)
        {
            switch (action)
            {
                case "add":
                    HexGrid.Instance.GetCell(pos).GetComponent<CellLogic>().AddOccupied();
                    break;
                case "remove":
                    HexGrid.Instance.GetCell(pos).GetComponent<CellLogic>().RemoveOccupied();

                    break;
            }

        }
        if (isClient && !isServer)
        {
            CmdAddOccupied(action, pos);
        }
    }

    [Command]
    void CmdAddOccupied(string action, Vector3 pos)
    {
        CellOccupiedStatus(action, pos);
    }
    public void CreatePlayerProject(int id, string title, string content, int environment, int social, int finance, int budget, int rating)
    {
        if (isServer)
        {
            ProjectManager.Instance.CSVProjects.AddNew(id, title, content, rating, social, environment, finance, budget);
            RpcCreatePlayerProject(id, title, content, environment, social, finance, budget, rating);

        }
        if (isClient && !isServer)
        {
            CmdCreatePlayerProject(id, title, content, environment, social, finance, budget, rating);
        }
    }

    public void ActivateProject(string action, Vector3 pos, string owner, int id)
    {
        if (isServer)
        {
            switch (action)
            {
                case "CreateProject":
                    ProjectManager.Instance.InstantiateProject(owner, id);
                    break;
                case "PlaceProject":
                    ProjectManager.Instance.PlaceProject(pos, owner, id);
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdActivateProject(action, pos, owner, id);
        }
    }

    public void UpdateData(string roletype, string datatype, int amount)
    {
        if (isServer)
        {
            GlobalManager.Instance.UpdateData(roletype, datatype, amount);
        }
        if (isClient && !isServer)
        {
            CmdSavePlayerData(roletype, datatype, amount);
        }
    }

    public void UpdateCellValue(string valuetype, int cellid, int amount)
    {
        if (isServer)
        {
            switch (valuetype)
            {
                case "Finance":
                    CellManager.Instance.UpdateFinance(cellid, amount);
                    break;
                case "Social":
                    CellManager.Instance.UpdateSocial(cellid, amount);
                    break;
                case "Environment":
                    CellManager.Instance.UpdateEnvironment(cellid, amount); 
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdUpdateCellValue(valuetype, cellid, amount);
        }
    }

    public void TakeRole(string role)
    {
        if (isServer)
        {
            switch (role)
            {
                case "Environment":
                    GlobalManager.Instance.SetTaken(role, true, ConnectionId);
                    break;
                case "Social":
                    GlobalManager.Instance.SetTaken(role, true, ConnectionId);
                    break;
                case "Finance":
                    GlobalManager.Instance.SetTaken(role, true, ConnectionId);
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdTakeRole(role);
        }
    }

    public void Vote(string vote, string owner, int projectnum)
    {
        if (isServer)
        {
            switch (vote)
            {
                case "Choice1":
                    ProjectManager.Instance.FindProject(projectnum).Choice1 += 1;
                    break;
                case "Choice2":
                    ProjectManager.Instance.FindProject(projectnum).Choice2 += 1;
                    break;
                case "Result_Choice1":
                    if (LocalManager.Instance.RoleType == owner)
                    {
                        ProjectManager.Instance.ProjectApproved(projectnum);
                    }

                    NotificationManager.Instance.AddNotification("Choice1", owner, projectnum);
                    RpcVote(vote, owner, projectnum);
                    break;
                case "Result_Choice2":
                    if (LocalManager.Instance.RoleType == owner)
                    {
                        ProjectManager.Instance.ProjectRejected(projectnum);
                    }
                    NotificationManager.Instance.AddNotification("Choice2", owner, projectnum);
                    RpcVote(vote, owner, projectnum);
                    break;
                default:
                    Debug.Log("something wrong in Vote switch");
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdVote(vote, owner, projectnum);
        }
    }

    [Command]
    void CmdUpdateCellValue(string valuetype, int cellid, int amount)
    {
        UpdateCellValue(valuetype, cellid, amount);
    }

    [Command]
    void CmdSavePlayerData(string roletype, string datatype, int budget)
    {
        UpdateData(roletype, datatype, budget);
    }

    [Command]
    void CmdActivateProject(string action, Vector3 pos,string owner, int id)
    {
        ActivateProject(action, pos, owner, id);
    }

    [Command]
    public void CmdTakeRole(string role)
    {
        TakeRole(role);
    }

    [Command]
    public void CmdVote(string vote, string owner, int num)
    {
        Vote(vote, owner, num);
    }

    [Command]
    void CmdCreatePlayerProject(int id, string title, string content, int environment, int social, int finance, int budget, int rating)
    {
        CreatePlayerProject(id, title, content, environment, social, finance, budget, rating);
    }

    [ClientRpc]
    void RpcCreatePlayerProject(int id, string title, string content, int environment, int social, int finance, int budget, int rating)
    {
        ProjectManager.Instance.CSVProjects.AddNew(id, title, content, rating, social, environment, finance, budget);
    }

    [ClientRpc]
    public void RpcVote(string vote, string owner, int projectnum)
    {
        if (!isServer)
        {
            switch (vote)
            {
                case "Result_Choice1":
                    if (LocalManager.Instance.RoleType == owner)
                    {
                        ProjectManager.Instance.ProjectApproved(projectnum);
                    }
                    NotificationManager.Instance.AddNotification("Choice1", owner, projectnum);
                    break;
                case "Result_Choice2":
                    if (LocalManager.Instance.RoleType == owner)
                    {
                        ProjectManager.Instance.ProjectRejected(projectnum);
                    }
                    NotificationManager.Instance.AddNotification("Choice2", owner, projectnum);
                    break;
                default:
                    Debug.Log("something wrong in Vote switch");
                    break;
            }
        }
    }
}
