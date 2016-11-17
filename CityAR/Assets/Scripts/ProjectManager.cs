using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ProjectManager : NetworkBehaviour
{

	public static ProjectManager Instance = null;
	public GameObject ProjectPrefab;
	public List<Project> Projects;
	public QuestManager Quests;
	public VoteManager VoteManager;
	public int SelectedProjectId;
	public Project SelectedProject;
	public int CurrentAvailableProject;
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
		CurrentAvailableProject = GenerateRandomProject();
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

		SelectedProject = project;
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
		if (SelectedProjectId == CurrentAvailableProject)
		{
			CurrentAvailableProject = GenerateRandomProject();
			UIManager.Instance.SetProjectButton(true);
		}
	}

	public void ProjectApproved(int num)
	{
		FindProject(num).InitiateProject();
		ResetUI();
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

	public void AddNewProject()
	{
		CurrentAvailableProject = GenerateRandomProject();
	}
	
	public void GetProject()
	{
		SelectedProjectId = CurrentAvailableProject;
		GlobalManager.Instance.SetCurrentProject(LocalManager.Instance.RoleType, SelectedProjectId);

	}
}
