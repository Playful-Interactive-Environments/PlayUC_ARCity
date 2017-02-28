using UnityEngine;
using System.Collections;

public class CellInterface : MonoBehaviour {

	public enum InterfaceState
	{
		Hidden, Grey, Details
	}
	public enum TextState
	{
		Default, PossibleChanges
	}
	public TextMesh[] StatusText;
	public TextMesh CellName;
	private CellLogic cell;
	public GameObject[] Images;
	public Material WhiteMat;
	public Material GreyMat;
	void Start ()
	{
		cell = GetComponent<CellLogic>();
		//CellText.transform.position = new Vector3(transform.position.x, CellText.transform.position.y / 297, transform.position.z);
		EventDispatcher.StartListening("ProjectSelected", ProjectSelected);
		CellName.text = "Area " + cell.CellId;
		//ChangeCellDisplay(InterfaceState.Default);
		ChangeCellText(TextState.Default);
		StatusText[0].richText = true;
		StatusText[1].richText = true;
		StatusText[2].richText = true;

	}

	void ProjectSelected()
	{
		//ResetCell();
	}

	void Update () {

	}

	public void ChangeCellText(TextState state)
	{
		switch (state)
		{
			case TextState.PossibleChanges:
				int fin = ProjectManager.Instance.GetFinanceInt(ProjectManager.Instance.SelectedCSV);
				int soc = ProjectManager.Instance.GetSocialInt(ProjectManager.Instance.SelectedCSV);
				int env = ProjectManager.Instance.GetEnvironmentInt(ProjectManager.Instance.SelectedCSV);

				StatusText[0].text = "" + cell.FinanceRate + "	 <i>" + fin + "</i>";
				StatusText[1].text = "" + cell.SocialRate + " " + soc;
				StatusText[2].text = "" + cell.EnvironmentRate + " " + env;
				break;
			case TextState.Default:
				StatusText[0].text = "" + cell.FinanceRate + "<color=green>" + 5 + "</color>";
				StatusText[1].text = "" + cell.SocialRate + "<b>" + 5 + "</b>";
				StatusText[2].text = "" + cell.EnvironmentRate + "<i>" + 5 + "</i>";
				break;
		}
	}

	public void ChangeCellDisplay(InterfaceState state)
	{
		switch (state)
		{
			//ENABLE TEXT AND ICONS ONLY FOR PLAYER ROLE
			case InterfaceState.Hidden:
				CellName.gameObject.SetActive(false);
				Images[0].SetActive(false);
				Images[1].SetActive(false);
				Images[2].SetActive(false);
				StatusText[0].gameObject.SetActive(false);
				StatusText[1].gameObject.SetActive(false);
				StatusText[2].gameObject.SetActive(false);
				break;
			case InterfaceState.Grey:
				//StatusText[0].gameObject.GetComponent<Renderer>().material = GreyMat;
				//StatusText[1].gameObject.GetComponent<Renderer>().material = GreyMat;
				//StatusText[2].gameObject.GetComponent<Renderer>().material = GreyMat;

				CellName.gameObject.SetActive(true);
				CellName.gameObject.GetComponent<Renderer>().material = GreyMat;
				Images[0].GetComponent<SpriteRenderer>().color = Color.grey;
				Images[1].GetComponent<SpriteRenderer>().color = Color.grey;
				Images[2].GetComponent<SpriteRenderer>().color = Color.grey;

				switch (LevelManager.Instance.RoleType)
				{
					case "Finance":
						Images[0].SetActive(true);
						Images[1].SetActive(false);
						Images[2].SetActive(false);
						StatusText[0].gameObject.SetActive(true);
						StatusText[1].gameObject.SetActive(false);
						StatusText[2].gameObject.SetActive(false);

						break;
					case "Social":

						Images[0].SetActive(false);
						Images[1].SetActive(true);
						Images[2].SetActive(false);
						StatusText[0].gameObject.SetActive(false);
						StatusText[1].gameObject.SetActive(true);
						StatusText[2].gameObject.SetActive(false);
						break;
					case "Environment":
						Images[0].SetActive(false);
						Images[1].SetActive(false);
						Images[2].SetActive(true);
						StatusText[0].gameObject.SetActive(false);
						StatusText[1].gameObject.SetActive(false);
						StatusText[2].gameObject.SetActive(true);
						break;
				}
				break;
			case InterfaceState.Details:
				//ENABLE ALL TEXT AND ICONS
				CellName.gameObject.SetActive(true);
				CellName.gameObject.GetComponent<Renderer>().material = WhiteMat;

				//StatusText[0].gameObject.GetComponent<Renderer>().material = WhiteMat;
				//StatusText[1].gameObject.GetComponent<Renderer>().material = WhiteMat;
				//StatusText[2].gameObject.GetComponent<Renderer>().material = WhiteMat;


				Images[0].GetComponent<SpriteRenderer>().color = Color.white;
				Images[1].GetComponent<SpriteRenderer>().color = Color.white;
				Images[2].GetComponent<SpriteRenderer>().color = Color.white;

				switch (LevelManager.Instance.RoleType)
				{
					case "Finance":
						Images[0].SetActive(true);
						Images[1].SetActive(true);
						Images[2].SetActive(true);
						StatusText[0].gameObject.SetActive(true);
						StatusText[1].gameObject.SetActive(true);
						StatusText[2].gameObject.SetActive(true);

						break;
					case "Social":

						Images[0].SetActive(true);
						Images[1].SetActive(true);
						Images[2].SetActive(true);
						StatusText[0].gameObject.SetActive(true);
						StatusText[1].gameObject.SetActive(true);
						StatusText[2].gameObject.SetActive(true);
						break;
					case "Environment":
						Images[0].SetActive(true);
						Images[1].SetActive(true);
						Images[2].SetActive(true);
						StatusText[0].gameObject.SetActive(true);
						StatusText[1].gameObject.SetActive(true);
						StatusText[2].gameObject.SetActive(true);
						break;
				}
				break;
		}
	}
	public void ResetCell()
	{
		//ChangeCellDisplay(InterfaceState.Default);
	}

	public void DisplayCell()
	{
		//ChangeCellDisplay(InterfaceState.Menu);
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
