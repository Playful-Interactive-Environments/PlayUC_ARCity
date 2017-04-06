using UnityEngine;
using System.Collections;
using Boo.Lang;
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

    //Animate Rise of numbers
    public GameObject FinanceRise;
    public GameObject SocialRise;
    public GameObject EnvironmentRise;
    public TextMeshPro FinanceRiseText;
    public TextMeshPro SocialRiseText;
    public TextMeshPro EnvironmentRiseText;
    private float FinRiseVal;
    private float SocRiseVal;
    private float EnvRiseVal;
    private bool animate;
    private float animateT = 5;


    void Start ()
	{
		cell = GetComponent<CellLogic>();
		//CellText.transform.position = new Vector3(transform.position.x, CellText.transform.position.y / 297, transform.position.z);
		//ChangeCellDisplay(InterfaceState.Grey);
		StatusText[0].richText = true;
		StatusText[1].richText = true;
		StatusText[2].richText = true;
	}

    IEnumerator ExpandDisplay(int finVal, int socVal, int envVal)
    {
        animateT = 5f;
        animate = true;
        Images[0].SetActive(true);
        Images[1].SetActive(true);
        Images[2].SetActive(true);

        iTween.ScaleTo(FinanceRise, iTween.Hash("y", cell.FinanceRate / 10f, "time", animateT));
        iTween.MoveTo(FinanceRise, iTween.Hash("y", cell.FinanceRate / 10f, "time", animateT));
        iTween.ScaleTo(SocialRise, iTween.Hash("y", cell.SocialRate / 10f, "time", animateT));
        iTween.MoveTo(SocialRise, iTween.Hash("y", cell.SocialRate / 10f, "time", animateT));
        iTween.ScaleTo(EnvironmentRise, iTween.Hash("y", cell.EnvironmentRate / 10f, "time", animateT));
        iTween.MoveTo(EnvironmentRise, iTween.Hash("y", cell.EnvironmentRate / 10f, "time", animateT));

        yield return new WaitForSeconds(animateT);
        animate = false;
    }

    void Update ()
	{
	    if (animate)
	    {
            FinanceRiseText.rectTransform.anchoredPosition3D = new Vector3(FinanceRise.transform.position.x, FinanceRise.transform.position.y + 10f, FinanceRise.transform.position.z);
            SocialRiseText.rectTransform.anchoredPosition3D = new Vector3(SocialRise.transform.position.x, SocialRise.transform.position.y + 10f, SocialRise.transform.position.z);
            EnvironmentRiseText.rectTransform.anchoredPosition3D = new Vector3(EnvironmentRise.transform.position.x, EnvironmentRise.transform.position.y + 10f, EnvironmentRise.transform.position.z);
	        FinanceRiseText.text = "" + cell.FinanceRate;
	        SocialRiseText.text = "" + cell.SocialRate;
	        EnvironmentRiseText.text = "" + cell.EnvironmentRate;
	    }
    }

    public void HighlightCell(int fin, int soc, int env)
    {
        StartCoroutine(ExpandDisplay(fin, soc, env));
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

				switch (LocalManager.Instance.RoleType)
				{
					case Vars.Player1:
						Images[0].SetActive(true);
						Images[1].SetActive(false);
						Images[2].SetActive(false);
						StatusText[0].gameObject.SetActive(true);
						StatusText[1].gameObject.SetActive(false);
						StatusText[2].gameObject.SetActive(false);

						break;
					case Vars.Player2:
						Images[0].SetActive(false);
						Images[1].SetActive(true);
						Images[2].SetActive(false);
						StatusText[0].gameObject.SetActive(false);
						StatusText[1].gameObject.SetActive(true);
						StatusText[2].gameObject.SetActive(false);
						break;
					case Vars.Player3:
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
				switch (LocalManager.Instance.RoleType)
				{
					case Vars.Player1:
						Images[0].SetActive(true);
						Images[1].SetActive(true);
						Images[2].SetActive(true);
						StatusText[0].gameObject.SetActive(true);
						StatusText[1].gameObject.SetActive(true);
						StatusText[2].gameObject.SetActive(true);
						break;
					case Vars.Player2:

						Images[0].SetActive(true);
						Images[1].SetActive(true);
						Images[2].SetActive(true);
						StatusText[0].gameObject.SetActive(true);
						StatusText[1].gameObject.SetActive(true);
						StatusText[2].gameObject.SetActive(true);
						break;
					case Vars.Player3:
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
