using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionManager : AManager<DiscussionManager>
{

    public Slider FinanceSlider;
    public Slider SocialSlider;
    public Slider EnvironmentSlider;
    public GameObject BlockInteraction;

    public TextMeshProUGUI FinanceText;
    public TextMeshProUGUI EnvironmentText;
    public TextMeshProUGUI SocialText;
    public TextMeshProUGUI InfluenceText;
    public TextMeshProUGUI CostText;

    public GameObject Votes;
    public GameObject YesVotesIcon;
    public GameObject NoVotesIcon;
    public TextMeshProUGUI VotersText;
    public TextMeshProUGUI YesVotesText;
    public TextMeshProUGUI NoVotesText;

    public Button ApproveButton;
    public Button DenyButton;

    public string Proposer;
    public int ProjectId;
    public int Environment;
    public int Social;
    public int Finance;
    public int Budget;
    public int Influence;

    private int ExtraInfluence;
    public int TotalInfluence;
    private int ExtraCost;
    private int sharedCost;

    void Start()
    {
        EventDispatcher.StartListening(Vars.ServerHandleDisconnect, ServerHandleDisconnect);
        EventDispatcher.StartListening(Vars.LocalClientDisconnect, LocalClientDisconnect);
        Reset();
    }

    void ServerHandleDisconnect()
    {
        if (ProjectManager.Instance.SelectedProject != null)
            CancelVote();
    }

    void LocalClientDisconnect()
    {
        if (ProjectManager.Instance.SelectedProject != null)
            Reset();
    }

    public void ChangeInfoScreen(string text)
    {
        UIManager.Instance.ShowInfoScreen();
        UIManager.Instance.InfoText.text = text;
        if (text == "Project Approved!")
            UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
        if (text == "Project Rejected!")
            UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
    }

    public void ChangeInfoScreen(string voter, string state)
    {
        UIManager.Instance.ShowInfoScreen();
        UIManager.Instance.InfoText.text = "<color=red><b>" + voter + " </color></b>" + TextManager.Instance.InfoTextVoted;
        switch (voter)
        {
            case Vars.Player1:
                if (state == Vars.Approved)
                {
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
                if (state == Vars.Denied)
                {
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                }
                break;
            case Vars.Player2:
                if (state == Vars.Approved)
                {
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
                if (state == Vars.Denied)
                {
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                }
                break;
            case Vars.Player3:
                if (state == Vars.Approved)
                {
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
                if (state == Vars.Denied)
                {
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                }
                break;
        }
    }

    void Update()
    {
        if (ProjectManager.Instance != null)
            UpdateVars();
    }

    public void SetupDiscussion(int id)
    {
        ProjectId = id;
        Proposer = ProjectManager.Instance.SelectedProject.ProjectOwner;
        Finance = ProjectManager.Instance.SelectedProject.Finance;
        Social = ProjectManager.Instance.SelectedProject.Social;
        Environment = ProjectManager.Instance.SelectedProject.Environment;
        Influence = ProjectManager.Instance.SelectedProject.Influence;
        Budget = ProjectManager.Instance.SelectedProject.Budget;
        sharedCost = 0;
        ExtraCost = 0;
        UIManager.Instance.InfoText.text = TextManager.Instance.InfoTextProposed + ": \n<color=red><b>" + Proposer + "</color></b>";
        UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ExclamationSprite;
        BlockInteraction.SetActive(true);
        ApproveButton.interactable = true;
        DenyButton.interactable = true;
        //Votes.SetActive(false);
    }

    void UpdateVars()
    {
        int finVal = Mathf.RoundToInt(Mathf.Abs(FinanceSlider.value));
        int socVal = Mathf.RoundToInt(Mathf.Abs(SocialSlider.value));
        int envVal = Mathf.RoundToInt(Mathf.Abs(EnvironmentSlider.value));

        if (FinanceSlider.value >= 0)
            FinanceText.text = "+ <color=green><b>" + finVal + "</color></b>";
        if (FinanceSlider.value < 0)
            FinanceText.text = "- <color=red><b>" + finVal + "</color></b>";

        if (SocialSlider.value >= 0)
            SocialText.text = "+ <color=green><b>" + socVal + "</color></b>";
        if (SocialSlider.value < 0)
            SocialText.text = "- <color=red><b>" + socVal + "</color></b>";

        if (EnvironmentSlider.value >= 0)
            EnvironmentText.text = "+ <color=green><b>" + envVal + "</color></b>";
        if (EnvironmentSlider.value < 0)
            EnvironmentText.text = "- <color=red><b>" + envVal + "</color></b>";

        //base influence + adjustments
        ExtraInfluence = (finVal + socVal + envVal) * 5; //multiplier 5
        InfluenceText.text = "<color=green><b>+" + ExtraInfluence + "</color></b>";

        //basic cost + extra financing
        if (ProjectManager.Instance.SelectedProject != null)
        {
            ExtraCost = (finVal + socVal + envVal) * 100; //multiplier 100
            CostText.text = "<color=red>-" + ExtraCost + "</color></b>";
        }
            //sharedCost =Mathf.Abs(Mathf.RoundToInt((float)ProjectManager.Instance.SelectedProject.Budget / (ProjectManager.Instance.SelectedProject.Choice1 + 1)));
        //update voters number
        if (ProjectManager.Instance.SelectedProject != null)
        {
            VotersText.text = "Voters: " +
              (ProjectManager.Instance.SelectedProject.Choice1 +
               ProjectManager.Instance.SelectedProject.Choice2) + "/" +
              ProjectManager.Instance.SelectedProject.VotesNeeded;
            YesVotesText.text = "" + ProjectManager.Instance.SelectedProject.Choice1;
            NoVotesText.text = "" + ProjectManager.Instance.SelectedProject.Choice2;
        }
    }

    public void VoteApprove()
    {
        if (SaveStateManager.Instance.GetBudget(LocalManager.Instance.RoleType) >= Mathf.Abs(ExtraCost))
        {
            ProjectManager.Instance.SelectedProject.Approved = true;
            LocalManager.Instance.NetworkCommunicator.UpdateProjectVars((int)FinanceSlider.value, (int)SocialSlider.value, (int)EnvironmentSlider.value);
            LocalManager.Instance.NetworkCommunicator.Vote(Vars.Choice1, LocalManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
            LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue1, -ExtraCost);
            LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue2, ExtraInfluence);
            LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "MoneySpent", ExtraCost);
            //UIManager.Instance.GameDebugText.text += "\n" + LocalManager.Instance.RoleType + " extra cost " + ExtraCost;
            UIManager.Instance.CreateText(Color.red, ExtraCost.ToString(), 50, .5f, 2f, new Vector2(UIManager.Instance.BudgetTextPos.x, UIManager.Instance.BudgetTextPos.y), new Vector2(UIManager.Instance.BudgetTextPos.x, 0));
            ShowVotes();
        }
    }

    public void VoteDeny()
    {
        ProjectManager.Instance.SelectedProject.Approved = false;
        LocalManager.Instance.NetworkCommunicator.Vote(Vars.Choice2, LocalManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
        LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue2, Influence);
        ShowVotes();
    }

    public void CancelVote()
    {
        LocalManager.Instance.NetworkCommunicator.Vote("Cancel", LocalManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
        Reset();
    }

    void ShowVotes()
    {
        ApproveButton.interactable = false;
        DenyButton.interactable = false;
    }

    public void Reset()
    {
        BlockInteraction.SetActive(false);
        Finance = 0;
        Social = 0;
        Environment = 0;
        ExtraCost = 0;
        Influence = 0;
        FinanceSlider.value = 0;
        SocialSlider.value = 0;
        EnvironmentSlider.value = 0;

    }
    //Debugs

    public void FinanceApprove()
    {
        LocalManager.Instance.NetworkCommunicator.Vote("Choice1", Vars.Player1, ProjectManager.Instance.SelectedProject.ID_Spawn);
    }

    public void SocialApprove()
    {
        LocalManager.Instance.NetworkCommunicator.Vote("Choice1", Vars.Player2, ProjectManager.Instance.SelectedProject.ID_Spawn);
    }

    public void EnvironmentApprove()
    {
        LocalManager.Instance.NetworkCommunicator.Vote("Choice1", Vars.Player3, ProjectManager.Instance.SelectedProject.ID_Spawn);
    }
}