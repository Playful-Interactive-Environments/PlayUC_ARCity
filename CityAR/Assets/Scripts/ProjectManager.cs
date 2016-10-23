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
            Pro1_ID = GenerateRandomProject();
            Pro2_ID = GenerateRandomProject();
            Pro3_ID = GenerateRandomProject();
        }
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
			"\n Voted Yes: " + VoteManager.Votes[num].Choice1 +
			"\n Voted No: " + VoteManager.Votes[num].Choice2;
	}

	public void ProjectRejected(int num)
	{
		if (ProjectProposer)
		{
			ProjectProposer = false;
			UIManager.Instance.Invoke("EndVoteButton", 5f);
		}
		UIManager.Instance.VoteStatus.text = "Project Rejected. Game will resume in 5 sec." +
			"\n Voted Yes: " + VoteManager.Votes[num].Choice1 +
			"\n Voted No: " + VoteManager.Votes[num].Choice2;
	}

	public int GenerateRandomProject()
	{
        if (ProjectIDs.Count == 0)
            PopulateIds();
        int randomProject = Random.Range(0, ProjectIDs.Count);
        int returnId = ProjectIDs[randomProject];
        Debug.Log(randomProject + " " + returnId);
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
