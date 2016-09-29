using UnityEngine;
using System.Collections;

public class CellInterface : MonoBehaviour {

	public enum InterfaceState
	{
		Default, Menu, Submenu
	}
	public InterfaceState CurrentState = InterfaceState.Default;
	public TextMesh PollText;
	public TextMesh JobsText;
	public GameObject TextBackground;
	public TextMesh Text;
	void Start () {
		PollText.transform.localScale /= 297;
		PollText.transform.position = new Vector3(transform.position.x, PollText.transform.position.y / 297, transform.position.z);
		JobsText.transform.localScale /= 297;
		JobsText.transform.position = new Vector3(transform.position.x, JobsText.transform.position.y / 297, transform.position.z);
		TextBackground.transform.localScale /= 297;
		TextBackground.transform.position = new Vector3(transform.position.x, TextBackground.transform.position.y / 297, transform.position.z);
		Text.transform.localScale /= 297;
		Text.transform.position = new Vector3(transform.position.x, Text.transform.position.y / 297, transform.position.z);
	}
	
	void Update () {
		switch (CurrentState)
		{
			case InterfaceState.Default:
				PollText.gameObject.SetActive(false);
				JobsText.gameObject.SetActive(false);
				TextBackground.gameObject.SetActive(false);
				Text.gameObject.SetActive(false);

				break;
			case InterfaceState.Menu:
				PollText.gameObject.SetActive(true);
				JobsText.gameObject.SetActive(true);
				TextBackground.gameObject.SetActive(true);
				Text.gameObject.SetActive(true);
				break;
			case InterfaceState.Submenu:
				break;
		}
	}
}
