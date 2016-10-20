using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
	public string NotificationType;
	public string NotificationTitle;
	public string NotificationContent;
	public int NotificationID;

	void Start ()
	{
		

	}
	
	void Update () {
		GetComponentInChildren<Text>().text = NotificationType + ": " + NotificationTitle;

		
	}

	public void AccessNotification()
	{
		switch (NotificationType)
		{
			case "Vote":
				UIManager.Instance.CurrentNotification = this.gameObject;
				UIManager.Instance.GameUI();
				UIManager.Instance.Invoke("EnableVoteUI", .1f);
				break;
			case "Event":

				break;
			case "Quest":

				break;
			default:
				break;
		}
	}
}
