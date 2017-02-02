using UnityEngine;
using System.Collections;

public class CellInterface : MonoBehaviour {

	public enum InterfaceState
	{
		Default, Menu, Submenu
	}
	public InterfaceState CurrentState = InterfaceState.Default;
	public TextMesh StatusText;
	public TextMesh CellName;
	private CellLogic cell;
	public GameObject[] Images;
	void Start ()
	{
		cell = GetComponent<CellLogic>();
		//CellText.transform.position = new Vector3(transform.position.x, CellText.transform.position.y / 297, transform.position.z);
		EventDispatcher.StartListening("ProjectSelected", ProjectSelected);
		CellName.text = "Area " + cell.CellId;
	}

	void ProjectSelected()
	{
		ResetCell();
	}

	void Update () {
		switch (LevelManager.Instance.RoleType)
		{
			case "Finance":
				StatusText.text = "" + cell.FinanceRate;
				Images[0].SetActive(true);
				Images[1].SetActive(false);
				Images[2].SetActive(false);
				break;
			case "Social":
				Images[1].SetActive(true);
				Images[0].SetActive(false);
				Images[2].SetActive(false);
				StatusText.text = "" + cell.SocialRate;
				break;
			case "Environment":
				Images[2].SetActive(true);
				Images[1].SetActive(false);
				Images[0].SetActive(false);
				StatusText.text = "" + cell.EnvironmentRate;
				break;
		}
		switch (CurrentState)
		{
			case InterfaceState.Default:
				StatusText.color = Color.grey;
		        CellName.color = Color.grey;
				Images[0].GetComponent<SpriteRenderer>().color = Color.grey;
				Images[1].GetComponent<SpriteRenderer>().color = Color.grey;
				Images[2].GetComponent<SpriteRenderer>().color = Color.grey;
				break;
			case InterfaceState.Menu:
				StatusText.color = Color.white;
				CellName.color = Color.white;
				Images[0].GetComponent<SpriteRenderer>().color = Color.white;
				Images[1].GetComponent<SpriteRenderer>().color = Color.white;
				Images[2].GetComponent<SpriteRenderer>().color = Color.white;
				break;
		}
	}

	public void ResetCell()
	{
		CurrentState = InterfaceState.Default;
	}

	public void DisplayCell()
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
