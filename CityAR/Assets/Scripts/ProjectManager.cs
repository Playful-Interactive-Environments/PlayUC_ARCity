using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProjectManager : NetworkBehaviour
{

	public static ProjectManager Instance = null;
	public GameObject ProjectPrefab;
	public QuestManager Quests;
	public int SelectedProjectId;
	public Project SelectedProject;
	public List<Project> Projects;
	public SyncListInt ProjectPool = new SyncListInt();
	public SyncListInt EnvironmentProjects = new SyncListInt();
	public SyncListInt SocialProjects = new SyncListInt();
	public SyncListInt FinanceProjects = new SyncListInt();

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
	}

	public void CreateRandomProject()
	{
		CellManager.Instance.NetworkCommunicator.ActivateProject("CreateProject", new Vector3(0,0,0), LocalManager.Instance.RoleType, GenerateRandomProject());
	}

	void PopulateIds()
	{
		if (isServer)
		{
			for (int i = 1; i <= Quests.CSVProjects.rowList.Count; i++)
			{
				ProjectPool.Add(i);
			}
		}
	}

	//called only on server
	public void InstantiateProject(string owner, int id)
	{
		GameObject gobj = Instantiate(ProjectPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		Project project = gobj.GetComponent<Project>();
		project.ProjectOwner = owner;
		project.ProjectId = id;
		project.Title = Quests.GetTitle(id);
		project.Description = Quests.GetContent(id);
		project.Rating = Quests.GetRatingInt(id);
		project.Social = Quests.GetSocialInt(id);
		project.Finance = Quests.GetFinanceInt(id);
		project.Environment = Quests.GetEnvironmentInt(id);
		project.Budget = Quests.GetBudgetInt(id);
		NetworkServer.Spawn(gobj);
		SaveProject(owner, id, project);
	}

	public void PlaceProject(Vector3 pos, string owner, int id)
	{
		Project project = FindProject(id);
		project.SetCell(pos);
		project.transform.position = pos;
		project.RepresentationCreated = true;
		RemoveProject(owner, id);
	}

	public int GenerateRandomProject()
	{
		if (ProjectPool.Count == 0)
			PopulateIds();
		int randomProject = Random.Range(0, ProjectPool.Count);
		int returnId = ProjectPool[randomProject];
		ProjectPool.RemoveAt(randomProject);
		return returnId;
	}

	public Project FindProject(int projectnum)
	{
		if (isClient && !isServer)
		{
			Projects.Clear();
			if (isClient && !isServer)
			{
				foreach (Project p in FindObjectsOfType<Project>())
				{
					Projects.Add(p);
				}
			}
		}

		foreach (Project p in Projects)
		{
			if (p.ProjectId == projectnum)
			{
				return p;
			}
		}
		return null;
	}


	public void ProjectApproved(int num)
	{
		FindProject(num).InitiateProject();
	}

	public void ProjectRejected(int num)
	{
		Project p = FindProject(num);
		Projects.Remove(p);
		Destroy(p.gameObject);
	}

	public void SaveProject(string type, int id, Project project)
	{
		Projects.Add(project);
		switch (type)
		{
			case "Environment":
				EnvironmentProjects.Add(id);
				break;
			case "Social":
				SocialProjects.Add(id);
				break;
			case "Finance":
				FinanceProjects.Add(id);
				break;
		}
	}

	public void RemoveProject(string type, int id)
	{
		switch (type)
		{
			case "Environment":
				EnvironmentProjects.Remove(id);
				break;
			case "Social":
				SocialProjects.Remove(id);
				break;
			case "Finance":
				FinanceProjects.Remove(id);
				break;
		}
	}

}
