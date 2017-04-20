using System;
using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

[NetworkSettings(channel = 1, sendInterval = 0.1f)]
public class NetworkCommunicator : NetworkBehaviour
{
    public int ConnectionId;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
            LocalManager.Instance.NetworkCommunicator = this;
    }

    void Start ()
    {
        if (isClient)
            //connectionToServer.logNetworkMessages = true;

        if (isServer)
            ConnectionId = connectionToClient.connectionId;
    }

    void Update () {
        if (isLocalPlayer)
            LocalManager.Instance.NetworkCommunicator = this;

        if (isServer)
            ConnectionId = connectionToClient.connectionId;
    }

    public void SendEvent(string name)
    {
        if (isServer)
        {
            RpcTriggerEvent(name);
        }
    }

    public void UpdateProjectVars(int fin, int soc, int env)
    {
        if (isServer)
        {
            ProjectManager.Instance.SelectedProject.Finance += fin;
            ProjectManager.Instance.SelectedProject.Social += soc;
            ProjectManager.Instance.SelectedProject.Environment += env;
        }
        if (isClient && !isServer)
        {
            CmdUpdateProjectVars(fin, soc, env);
        }
    }

    public void SetPlayerState(string player, string state)
    {
        if (isServer)
        {
            GameManager.Instance.SetState(player, state);
            RpcSetPlayerState(player, state);
        }

        if (isClient && !isServer)
        {
            CmdSetPlayerState(player, state);
        }
    }

    public void ActivateProject(string action, int cellid, Vector3 pos, Vector3 rot, string owner, int id)
    {
        if (isServer)
        {
            switch (action)
            {
                case "CreateProject":
                    ProjectManager.Instance.SpawnProject(cellid, pos, rot, owner, id);
                    break;

            }
        }
        if (isClient && !isServer)
        {
            CmdActivateProject(action, cellid, pos, rot, owner, id);
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
                case Vars.Player1:
                    CellManager.Instance.UpdateFinance(cellid, amount);
                    break;
                case Vars.Player2:
                    CellManager.Instance.UpdateSocial(cellid, amount);
                    break;
                case Vars.Player3:
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
            SaveStateManager.Instance.SetTaken(role, true, ConnectionId);
        }
        if (isClient && !isServer)
        {
            CmdTakeRole(role);
        }
    }

    public void Vote(string vote, string voter, int projectnum)
    {
        if (isServer)
        {
            switch (vote)
            {
                case Vars.Choice1:
                    ProjectManager.Instance.SelectedProject.Choice1 += 1;
                    DiscussionManager.Instance.ChangeVoterState(voter, Vars.Approved);
                    SaveStateManager.Instance.UpdateData(voter, Vars.Approved, 0);
                    RpcVote(vote, voter, projectnum);
                    break;
                case Vars.Choice2:
                    ProjectManager.Instance.SelectedProject.Choice2 += 1;
                    DiscussionManager.Instance.ChangeVoterState(voter, Vars.Denied);
                    SaveStateManager.Instance.UpdateData(voter, Vars.Denied, 0);
                    RpcVote(vote, voter, projectnum);
                    break;
                case Vars.ResultChoice1:
                    ProjectManager.Instance.ProjectApproved(projectnum);
                    RpcVote(vote, voter, projectnum);
                    break;
                case Vars.ResultChoice2:
                    ProjectManager.Instance.ProjectRejected(projectnum);
                    RpcVote(vote, voter, projectnum);
                    break;
                case "Cancel":
                    ProjectManager.Instance.ProjectCanceled(projectnum);
                    break;
                default:
                    Debug.Log("something wrong in Vote switch");
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdVote(vote, voter, projectnum);
        }
    }

    [Command]
    void CmdUpdateProjectVars(int fin, int soc, int env)
    {
        UpdateProjectVars(fin, soc, env);
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
    void CmdActivateProject(string action, int cellid,Vector3 pos, Vector3 rotation, string owner, int id)
    {
        ActivateProject(action, cellid, pos, rotation, owner, id);
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
    void CmdSetPlayerState(string player, string state)
    {
        SetPlayerState(player, state);
    }

    [ClientRpc]
    void RpcSetPlayerState(string player, string state)
    {
        if (!isServer)
            GameManager.Instance.SetState(player, state);
    }

    [ClientRpc]
    public void RpcVote(string vote, string voter, int projectnum)
    {
        if (!isServer)
        {
            switch (vote)
            {
                case Vars.Choice1:
                    DiscussionManager.Instance.ChangeVoterState(voter, Vars.Approved);
                    break;
                case Vars.Choice2:
                    DiscussionManager.Instance.ChangeVoterState(voter, Vars.Denied);
                    break;
                case Vars.ResultChoice1:
                    ProjectManager.Instance.ProjectApproved(projectnum);
                    break;
                case Vars.ResultChoice2:
                    ProjectManager.Instance.ProjectRejected(projectnum);
                    break;
                default:
                    Debug.Log("something wrong in Vote switch");
                    break;
            }
        }
    }
    [ClientRpc]
    void RpcTriggerEvent(string name)
    {
        if (name == Vars.ServerHandleDisconnect)
            GameManager.Instance.ResetDiscussion();
    }
}
