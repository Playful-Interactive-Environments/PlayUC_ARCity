using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProjectManager : NetworkBehaviour
{

	public static ProjectManager Instance = null;
	public GameObject ProjectPrefab;
	public List<Project> Projects;
	public QuestManager Quests;
	public int SelectedProjectId;
	public Project SelectedProject;
	public int CurrentAvailableProject;
	public SyncListInt ProjectIDs = new SyncListInt();
	//ProjectTemplate
	public GameObject ProjectTemplate;
	public GridLayoutGroup GridGroup;
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

	}


	void Update()
	{

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
//TODO: handle if client restarted
		ResetUI();
		SelectedProjectId = GlobalManager.Instance.GetCurrentProject(LocalManager.Instance.RoleType);
	}


	//called only on server
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

	public void ResetUI()
	{
		CurrentAvailableProject = GenerateRandomProject();
		UIManager.Instance.SetProjectButton(true);
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
		GridGroup = GameObject.Find("ProjectLayout").GetComponent<GridLayoutGroup>();
		ProjectTemplate = GameObject.Find("ProjectTemplate");
		GameObject gameobj = Instantiate(ProjectTemplate, transform.position, Quaternion.identity) as GameObject;
		gameobj.transform.parent = GridGroup.transform;
		gameobj.transform.localScale = new Vector3(2, 2, 2);
	}
	public void GetProject()
	{
		SelectedProjectId = CurrentAvailableProject;
		GlobalManager.Instance.SetCurrentProject(LocalManager.Instance.RoleType, CurrentAvailableProject);
		AddProject();
	}
	public Project FindProject(int projectnum)
	{
		if (isClient && !isServer)
		{
			UpdateLocalProjectList();
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
	void UpdateLocalProjectList()
	{
		Projects.Clear();
		if (isClient && !isServer)
		{
			Projects.Add(FindObjectOfType<Project>());
		}
	}
}
