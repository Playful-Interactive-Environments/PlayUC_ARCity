using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ProjectManager : NetworkBehaviour
{

	public static ProjectManager Instance = null;
	public bool ProjectProposer;
	public GameObject ChosenProject;
	public GameObject ProjectPrefab;
	public List<GameObject> Projects;
	public QuestManager Quests;
	public VoteManager VoteManager;
	public int CurrentID;
	public int Pro1_ID;
	public int Pro2_ID;
	public int Pro3_ID;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		Quests = QuestManager.Instance;
		VoteManager = VoteManager.Instance;
		Pro1_ID = GenerateRandomProject();
		Pro2_ID = GenerateRandomProject();
		Pro3_ID = GenerateRandomProject();
	}
	
	void Update ()
	{
		
	}

	public void ProjectApproved(int num)
	{
		if (ProjectProposer)
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
			UIManager.Instance.Invoke("EndVoteButton", 5f);
			ProjectProposer = false;
		}
		UIManager.Instance.VoteStatus.text = "Project Approved. Game will resume in 5 sec." +
			"\n Voted Yes: " + VoteManager.Choice1_Votes +
			"\n Voted No: " + VoteManager.Choice2_Votes;

	}

	public void ProjectRejected(int num)
	{
		if (ProjectProposer)
		{
			ProjectProposer = false;
			UIManager.Instance.Invoke("EndVoteButton", 5f);
		}
		UIManager.Instance.VoteStatus.text = "Project Rejected. Game will resume in 5 sec." +
			"\n Voted Yes: " + VoteManager.Choice1_Votes +
			"\n Voted No: " + VoteManager.Choice2_Votes;
	}

	public int GenerateRandomProject()
	{
		return (int)Random.Range(1, Quests.CSVProjects.GetRowList().Count + 1);
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
