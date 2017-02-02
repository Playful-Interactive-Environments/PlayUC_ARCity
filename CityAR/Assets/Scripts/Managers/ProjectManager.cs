using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ProjectManager : NetworkBehaviour
{

	public static ProjectManager Instance = null;
	public GameObject ProjectPrefab;
	public CSVProjects CSVProjects;
	public QuestManager Quests;
	public int SelectedProjectId;
	public Project SelectedProject;
	public List<Project> Projects;
	public SyncListInt ProjectPool = new SyncListInt();
	public SyncListInt EnvironmentProjects = new SyncListInt();
	public SyncListInt SocialProjects = new SyncListInt();
	public SyncListInt FinanceProjects = new SyncListInt();
	public GameObject ProjectTemplate;
	public GridLayoutGroup GridGroup;

	private int CurrentProjectId = 0;
	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		CSVProjects = CSVProjects.Instance;
	}

	void Start()
	{
	}

	public void UnlockProject(int id)
	{
		GridGroup = GameObject.Find("ProjectLayout").GetComponent<GridLayoutGroup>();
		ProjectTemplate = GameObject.Find("ProjectTemplate");
		GameObject projectButton = Instantiate(ProjectTemplate, transform.position, Quaternion.identity);
		projectButton.transform.parent = GridGroup.transform;
		projectButton.transform.localScale = new Vector3(1, 1, 1);
		projectButton.GetComponent<ProjectButton>().SetupProjectButton(id);
		UIManager.Instance.ProjectButton.image.color = Color.red;
	}

	//called only on server
	public void SpawnProject(int cellid, string owner, int id)
	{
		GameObject gobj = Instantiate(ProjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);


		Project project = gobj.GetComponent<Project>();
		project.ProjectOwner = owner;
		project.Id_CSV = id;
		project.ID_Spawn = CurrentProjectId;
		project.Title = GetTitle(id);
		project.Description = GetContent(id);
		project.Influence = GetInfluenceInt(id);
		project.Social = GetSocialInt(id);
		project.Finance = GetFinanceInt(id);
		project.Environment = GetEnvironmentInt(id);
		project.Budget = GetBudgetInt(id);
		project.SetCell(cellid);
		project.transform.position = CellGrid.Instance.GetCell(cellid).transform.position;
		project.RepresentationCreated = true;
		NetworkServer.Spawn(gobj);
		SaveProject(owner, CurrentProjectId, project);
		CurrentProjectId++;
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
			if (p.ID_Spawn == projectnum)
			{
				return p;
			}
		}
		return null;
	}

	public void ProjectApproved(int num)
	{
		FindProject(num).ShowApproved();
	}

	public void ProjectRejected(int num)
	{
		FindProject(num).ShowRejected();
		if (isServer)
			Projects.Remove(FindProject(num));
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
	#region CSV Handlers

	public string GetTitle(int num)
	{
		return CSVProjects.Find_ID(num).title;
	}
	public string GetContent(int num)
	{
		return CSVProjects.Find_ID(num).content;
	}

	public int GetSocialInt(int num)
	{
		return ConvertString(CSVProjects.Find_ID(num).social);
	}
	public string GetSocialString(int num)
	{
		return CSVProjects.Find_ID(num).social;
	}
	public int GetFinanceInt(int num)
	{
		return ConvertString(CSVProjects.Find_ID(num).finance);
	}
	public string GetFinanceString(int num)
	{
		return CSVProjects.Find_ID(num).finance;
	}
	public int GetInfluenceInt(int num)
	{
		return ConvertString(CSVProjects.Find_ID(num).influence);
	}
	public string GetInfluenceString(int num)
	{
		return CSVProjects.Find_ID(num).influence;
	}
	public int GetEnvironmentInt(int num)
	{
		return ConvertString(CSVProjects.Find_ID(num).environment);
	}
	public string GetEnvironmentString(int num)
	{
		return CSVProjects.Find_ID(num).environment;
	}
	public int GetBudgetInt(int num)
	{
		return ConvertString(CSVProjects.Find_ID(num).cost);
	}
	public string GetBudgetString(int num)
	{
		return CSVProjects.Find_ID(num).cost;
	}
	private int ConvertString(string input)
	{
		int parsedInt = 0;
		int.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
		return parsedInt;

	}
	string FormatSign(int num)
	{
		string text = "";
		if (num > 0)
			text = "+" + num;
		if (num < 0)
			text = "" + num;
		if (num == 0)
			text = "" + num;
		return text;
	}
	#endregion
}
