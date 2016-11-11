using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[System.Runtime.InteropServices.Guid("2B3BA483-C160-4892-9EF2-05AB923737C1")]
public class UIManager : AManager<UIManager>
{
	public ProjectManager Projects;

	//CANVAS
	public Canvas GameCanvas;
	public Canvas NetworkCanvas;
	public Canvas RoleCanvas;
	public Canvas QuestCanvas;
	public Canvas ResultCanvas;
	public Canvas EventCanvas;
	public Canvas ProjectCanvas;
	public Canvas VoteCanvas;
	public Canvas NotificationCanvas;
	public Canvas BuildCanvas;
	public Canvas ProjectInfoCanvas;
	public Text DebugText;
	//CHOOSE ROLE
	public Button Environment;
	public Button Finance;
	public Button Social;
	//GAME CANVAS
	public Button HeatmapButton;
	public Button Switch;
	public Button NotificationButton;
	public Text RatingText;
	public Text BudgetText;
	public Text TimeText;
	//QUEST & RESULT CANVAS
	public Text Title;
	public Text Content;
	public Text Choice1;
	public Text Choice2;
	public Text Result;
	public Quest CurrentQuest;
	//PROJECT & VOTING CANVAS
	public Button ProjectButton_1;
	public Button ProjectButton_2;
	public Button ProjectButton_3;
	public Button ProposeButton;
	public Text Project_Content;
	public Text VoteStatus;
	public Button Vote_Choice1_Button;
	public Button Vote_Choice2_Button;
	public Text Proposed_Description;
	public Text EventText;
	public int CurrentProjectButton;
	//PROJECT INFO CANVAS
	public Text ProjectInfo;

