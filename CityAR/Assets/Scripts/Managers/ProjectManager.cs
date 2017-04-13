using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Networking;
using UnityEngine.UI;
[NetworkSettings(channel = 2, sendInterval = 0.1f)]
public class ProjectManager : NetworkBehaviour
{

    public static ProjectManager Instance = null;
    public GameObject ProjectPrefab;
    public ProjectDummy CurrentDummy;
    public CSVProjects CSVProjects;
    public QuestManager Quests;
    public Project SelectedProject;
    [SyncVar] public int SelectedProjectId;
    public List<Project> Projects;
    public List<GameObject> ProjectButtons = new List<GameObject>();
    public GameObject ProjectTemplate;
    public GridLayoutGroup GridGroup;
    private int CurrentProjectId = 0;
    private bool canSpawn = true;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        CSVProjects = CSVProjects.Instance;
    }

    void Start()
    {
        EventDispatcher.StartListening(Vars.LocalClientDisconnect, NetworkDisconnect);
    }

    void Update()
    {
        if (isServer && SelectedProject != null)
            SelectedProjectId = SelectedProject.ID_Spawn;
    }
    void NetworkDisconnect()
    {
        ProjectButtons.Clear();
    }

    public void UnlockProject(int id)
    {
        GridGroup = GameObject.Find("ProjectLayout").GetComponent<GridLayoutGroup>();
        ProjectTemplate = GameObject.Find("ProjectTemplate");
        GameObject projectButton = Instantiate(ProjectTemplate, transform.position, Quaternion.identity);
        projectButton.transform.SetParent(GridGroup.transform);
        projectButton.transform.localScale = new Vector3(1, 1, 1);
        projectButton.GetComponent<ProjectButton>().SetupProjectButton(id);
        ProjectButtons.Add(projectButton);
        UIManager.Instance.ProjectButton.image.color = Color.red;
    }
    //called only on server
    public void SpawnProject(int cellid, Vector3 pos, Vector3 rot, string owner, int id)
    {
        if (canSpawn)
        {
            GameObject gobj = Instantiate(ProjectPrefab, pos, Quaternion.identity);
            Project project = gobj.GetComponent<Project>();
            project.SetProject(owner, id, CurrentProjectId, GetCSVTitle(id), GetCSVContent(id), GetInfluenceInt(id),
                GetSocialInt(id), GetFinanceInt(id), GetEnvironmentInt(id), GetBudgetInt(id), GetCooldown(id), GetMiniGame(id), cellid, GetRepresentation(id), pos, rot);
            NetworkServer.Spawn(gobj);
            CurrentProjectId++;

            //can not spawn until current project is accepted/reject/canceled
            canSpawn = false;
        }
    }

    public void LockButton(int id)
    {
        foreach (GameObject button in ProjectButtons)
        {
            if (button.GetComponent<ProjectButton>().ProjectCSVId == id)
            {
                button.GetComponent<ProjectButton>().LockProject();
            }
        }
    }

    public void UnlockButton(int id)
    {
        foreach (GameObject button in ProjectButtons)
        {
            if (button.GetComponent<ProjectButton>().ProjectCSVId == id)
            {
                button.GetComponent<ProjectButton>().UnlockProject();
            }
        }
    }

    public Project FindProject(int projectnum)
    {
        if(isServer)
            canSpawn = true;
        foreach (Project project in FindObjectsOfType<Project>())
        {
            if (project.ID_Spawn == projectnum)
            {
                return project;
            }
        }
        return null;
    }

    public void Remove(int id)
    {
        Projects.RemoveAll(x => x.ID_Spawn == id);
    }

    public void ProjectApproved(int num)
    {
        FindProject(num).TriggerApproved();
    }

    public void ProjectRejected(int num)
    {
        FindProject(num).TriggerRejected();
    }
    public void ProjectCanceled(int num)
    {
        FindProject(num).TriggerCanceled();
    }

    public void LoadLanguage()
    {
        foreach (GameObject button in ProjectButtons)
        {
            button.GetComponent<ProjectButton>().SetupProjectButton(button.GetComponent<ProjectButton>().ProjectCSVId);
        }
        foreach (Project project in Projects)
        {
            project.ChangeLanguage(GetCSVTitle(project.Id_CSV), GetCSVContent(project.Id_CSV));
        }
    }
    #region CSV Handlers

    public string GetCSVTitle(int num)
    {
        return CSVProjects.Find_ID(num).title;
    }
    public string GetCSVContent(int num)
    {
        return CSVProjects.Find_ID(num).content;
    }

    public int GetSocialInt(int num)
    {
        return ConvertToInt(CSVProjects.Find_ID(num).social);
    }
    public string GetCSVSocialString(int num)
    {
        return CSVProjects.Find_ID(num).social;
    }
    public int GetFinanceInt(int num)
    {
        return ConvertToInt(CSVProjects.Find_ID(num).finance);
    }
    public string GetCSVFinanceString(int num)
    {
        return CSVProjects.Find_ID(num).finance;
    }
    public int GetInfluenceInt(int num)
    {
        return ConvertToInt(CSVProjects.Find_ID(num).influence);
    }
    public string GetCSVInfluenceString(int num)
    {
        return CSVProjects.Find_ID(num).influence;
    }
    public int GetEnvironmentInt(int num)
    {
        return ConvertToInt(CSVProjects.Find_ID(num).environment);
    }
    public string GetCSVEnvironmentString(int num)
    {
        return CSVProjects.Find_ID(num).environment;
    }
    public int GetBudgetInt(int num)
    {
        return ConvertToInt(CSVProjects.Find_ID(num).cost);
    }
    public string GetCSVBudgetString(int num)
    {
        return CSVProjects.Find_ID(num).cost;
    }
    public float GetCooldown(int num)
    {
        return ConvertToFloat(CSVProjects.Find_ID(num).cooldown);
    }
    public string GetMiniGame(int num)
    {
        return CSVProjects.Find_ID(num).minigame;
    }
    private int ConvertToInt(string input)
    {
        int parsedInt = 0;
        int.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
        return parsedInt;
    }
    public int GetRepresentation(int num)
    {
        return ConvertToInt(CSVProjects.Find_ID(num).reprid);
    }
    private float ConvertToFloat(string input)
    {
        float parsedInt = 0;
        float.TryParse(input, NumberStyles.AllowLeadingSign, null, out parsedInt);
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