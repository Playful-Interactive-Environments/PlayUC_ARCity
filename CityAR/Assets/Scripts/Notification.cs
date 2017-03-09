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
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);

	}

	void Update ()
	{

	}
	void NetworkDisconnect()
	{
		if (transform.name != "NotificationTemplate")
			Destroy(gameObject);
	}

	public void AccessNotification()
	{
		NotificationManager.Instance.CurrentNotification = this.gameObject;
		switch (NotificationType)
		{
			case "Choice1":
				UIManager.Instance.EventText.text = "Project " + NotificationTitle + " passed!";
				UIManager.Instance.DisplayNotificationResult();
				NotificationManager.Instance.RemoveNotification(NotificationID);
				break;
			case "Choice2":
				UIManager.Instance.EventText.text = "Project " + NotificationTitle + " failed!";
				UIManager.Instance.DisplayNotificationResult();
				NotificationManager.Instance.RemoveNotification(NotificationID);
				break;
			case "Event":
				UIManager.Instance.EventText.text = NotificationTitle + ":\n" + NotificationContent;
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
				GetComponentInChildren<Text>().text = "Project\n" +  NotificationTitle + " is Approved.";
				break;
			case "Choice2":
				GetComponentInChildren<Text>().text = "Project\n" + NotificationTitle + " is Denied.";
				break;
			case "Event":
				GetComponentInChildren<Text>().text = "Event: " + NotificationTitle;
				break;
		}
	}
}