	void Start ()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		ResetMenus();
	}

	void Update ()
	{
		UpdateRoleButtons();
		Projects = ProjectManager.Instance;

	}
	#region Game UI

	public void MenuUI()
	{
		if (NetworkCanvas.gameObject.activeInHierarchy == false)
		{
			NetworkCanvas.gameObject.SetActive(true);
			GameCanvas.gameObject.SetActive(false);
			NotificationButton.gameObject.SetActive(false);

		}
		else
		{
			NetworkCanvas.gameObject.SetActive(false);
			GameCanvas.gameObject.SetActive(true);
			NotificationButton.gameObject.SetActive(true);

		}
	}

	public void NotificationUI()
	{
		if (NotificationCanvas.enabled == false)
		{
			SetNotificationState(false);
			GameCanvas.gameObject.SetActive(false);
			NotificationCanvas.enabled = true;
		}
		else
		{
			//NotificationCanvas.gameObject.SetActive(false);
			GameCanvas.gameObject.SetActive(true);
			NotificationCanvas.enabled = false;
		}
	}

	public void GameUI()
	{
		VoteCanvas.gameObject.SetActive(false);
		NotificationCanvas.enabled = false;
		ProjectCanvas.gameObject.SetActive(false);
		RoleCanvas.gameObject.SetActive(false);
		QuestCanvas.gameObject.SetActive(false);
		ResultCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(true);
		Switch.gameObject.SetActive(true);
		NotificationButton.gameObject.SetActive(true);

	}
	public void ShowProjectInfo()
	{
		GameCanvas.gameObject.SetActive(false);
		NotificationButton.gameObject.SetActive(false);
		ProjectInfoCanvas.gameObject.SetActive(true);

	}

	public void CloseProjectInfo()
	{
		ProjectInfoCanvas.gameObject.SetActive(false);

		NotificationButton.gameObject.SetActive(true);
		GameCanvas.gameObject.SetActive(true);
	}
	public void ToggleSocialMap()
	{
		EventManager.TriggerEvent("SocialMap");
		Invoke("RefreshGrid", .01f);
	}
	public void ToggleEnvironmentMap()
	{
		EventManager.TriggerEvent("EnvironmentMap");
		Invoke("RefreshGrid", .01f);
	}

	public void ToggleFinanceMap()
	{
		EventManager.TriggerEvent("FinanceMap");
		Invoke("RefreshGrid", .01f);
	}

	public void RefreshGrid()
	{
		HexGrid.Instance.Refresh();
	}
	public void RestartApp()
	{
		NetworkingManager.Instance.StopHosting();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	#endregion

	#region Quest UI
	public void QuestUI(Quest quest)
	{
		CurrentQuest = quest;
		GameCanvas.gameObject.SetActive(false);
		NotificationButton.gameObject.SetActive(false);
		QuestCanvas.gameObject.SetActive(true);
		Title.text = CurrentQuest.Title;
		Content.text = CurrentQuest.Content;
		Choice1.text = CurrentQuest.Choice1;
		Choice2.text = CurrentQuest.Choice2;
	}

	public void Choose_1()
	{
		CurrentQuest.ChooseEffect1();
		MakeChoice();
		Result.text = CurrentQuest.Result1 + "\n" + CurrentQuest.Effect1;
	}
	public void Choose_2()
	{
		CurrentQuest.ChooseEffect2();
		MakeChoice();
		Result.text = CurrentQuest.Result2 + "\n" + CurrentQuest.Effect2;

	}

	public void MakeChoice()
	{

		QuestCanvas.gameObject.SetActive(false);
		ResultCanvas.gameObject.SetActive(true);
	}
	public void ExitQuestUI()
	{
		QuestCanvas.gameObject.SetActive(false);
		ResultCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(true);
		NotificationButton.gameObject.SetActive(true);


	}
	#endregion

	#region Project & Vote UI
	public void ShowProjects()
	{
		if (ProjectButton_1.gameObject.activeInHierarchy)
		{
			ProjectButton_1.gameObject.SetActive(false);
			ProjectButton_2.gameObject.SetActive(false);
			ProjectButton_3.gameObject.SetActive(false);
		}
		else
		{
			ProjectButton_1.gameObject.SetActive(true);
			ProjectButton_2.gameObject.SetActive(true);
			ProjectButton_3.gameObject.SetActive(true);
		}
	}

	public void PressProjectButton_1()
	{
		CurrentProjectButton = 1;
		Projects.GetProject(CurrentProjectButton);
		ProjectContent(Projects.CurrentID);
		ProjectUI();
	}
	public void PressProjectButton_2()
	{
		CurrentProjectButton = 2;
		Projects.GetProject(CurrentProjectButton);
		ProjectContent(Projects.CurrentID);
		ProjectUI();
	}

	public void PressProjectButton_3()
	{
		CurrentProjectButton = 3;
		Projects.GetProject(CurrentProjectButton);
		ProjectContent(Projects.CurrentID);
		ProjectUI();
	}

	public void ProjectUI()
	{
		if (GameCanvas.gameObject.activeInHierarchy)
		{
			ProjectCanvas.gameObject.SetActive(true);
			GameCanvas.gameObject.SetActive(false);
			NotificationButton.gameObject.SetActive(false);
		}
		else
		{
			ProjectCanvas.gameObject.SetActive(false);
			GameCanvas.gameObject.SetActive(true);
			NotificationButton.gameObject.SetActive(true);
		}
	}

	public void PlaceProject()
	{
		if (CameraControl.Instance.LastTouchedCell != null)
		{
			CellManager.Instance.NetworkCommunicator.BuildProject(CameraControl.Instance.LastTouchedCell.CellPos, LocalManager.Instance.RoleType, ProjectManager.Instance.CurrentID);
			CellManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Budget", ProjectManager.Instance.Quests.GetCost(ProjectManager.Instance.CurrentID));
			CellManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Rating", ProjectManager.Instance.Quests.GetRating(ProjectManager.Instance.CurrentID));
			ProjectDescription(ProjectManager.Instance.CurrentID);
			ProjectButtonState(CurrentProjectButton, false);
			BuildCanvas.gameObject.SetActive(false);
			EnableVoteUI();
		}
	}

	public void ShowPlacementCanvas()
	{
		ProjectCanvas.gameObject.SetActive(false);
		VoteCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(false);
		NotificationButton.gameObject.SetActive(false);
		BuildCanvas.gameObject.SetActive(true);
	}

	public void HidePlacementCanvas()
	{
		GameCanvas.gameObject.SetActive(true);
		NotificationButton.gameObject.SetActive(true);
		BuildCanvas.gameObject.SetActive(false);
	}

	public void EnableVoteUI()
	{
		VoteStatus.text = "Please Vote.";
		ProjectCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(false);
		NotificationButton.gameObject.SetActive(false);
		VoteCanvas.gameObject.SetActive(true);
	}
	public void Vote_Choice1()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice1","", ProjectManager.Instance.CurrentID);
		ProjectManager.Instance.CurrentProject.LocalVote = true;
		EndVote();
	}
	public void Vote_Choice2()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice2", "", ProjectManager.Instance.CurrentID);
		ProjectManager.Instance.CurrentProject.LocalVote = true;

		EndVote();
	}
	public void ProjectButtonState(int buttonnum, bool state)
	{
		switch (buttonnum)
		{
			case 1:
				ProjectButton_1.interactable = state;
				break;
			case 2:
				ProjectButton_2.interactable = state;
				break;
			case 3:
				ProjectButton_3.interactable = state;
				break;
			default:
				break;
		}
	}
	
	public void EndVote()
	{
		ProjectCanvas.gameObject.SetActive(false);
		VoteCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(true);
		NotificationButton.gameObject.SetActive(true);

		Vote_Choice1_Button.interactable = true;
		Vote_Choice2_Button.interactable = true;
		Project_Content.gameObject.SetActive(true);
		Proposed_Description.gameObject.SetActive(true);
	}
	public void ProjectContent(int projectnum)
	{
		Project_Content.text = QuestManager.Instance.GetProjectDescription(projectnum);
	}

	public void ProjectDescription(int projectnum)
	{
		Proposed_Description.text = QuestManager.Instance.GetProjectDescription(projectnum);
	}

	public void DisplayEventCanvas()
	{
		NotificationButton.gameObject.SetActive(false);
		NotificationCanvas.enabled = false;
		EventCanvas.enabled = true;
	}

	public void CloseEventCanvas()
	{
		NotificationButton.gameObject.SetActive(true);
		NotificationCanvas.enabled = true;
		EventCanvas.enabled = false;
	}

	public void SetNotificationState(bool state)
	{
		if (state)
		{
			NotificationButton.GetComponentInChildren<Text>().text = "!!!";
			NotificationButton.image.color = Color.red;
		}
		else
		{
			NotificationButton.GetComponentInChildren<Text>().text = "!";
			NotificationButton.image.color = Color.white;
		}
	}
	#endregion

	#region Choose Roles

	public void ChooseEnvironment()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Environment");
		HeatmapButton.onClick.RemoveAllListeners();
		HeatmapButton.onClick.AddListener(() => ToggleEnvironmentMap());
		LocalManager.Instance.RoleType = "Environment";
		Invoke("GameUI", .1f);
	}

	public void ChooseFinance()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Finance");
		HeatmapButton.onClick.RemoveAllListeners();
		HeatmapButton.onClick.AddListener(() => ToggleFinanceMap());
		LocalManager.Instance.RoleType = "Finance";
		Invoke("GameUI", .1f);
	}

	public void ChooseSocial()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Social");
		HeatmapButton.onClick.RemoveAllListeners();
		HeatmapButton.onClick.AddListener(() => ToggleSocialMap());
		LocalManager.Instance.RoleType = "Social";

		Invoke("GameUI", .1f);
	}
    
	void UpdateRoleButtons()
	{
		if (GlobalManager.Instance != null)
        {
            if (GlobalManager.Instance.GetTaken("Environment"))
			{
				Environment.interactable = false;
			}
			else
			{
				Environment.interactable = true;
			}
			if (GlobalManager.Instance.GetTaken("Social"))
			{
				Social.interactable = false;
			}
			else
			{
				Social.interactable = true;
			}
			if (GlobalManager.Instance.GetTaken("Finance"))
			{
				Finance.interactable = false;
			}
			else
			{
				Finance.interactable = true;
			}
		}
	}

    public void RoleUI()
	{
		NetworkCanvas.gameObject.SetActive(false);
		RoleCanvas.gameObject.SetActive(true);
	}
	#endregion

	public void ResetMenus()
	{
		NetworkCanvas.gameObject.SetActive(true);
		GameCanvas.gameObject.SetActive(false);
		RoleCanvas.gameObject.SetActive(false);
		QuestCanvas.gameObject.SetActive(false);
		ResultCanvas.gameObject.SetActive(false);
		ProjectCanvas.gameObject.SetActive(false);
		VoteCanvas.gameObject.SetActive(false);
		BuildCanvas.gameObject.SetActive(false);
		NotificationCanvas.gameObject.SetActive(true);
		NotificationCanvas.enabled = false;
		EventCanvas.gameObject.SetActive(true);
		EventCanvas.enabled = false;
		ProjectInfoCanvas.gameObject.SetActive(false);
		Switch.gameObject.SetActive(false);
		NotificationButton.gameObject.SetActive(false);    
	}

	public void DebugButton()
	{
		//Vote_Choice1();
		Vote_Choice2();
		//ProjectManager.Instance.AddProject();
		//ProjectManager.Instance.ProjectApproved(2);
	}
}