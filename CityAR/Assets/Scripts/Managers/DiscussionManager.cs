using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionManager : AManager<DiscussionManager> {

	public Slider FinanceSlider;
	public Slider SocialSlider;
	public Slider EnvironmentSlider;
	public GameObject BlockInteraction;

	public TextMeshProUGUI FinanceText;
	public TextMeshProUGUI EnvironmentText;
	public TextMeshProUGUI SocialText;
	public TextMeshProUGUI InfluenceText;
	public TextMeshProUGUI CostText;

	public GameObject VoteStateSprites;
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

	void Start ()
	{
		EventDispatcher.StartListening("ClientDisconnect", ClientDisconnect);
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
        BlockInteraction.SetActive(false);

    }

    void ClientDisconnect()
	{
		if(ProjectManager.Instance.SelectedProject  != null)
			CancelVote();
	}
	void NetworkDisconnect()
	{
		if (ProjectManager.Instance.SelectedProject != null)
			Reset();
	}
	public void ChangeVoterState(string voter, string state)
    {
        UIManager.Instance.ShowInfoScreen();
        switch (voter)
		{
			case "Environment":
		        if (state == "Approved")
                {
                    EnvironmentVote.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                    UIManager.Instance.InfoText.text = "Player <color=red>" + voter + "</color> voted";
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
                if (state == "Denied")
		        {
                    EnvironmentVote.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                    UIManager.Instance.InfoText.text = "Player <color=red>" + voter + "</color> voted";
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                }
                break;
			case "Social":
		        if (state == "Approved")
                {
                    SocialVote.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                    UIManager.Instance.InfoText.text = "Player <color=red>" + voter + "</color> voted";
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
		        if (state == "Denied")
                {
                    SocialVote.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                    UIManager.Instance.InfoText.text = "Player <color=red>" + voter + "</color> voted";
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                }
				break;
			case "Finance":
		        if (state == "Approved")
                {
                    FinanceVote.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                    UIManager.Instance.InfoText.text = "Player <color=red>" + voter + "</color> voted";
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ApproveSprite;
                }
		        if (state == "Denied")
                {
                    FinanceVote.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                    UIManager.Instance.InfoText.text = "Player <color=red>" + voter + "</color> voted";
                    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.DenySprite;
                }
				break;
		}
    }

	void Update()
	{
		if (ProjectManager.Instance != null)
			UpdateVars();
		if (Input.GetKeyDown(KeyCode.B))
		{
			DebugApprove();
		}
		if (Input.GetKeyDown(KeyCode.V))
		{
			DebugDeny();
		}
	}

	public void DebugApprove()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice1", "Environment", ProjectManager.Instance.SelectedProject.ID_Spawn);
		CellManager.Instance.NetworkCommunicator.Vote("Choice1", "Social", ProjectManager.Instance.SelectedProject.ID_Spawn);
	}

	public void DebugDeny()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice2", "Environment", ProjectManager.Instance.SelectedProject.ID_Spawn);
		CellManager.Instance.NetworkCommunicator.Vote("Choice2", "Social", ProjectManager.Instance.SelectedProject.ID_Spawn);
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

		SocialVote.GetComponent<Image>().sprite = UIManager.Instance.DefaultSprite;
		FinanceVote.GetComponent<Image>().sprite = UIManager.Instance.DefaultSprite;
		EnvironmentVote.GetComponent<Image>().sprite = UIManager.Instance.DefaultSprite;

		VoteStateSprites.SetActive(false);
		ApproveButton.gameObject.SetActive(true);
		DenyButton.gameObject.SetActive(true);
		BlockInteraction.SetActive(true);

        UIManager.Instance.InfoText.text = "Project Started By:\n <color=red>" + Proposer +"</color>";
	    UIManager.Instance.InfoIcon.GetComponent<Image>().sprite = UIManager.Instance.ExclamationSprite;
	}

    void UpdateVars()
	{
		int finVal = Mathf.RoundToInt(Mathf.Abs(FinanceSlider.value));
		int socVal = Mathf.RoundToInt(Mathf.Abs(SocialSlider.value));
		int envVal = Mathf.RoundToInt(Mathf.Abs(EnvironmentSlider.value));

		if (FinanceSlider.value >= 0)
			FinanceText.text = "" + Finance + " + <color=green><b>" + finVal + "</color></b>";
		if (FinanceSlider.value <0)
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
		if(ProjectManager.Instance.SelectedProject !=null)
		sharedCost =
			Mathf.Abs(Mathf.RoundToInt((float)ProjectManager.Instance.SelectedProject.Budget / (ProjectManager.Instance.SelectedProject.Choice1 + 1)));
		currentExtraCost = (finVal + socVal + envVal) * 100;
		CostText.text = "<color=red>-" + (sharedCost + currentExtraCost) + "</color></b>";
	}

	public void VoteApprove()
	{
		ExtraCost = currentExtraCost;
		TotalInfluence = currentExtraInfluence + Influence;
		CellManager.Instance.NetworkCommunicator.UpdateProjectVars((int)FinanceSlider.value, (int)SocialSlider.value, (int)EnvironmentSlider.value);
		CellManager.Instance.NetworkCommunicator.Vote("Choice1", LevelManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
		HideButtons();
	}

	public void VoteDeny()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice2", LevelManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
		CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Influence", Influence);
		HideButtons();
	}

	void CancelVote()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Cancel", LevelManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
		Reset();
	}

	void HideButtons()
	{
		VoteStateSprites.SetActive(true);
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
	}
}