using UnityEngine;
using System.Collections;
using System.Globalization;
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
        if (isServer && ProjectManager.Instance.ChosenProject != null)
        {
            GameObject project = ProjectManager.Instance.ChosenProject;
            NetworkServer.Spawn(project);
            project.transform.position = pos;
            project.GetComponent<Project>().PlaceProject(pos);
            project.SetActive(true);
            ProjectManager.Instance.ChosenProject = null;
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
                    RoleManager.Instance.RoleType = "Environment";
                    RoleManager.Instance.EnvironmentPlayer = this;
                    break;
                case "Social":
                    RoleManager.Instance.Social = true;
                    RoleManager.Instance.RoleType = "Social";
                    RoleManager.Instance.SocialPlayer = this;
                    break;
                case "Finance":
                    RoleManager.Instance.Finance = true;
                    RoleManager.Instance.RoleType = "Finance";
                    RoleManager.Instance.FinancePlayer = this;
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
                case "StartVote":
                    VoteManager.Instance.StartNewVote(projectnum, owner);
                    VoteManager.Instance.AddNotification("Vote", owner, projectnum);
                    RpcVote(vote, owner, projectnum);
                    if (RoleManager.Instance.RoleType == owner)
                    {
                        Debug.Log("Server " + RoleManager.Instance.RoleType + owner);
                        UIManager.Instance.GameUI();
                        UIManager.Instance.ProjectDescription(projectnum);
                        UIManager.Instance.EnableVoteUI();
                    }
                    break;
                case "Choice1":
                    VoteManager.Instance.AddVote(projectnum, 0);
                    break;
                case "Choice2":
                    VoteManager.Instance.AddVote(projectnum, 1);
                    break;
                case "Result_Choice1":
                    if (RoleManager.Instance.RoleType == owner)
                        ProjectManager.Instance.ProjectApproved(projectnum);
                    VoteManager.Instance.RemoveNotification(projectnum);
                    VoteManager.Instance.AddNotification("Choice1", owner, projectnum);
                    Debug.Log("great success");
                    RpcVote(vote, owner, projectnum);

                    break;
                case "Result_Choice2":
                    if (RoleManager.Instance.RoleType == owner)
                        ProjectManager.Instance.ProjectRejected(projectnum);
                    VoteManager.Instance.RemoveNotification(projectnum);
                    VoteManager.Instance.AddNotification("Choice2", owner, projectnum);
                    Debug.Log("fail");
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
    public void CmdSpawnObject(Vector3 pos)
    {
        SpawnObject(pos);
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
    [ClientRpc]
    public void RpcVote(string vote, string owner, int projectnum)
    {
        if (!isServer)
        {
            switch (vote)
            {
                case "StartVote":
                    VoteManager.Instance.AddNotification("Vote", owner, projectnum);
                    if (RoleManager.Instance.RoleType == owner)
                    {
                        Debug.Log("Client " + RoleManager.Instance.RoleType + owner);
                        UIManager.Instance.DebugText.text = RoleManager.Instance.RoleType + owner + "";
                        UIManager.Instance.GameUI();
                        UIManager.Instance.ProjectDescription(projectnum);
                        UIManager.Instance.EnableVoteUI();
                    }

                    break;
                case "Result_Choice1":
                    if (RoleManager.Instance.RoleType == owner)
                        ProjectManager.Instance.ProjectApproved(projectnum);

                    VoteManager.Instance.RemoveNotification(projectnum);
                    VoteManager.Instance.AddNotification("Choice1", owner, projectnum);
                    UIManager.Instance.DebugText.text = projectnum + " success";
                    break;
                case "Result_Choice2":
                    if (RoleManager.Instance.RoleType == owner)
                        ProjectManager.Instance.ProjectRejected(projectnum);

                    VoteManager.Instance.RemoveNotification(projectnum);
                    VoteManager.Instance.AddNotification("Choice2", owner, projectnum);
                    UIManager.Instance.DebugText.text = projectnum + "fail";
                    break;
                default:
                    Debug.Log("something wrong in Vote switch");
                    break;
            }
        }
    }
}
