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
	//Vars set by project manager on server
	public string Title;
	public string Description;
	public int Rating;
	public int Social;
	public int Environment;
	public int Finance;
	public int Cost;
	[SyncVar]
	public string ProjectOwner;
	[SyncVar]
	private int financeeffect;
	[SyncVar]
	private int socialeffect;
	[SyncVar]
	private int environmenteffect;
	//voting
	[SyncVar]
	public int ProjectId;
	[SyncVar]
	public int RepresentationId;
	public bool Approved;
	public int Choice1;
	public int Choice2;
	public bool LocalVote;


	void Start () {
		transform.name = "Project";
		transform.parent = CellManager.Instance.ImageTarget.transform;
		iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 0, "time", .1f));
		ProjectManager.Instance.SelectedProject = GetComponent<Project>();
		if (isServer)
			RepresentationId = Random.Range(0, BuildingSets.Length - 1);
		Invoke("CreateRepresentation", .1f);
		if (LocalManager.Instance.RoleType == ProjectOwner)
		{
			LocalVote = true;
		}
		Choice1 += 1;
	}

	void CreateRepresentation()
	{
		GameObject representation = Instantiate(BuildingSets[RepresentationId], transform.position, Quaternion.identity) as GameObject;
		representation.transform.parent = RepresentationParent.transform;
		representation.transform.localScale = new Vector3(1, 1, 1);
		representation.transform.localEulerAngles += new Vector3(0,180,0);
		if (isServer)
			transform.position += CellLogic.GetPositionOffset();

	}

	void Update () {
		if (isServer && !Approved && Choice1 + Choice2 >= 2)
		{
			if (Choice1 > Choice2)
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice1", ProjectOwner, ProjectId);
				Approved = true;
				//InitiateProject();
			}
			else if (Choice2 > Choice1)
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice2", ProjectOwner, ProjectId);
				//RemoveProject();
				Approved = true;

			}
			else return;
		}
	}

	public void InitiateProject()
	{
		if (isServer)
		{
			financeeffect = Finance * GlobalManager.Instance.MonthDuration;
			socialeffect = Social * GlobalManager.Instance.MonthDuration;
			environmenteffect = Environment * GlobalManager.Instance.MonthDuration;

			CellManager.Instance.UpdateFinance(Cell.CellId, financeeffect);
			CellManager.Instance.UpdateSocial(Cell.CellId, socialeffect);
			CellManager.Instance.UpdateEnvironment(Cell.CellId, environmenteffect);
		    Debug.Log(financeeffect + " " + " " + socialeffect + " " + environmenteffect);
		}

		EffectText.text = "F: " + financeeffect + "\nS: " + socialeffect + "\nE: " + environmenteffect;
		iTween.FadeTo(EffectText.gameObject, iTween.Hash("alpha", 1, "time", .5f));
		ProjectLogo.SetActive(false);
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
