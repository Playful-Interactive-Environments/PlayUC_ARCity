using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class VoteManager : NetworkBehaviour
{
	public class Vote
	{
		public int ProjectNumber;
		public int Choice1;
		public int Choice2;
		public int Votes;
		public string ProjectOwner;
		public bool VoteFinished;
	}
	[SerializeField]
	public Dictionary<int, Vote> Votes = new Dictionary<int, Vote>();
	public static VoteManager Instance = null;
	//NOTIFICATION CANVAS
	public GridLayoutGroup GridGroup;
	public Button ButtonTemplate;
	public List<Button> NotificationButtons = new List<Button>();
	public GameObject CurrentNotification;

	void Start () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		ButtonTemplate = GameObject.Find("NotificationTemplate").GetComponent<Button>();

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
				Debug.Log(v.ProjectNumber + " " + v.Votes);
				v.Choice1 += 1;
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
		if (Votes != null && Votes.Keys.Count > 0 && isServer)
		{
			foreach (int key in Votes.Keys)
			{
				if (Votes[key].Votes >= 3)
				{
					if (Votes[key].Choice1 > Votes[key].Choice2 && !Votes[key].VoteFinished)
					{
						Votes[key].VoteFinished = true;
						Votes[key].Votes = 0;
						Votes[key].Choice2 = 0;
						Votes[key].Choice1 = 0;
						CellManager.Instance.NetworkCommunicator.Vote("Result_Choice1", Votes[key].ProjectOwner, Votes[key].ProjectNumber);
					}

					else if (Votes[key].Choice1 < Votes[key].Choice2)
					{
						Votes[key].VoteFinished = true;
						Votes[key].Votes = 0;
						Votes[key].Choice2 = 0;
						Votes[key].Choice1 = 0;
						CellManager.Instance.NetworkCommunicator.Vote("Result_Choice2", Votes[key].ProjectOwner, Votes[key].ProjectNumber);
					}
				}
			}
		}
	}

	public void AddNotification(string type, string owner, int projectnum)
	{
		//create new notification button


		GridGroup = GameObject.Find("GridLayout").GetComponent<GridLayoutGroup>();
		Button button = Instantiate(ButtonTemplate, transform.position, Quaternion.identity) as Button;
		button.transform.parent = GridGroup.transform;
		button.transform.localScale = new Vector3(1, 1, 1);
		button.gameObject.SetActive(true);
		NotificationButtons.Add(button);
		//create notification
		string title = QuestManager.Instance.GetTitle(projectnum);
		string content = QuestManager.Instance.GetProjectDescription(projectnum);
		Notification notification = button.GetComponent<Notification>();
		notification.NotificationType = type;
		notification.NotificationTitle = title;
		notification.NotificationContent = content;
		notification.NotificationID = projectnum;
		notification.NotificationOwner = owner;
		notification.UpdateButtonTitle();
		UIManager.Instance.SetNotificationState(true);
	}

	public Notification GetNotification(int projectnum)
	{
		Notification notification = new Notification();
		foreach (Button b in NotificationButtons)
		{
			Notification n = b.GetComponent<Notification>();
			if (n.NotificationID == projectnum)
			{
				notification = n;
			}
		}
		return notification;
	}

	public void RemoveNotification(int projectnum)
	{
		Notification n = GetNotification(projectnum);
		NotificationButtons.Remove(n.GetComponent<Button>());
		Destroy(n.gameObject);
	}
}
