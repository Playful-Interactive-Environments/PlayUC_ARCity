using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
	public Text DebugText;
	//CHOOSE ROLE
	public Button Environment;
	public Button Finance;
	public Button Social;
	//GAME CANVAS
	public Button HeatmapButton;
	public Button Switch;
	//QUEST & RESULT CANVAS
	public Text Title;
	public Text Content;
	public Text Choice1;
	public Text Choice2;
	public Text Choice3;
	public Text Result;
	//PROJECT & VOTING CANVAS
	public Button ProjectButton_1;
	public Button ProjectButton_2;
	public Button ProjectButton_3;
	public Text Project_Content;
	public Text VoteStatus;
	public Button Vote_Choice1_Button;
	public Button Vote_Choice2_Button;
	public Text Proposed_Description;
	public Text EventText;
	public int CurrentProject;


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
	public void ToggleSocialMap()
	{
		EventManager.TriggerEvent("SocialMap");
		Invoke("RefreshGrid", .01f);
		Debug.Log("SocialMap");
	}
	public void ToggleEnvironmentMap()
	{
		EventManager.TriggerEvent("EnvironmentMap");
		Invoke("RefreshGrid", .01f);
		Debug.Log("EnvironmentMap");
	}

	public void ToggleFinanceMap()
	{
		EventManager.TriggerEvent("FinanceMap");
		Invoke("RefreshGrid", .01f);
		Debug.Log("FinanceMap");    
	}

	public void RefreshGrid()
	{
		HexGrid.Instance.Refresh();
	}
	public void MenuUI()
	{
		if (NetworkCanvas.gameObject.activeInHierarchy == false)
		{
			NetworkCanvas.gameObject.SetActive(true);
			GameCanvas.gameObject.SetActive(false);
		}
		else
		{
			NetworkCanvas.gameObject.SetActive(false);
			GameCanvas.gameObject.SetActive(true);
		}
	}

	public void RestartApp()
	{
		NetworkingManager.Instance.StopHosting();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void NotificationUI()
	{
		if (NotificationCanvas.enabled == false)
		{
			//NotificationCanvas.gameObject.SetActive(true);
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
	}
	#endregion

	#region Quest UI
	public void QuestUI()
	{
		if (GameCanvas.gameObject.activeInHierarchy)
		{
			QuestCanvas.gameObject.SetActive(true);
			GameCanvas.gameObject.SetActive(false);
			QuestManager.Instance.GetQuest();
		}
		else
		{
			QuestCanvas.gameObject.SetActive(false);
			GameCanvas.gameObject.SetActive(true);
		}
	}

	public void Choose_1()
	{

		QuestManager.Instance.GetResult(1);
		MakeChoice();
	}
	public void Choose_2()
	{

		QuestManager.Instance.GetResult(2);
		MakeChoice();
	}
	public void Choose_3()
	{

		QuestManager.Instance.GetResult(3);
		MakeChoice();
	}
	public void MakeChoice()
	{
		QuestCanvas.gameObject.SetActive(false);
		ResultCanvas.gameObject.SetActive(true);
	}
	public void Back()
	{
		ResultCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(true);

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
		CurrentProject = 1;
		Projects.GetProject(CurrentProject);
		ProjectContent(Projects.CurrentID);
		ProjectUI();
	}
	public void PressProjectButton_2()
	{
		CurrentProject = 2;
		Projects.GetProject(CurrentProject);
		ProjectContent(Projects.CurrentID);
		ProjectUI();

	}
	public void PressProjectButton_3()
	{
		CurrentProject = 3;
		Projects.GetProject(CurrentProject);
		ProjectContent(Projects.CurrentID);
		ProjectUI();

	}

	public void ProjectUI()
	{
		if (GameCanvas.gameObject.activeInHierarchy)
		{
			ProjectCanvas.gameObject.SetActive(true);
			GameCanvas.gameObject.SetActive(false);
		}
		else
		{
			ProjectCanvas.gameObject.SetActive(false);
			GameCanvas.gameObject.SetActive(true);
		}
	}
	public void ProposeProject()
	{
		switch (CurrentProject)
		{
			case 1:
				ProjectButton_1.interactable = false;
				break;
			case 2:
				ProjectButton_2.interactable = false;
				break;
			case 3:
				ProjectButton_3.interactable = false;
				break;

		}
		CellManager.Instance.NetworkCommunicator.Vote("StartVote", RoleManager.Instance.RoleType, Projects.CurrentID);
	}

	public void EnableVoteUI()
	{
		VoteStatus.text = "Please Vote.";
		ProjectCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(false);
		VoteCanvas.gameObject.SetActive(true);
	}
	public void Vote_Choice1()
	{
		//depends on Notification - when clicked in adds its id as CurrentID
		CellManager.Instance.NetworkCommunicator.Vote("Choice1","", ProjectManager.Instance.CurrentID);
		VoteManager.Instance.RemoveNotification(ProjectManager.Instance.CurrentID);
		VoteManager.Instance.AddNotification("Waiting", "", ProjectManager.Instance.CurrentID);

		VoteStatus.text = "You Voted. Please wait for other players.";
		Vote_Choice1_Button.interactable = false;
		Vote_Choice2_Button.interactable = false;
		Project_Content.gameObject.SetActive(false);
		Proposed_Description.gameObject.SetActive(false);

		//Invoke("EndVote", 2f);
	}

	public void Vote_Choice2()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice2","", ProjectManager.Instance.CurrentID);
		VoteManager.Instance.RemoveNotification(ProjectManager.Instance.CurrentID);
		VoteManager.Instance.AddNotification("Waiting", "", ProjectManager.Instance.CurrentID);

		VoteStatus.text = "You Voted. Please wait for other players.";
		Vote_Choice1_Button.interactable = false;
		Vote_Choice2_Button.interactable = false;
		Project_Content.gameObject.SetActive(false);
		Proposed_Description.gameObject.SetActive(false);
		//Invoke("EndVote", 2f);
	}

	public void EndVote()
	{
		//Restore Canvas State
		ProjectCanvas.gameObject.SetActive(false);
		VoteCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(true);
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
		EventCanvas.enabled = true;
	}

	public void CloseEventCanvas()
	{
		EventCanvas.enabled = false;
	}
	#endregion

	#region Choose Roles

	public void ChooseEnvironment()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Environment");
		HeatmapButton.onClick.RemoveAllListeners();
		HeatmapButton.onClick.AddListener(() => ToggleEnvironmentMap());
		HeatmapButton.GetComponentInChildren<Text>().text = "Environment";
		Invoke("GameUI", .1f);
	}

	public void ChooseFinance()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Finance");
		HeatmapButton.onClick.RemoveAllListeners();
		HeatmapButton.onClick.AddListener(() => ToggleFinanceMap());
		HeatmapButton.GetComponentInChildren<Text>().text = "Finance";
		Invoke("GameUI", .1f);
	}

	public void ChooseSocial()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Social");
		HeatmapButton.onClick.RemoveAllListeners();
		HeatmapButton.onClick.AddListener(() => ToggleSocialMap());
		HeatmapButton.GetComponentInChildren<Text>().text = "Social";

		Invoke("GameUI", .1f);
	}
	void UpdateRoleButtons()
	{
		if (RoleManager.Instance != null)
		{
			if (RoleManager.Instance.Environment)
			{
				Environment.interactable = false;
			}
			else
			{
				Environment.interactable = true;
			}
			if (RoleManager.Instance.Social)
			{
				Social.interactable = false;
			}
			else
			{
				Social.interactable = true;
			}
			if (RoleManager.Instance.Finance)
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
		Switch.gameObject.SetActive(false);

		NotificationCanvas.gameObject.SetActive(true);
		EventCanvas.gameObject.SetActive(true);
		NotificationCanvas.enabled = false;
		EventCanvas.enabled = false;

	}
	public void DebugButton()
	{
		Vote_Choice1();
	}
}