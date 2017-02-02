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
	private GameObject _projectButton;
	public GameObject[] PlayerLogos;
	[SyncVar]
	public bool RepresentationCreated;
	//Vars set by project manager on server
	public string Title;
	public string Description;
	public int Influence;
	public int Budget;
	[SyncVar]
	public int Social;
	[SyncVar]
	public int Environment;
	[SyncVar]
	public int Finance;
	[SyncVar]
	public string ProjectOwner;
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
	public bool Approved;
	public bool LocalVote;
	private Vector3 CellPos;

	//contextual text
	public GameObject TextHolder;
	public TextMesh TitleText;
	public TextMesh ContentText;
	public TextMesh FinanceText;
	public TextMesh SocialText;
	public TextMesh EnvironmentText;

	//cardtext


	void Start ()
	{
		transform.name = "Project" + ID_Spawn;
		transform.parent = CellManager.Instance.ImageTarget.transform;
		RepresentationParent.SetActive(false);
		TextHolder.SetActive(false);
		GetComponent<BoxCollider>().enabled = false;
		if (isServer)
		{
			RepresentationId = Random.Range(0, BuildingSets.Length - 1);
			SaveStateManager.Instance.LogEvent("PLAYER " + ProjectOwner + " PROJECT " + Title);
			Choice1 += 1;
		}

		if (LevelManager.Instance.RoleType == ProjectOwner)
		{
			LocalVote = true;
			//CreateProjectButton();
		}
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
		EventDispatcher.StartListening("ProjectSelected", ProjectSelected);
		EventDispatcher.StartListening("PlacementMap", ProjectSelected);
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
		if (RepresentationCreated && !RepresentationParent.activeInHierarchy)
		{
			CreateRepresentation();
			GetComponent<BoxCollider>().enabled = true;
		}
	}
	void ProjectSelected()
	{
		TextHolder.SetActive(false);
	}

	void NetworkDisconnect()
	{
		Destroy(_projectButton);
	}



	public void CreateRepresentation()
	{
		//create 3d representation
		GameObject representation = Instantiate(BuildingSets[RepresentationId], transform.position, Quaternion.identity) as GameObject;
		representation.transform.parent = RepresentationParent.transform;
		representation.transform.localScale = new Vector3(1, 1, 1);
		representation.transform.localEulerAngles += new Vector3(0, 180, 0);
		RepresentationParent.SetActive(true);
		TitleText.text = Title;
		ContentText.text = Description;
		EnvironmentText.text = "" + Environment;
		SocialText.text = "" + Social;
		FinanceText.text = "" + Finance;
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
		//remove button
		Destroy(_projectButton);
		if (isServer)
			transform.position += CellLogic.GetPositionOffset();
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
		EventDispatcher.TriggerEvent("ProjectSelected");
		if (LocalVote)
		{
			Invoke("ShowProjectInfo", .1f);
		}
		if (!LocalVote)
		{
			Invoke("ShowVoteCanvas", .1f);
		}
	}

	public void ShowVoteCanvas()
	{
		TextHolder.SetActive(true);
		ProjectManager.Instance.SelectedProjectId = ID_Spawn;
		UIManager.Instance.EnableVoteUI();
	}

	public void ShowProjectInfo()
	{
		TextHolder.SetActive(true);
		ProjectManager.Instance.SelectedProjectId = ID_Spawn;
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
		CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Budget", ProjectManager.Instance.GetBudgetInt(ProjectManager.Instance.SelectedProjectId));
		CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Rating", ProjectManager.Instance.GetInfluenceInt(ProjectManager.Instance.SelectedProjectId));
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
		Destroy(gameObject);
	}
}
