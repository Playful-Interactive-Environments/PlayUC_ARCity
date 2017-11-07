using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class Project : NetworkBehaviour
{
    public GameObject[] FinanceRepr;
    public GameObject[] SocialRepr;
    public GameObject[] EnvironmentRepr;

    public GameObject RepresentationParent;
    public GameObject ProjectSign;

    public ParticleSystem ParticlesResult;
    public ParticleSystem ParticlesVoting;
    public ParticleSystem ParticlesStart;
    public TextMeshPro ProjectSignText;
    public TextMeshPro ProjectSignText2;

    //Vars set by project manager on server
    [SyncVar]
    public bool ProjectCreated;
    [SyncVar]
    public string Title;
    [SyncVar]
    public string Description;
    [SyncVar]
    public int Influence;
    [SyncVar]
    public int Budget;
    [SyncVar]
    public int Social;
    [SyncVar]
    public int Environment;
    [SyncVar]
    public int Finance;
    [SyncVar]
    public string ProjectOwner;
    [SyncVar]
    public float Cooldown;
    [SyncVar]
    public string MiniGame;
    [SyncVar]
    public int ID_Spawn;
    [SyncVar]
    public int Id_CSV;
    [SyncVar]
    public int RepresentationId;
    [SyncVar]
    public int Choice1;
    [SyncVar]
    public int Choice2;
    [SyncVar]
    public bool VoteFinished;
    [SyncVar]
    private Vector3 reprRot;
    [SyncVar]
    private Vector3 projectPos;
    [SyncVar]
    private Vector3 CellPos;
    [SyncVar]
    public int CellId;
    [SyncVar]
    public int VotesNeeded;
    public bool Approved;
    private Material placementMat;
    private GameObject buildingRep;
    private GameObject transparentRep;
    private Vector3 SavedPos;
    private Vector3 SavedScale;
    private Vector3 SavedRotation;

    void Start()
    {
        transform.name = "Project" + ID_Spawn;
        transform.parent = LocalManager.Instance.ImageTarget.transform;
        ProjectManager.Instance.LockButton(Id_CSV);
        CreateRepresentation();
        ProjectManager.Instance.Projects.Add(this);
        ProjectManager.Instance.SelectedProject = this;
        VotesNeeded = GameManager.Instance.ClientsConnected;
        if (isServer)
        {
            SaveStateManager.Instance.LogEvent("PLAYER " + ProjectOwner + " PROJECT " + Title);
            SaveStateManager.Instance.AddProjectAction(ProjectOwner, "Propose");
        }
    }

    void CheckParent()
    {
        if (transform.parent != LocalManager.Instance.ImageTarget.transform)
        {
            transform.parent = LocalManager.Instance.ImageTarget.transform;
            transform.localScale = SavedScale;
            transform.localPosition = SavedPos;
            transform.localRotation = Quaternion.Euler(SavedRotation);
        }
    }

    void Update()
    {
        if (isServer && !VoteFinished && Choice1 + Choice2 >= VotesNeeded)
        {
            if (Choice1 > Choice2)
            {
                TransferValues();
                LocalManager.Instance.NetworkCommunicator.Vote(Vars.ResultChoice1, ProjectOwner, ID_Spawn);
                VoteFinished = true;
            }
            if (Choice2 > Choice1)
            {
                LocalManager.Instance.NetworkCommunicator.Vote(Vars.ResultChoice2, ProjectOwner, ID_Spawn);
                VoteFinished = true;
            }
            if (Choice1 == Choice2)
            {
                int random = Utilities.RandomInt(0, 2);
                if (random == 0)
                    Choice1 += 1;
                if (random == 1)
                    Choice2 += 1;
            }
            LocalManager.Instance.NetworkCommunicator.SetGlobalState(Vars.DiscussionEnd);
        }
        if (VotesNeeded > GameManager.Instance.ClientsConnected)
            VotesNeeded = GameManager.Instance.ClientsConnected;
        transform.position = projectPos;
    }

    public void SetProject(string owner, int idcsv, int idspawn, string title, string description,
        int influence, int social, int finance, int environment, int budget, float cooldown,
        string minigame, int cellid, int reprID, Vector3 pos, Vector3 rot)
    {
        ProjectOwner = owner;
        Id_CSV = idcsv;
        ID_Spawn = idspawn;
        Title = title;
        Description = description;
        Influence = influence;
        Social = social;
        Finance = finance;
        Environment = environment;
        Budget = budget;
        Cooldown = cooldown;
        MiniGame = minigame;
        CellId = cellid;
        RepresentationId = reprID;
        reprRot = rot;
        projectPos = pos;

        LocalManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, Vars.MainValue1, Budget);
        LocalManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "MoneySpent", Mathf.Abs(Budget));
        LocalManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, Vars.MainValue2, Influence);
        //UIManager.Instance.GameDebugText.text += "\nProposed for:" + Budget;
        UIManager.Instance.CreateText(Color.red, Budget.ToString(), 50, .5f, 2f, new Vector2(UIManager.Instance.BudgetTextPos.x, UIManager.Instance.BudgetTextPos.y), new Vector2(UIManager.Instance.BudgetTextPos.x, 0));
    }

    public void ChangeLanguage(string title, string content)
    {
        Title = title;
        Description = content;
    }

    public void CreateRepresentation()
    {
        //create 3d representation
        switch (ProjectOwner)
        {
            case Vars.Player1:
                buildingRep = Instantiate(FinanceRepr[RepresentationId], transform.position, Quaternion.identity);
                transparentRep = Instantiate(FinanceRepr[RepresentationId], transform.position, Quaternion.identity);
                break;
            case Vars.Player2:
                buildingRep = Instantiate(SocialRepr[RepresentationId], transform.position, Quaternion.identity);
                transparentRep = Instantiate(SocialRepr[RepresentationId], transform.position, Quaternion.identity);
                break;
            case Vars.Player3:
                buildingRep = Instantiate(EnvironmentRepr[RepresentationId], transform.position, Quaternion.identity);
                transparentRep = Instantiate(EnvironmentRepr[RepresentationId], transform.position, Quaternion.identity);
                break;
        }
        buildingRep.transform.parent = RepresentationParent.transform;
        buildingRep.transform.localScale = new Vector3(1f, 1f, 1f);
        buildingRep.transform.localEulerAngles = reprRot;
        transparentRep.transform.parent = RepresentationParent.transform;
        transparentRep.transform.localScale = new Vector3(1f, 1f, 1f);
        transparentRep.transform.localEulerAngles = reprRot;
        placementMat = GameManager.Instance.placementMat;
        ProjectSignText.text = Title;
        ProjectSignText2.text = Title;
        ProjectSign.transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z + 10);
        ProjectSign.transform.localEulerAngles = new Vector3(reprRot.x, reprRot.y + 100f, reprRot.z);
        SavedScale = transform.localScale;
        SavedPos = transform.localPosition;
        SavedRotation = transform.rotation.eulerAngles;
        InvokeRepeating("CheckParent", 1f, 3f);
        foreach (Renderer child in transparentRep.GetComponentsInChildren<Renderer>())
        {
            child.material = placementMat;
        }
        RepresentationParent.SetActive(true);
        if (!VoteFinished)
            TransparentOn();
        if (VoteFinished)
            TransparentOff();
    }

    public void TransparentOn()
    {
        transparentRep.SetActive(true);
        buildingRep.SetActive(false);
        ParticlesStart.Play();
        ParticlesVoting.Play();
    }

    public void TransparentOff()
    {
        transparentRep.SetActive(false);
        buildingRep.SetActive(true);
        ParticlesVoting.Stop();
    }

    public void ShowProjectCanvas()
    {
        ProjectManager.Instance.SelectedProject = GetComponent<Project>();
        Invoke("ShowProject", .1f);
    }

    public void ShowProject()
    {
        ProjectManager.Instance.SelectedProject = this;
        UIManager.Instance.ShowProjectDisplay();
    }

    public void TransferValues()
    {
        CellManager.Instance.UpdateFinance(CellId, Finance);
        CellManager.Instance.UpdateSocial(CellId, Social);
        CellManager.Instance.UpdateEnvironment(CellId, Environment);
    }

    public void TriggerApproved()
    {
        var mainModule = ParticlesResult.main;
        switch (ProjectOwner)
        {
            case Vars.Player1:
                mainModule.startColor = new Color(0, 113f / 255, 188f / 255, 255f / 255);
                break;
            case Vars.Player2:
                mainModule.startColor = new Color(251f / 255, 176f / 255, 59f / 255, 255f / 255);
                break;
            case Vars.Player3:
                mainModule.startColor = new Color(166f / 255, 199f / 255, 56f / 255, 255f / 255);
                break;
        }
        DiscussionManager.Instance.ChangeInfoScreen("Project Approved!");
        //UIManager.Instance.GameDebugText.text += "\n" + VotesNeeded + " voters: " + "Yes " + Choice1 + " No " + Choice2;
        if (isServer)
            LocalManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Successful", 0);
        TransparentOff();
        ParticlesResult.Play();
        CellGrid.Instance.GetCellInterface(CellId).HighlightCell(Finance, Social, Environment);
    }

    public void TriggerRejected()
    {
        var mainModule = ParticlesResult.main;
        mainModule.startColor = Color.red;
        ParticlesResult.Play();
        TransparentOff();
        DiscussionManager.Instance.ChangeInfoScreen("Project Rejected!");

        if (isServer)
        {
            LocalManager.Instance.NetworkCommunicator.UpdateData(ProjectOwner, "Failed", 0);
            Invoke("RemoveProject", 10f);
            Invoke("DestroyObject", 11);
        }
    }

    public void TriggerCanceled()
    {
        if (isServer)
        {
            LocalManager.Instance.NetworkCommunicator.SetGlobalState(Vars.DiscussionEnd);
        }
        Invoke("RemoveProject", 0f);
        Invoke("DestroyObject", .5f);
    }

    public void RemoveProject()
    {
        ProjectManager.Instance.Remove(ID_Spawn);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}