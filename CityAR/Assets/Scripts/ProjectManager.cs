using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ProjectManager : NetworkBehaviour
{
	public class ProjectTab
	{
		public int ID;
	}
	public static ProjectManager Instance = null;
	public GameObject ProjectPrefab;
	public List<Project> Projects;
	public QuestManager Quests;
	public VoteManager VoteManager;
	public int CurrentID;
	public Project CurrentProject;
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

	public void BuildProject(Vector3 pos, string owner, int id)
	{
		GameObject gobj = Instantiate(ProjectPrefab, pos, Quaternion.identity) as GameObject;

		Project project = gobj.GetComponent<Project>();
		Projects.Add(project);
		project.ProjectOwner = owner;
		project.ProjectId = id;
		project.Title = Quests.GetTitle(id);
		project.Description = Quests.GetContent(id);
		project.Rating = Quests.GetRating(id);
		project.Social = Quests.GetSocial(id);
		project.Finance = Quests.GetFinance(id);
		project.Environment = Quests.GetEnvironment(id);
		project.Cost = Quests.GetCost(id);
		project.SetCell(pos);
		project.transform.position = pos;

		CurrentProject = project;
		NetworkServer.Spawn(gobj);
	}

	public Project FindProject(int projectnum)
	{
		foreach (Project p in Projects)
		{
			if (p.ProjectId == projectnum)
			{
				return p;
			}
		}
		return null;
	}

	public void ResetUI()
	{
		if (CurrentID == Pro1.ID)
		{
			Pro1.ID = GenerateRandomProject();
			//UIManager.Instance.ProjectButton_1.image.color = Color.white;
			UIManager.Instance.ProjectButtonState(1, true);
		}
		if (CurrentID == Pro2.ID)
		{
			Pro2.ID = GenerateRandomProject();
			UIManager.Instance.ProjectButtonState(2, true);

			//UIManager.Instance.ProjectButton_2.image.color = Color.white;
		}
		if (CurrentID == Pro3.ID)
		{
			Pro3.ID = GenerateRandomProject();
			UIManager.Instance.ProjectButtonState(3, true);

			//UIManager.Instance.ProjectButton_3.image.color = Color.white;
		}
	}
	public void ProjectApproved(int num)
	{
		UIManager.Instance.VoteStatus.text = "Project Approved. You may now build it.";
		ResetUI();
		/*
		if (num == Pro1.ID)
		{
			UIManager.Instance.ProjectButton_1.image.color = Color.red;
		}

		if (num == Pro2.ID)
		{
			UIManager.Instance.ProjectButton_2.image.color = Color.red;
		}

		if (num == Pro3.ID)
		{
			UIManager.Instance.ProjectButton_3.image.color = Color.red;
		}*/

	}

	public void ProjectRejected(int num)
	{
		Project p = FindProject(num);
		Projects.Remove(p);
		Destroy(p.gameObject);
		ResetUI();
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
				break;
			case 2:
				CurrentID = Pro2.ID;
				break;
			case 3:
				CurrentID = Pro3.ID;
				break;
			default:
				break;
		}
	}
}
