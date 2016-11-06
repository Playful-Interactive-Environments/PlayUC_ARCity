using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Project : NetworkBehaviour
{
	public string Title;
	public string Description;
	public int Rating;
	public int Social;
	public int Environment;
	public int Finance;
	public int Cost;
	public int ProjectId;

    public TextMesh EffectText;
	public HexCell Cell;

	public int EffectiveTime = 12;

	void Start () {
		transform.name = "Project";
		transform.parent = CellManager.Instance.ImageTarget.transform;
        iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 0, "time", .1f));

    }

    void Update () {
	
	}

	public void ProjectEffect()
	{
		CellManager.Instance.UpdateFinance(Cell.CellId, Finance);
		CellManager.Instance.UpdateSocial(Cell.CellId, Social);
		CellManager.Instance.UpdateEnvironment(Cell.CellId, Environment);
	    EffectText.text = "Finance: " + Finance + "\nSocial: " + Social + "\nEnvironment: " + Environment;
        if (EffectiveTime == 0)
		{
			Debug.Log(Cell.GetComponent<CellLogic>().GetVars());
			CancelInvoke("ProjectEffect");
		}
	    StartCoroutine(AnimateText());
		EffectiveTime--;
	}

	public void ShowProjectInfo()
	{
		UIManager.Instance.ProjectInfo.text = QuestManager.Instance.GetProjectDescription(ProjectId);
		UIManager.Instance.ShowProjectInfo();
	}

	public void PlaceProject(Vector3 pos)
	{
		Cell = HexGrid.Instance.GetCell(pos);

		RoleManager.Instance.Budget += Cost;
		RoleManager.Instance.Rating += Rating;

		InvokeRepeating("ProjectEffect", 0f, GlobalManager.Instance.MonthDuration);
        Debug.Log(Cell.GetComponent<CellLogic>().GetVars());
        
	}

    IEnumerator AnimateText()
    {
        iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 1, "time", .5f));
        yield return new WaitForSeconds(3f);
        iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 0, "time", .5f));

    }
}
