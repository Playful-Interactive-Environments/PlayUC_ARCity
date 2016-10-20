using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
public class VoteManager : NetworkBehaviour
{
	public class Vote
	{
		public int ProjectNumber;
		public int Choice1;
		public int Choice2;
		public int Votes;
		public bool VoteFinished;
	}

	public Dictionary<int, Vote> Votes = new Dictionary<int, Vote>();
	public static VoteManager Instance = null;
	public int testint;
	[SyncVar]
	public int Choice1_Votes;
	[SyncVar]
	public int Choice2_Votes;
	[SyncVar]
	public bool VoteStarted;
	void Start () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}
	
	public void StartNewVote(int projectnum)
	{
		if (isServer)
		{
			Vote v = new Vote();
			Votes.Add(projectnum, v);
			v.ProjectNumber = projectnum;
			testint = projectnum;
		}


		string title = QuestManager.Instance.GetTitle(projectnum);
		string content = QuestManager.Instance.GetProjectDescription(projectnum);
		UIManager.Instance.AddNotification("Vote", title, content, projectnum);
	}

	public void AddVote(int projectnum, int vote)
	{
		Vote v;
		if (Votes.TryGetValue(projectnum, out v))
		{
			v.Votes += vote;
			Debug.Log(v.Votes);
		}
	}

	void Update ()
	{
		if (Votes != null)
		{
			foreach (int key in Votes.Keys)
			{
				if (Votes[key].Votes == 3)
				{
				    if (Votes[key].Choice1 > Votes[key].Choice2)
				    {
                        Debug.Log("Yes" + Votes[key].Choice1);
                        Votes.Remove(key);
                    }
	
                    else if (Votes[key].Votes == 3)
                    {
                        Debug.Log("No" + Votes[key].Votes);
                        Votes.Remove(key);

                    }
                }
				

			}
		}

		if ((Choice1_Votes + Choice2_Votes) == 3 && VoteStarted && isServer)
		{
			if (Choice1_Votes > Choice2_Votes)
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice1", ProjectManager.Instance.CurrentID);
				Debug.Log("Result_Choice1");
				VoteStarted = false;
			}
			else if (Choice2_Votes > Choice1_Votes)
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice2", ProjectManager.Instance.CurrentID);
				Debug.Log("Result_Choice2");
				VoteStarted = false;
			}
			else
			{
				Debug.Log("There is a bug");
			}
		}
	}
	public void TriggerVote(int num)
	{
		ProjectManager.Instance.CurrentID = num;
		UIManager.Instance.ProjectDescription(num);
		if (!VoteStarted)
		{
			Choice1_Votes = 0;
			Choice2_Votes = 0;
			VoteStarted = true;
		}
	}
}
