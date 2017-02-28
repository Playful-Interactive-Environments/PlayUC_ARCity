using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
[NetworkSettings(channel = 1, sendInterval = 0.2f)]
public class Project : NetworkBehaviour
{
	public GameObject Cell;
	public CellLogic CellLogic;
	public GameObject[] BuildingSets;
	public GameObject RepresentationParent;
	public GameObject[] PlayerLogos;
	public GameObject[] ApprovalLogos;
	//Vars set by project manager on server
	[SyncVar]
	public bool ProjectCreated;
	[SyncVar]
	public string Title;
	[SyncVar]
	public string Description;
	[SyncVar]
	public int Influence;
	[SyncVar]
	public int Budget;
	[SyncVar]
	public int Social;
	[SyncVar]
	public int Environment;
	[SyncVar]
	public int Finance;
	[SyncVar]
	public string ProjectOwner;
	[SyncVar]
	public float Cooldown;
	[SyncVar]
	public string MiniGame;
	//voting
	[SyncVar]
	public int ID_Spawn;
	[SyncVar]
	public int Id_CSV;
	[SyncVar]
	public int RepresentationId;
	[SyncVar]
	public int Choice1;
	[SyncVar]
	public int Choice2;
	[SyncVar]
	public bool VoteFinished;
	[SyncVar]
	public bool Approved;
	[SyncVar]
	private Vector3 reprRot;
	[SyncVar]
	private Vector3 projectPos;
	private Vector3 CellPos;
	private Renderer[] allRenderers;
	public Material transparentStatic;
	private Material buildingMat;
	private GameObject representation;
	public bool LocalVote;


	void Start ()
	{
		transform.name = "Project" + ID_Spawn;
		transform.parent = CellManager.Instance.ImageTarget.transform;
		ProjectManager.Instance.ActivateButtonCooldown(Id_CSV);
		CreateRepresentation();
		ShowLogo();
		ProjectManager.Instance.Projects.Add(this);
		if (isServer)
		{
			SaveStateManager.Instance.LogEvent("PLAYER " + ProjectOwner + " PROJECT " + Title);
			Choice1 += 1;
		}


		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
	}

	void OnEnable()
	{
		ShowLogo();
	}

	void Update()
	{
		if (isServer && !VoteFinished && Choice1 + Choice2 >= 2)
		{
			if (Choice1 > Choice2)
			{
				TransferValues();
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice1", ProjectOwner, ID_Spawn);
				Approved = true;
				VoteFinished = true;

			}
			if (Choice2 > Choice1)
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice2", ProjectOwner, ID_Spawn);
				Approved = false;
				VoteFinished = true;
			}
		}
		transform.position = projectPos;
		LocalVote = LevelManager.Instance.RoleType == ProjectOwner;
	}

	void NetworkDisconnect()
	{
		//Destroy(gameObject);
	}

	public void SetProject(string owner, int idcsv, int idspawn, string title, string description,
		int influence, int social, int finance, int environment, int budget, float cooldown, string minigame, int cellid, int reprID, Vector3 pos, Vector3 rot)
	{
		ProjectOwner = owner;
		Id_CSV = idcsv;
		ID_Spawn = idspawn;
		Title = title;
		Description = description;
		Influence = influence;
		Social = social;
		Finance = finance;
		Environment = environment;
		Budget = budget;
		Cooldown = cooldown;
		MiniGame = minigame;
		SetCell(cellid);
		RepresentationId = reprID;
		reprRot = rot;
		projectPos = pos;
	}

	public void CreateRepresentation()
	{
		//create 3d representation
		representation = Instantiate(BuildingSets[RepresentationId], transform.position, Quaternion.identity);
		representation.transform.parent = RepresentationParent.transform;
		representation.transform.localScale = new Vector3(.5f, .5f, .5f);
		representation.transform.localEulerAngles = reprRot;
		RepresentationParent.SetActive(true);
		allRenderers = representation.GetComponentsInChildren<Renderer>();
		buildingMat = allRenderers[0].GetComponent<Renderer>().material;
		ShowLogo();
	}

	public void TransparentOn()
	{
		allRenderers = representation.GetComponentsInChildren<Renderer>();
		foreach (Renderer child in allRenderers)
		{
			child.material = transparentStatic;
		}
		HideLogo();

	}
	public void TransparentOff()
	{
		allRenderers = representation.GetComponentsInChildren<Renderer>();
		foreach (Renderer child in allRenderers)
		{
			child.material = buildingMat;
		}
		ShowLogo();
	}

	public void HideLogo()
	{
		foreach (GameObject logo in PlayerLogos)
			logo.GetComponent<Renderer>().enabled = false;
		foreach (GameObject logo in ApprovalLogos)
			logo.GetComponent<Renderer>().enabled = false;
	}
	void ShowLogo()
	{
		HideLogo();
		//enable player logo
		switch (ProjectOwner)
		{
			case "Finance":
				PlayerLogos[0].GetComponent<Renderer>().enabled = true;
				break;
			case "Social":
				PlayerLogos[1].GetComponent<Renderer>().enabled = true;
				break;
			case "Environment":
				PlayerLogos[2].GetComponent<Renderer>().enabled = true;
				break;
		}
		if(!VoteFinished)
			ApprovalLogos[0].GetComponent<Renderer>().enabled = true;
		if (Approved && VoteFinished)
			ApprovalLogos[1].GetComponent<Renderer>().enabled = true;
		if (!Approved && VoteFinished)
			ApprovalLogos[2].GetComponent<Renderer>().enabled = true;
	}

	public void SetCell(int cellid)
	{
		Cell = CellGrid.Instance.GetCell(cellid);
		CellLogic = Cell.GetComponent<CellLogic>();
	}

	public void ShowProjectCanvas()
	{
		ProjectManager.Instance.SelectedProject = GetComponent<Project>();
		if (LocalVote || VoteFinished)
		{
			Invoke("ShowProjectInfo", .1f);
			UIManager.Instance.DebugText.text = LocalVote + " " +VoteFinished  ;
		}
		if (!LocalVote && !VoteFinished)
		{
			Invoke("ShowVoteCanvas", .1f);
		}
	}
	public void AddLocalVote()
	{
		LocalVote = true;
	}

	public void ShowVoteCanvas()
	{
		ProjectManager.Instance.SelectedCSV = Id_CSV;
		ProjectManager.Instance.SelectedId = ID_Spawn;
		UIManager.Instance.EnableVoteUI();
	}

	public void ShowProjectInfo()
	{
		ProjectManager.Instance.SelectedCSV = Id_CSV;
		ProjectManager.Instance.SelectedId = ID_Spawn;
		UIManager.Instance.ProjectInfoUI();
	}

	public void TransferValues()
	{
		CellManager.Instance.UpdateFinance(CellLogic.CellId, Finance);
		CellManager.Instance.UpdateSocial(CellLogic.CellId, Social);
		CellManager.Instance.UpdateEnvironment(CellLogic.CellId, Environment);
	}

	public void ShowApproved()
	{
		ShowLogo();
		if (LevelManager.Instance.RoleType == ProjectOwner)
		{
			CellManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Budget", Budget);
			CellManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Influence", Influence);
		}

	}

	public void ShowRejected()
	{
		ShowLogo();
		if (isServer)
		{
			Invoke("RemoveProject", 10f);
			Invoke("DestroyObject", 11);
		}

	}

	public void RemoveProject()
	{
		ProjectManager.Instance.Remove(ID_Spawn);
	
	}

	void DestroyObject()
	{
		Destroy(gameObject);
	}
}