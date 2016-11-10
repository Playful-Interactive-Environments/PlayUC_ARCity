using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Project : NetworkBehaviour
{
	public TextMesh EffectText;
	public HexCell Cell;

	public string ProjectOwner;
	public int EffectiveTime = 12;
	public string Title;
	public string Description;
	public int Rating;
	public int Social;
	public int Environment;
	public int Finance;
	public int Cost;
	//voting
	[SyncVar]
	public int ProjectId;
	[SyncVar]
	public bool Approved;
	public int Choice1;
	public int Choice2;
	public bool LocalVote;

	
	void Start () {
		transform.name = "Project";
		transform.parent = CellManager.Instance.ImageTarget.transform;
		iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 0, "time", .1f));
		ProjectManager.Instance.CurrentProject = GetComponent<Project>();
	}

	void Update () {
		if (isServer && !Approved && Choice1 + Choice2 == 3)
		{
			if (Choice1 > Choice2)
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice1", ProjectOwner, ProjectId);
			}
			else
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice2", ProjectOwner, ProjectId);
			}
			Approved = true;
		}
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

	public void SetCell(Vector3 pos)
	{
		Cell = HexGrid.Instance.GetCell(pos);
	}
	public void PlaceProject()
	{
		InvokeRepeating("ProjectEffect", 0f, GlobalManager.Instance.MonthDuration);
	}

	IEnumerator AnimateText()
	{
		iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 1, "time", .5f));
		yield return new WaitForSeconds(3f);
		iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 0, "time", .5f));
	}

	public void ShowProjectCanvas()
	{
		if (LocalVote)
		{
			ShowProjectInfo();
		}
		if (!LocalVote)
		{
			ShowVoteCanvas();
		}

	}

	public void ShowVoteCanvas()
	{
		ProjectManager.Instance.CurrentProject = GetComponent<Project>();
		ProjectManager.Instance.CurrentID = ProjectId;
		UIManager.Instance.ProjectDescription(ProjectId);
		UIManager.Instance.EnableVoteUI();
	}

	public void ShowProjectInfo()
	{
		UIManager.Instance.ProjectInfo.text = QuestManager.Instance.GetProjectDescription(ProjectId);
		UIManager.Instance.ShowProjectInfo();
	}

	public void RemoveProject()
	{
		Destroy(gameObject);
	}
}
