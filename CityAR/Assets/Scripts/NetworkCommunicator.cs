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
                    RoleManager.Instance.EnvironmentPlayer = this;
                    break;
                case "Social":
                    RoleManager.Instance.Social = true;
                    RoleManager.Instance.SocialPlayer = this;
                    break;
                case "Finance":
                    RoleManager.Instance.Finance = true;
                    RoleManager.Instance.FinancePlayer = this;
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdTakeRole(role);
        }
    }

    public void Vote(string vote, int projectnum)
    {

        if (isServer)
        {
            switch (vote)
            {
                case "StartVote":
                    //VoteManager.Instance.TriggerVote(projectnum);
                    VoteManager.Instance.StartNewVote(projectnum);
                    UIManager.Instance.EnableVoteUI();
                    RpcVote(vote, projectnum);
                    break;
                case "Choice1":
                    VoteManager.Instance.AddVote(projectnum, 1);
                    break;
                case "Choice2":
                    VoteManager.Instance.AddVote(projectnum, 0);
                    break;
                case "Result_Choice1":
                    ProjectManager.Instance.ProjectApproved(projectnum);
                    RpcVote(vote, projectnum);
                    break;
                case "Result_Choice2":
                    ProjectManager.Instance.ProjectRejected(projectnum);
                    RpcVote(vote, projectnum);
                    break;
                default:
                    Debug.Log("something wrong in Vote switch");
                    break;
            }
        }
        if (isClient && !isServer)
        {
            CmdVote(vote, projectnum);
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
    public void CmdVote(string vote, int num)
    {
        Vote(vote, num);
    }
    [ClientRpc]
    public void RpcVote(string vote, int num)
    {
        switch (vote)
        {
            case "StartVote":
                VoteManager.Instance.StartNewVote(num);
                break;
            case "Result_Choice1":
                ProjectManager.Instance.ProjectApproved(num);
                RpcVote(vote, num);
                break;
            case "Result_Choice2":
                ProjectManager.Instance.ProjectRejected(num);
                RpcVote(vote, num);
                break;
            default:
                Debug.Log("something wrong in Vote switch");
                break;
        }
    }
}
