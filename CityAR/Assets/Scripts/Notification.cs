using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
	public string NotificationType;
	public string NotificationTitle;
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
		NotificationManager.Instance.CurrentNotification = this.gameObject;

		switch (NotificationType)
		{
			case "Vote":
				ProjectManager.Instance.SelectedProjectId = NotificationID;
				UIManager.Instance.NotificationUI();
				UIManager.Instance.EnableVoteUI();
				break;
			case "Choice1":
				ProjectManager.Instance.SelectedProjectId = NotificationID;
				UIManager.Instance.EventText.text = "Project " + NotificationTitle + " passed!";
				UIManager.Instance.DisplayEventCanvas();
				NotificationManager.Instance.RemoveNotification(NotificationID);
				break;
			case "Choice2":
				ProjectManager.Instance.SelectedProjectId = NotificationID;
				UIManager.Instance.EventText.text = "Project " + NotificationTitle + " failed!";
				UIManager.Instance.DisplayEventCanvas();
				NotificationManager.Instance.RemoveNotification(NotificationID);
				break;
			case "Waiting":
				ProjectManager.Instance.SelectedProjectId = NotificationID;
				UIManager.Instance.EventText.text = "Waiting for votes on: " + NotificationTitle;
				UIManager.Instance.DisplayEventCanvas();
				//VoteManager.Instance.RemoveNotification(NotificationID);
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
				GetComponentInChildren<Text>().text = NotificationType + ":\n" + NotificationOwner + " Proposes: " + NotificationTitle;
				break;
			case "Choice1":
				GetComponentInChildren<Text>().text = "Event:\n" +  NotificationTitle + " is Approved.";
				break;
			case "Choice2":
				GetComponentInChildren<Text>().text = "Event:\n" + NotificationTitle + " is Denied.";
				break;
			case "Waiting":
				GetComponentInChildren<Text>().text = "Initiated \n" + NotificationTitle;
				break;
			default:
				break;
		}
	}
}
