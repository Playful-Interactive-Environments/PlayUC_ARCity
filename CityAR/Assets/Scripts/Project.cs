﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
[NetworkSettings(channel = 1, sendInterval = 0.2f)]
public class Project : NetworkBehaviour
{
	public HexCell Cell;
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
	public int Rating;
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
	public int ProjectId;
	[SyncVar]
	public int RepresentationId;
	public bool Approved;
	[SyncVar]
	public int Choice1;
	[SyncVar]
	public int Choice2;
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
	public GameObject ProjectTemplate;
	public GridLayoutGroup GridGroup;

	void Start ()
	{
		transform.name = "Project";
		transform.parent = CellManager.Instance.ImageTarget.transform;
		RepresentationParent.SetActive(false);
		TextHolder.SetActive(false);

		GetComponent<BoxCollider>().enabled = false;
		if (isServer)
		{
			RepresentationId = Random.Range(0, BuildingSets.Length - 1);
			GlobalManager.Instance.LogEvent("PLAYER " + ProjectOwner + " PROJECT " + Title);
			Choice1 += 1;
		}

		if (LocalManager.Instance.RoleType == ProjectOwner)
		{
			LocalVote = true;
			CreateProjectButton();
		}
		EventManager.StartListening("NetworkDisconnect", NetworkDisconnect);
		EventManager.StartListening("ProjectSelected", ProjectSelected);
		EventManager.StartListening("PlacementMap", ProjectSelected);
	}

	void Update()
	{
		if (isServer && !Approved && Choice1 + Choice2 >= 2)
		{
			if (Choice1 > Choice2)
			{
				InitiateProject();
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice1", ProjectOwner, ProjectId);
				Approved = true;
			}
			else if (Choice2 > Choice1)
			{
				CellManager.Instance.NetworkCommunicator.Vote("Result_Choice2", ProjectOwner, ProjectId);
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

	public void CreateProjectButton()
	{
		GridGroup = GameObject.Find("ProjectLayout").GetComponent<GridLayoutGroup>();
		ProjectTemplate = GameObject.Find("ProjectTemplate");
		_projectButton = Instantiate(ProjectTemplate, transform.position, Quaternion.identity) as GameObject;
		_projectButton.transform.parent = GridGroup.transform;
		_projectButton.transform.localScale = new Vector3(1, 1, 1);
		_projectButton.GetComponent<Button>().onClick.AddListener(() => SelectProject());
		_projectButton.GetComponent<ProjectText>().SetText(ProjectId);
		ProjectManager.Instance.SelectedProject = GetComponent<Project>();
        UIManager.Instance.ProjectButton.image.color = Color.red;

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

	void SelectProject()
	{
		ProjectManager.Instance.SelectedProjectId = ProjectId;
		UIManager.Instance.ShowPlacementCanvas();
	}

	public void SetCell(Vector3 pos)
	{
		Cell = HexGrid.Instance.GetCell(pos);
		CellManager.Instance.NetworkCommunicator.CellOccupiedStatus("add", Cell.CellId);

		CellLogic = Cell.GetComponent<CellLogic>();
	}

	public void ShowProjectCanvas()
	{
		ProjectManager.Instance.SelectedProject = GetComponent<Project>();
		EventManager.TriggerEvent("ProjectSelected");
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
		ProjectManager.Instance.SelectedProjectId = ProjectId;
		UIManager.Instance.EnableVoteUI();
	}

	public void ShowProjectInfo()
	{
		TextHolder.SetActive(true);
		ProjectManager.Instance.SelectedProjectId = ProjectId;
	}

	public void InitiateProject()
	{
		CellManager.Instance.UpdateFinance(Cell.CellId, Finance);
		CellManager.Instance.UpdateSocial(Cell.CellId, Social);
		CellManager.Instance.UpdateEnvironment(Cell.CellId, Environment);
		Debug.Log(Finance + " " + " " + Social + " " + Environment);
	}

	public void ShowApproved()
	{
		foreach (GameObject logo in PlayerLogos)
		{
			logo.GetComponent<Renderer>().enabled = false;
		}
		PlayerLogos[3].GetComponent<Renderer>().enabled = true;

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
		CellManager.Instance.NetworkCommunicator.CellOccupiedStatus("remove", Cell.CellId);
		Destroy(gameObject);
	}
}
