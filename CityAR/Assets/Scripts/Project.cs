﻿using UnityEngine;
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
	[SyncVar]
	private Vector3 CellPos;
	private Renderer[] allRenderers;
	public Material transparentStatic;
	private Material buildingMat;
	private GameObject representation;

	void Start ()
	{
		transform.name = "Project" + ID_Spawn;
		transform.parent = CellManager.Instance.ImageTarget.transform;
		ProjectManager.Instance.ActivateButtonCooldown(Id_CSV);
		CreateRepresentation();
		ShowLogo();
		if (isServer)
		{
			SaveStateManager.Instance.LogEvent("PLAYER " + ProjectOwner + " PROJECT " + Title);
			//Choice1 += 1;
		}
        ProjectManager.Instance.Projects.Add(this);

        ProjectManager.Instance.SelectedProject = this;
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
	}

	void OnEnable()
	{
		ShowLogo();
	}

	void Update()
	{
		if (isServer && !VoteFinished && Choice1 + Choice2 >= 3)
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
			CellManager.Instance.NetworkCommunicator.SetPlayerState(ProjectOwner, "DiscussionEnd");

		}
		transform.position = projectPos;
	}

	void NetworkDisconnect()
	{
		//Destroy(gameObject);
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
		representation.transform.localScale = new Vector3(1f, 1f, 1f);
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
		Invoke("ShowProject", .1f);
	}

	public void ShowProject()
	{
		ProjectManager.Instance.SelectedProject = this;
		UIManager.Instance.ShowProjectInfo();
	}

	public void TransferValues()
	{
		CellManager.Instance.UpdateFinance(CellLogic.CellId, Finance);
		CellManager.Instance.UpdateSocial(CellLogic.CellId, Social);
		CellManager.Instance.UpdateEnvironment(CellLogic.CellId, Environment);
	}

	public void ShowApproved()
	{
		var mainModule = ParticlesApproval.main;
		switch (ProjectOwner)
		{
			case "Finance":
				mainModule.startColor = Color.blue;

				break;
			case "Social":
				mainModule.startColor = new Color(1f, 0.64f, 0f);

				break;
			case "Environment":
				mainModule.startColor = Color.green;
				break;
		}
		ParticlesApproval.Play();
		ShowLogo();
		if (LevelManager.Instance.RoleType == ProjectOwner)
		{
			CellManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Budget", Budget);
			CellManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Influence", Influence);
		}
	}

	public void ShowRejected()
	{
		var mainModule = ParticlesApproval.main;
		mainModule.startColor = Color.red;

		ParticlesApproval.Play();
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