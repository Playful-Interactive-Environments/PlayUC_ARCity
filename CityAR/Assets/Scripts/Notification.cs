using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
	public string NotificationType;
	public string NotificationTitle;
	public string NotificationContent;
	public int NotificationID;
	public string NotificationOwner;
	void Start ()
	{
		
	}
	
	void Update ()
	{
	}

	public void AccessNotification()
	{
		VoteManager.Instance.CurrentNotification = this.gameObject;

		switch (NotificationType)
		{
			case "Vote":
				ProjectManager.Instance.CurrentID = NotificationID;
				UIManager.Instance.GameUI();
				UIManager.Instance.ProjectDescription(NotificationID);
				UIManager.Instance.Invoke("EnableVoteUI", .1f);
				break;
			case "Choice1":
				ProjectManager.Instance.CurrentID = NotificationID;
				UIManager.Instance.GameUI();
				UIManager.Instance.EventText.text = "Project " + NotificationTitle + " passed!";
				UIManager.Instance.DisplayEventCanvas();
				VoteManager.Instance.RemoveNotification(NotificationID);
				break;
			case "Choice2":
				ProjectManager.Instance.CurrentID = NotificationID;
				UIManager.Instance.GameUI();
				UIManager.Instance.EventText.text = "Project " + NotificationTitle + " failed!";
				UIManager.Instance.DisplayEventCanvas();
				VoteManager.Instance.RemoveNotification(NotificationID);
				break;
			case "Waiting":
				UIManager.Instance.GameUI();
				UIManager.Instance.EventText.text = "Waiting for votes on: " + NotificationTitle;
				UIManager.Instance.DisplayEventCanvas();
				break;
			default:
				break;
		}
	}

	public void UpdateButtonTitle()
	{

		switch (NotificationType)
		{
			case "Vote":
				GetComponentInChildren<Text>().text = NotificationType + " : " + NotificationOwner + " Proposes: " + NotificationTitle;
				break;
			case "Choice1":
				GetComponentInChildren<Text>().text = "Event : " +  NotificationTitle + " is Approved.";
				break;
			case "Choice2":
				GetComponentInChildren<Text>().text = "Event : " + NotificationTitle + " is Denied.";
				break;
			case "Waiting":
				GetComponentInChildren<Text>().text = "Initiated " + NotificationTitle;
				break;
			default:
				break;
		}
	}
}
