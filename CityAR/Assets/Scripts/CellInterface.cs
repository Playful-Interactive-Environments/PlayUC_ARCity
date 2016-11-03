using UnityEngine;
using System.Collections;

public class CellInterface : MonoBehaviour {

	public enum InterfaceState
	{
		Default, Menu, Submenu
	}
	public InterfaceState CurrentState = InterfaceState.Default;
	public TextMesh CellText;

	void Start () {

		CellText.transform.localScale /= 297;
		CellText.transform.position = new Vector3(transform.position.x, CellText.transform.position.y / 297, transform.position.z);
		CellText.text = GetComponent<HexCell>().CellId +"";
	}
	
	void Update () {


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

/*		foreach(GameObject buttonObject in ButtonObjects)
		{
			buttonObject.transform.localScale /= 297;
			buttonObject.transform.position = new Vector3(transform.position.x, buttonObject.transform.position.y / 297, transform.position.z);

		}
		ButtonObjects[0].transform.LookAt(Camera.main.transform);
		ButtonObjects[1].transform.LookAt(Camera.main.transform);
		switch (CurrentState)
		{
			case InterfaceState.Default:
				foreach (GameObject buttonObject in ButtonObjects)
				{
					buttonObject.SetActive(true);
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
*/
