using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
[NetworkSettings(channel = 1, sendInterval = 0.2f)]
public class Project : NetworkBehaviour
{
	public GameObject[] BuildingSets;
	public GameObject RepresentationParent;
	public ParticleSystem ParticlesApproval;
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
	private Vector3 reprRot;
	[SyncVar]
	private Vector3 projectPos;
	[SyncVar]
	private Vector3 CellPos;
    [SyncVar]
    private int cellId;
    public bool Approved;
	public Material transparentStatic;
	private GameObject buildingRep;
	private GameObject transparentRep;

	void Start ()
	{
		transform.name = "Project" + ID_Spawn;
		transform.parent = LocalManager.Instance.ImageTarget.transform;
		ProjectManager.Instance.LockButton(Id_CSV);
		CreateRepresentation();

		ProjectManager.Instance.Projects.Add(this);
		ProjectManager.Instance.SelectedProject = this;
		if (isServer)
		{
			SaveStateManager.Instance.LogEvent("PLAYER " + ProjectOwner + " PROJECT " + Title);
			SaveStateManager.Instance.AddProjectAction(ProjectOwner, "Propose");
			//Choice1 += 1;
		}
	}

	void Update()
	{
		if (isServer && !VoteFinished && Choice1 + Choice2 >= 3)
		{
			if (Choice1 > Choice2)
			{
				TransferValues();
                LocalManager.Instance.NetworkCommunicator.Vote(Vars.ResultChoice1, ProjectOwner, ID_Spawn);
				VoteFinished = true;
			}
			if (Choice2 > Choice1)
			{
                LocalManager.Instance.NetworkCommunicator.Vote(Vars.ResultChoice2, ProjectOwner, ID_Spawn);
				VoteFinished = true;
			}
            LocalManager.Instance.NetworkCommunicator.SetPlayerState(ProjectOwner, "DiscussionEnd");

		}
		transform.position = projectPos;
	}

	public void SetProject(string owner, int idcsv, int idspawn, string title, string description,
		int influence, int social, int finance, int environment, int budget, float cooldown, 
		string minigame, int cellid, int reprID, Vector3 pos, Vector3 rot)
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
        cellId = cellid;
        RepresentationId = reprID;
		reprRot = rot;
		projectPos = pos;
	}

    public void ChangeLanguage(string title, string content)
    {
        Title = title;
        Description = content;
    }
	public void CreateRepresentation()
	{
		//create 3d representation
		buildingRep = Instantiate(BuildingSets[RepresentationId], transform.position, Quaternion.identity);
		buildingRep.transform.parent = RepresentationParent.transform;
		buildingRep.transform.localScale = new Vector3(1f, 1f, 1f);
		buildingRep.transform.localEulerAngles = reprRot;
		transparentRep = Instantiate(BuildingSets[RepresentationId], transform.position, Quaternion.identity);
		transparentRep.transform.parent = RepresentationParent.transform;
		transparentRep.transform.localScale = new Vector3(1f, 1f, 1f);
		transparentRep.transform.localEulerAngles = reprRot;
		foreach (Renderer child in transparentRep.GetComponentsInChildren<Renderer>())
		{
			child.material = transparentStatic;
		}
		RepresentationParent.SetActive(true);
	}

	public void TransparentOn()
	{
		transparentRep.SetActive(true);
		buildingRep.SetActive(false);
	}

	public void TransparentOff()
	{
		transparentRep.SetActive(false);
		buildingRep.SetActive(true);
	}

	public void ShowProjectCanvas()
	{
		ProjectManager.Instance.SelectedProject = GetComponent<Project>();
		Invoke("ShowProject", .1f);
	}

	public void ShowProject()
	{
		ProjectManager.Instance.SelectedProject = this;
		UIManager.Instance.ShowProjectDisplay();
	}

	public void TransferValues()
	{
		CellManager.Instance.UpdateFinance(cellId, Finance);
		CellManager.Instance.UpdateSocial(cellId, Social);
		CellManager.Instance.UpdateEnvironment(cellId, Environment);
	}

	public void TriggerApproved()
	{
		var mainModule = ParticlesApproval.main;
		switch (ProjectOwner)
		{
			case Vars.Player1:
				mainModule.startColor = Color.blue;
				break;
			case Vars.Player2:
				mainModule.startColor = new Color(1f, 0.64f, 0f);
				break;
			case Vars.Player3:
				mainModule.startColor = Color.green;
				break;
		}
		ParticlesApproval.Play();
		//Only approved pay
		if (Approved)
		{
			int toPay = Mathf.RoundToInt(Mathf.Abs(Budget / Choice1) + DiscussionManager.Instance.ExtraCost);
            LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue1, -toPay);
            LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue2, DiscussionManager.Instance.TotalInfluence);
            LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "MoneySpent",toPay);
            CellGrid.Instance.GetCell(cellId).GetComponent<CellInterface>().HighlightCell(Finance, Social, Environment);
		}
        if (isServer)
		{
            LocalManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Successful", 0);
		}
	}

	public void TriggerRejected()
	{
		var mainModule = ParticlesApproval.main;
		mainModule.startColor = Color.red;
		ParticlesApproval.Play();
		if (isServer)
		{
            LocalManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Failed", 0);
			Invoke("RemoveProject", 10f);
			Invoke("DestroyObject", 11);
		}
	}

	public void TriggerCanceled()
	{
		Invoke("RemoveProject", 0f);
		Invoke("DestroyObject", .5f);
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