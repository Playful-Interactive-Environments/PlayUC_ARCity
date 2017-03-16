using UnityEngine;
using System.Collections;
using TMPro;

public class CellInterface : MonoBehaviour {

	public enum InterfaceState
	{
		Hide, Grey, White
	}
	public enum TextState
	{
		Grey, White, Changes
	}
	 
	public TextState CurrentTextState;
	public TextMeshPro[] StatusText;
	public TextMeshPro CellName;
	private CellLogic cell;
	public GameObject[] Images;
	public Material WhiteMat;
	public Material GreyMat;
	void Start ()
	{
		cell = GetComponent<CellLogic>();
		//CellText.transform.position = new Vector3(transform.position.x, CellText.transform.position.y / 297, transform.position.z);
		//ChangeCellDisplay(InterfaceState.Grey);
		StatusText[0].richText = true;
		StatusText[1].richText = true;
		StatusText[2].richText = true;
	}

	void Update ()
	{
	}

	public void ChangeCellText(TextState state)
	{
	    CurrentTextState = state;
		switch (state)
		{
			case TextState.Changes:
				int fin = ProjectManager.Instance.GetFinanceInt(ProjectManager.Instance.CurrentDummy.Id_CSV);
				int soc = ProjectManager.Instance.GetSocialInt(ProjectManager.Instance.CurrentDummy.Id_CSV);
				int env = ProjectManager.Instance.GetEnvironmentInt(ProjectManager.Instance.CurrentDummy.Id_CSV);
				CellName.text = "<color=white>Area " + cell.CellId + "</color>";
				if (fin >= 0)
					StatusText[0].text = "<color=white>" + cell.FinanceRate + "</color>" + " <color=green><b><size=2>+" + fin + "</color></b></size>";
				if(soc >= 0)
					StatusText[1].text = "<color=white>" + cell.SocialRate + "</color>" + " <color=green><b><size=2>+" + soc + "</color></b></size>";
				if(env >=0)
					StatusText[2].text = "<color=white>" + cell.EnvironmentRate + "</color>" + " <color=green><b><size=2>+" + env + "</color></b></size>";
				if (fin < 0)
					StatusText[0].text = "<color=white>" + cell.FinanceRate + "</color>" + " <color=red><b><size=2>" + fin + "</color>";
				if (soc < 0)
					StatusText[1].text = "<color=white>" + cell.SocialRate + "</color>" + " <color=red><b><size=2>" + soc + "</color>";
				if (env < 0)
					StatusText[2].text = "<color=white>" + cell.EnvironmentRate + "</color>" + " <color=red><b><size=2>" + env + "</color>";
				break;
			case TextState.Grey:
				CellName.text = "<color=#c0c0c0ff>Area " + cell.CellId + "</color>";
				StatusText[0].text = "<color=#c0c0c0ff>" + cell.FinanceRate + "</color>";
				StatusText[1].text = "<color=#c0c0c0ff>" + cell.SocialRate + "</color>";
				StatusText[2].text = "<color=#c0c0c0ff>" + cell.EnvironmentRate + "</color>";
				break;
			case TextState.White:
				CellName.text = "<color=white>Area " + cell.CellId + "</color>";
				StatusText[0].text = "<color=white>" + cell.FinanceRate + "</color>";
				StatusText[1].text = "<color=white>" + cell.SocialRate + "</color>";
				StatusText[2].text = "<color=white>" + cell.EnvironmentRate + "</color>";
				break;
		}
	}

	public void ChangeCellDisplay(InterfaceState state)
	{
		switch (state)
		{
			//ENABLE TEXT AND ICONS ONLY FOR PLAYER ROLE
			case InterfaceState.Hide:
				CellName.gameObject.SetActive(false);
				Images[0].SetActive(false);
				Images[1].SetActive(false);
				Images[2].SetActive(false);
				StatusText[0].gameObject.SetActive(false);
				StatusText[1].gameObject.SetActive(false);
				StatusText[2].gameObject.SetActive(false);
				break;
			case InterfaceState.Grey:
				CellName.gameObject.SetActive(true);
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
			case InterfaceState.White:
				//ENABLE ALL TEXT AND ICONS
				CellName.gameObject.SetActive(true);
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
}
