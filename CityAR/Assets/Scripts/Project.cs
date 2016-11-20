using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Project : NetworkBehaviour
{
	public TextMesh EffectText;
	public HexCell Cell;
	public CellLogic CellLogic;
	public GameObject[] BuildingSets;
	public GameObject RepresentationParent;
	public GameObject ProjectLogo;
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
	public bool Approved;
	public int Choice1;
	public int Choice2;
	public bool LocalVote;

	
	void Start () {
		transform.name = "Project";
		transform.parent = CellManager.Instance.ImageTarget.transform;
		iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 0, "time", .1f));
		ProjectManager.Instance.SelectedProject = GetComponent<Project>();
		Invoke("CreateRepresentation", .1f);
	}

	void CreateRepresentation()
	{
		GameObject representation = Instantiate(BuildingSets[Random.Range(0, BuildingSets.Length-1)], transform.position, Quaternion.identity) as GameObject;
		representation.transform.parent = RepresentationParent.transform;
		representation.transform.localScale = new Vector3(1, 1, 1);
		representation.transform.localEulerAngles += new Vector3(0,180,0);
		transform.position += CellLogic.GetPositionOffset();

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

	public void InitiateProject()
	{
		int financeeffect = Finance * GlobalManager.Instance.MonthDuration;
		int socialeffect = Social * GlobalManager.Instance.MonthDuration;
		int environmenteffect = Environment * GlobalManager.Instance.MonthDuration;

		CellManager.Instance.UpdateFinance(Cell.CellId, financeeffect);
		CellManager.Instance.UpdateSocial(Cell.CellId, socialeffect);
		CellManager.Instance.UpdateEnvironment(Cell.CellId, environmenteffect);
		EffectText.text = "F: " + financeeffect + "\nS: " + socialeffect + "\nE: " + environmenteffect;
		iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 1, "time", .5f));
		iTween.FadeTo(ProjectLogo, iTween.Hash("alpha", 0, "time", .5f));
		EffectiveTime--;
	}

	public void SetCell(Vector3 pos)
	{
		Cell = HexGrid.Instance.GetCell(pos);
		CellLogic = Cell.GetComponent<CellLogic>();
		CellLogic.AddOccupied();
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
		ProjectManager.Instance.SelectedProject = GetComponent<Project>();
		ProjectManager.Instance.SelectedProjectId = ProjectId;
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
		CellLogic.RemoveOccupied();
		Destroy(gameObject);
	}
}
