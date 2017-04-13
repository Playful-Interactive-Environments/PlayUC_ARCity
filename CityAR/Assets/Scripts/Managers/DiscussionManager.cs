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

    public GameObject SocialVote;
    public GameObject EnvironmentVote;
    public GameObject FinanceVote;

    public Button ApproveButton;
    public Button DenyButton;

    public string Proposer;
    public int ProjectId;
    public int Environment;
    public int Social;
    public int Finance;
    public int Budget;
    public int Influence;

    private int currentExtraInfluence;
    public int TotalInfluence;
    private int currentExtraCost;
    public int ExtraCost;
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
    public void ChangeVoterState(string voter, string state)
    {
        UIManager.Instance.ShowInfoScreen();
        UIManager.Instance.InfoText.text = "<color=red><b>" + voter + " </color></b>" + TextManager.Instance.InfoTextVoted;
        switch (voter)
        {
            case Vars.Player1:
                if (state == Vars.Approved)
                {
                    FinanceVote.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
                if (state == Vars.Denied)
                {
                    FinanceVote.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                }
                break;
            case Vars.Player2:
                if (state == Vars.Approved)
                {
                    SocialVote.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
                if (state == Vars.Denied)
                {
                    SocialVote.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                }
                break;
            case Vars.Player3:
                if (state == Vars.Approved)
                {
                    EnvironmentVote.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
                if (state == Vars.Denied)
                {
                    EnvironmentVote.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
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
        currentExtraCost = 0;
        UIManager.Instance.InfoText.text = TextManager.Instance.InfoTextProposed + ": \n<color=red><b>" + Proposer + "</color></b>";
        UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ExclamationSprite;
        BlockInteraction.SetActive(true);
    }

    void UpdateVars()
    {
        int finVal = Mathf.RoundToInt(Mathf.Abs(FinanceSlider.value));
        int socVal = Mathf.RoundToInt(Mathf.Abs(SocialSlider.value));
        int envVal = Mathf.RoundToInt(Mathf.Abs(EnvironmentSlider.value));

        if (FinanceSlider.value >= 0)
            FinanceText.text = "" + Finance + " + <color=green><b>" + finVal + "</color></b>";
        if (FinanceSlider.value < 0)
            FinanceText.text = "" + Finance + " - <color=red><b>" + finVal + "</color></b>";

        if (SocialSlider.value >= 0)
            SocialText.text = "" + Social + " + <color=green><b>" + socVal + "</color></b>";
        if (SocialSlider.value < 0)
            SocialText.text = "" + Social + " - <color=red><b>" + socVal + "</color></b>";

        if (EnvironmentSlider.value >= 0)
            EnvironmentText.text = "" + Environment + " + <color=green><b>" + envVal + "</color></b>";
        if (EnvironmentSlider.value < 0)
            EnvironmentText.text = "" + Environment + " - <color=red><b>" + envVal + "</color></b>";

        //base influence + adjustments
        currentExtraInfluence = (finVal + socVal + envVal) * 5;
        InfluenceText.text = "<color=green><b>+" + (Influence + currentExtraInfluence) + "</color></b>";

        //base cost/number of positive votes + extra cost from adjustments
        if (ProjectManager.Instance.SelectedProject != null)
            sharedCost =
                Mathf.Abs(Mathf.RoundToInt((float)ProjectManager.Instance.SelectedProject.Budget / (ProjectManager.Instance.SelectedProject.Choice1 + 1)));
        currentExtraCost = (finVal + socVal + envVal) * 100;
        CostText.text = "<color=red>-" + (sharedCost + currentExtraCost) + "</color></b>";
    }

    public void VoteApprove()
    {
        ExtraCost = currentExtraCost;
        TotalInfluence = currentExtraInfluence + Influence;
        ProjectManager.Instance.SelectedProject.Approved = true;
        LocalManager.Instance.NetworkCommunicator.UpdateProjectVars((int)FinanceSlider.value, (int)SocialSlider.value, (int)EnvironmentSlider.value);
        LocalManager.Instance.NetworkCommunicator.Vote(Vars.Choice1, LocalManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
        HideButtons();
    }

    public void VoteDeny()
    {
        ProjectManager.Instance.SelectedProject.Approved = false;
        LocalManager.Instance.NetworkCommunicator.Vote(Vars.Choice2, LocalManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
        LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue2, Influence);
        HideButtons();
    }

    void CancelVote()
    {
        LocalManager.Instance.NetworkCommunicator.Vote("Cancel", LocalManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
        Reset();
    }

    void HideButtons()
    {
        SocialVote.GetComponent<Image>().enabled = true;
        FinanceVote.GetComponent<Image>().enabled = true;
        EnvironmentVote.GetComponent<Image>().enabled = true;
        ApproveButton.gameObject.SetActive(false);
        DenyButton.gameObject.SetActive(false);
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
        SocialVote.GetComponent<Image>().sprite = UIManager.Instance.DefaultSprite;
        FinanceVote.GetComponent<Image>().sprite = UIManager.Instance.DefaultSprite;
        EnvironmentVote.GetComponent<Image>().sprite = UIManager.Instance.DefaultSprite;
        SocialVote.GetComponent<Image>().enabled = false;
        FinanceVote.GetComponent<Image>().enabled = false;
        EnvironmentVote.GetComponent<Image>().enabled = false;
        ApproveButton.gameObject.SetActive(true);
        DenyButton.gameObject.SetActive(true);
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