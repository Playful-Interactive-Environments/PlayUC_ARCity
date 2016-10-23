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
		GetComponentInChildren<Text>().text = NotificationOwner + " Proposes: " + NotificationType + " " + NotificationTitle;	
	}

	public void AccessNotification()
	{
		switch (NotificationType)
		{
			case "Vote":
				UIManager.Instance.CurrentNotification = this.gameObject;
				UIManager.Instance.GameUI();
                UIManager.Instance.ProjectDescription(NotificationID);
                UIManager.Instance.Invoke("EnableVoteUI", .1f);
		        ProjectManager.Instance.CurrentID = NotificationID;
                break;
			case "Choice1":
                //UIManager.Instance.AddNotification("Event", NotificationID);
                Debug.Log("Notification 1");
				break;
			case "Choice2":
                Debug.Log("Notification 2");

                break;
            case "Event":

		        break;
			default:
				break;
		}
	}
}
