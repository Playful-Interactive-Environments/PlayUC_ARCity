using UnityEngine;
using System.Collections;

public class CellInterface : MonoBehaviour {

	public enum InterfaceState
	{
		Default, Menu, Submenu
	}
	public InterfaceState CurrentState = InterfaceState.Default;
	public GameObject[] ButtonObjects;

	void Start () {
		foreach(GameObject buttonObject in ButtonObjects)
		{
			buttonObject.transform.localScale /= 297;
			buttonObject.transform.position = new Vector3(transform.position.x, buttonObject.transform.position.y / 297, transform.position.z);
		}
	}
	
	void Update () {

		ButtonObjects[0].transform.LookAt(Camera.main.transform);
		ButtonObjects[1].transform.LookAt(Camera.main.transform);
		switch (CurrentState)
		{
			case InterfaceState.Default:
				foreach (GameObject buttonObject in ButtonObjects)
				{
					buttonObject.SetActive(false);
				}
				break;
			case InterfaceState.Menu:
				foreach (GameObject buttonObject in ButtonObjects)
				{
					buttonObject.SetActive(true);
				}
				break;
			case InterfaceState.Submenu:
				break;
		}
	}
	public void ButtonClick()
	{
		CurrentState = InterfaceState.Default;
	}
	public void TriggerMenu()
	{
		CurrentState = InterfaceState.Menu;
	}
}
