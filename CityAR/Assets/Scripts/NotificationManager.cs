using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class NotificationManager : NetworkBehaviour
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
	public static NotificationManager Instance = null;
	//NOTIFICATION CANVAS
	public GridLayoutGroup GridGroup;
	public Button ButtonTemplate;
	public GameObject CurrentNotification;
	//saves all notifications
	public List<Button> NotificationButtons = new List<Button>();


	void Start () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		ButtonTemplate = GameObject.Find("NotificationTemplate").GetComponent<Button>();

	}

	public void AddNotification(string type, string owner, int projectnum)
	{
		//create new notification button
		GridGroup = GameObject.Find("NotificationLayout").GetComponent<GridLayoutGroup>();
		Button button = Instantiate(ButtonTemplate, transform.position, Quaternion.identity) as Button;
		button.transform.parent = GridGroup.transform;
		button.transform.localScale = new Vector3(1, 1, 1);
		button.gameObject.SetActive(true);
		NotificationButtons.Add(button);
		//create notification
		string title = QuestManager.Instance.GetTitle(projectnum);
		Notification notification = button.GetComponent<Notification>();
		notification.NotificationType = type;
		notification.NotificationTitle = title;
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
