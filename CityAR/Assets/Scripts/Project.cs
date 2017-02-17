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
	public bool Approved;
	public bool LocalVote;
	private Vector3 CellPos;
	public Material[] Materials;

	void Start ()
	{
		transform.name = "Project" + ID_Spawn;
		transform.parent = CellManager.Instance.ImageTarget.transform;
		ProjectManager.Instance.ActivateButtonCooldown(Id_CSV);
		CreateRepresentation();
		UpdateLogo();
		if (isServer)
		{
			SaveStateManager.Instance.LogEvent("PLAYER " + ProjectOwner + " PROJECT " + Title);
			Choice1 += 1;
		}

		if (LevelManager.Instance.RoleType == ProjectOwner)
		{
			LocalVote = true;
		}
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
		if (isServer)
			Invoke("RemoveProject", 45f);
	}

	void OnEnable()
	{
		UpdateLogo();
		if (LevelManager.Instance.RoleType == ProjectOwner)
		{
			LocalVote = true;
		}
	}

	void Update()
	{
		if (isServer && !Approved && Choice1 + Choice2 >= 2)
		{
			if (Choice1 > Choice2)
			{
				InitiateProject();
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice1", ProjectOwner, ID_Spawn);
				Approved = true;
			}
			else if (Choice2 > Choice1)
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice2", ProjectOwner, ID_Spawn);
				Approved = true;
			}
		}
	}

	void NetworkDisconnect()
	{
		//Destroy(gameObject);
	}

	public void SetProject(string owner, int idcsv, int idspawn, string title, string description,
		int influence, int social, int finance, int environment, int budget, float cooldown, string minigame, int cellid, int reprID )
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
	}

	public void CreateRepresentation()
	{
		//create 3d representation
		GameObject representation = Instantiate(BuildingSets[RepresentationId], transform.position, Quaternion.identity);
		representation.transform.parent = RepresentationParent.transform;
		representation.transform.localScale = new Vector3(.5f, .5f, .5f);
		representation.transform.localEulerAngles += new Vector3(0, 180, 0);
		RepresentationParent.SetActive(true);
		Renderer[] allChildren = representation.GetComponentsInChildren<Renderer>();
		switch (ProjectOwner)
		{
			case "Finance":
				foreach (Renderer child in allChildren)
				{
					child.material = Materials[0];
				}
				break;
			case "Social":
				foreach (Renderer child in allChildren)
				{
					child.material = Materials[1];
				}
				break;
			case "Environment":
				foreach (Renderer child in allChildren)
				{
					child.material = Materials[2];
				}
				break;
		}
		if (isServer)
			transform.position += CellLogic.GetPositionOffset();
		UpdateLogo();
	}

	void UpdateLogo()
	{
		foreach (GameObject logo in PlayerLogos)
		{
			logo.GetComponent<Renderer>().enabled = false;
		}
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
	}

	public void SetCell(int cellid)
	{
		Cell = CellGrid.Instance.GetCell(cellid);
		CellManager.Instance.NetworkCommunicator.CellOccupiedStatus("add", cellid);
		CellLogic = Cell.GetComponent<CellLogic>();
	}

	public void ShowProjectCanvas()
	{
		ProjectManager.Instance.SelectedProject = GetComponent<Project>();
		if (LocalVote || (!LocalVote && Approved))
		{
			Invoke("ShowProjectInfo", .1f);
		}
		if (!LocalVote && !Approved)
		{
			Invoke("ShowVoteCanvas", .1f);
		}
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

	public void InitiateProject()
	{
		CellManager.Instance.UpdateFinance(CellLogic.CellId, Finance);
		CellManager.Instance.UpdateSocial(CellLogic.CellId, Social);
		CellManager.Instance.UpdateEnvironment(CellLogic.CellId, Environment);
		Debug.Log(Finance + " " + " " + Social + " " + Environment);
	}

	public void ShowApproved()
	{
		foreach (GameObject logo in PlayerLogos)
		{
			logo.GetComponent<Renderer>().enabled = false;
		}
		PlayerLogos[3].GetComponent<Renderer>().enabled = true;
		if (LevelManager.Instance.RoleType == ProjectOwner)
		{
			CellManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Budget", Budget);
			CellManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Influence", Influence);
		}
		if (isServer)
			Invoke("RemoveProject", 15f);
	}

	public void ShowRejected()
	{
		foreach (GameObject logo in PlayerLogos)
		{
			logo.GetComponent<Renderer>().enabled = false;
		}
		PlayerLogos[4].GetComponent<Renderer>().enabled = true;
		Debug.Log("Rejected");
		if(isServer)
			Invoke("RemoveProject", 5f);
	}

	public void RemoveProject()
	{
		CellManager.Instance.NetworkCommunicator.CellOccupiedStatus("remove", CellLogic.CellId);
		ProjectManager.Instance.Remove(ID_Spawn);
		Invoke("DestroyObject", 5f);
	}

	void DestroyObject()
	{
		Destroy(gameObject);

	}
}
