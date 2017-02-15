using System;
using UnityEngine;
using System.Collections;
using System.Globalization;
using System.Threading;
using UnityEngine.Networking;
[NetworkSettings(channel = 1, sendInterval = 0.2f)]
public class NetworkCommunicator : NetworkBehaviour
{

    public int ConnectionId;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (CellManager.Instance != null && isLocalPlayer)
            CellManager.Instance.NetworkCommunicator = this;
    }

    void Start ()
    {
        if (isClient)
            //connectionToServer.logNetworkMessages = true;

        if (isServer)
            ConnectionId = connectionToClient.connectionId;
    }

    void Update () {
        if (CellManager.Instance != null && isLocalPlayer)
            CellManager.Instance.NetworkCommunicator = this;
    }

    public void HandleEvent(string action, int id)
    {
        if (isServer)
        {
            switch (action)
            {
                case "StartEvent":
                    EventManager.Instance.CreateEvent(id);
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdHandleEvent(action, id);
        }
    }

    public void SetPlayerState(string player, string state)
    {
        if (isServer)
        {
            switch (player)
            {
                case "Finance":
                    GameManager.Instance.FinanceState = state;
                    break;
                case "Social":
                    GameManager.Instance.SocialState = state;
                    break;
                case "Environment":
                    GameManager.Instance.EnvironmentState = state;
                    break;
            }
        }
        
        if (isClient && !isServer)
        {
            CmdSetPlayerState(player, state);
        }
    }

    [Command]
    void CmdSetPlayerState(string player, string state)
    {
        SetPlayerState(player, state);
    }

    public void CellOccupiedStatus(string action, int id)
    {
        if (isServer)
        {
            switch (action)
            {
                case "add":
                    SaveStateManager.Instance.AddOccupied(id);
                    break;
                case "remove":
                    SaveStateManager.Instance.RemoveOccupied(id);
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdAddOccupied(action, id);
        }
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

    public void ActivateProject(string action, int cellid, string owner, int id)
    {
        if (isServer)
        {
            switch (action)
            {
                case "CreateProject":
                    ProjectManager.Instance.SpawnProject(cellid, owner, id);
                    break;

            }
        }
        if (isClient && !isServer)
        {
            CmdActivateProject(action, cellid, owner, id);
        }
    }

    public void UpdateData(string roletype, string datatype, int amount)
    {
        if (isServer)
        {
            SaveStateManager.Instance.UpdateData(roletype, datatype, amount);
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
                    SaveStateManager.Instance.SetTaken(role, true, ConnectionId);
                    break;
                case "Social":
                    SaveStateManager.Instance.SetTaken(role, true, ConnectionId);
                    break;
                case "Finance":
                    SaveStateManager.Instance.SetTaken(role, true, ConnectionId);
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
                    ProjectManager.Instance.ProjectApproved(projectnum);
                    NotificationManager.Instance.AddNotification("Choice1", owner, projectnum);
                    RpcVote(vote, owner, projectnum);
                    break;
                case "Result_Choice2":
                    ProjectManager.Instance.ProjectRejected(projectnum);
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
    void CmdHandleEvent(string action, int id)
    {
        HandleEvent(action, id);
    }

    [Command]
    void CmdAddOccupied(string action, int id)
    {
        CellOccupiedStatus(action, id);
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
    void CmdActivateProject(string action, int cellid, string owner, int id)
    {
        ActivateProject(action, cellid, owner, id);
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
                    ProjectManager.Instance.ProjectApproved(projectnum);
                    NotificationManager.Instance.AddNotification("Choice1", owner, projectnum);
                    break;
                case "Result_Choice2":
                    ProjectManager.Instance.ProjectRejected(projectnum);
                    NotificationManager.Instance.AddNotification("Choice2", owner, projectnum);
                    break;
                default:
                    Debug.Log("something wrong in Vote switch");
                    break;
            }
        }
    }
}
