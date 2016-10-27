using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ProjectManager : NetworkBehaviour
{

	public static ProjectManager Instance = null;
	public GameObject ChosenProject;
	public GameObject ProjectPrefab;
	public List<GameObject> Projects;
	public QuestManager Quests;
	public VoteManager VoteManager;
	public int CurrentID;
	public int Pro1_ID;
	public int Pro2_ID;
	public int Pro3_ID;
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
			for (int i = 0; i <= Quests.CSVProjects.rowList.Count; i++)
			{
				ProjectIDs.Add(i);
			}
		}
		Pro1_ID = GenerateRandomProject();
		Pro2_ID = GenerateRandomProject();
		Pro3_ID = GenerateRandomProject();
	}
	void Update ()
	{
		if (VoteManager.Instance!=null)
		{
			VoteManager = VoteManager.Instance;
		}
	}

	public void ProjectApproved(int num)
	{
		GameObject gobj = Instantiate(ProjectPrefab, transform.position, Quaternion.identity) as GameObject;
		Project project = gobj.GetComponent<Project>();
		project.Radius = Quests.GetRadius(num);
		project.Social = Quests.GetSocial(num);
		project.Finance = Quests.GetFinance(num);
		project.Environment = Quests.GetEnvironment(num);
		gobj.SetActive(false);
		ChosenProject = gobj;
		Projects.Add(gobj);
		UIManager.Instance.Invoke("EndVote", 5f);

	    UIManager.Instance.VoteStatus.text = "Project Approved. Game will resume in 5 sec.";
		if(num == Pro1_ID)
			Add_Project1();
		if(num == Pro2_ID)
			Add_Project2();
		if(num == Pro3_ID)
			Add_Project3();
	}

	public void ProjectRejected(int num)
	{
		UIManager.Instance.Invoke("EndVote", 5f);
	    UIManager.Instance.VoteStatus.text = "Project Rejected. Game will resume in 5 sec.";
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

	public void Add_Project1()
	{
		UIManager.Instance.ProjectButton_1.interactable = true;
		Pro1_ID = GenerateRandomProject();
	}

	public void Add_Project2()
	{
		UIManager.Instance.ProjectButton_2.interactable = true;
		Pro2_ID = GenerateRandomProject();
	}

	public void Add_Project3()
	{
		UIManager.Instance.ProjectButton_3.interactable = true;
		Pro3_ID = GenerateRandomProject();
	}


	public void GetProject(int buttonnum)
	{
		switch (buttonnum)
		{
			case 1:
				CurrentID = Pro1_ID;
				break;
			case 2:
				CurrentID = Pro2_ID;
				break;
			case 3:
				CurrentID = Pro3_ID;
				break;
			default:
				break;
		}
	}
}
