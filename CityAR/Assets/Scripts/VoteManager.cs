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
        public string ProjectOwner;
    }
    [SerializeField]
	public Dictionary<int, Vote> Votes = new Dictionary<int, Vote>();
	public static VoteManager Instance = null;

	void Start () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
	}
	
	public void StartNewVote(int projectnum, string owner)
	{
		if (isServer)
        {
            if (!Votes.ContainsKey(projectnum))
            {
                Vote v = new Vote();
                Votes.Add(projectnum, v);
                v.ProjectNumber = projectnum;
                v.ProjectOwner = owner;
                Debug.Log(projectnum + " " + owner);
            }
            else
            {
                Debug.Log("Project already in Votes.");
            }

        }
    }

	public void AddVote(int projectnum, int choice)
	{
		Vote v;
	    if (Votes.TryGetValue(projectnum, out v))
	    {
            //add one vote
	        v.Votes += 1;
            //voted for choice1
	        if (choice == 0)
	        {
	            v.Choice1 += 1;
                Debug.Log(v.Choice1);

            }
            //voted for choice2
            else if (choice == 1)
	        {
	            v.Choice2 += 1;
	        }
	    }
	    else
	    {
	        Debug.Log("Project not found " + projectnum);
	    }
	}

	void Update ()
	{
		if (Votes != null && Votes.Keys.Count > 0)
		{
			foreach (int key in Votes.Keys)
			{
				if (Votes[key].Votes == 3)
				{
				    if (Votes[key].Choice1 > Votes[key].Choice2)
				    {
                        Debug.Log("Yes" + Votes[key].Choice1);
                        UIManager.Instance.FindNotification("Choice1", Votes[key].ProjectNumber);
                    }
	
                    else if (Votes[key].Choice1 < Votes[key].Choice2)
                    {
                        Debug.Log("No" + Votes[key].Votes);
                        UIManager.Instance.FindNotification("Choice2", Votes[key].ProjectNumber);

                    }
                    Votes[key].Choice1 = 0;
                    Votes[key].Choice2 = 0;
                    Votes[key].Votes = 0;
                }
			}
		}
	}
}
