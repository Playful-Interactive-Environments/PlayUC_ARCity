using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscussionPanel : MonoBehaviour {

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

	public Sprite DefaultSprite;
	public Sprite ApproveSprite;
	public Sprite DenySprite;

	public Button ApproveButton;
	public Button DenyButton;

	public string Proposer;
	public int ProjectId;
	public int Environment;
	public int Social;
	public int Finance;
	public int Influence;

	public int ExtraInfluence;
	public int ExtraCost;

	void Start ()
	{

	}

	public void ChangeVoterState(string voter, string state)
	{
		switch (voter)
		{
			case "Environment":
				if(state == "Approved")
					EnvironmentVote.GetComponent<Image>().sprite = ApproveSprite;
				if (state == "Denied")
					EnvironmentVote.GetComponent<Image>().sprite = DenySprite;
				break;
			case "Social":
				if (state == "Approved")
					SocialVote.GetComponent<Image>().sprite = ApproveSprite;
				if (state == "Denied")
					SocialVote.GetComponent<Image>().sprite = DenySprite;
				break;
			case "Finance":
				if (state == "Approved")
					FinanceVote.GetComponent<Image>().sprite = ApproveSprite;
				if (state == "Denied")
                    FinanceVote.GetComponent<Image>().sprite = DenySprite;
				break;
		}
	}

	void Update()
	{
		UpdateVars();
	    if (Input.GetKeyDown(KeyCode.B))
	    {
            CellManager.Instance.NetworkCommunicator.Vote("Choice1", "Environment", ProjectManager.Instance.SelectedProject.ID_Spawn);
            CellManager.Instance.NetworkCommunicator.Vote("Choice1", "Social", ProjectManager.Instance.SelectedProject.ID_Spawn);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            CellManager.Instance.NetworkCommunicator.Vote("Choice2", "Environment", ProjectManager.Instance.SelectedProject.ID_Spawn);
            CellManager.Instance.NetworkCommunicator.Vote("Choice2", "Social", ProjectManager.Instance.SelectedProject.ID_Spawn);
        }
    }

    public void SetupDiscussion(int id)
	{
		ProjectId = id;
		Proposer = ProjectManager.Instance.SelectedProject.ProjectOwner;
		Finance = ProjectManager.Instance.SelectedProject.Finance;
		Social = ProjectManager.Instance.SelectedProject.Social;
		Environment = ProjectManager.Instance.SelectedProject.Environment;
		Influence = ProjectManager.Instance.SelectedProject.Influence;

		SocialVote.GetComponent<Image>().sprite = DefaultSprite;
		FinanceVote.GetComponent<Image>().sprite = DefaultSprite;
		EnvironmentVote.GetComponent<Image>().sprite = DefaultSprite;

		VoteStateSprites.SetActive(false);
		ApproveButton.gameObject.SetActive(true);
		DenyButton.gameObject.SetActive(true);
		//BlockInteraction.SetActive(true);
	}

	void UpdateVars()
	{
		FinanceText.text = "" + Finance + " + <color=#0071BCFF><b>" + FinanceSlider.value + "</color></b>";
		SocialText.text = "" + Social + " + <color=#FBB03BFF><b>" + SocialSlider.value + "</color></b>";
		EnvironmentText.text = "" + Environment + " + <color=#A6C738><b>" + EnvironmentSlider.value + "</color></b>";
		ExtraInfluence = (int) (FinanceSlider.value + SocialSlider.value + EnvironmentSlider.value);
		InfluenceText.text = "" + Influence + " + <color=#F8D40097><b>" + ExtraInfluence + "</color></b>";
		ExtraCost = (int) (FinanceSlider.value + SocialSlider.value + EnvironmentSlider.value) * -100;
		CostText.text = "" + ExtraCost;
	}

	public void VoteApprove()
	{
		CellManager.Instance.NetworkCommunicator.UpdateProjectVars((int)FinanceSlider.value, (int)SocialSlider.value, (int)EnvironmentSlider.value);
        CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Influence", Influence + ExtraInfluence);
        CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Budget", ExtraCost);
        CellManager.Instance.NetworkCommunicator.Vote("Choice1", LevelManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
		HideButtons();
    }

    public void VoteDeny()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice2", LevelManager.Instance.RoleType, ProjectManager.Instance.SelectedProject.ID_Spawn);
		CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Influence", Influence);
		HideButtons();
	}

	void HideButtons()
	{
		VoteStateSprites.SetActive(true);
		ApproveButton.gameObject.SetActive(false);
		DenyButton.gameObject.SetActive(false);
	}

	public void Reset()
	{
		//BlockInteraction.SetActive(false);
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