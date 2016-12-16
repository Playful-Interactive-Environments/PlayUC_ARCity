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
			case "Choice1":
				ProjectManager.Instance.SelectedProjectId = NotificationID;
				UIManager.Instance.EventText.text = "Project " + NotificationTitle + " passed!";
				UIManager.Instance.DisplayNotificationResult();
				NotificationManager.Instance.RemoveNotification(NotificationID);
				break;
			case "Choice2":
				ProjectManager.Instance.SelectedProjectId = NotificationID;
				UIManager.Instance.EventText.text = "Project " + NotificationTitle + " failed!";
				UIManager.Instance.DisplayNotificationResult();
				NotificationManager.Instance.RemoveNotification(NotificationID);
				break;
		}
	}

	public void UpdateButtonTitle()
	{
		switch (NotificationType)
		{
			case "Choice1":
				GetComponentInChildren<Text>().text = "Event:\n" +  NotificationTitle + " is Approved.";
				break;
			case "Choice2":
				GetComponentInChildren<Text>().text = "Event:\n" + NotificationTitle + " is Denied.";
				break;
		}
	}
}
