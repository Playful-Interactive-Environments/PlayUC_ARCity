using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ProjectManager : NetworkBehaviour
{
	public class ProjectTab
	{
		public int ID;
		public bool Proposed;
		public bool Approved;
	}
	public static ProjectManager Instance = null;
	public GameObject ChosenProject;
	public GameObject ProjectPrefab;
	public List<GameObject> Projects;
	public QuestManager Quests;
	public VoteManager VoteManager;
	public int CurrentID;
	public ProjectTab Pro1 = new ProjectTab();
	public ProjectTab Pro2 = new ProjectTab();
	public ProjectTab Pro3 = new ProjectTab();
	public SyncListInt ProjectIDs = new SyncListInt();

	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		Invoke("PopulateIds", .1f);
		Quests = QuestManager.Instance;
		VoteManager = VoteManager.Instance;
	}

	void PopulateIds()
	{
		if (isServer)
		{
			for (int i = 1; i <= Quests.CSVProjects.rowList.Count; i++)
			{
				ProjectIDs.Add(i);
			}
		}
		Pro1.ID = GenerateRandomProject();
		Pro2.ID = GenerateRandomProject();
		Pro3.ID = GenerateRandomProject();
	}

	void Update ()
	{
		if (VoteManager.Instance!=null)
		{
			VoteManager = VoteManager.Instance;
		}
	}

	public void BuildProject(Vector3 pos, int id)
	{
		GameObject gobj = Instantiate(ProjectPrefab, pos, Quaternion.identity) as GameObject;
		ChosenProject = gobj;
		Projects.Add(gobj);

		Project project = gobj.GetComponent<Project>();
		project.ProjectId = id;
		project.Title = Quests.GetTitle(id);
		project.Description = Quests.GetContent(id);
		project.Rating = Quests.GetRating(id);
		project.Social = Quests.GetSocial(id);
		project.Finance = Quests.GetFinance(id);
		project.Environment = Quests.GetEnvironment(id);
		project.Cost = Quests.GetCost(id);


		RoleManager.Instance.Budget += Quests.GetCost(id); ;
		RoleManager.Instance.Rating += Quests.GetRating(id); ;
		CellManager.Instance.NetworkCommunicator.SpawnObject(pos);
		ResetUI();
	}

	public void ResetUI()
	{
		UIManager.Instance.DebugText.text = "spawn";
		UIManager.Instance.VoteStatus.text = "";
		if (CurrentID == Pro1.ID)
		{
			Pro1.ID = GenerateRandomProject();
			Pro1.Approved = false;
			Pro1.Proposed = false;
			UIManager.Instance.BuildButton.interactable = false;
			UIManager.Instance.ProposeButton.interactable = true;
			UIManager.Instance.ProjectButton_1.image.color = Color.white;
		}
		if (CurrentID == Pro2.ID)
		{
			Pro2.ID = GenerateRandomProject();
			Pro2.Approved = false;
			Pro2.Proposed = false;
			UIManager.Instance.BuildButton.interactable = false;
			UIManager.Instance.ProposeButton.interactable = true;
			UIManager.Instance.ProjectButton_2.image.color = Color.white;
		}
		if (CurrentID == Pro3.ID)
		{
			Pro3.ID = GenerateRandomProject();
			Pro3.Approved = false;
			Pro3.Proposed = false;
			UIManager.Instance.BuildButton.interactable = false;
			UIManager.Instance.ProposeButton.interactable = true;
			UIManager.Instance.ProjectButton_3.image.color = Color.white;
		}
	}
	public void ProjectApproved(int num)
	{
		UIManager.Instance.VoteStatus.text = "Project Approved. You may now build it.";
		UIManager.Instance.BuildButton.interactable = true;
		if (num == Pro1.ID)
		{
			Pro1.Approved = true;
			UIManager.Instance.ProjectButton_1.image.color = Color.red;
		}

		if (num == Pro2.ID)
		{
			Pro2.Approved = true;
			UIManager.Instance.ProjectButton_2.image.color = Color.red;
		}

		if (num == Pro3.ID)
		{
			Pro3.Approved = true;
			UIManager.Instance.ProjectButton_3.image.color = Color.red;
		}
	}

	public void ProjectRejected(int num)
	{
		ResetUI();
		//UIManager.Instance.Invoke("EndVote", 5f);
		//UIManager.Instance.VoteStatus.text = "Project Rejected.";
	}

	public int GenerateRandomProject()
	{
		if (ProjectIDs.Count == 0)
			PopulateIds();
		int randomProject = Random.Range(0, ProjectIDs.Count);
		int returnId = ProjectIDs[randomProject];
		ProjectIDs.RemoveAt(randomProject);
		return returnId;
	}

	public void AddProject()
	{
		CurrentID = GenerateRandomProject();
	}
	
	public void GetProject(int buttonnum)
	{
		switch (buttonnum)
		{
			case 1:
				CurrentID = Pro1.ID;
				if (Pro1.Proposed) UIManager.Instance.ProposeButton.interactable = false;
				else UIManager.Instance.ProposeButton.interactable = true;

				if (Pro1.Approved)
					UIManager.Instance.BuildButton.interactable = true;
				else UIManager.Instance.BuildButton.interactable = false;
				break;
			case 2:
				CurrentID = Pro2.ID;
				if (Pro2.Proposed)
					UIManager.Instance.ProposeButton.interactable = false;
				else UIManager.Instance.ProposeButton.interactable = true;
				if (Pro2.Approved)
					UIManager.Instance.BuildButton.interactable = true;
				else UIManager.Instance.BuildButton.interactable = false;
				break;
			case 3:
				CurrentID = Pro3.ID;
				if (Pro3.Proposed)
					UIManager.Instance.ProposeButton.interactable = false;
				else UIManager.Instance.ProposeButton.interactable = true;
				if (Pro3.Approved)
					UIManager.Instance.BuildButton.interactable = true;
				else UIManager.Instance.BuildButton.interactable = false;
				break;
			default:
				break;
		}
	}
}
