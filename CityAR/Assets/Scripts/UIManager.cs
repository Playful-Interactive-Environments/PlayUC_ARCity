using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[System.Runtime.InteropServices.Guid("2B3BA483-C160-4892-9EF2-05AB923737C1")]
public class UIManager : AManager<UIManager>
{
	public ProjectManager Projects;
	//Switch UI ELEMENTS
	public enum UiState
	{
		Network, Role, Game, Projects, DesignProject, Placement, Notifications, Event, Vote, Quest, Result
	}

	public UiState CurrentState;
	//CANVAS
	public Canvas GameCanvas;
	public Canvas NetworkCanvas;
	public Canvas RoleCanvas;
	public Canvas QuestCanvas;
	public Canvas ResultCanvas;
	public Canvas EventCanvas;
	public Canvas ProjectCanvas;
    public Canvas ProjectDesignCanvas;
	public Canvas VoteCanvas;
	public Canvas NotificationCanvas;
	public Canvas PlacementCanvas;
	public Text DebugText;
	//CHOOSE ROLE
	public Button Environment;
	public Button Finance;
	public Button Social;
	//GAME CANVAS
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
	public GameObject[] ResultIcons;
	public Text[] ResultText;
	public Vector3[] iconPositions;
	private int currentIcon;
	public Vector3[] textPositions;
	private int currentText;
	Vector3 hiddenPos = new Vector3(-100, 1000,0);
	public Quest CurrentQuest;
	//PROJECT & VOTING CANVAS
	public Button ProjectButton;
	public GameObject VoteDescription;
	public Text EventText;
	public int CurrentProjectButton;
	//PROJECT INFO CANVAS
	public GameObject ProjectInfo;

	void Start ()
	{
		ResetMenus();
	}

	void Update ()
	{
		UpdateRoleButtons();
		if (ProjectManager.Instance != null)
		{
			Projects = ProjectManager.Instance;
		}
	}

	public void Change(UiState state)
	{
		NetworkCanvas.gameObject.SetActive(true);
		NetworkCanvas.enabled = false;
		GameCanvas.gameObject.SetActive(true);
		GameCanvas.enabled = false;
		RoleCanvas.gameObject.SetActive(true);
		RoleCanvas.enabled = false;
		QuestCanvas.gameObject.SetActive(true);
		QuestCanvas.enabled = false;
		ResultCanvas.gameObject.SetActive(true);
		ResultCanvas.enabled = false;
		VoteCanvas.gameObject.SetActive(true);
		VoteCanvas.enabled = false;
		PlacementCanvas.gameObject.SetActive(true);
		PlacementCanvas.enabled = false;
		NotificationCanvas.gameObject.SetActive(true);
		NotificationCanvas.enabled = false;
		EventCanvas.gameObject.SetActive(true);
		EventCanvas.enabled = false;
		ProjectCanvas.gameObject.SetActive(true);
		ProjectCanvas.enabled = false;
        ProjectDesignCanvas.gameObject.SetActive(true);
        ProjectDesignCanvas.enabled = false;
        Switch.gameObject.SetActive(true);
		Switch.enabled = false;
		NotificationButton.gameObject.SetActive(false);
		NotificationButton.enabled = false;
		ProjectButton.gameObject.SetActive(false);
		ProjectButton.enabled = false;
		CurrentState = state;
		switch (CurrentState)
		{
			case UiState.Network:
				NetworkCanvas.enabled = true;
				Switch.enabled = true;
				break;
			case UiState.Role:
				RoleCanvas.enabled = true;
				break;
			case UiState.Game:
				GameCanvas.enabled = true;
				ProjectButton.enabled = true;
				ProjectButton.gameObject.SetActive(true);
				NotificationButton.enabled = true;
				NotificationButton.gameObject.SetActive(true);
				Switch.enabled = true;
				break;
			case UiState.Projects:
				ProjectCanvas.enabled = true;
				ProjectButton.enabled = true;
				ProjectButton.gameObject.SetActive(true);
				break;
            case UiState.DesignProject:
                ProjectDesignCanvas.enabled = true;
                break;
            case UiState.Placement:
				PlacementCanvas.enabled = true;
				break;
			case UiState.Vote:
				VoteDescription.GetComponent<ProjectText>().SetText(ProjectManager.Instance.SelectedProjectId);
				VoteCanvas.enabled = true;
				break;
			case UiState.Notifications:
				NotificationCanvas.enabled = true;
				NotificationButton.enabled = true;
				NotificationButton.gameObject.SetActive(true);
				break;
			case UiState.Event:
				EventCanvas.enabled = true;
				break;
			case UiState.Quest:
				QuestCanvas.enabled = true;
				break;
			case UiState.Result:
				ResultCanvas.enabled = true;
				break;
		}
}
	#region Game UI

	public void MenuUI()
	{
		if (NetworkCanvas.enabled == false)
			Change(UiState.Network);
		else
			GameUI();
	}

	public void NotificationUI()
	{
		if (NotificationCanvas.enabled == false)
			Change(UiState.Notifications);
		else
			GameUI();
	}

	public void GameUI()
	{
		Change(UiState.Game);
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
		Change(UiState.Quest);
		CurrentQuest = quest;
		Title.text = CurrentQuest.Title;
		Content.text = CurrentQuest.Content;
		Choice1.text = "1. " + CurrentQuest.Choice1;
		Choice2.text = "2. " + CurrentQuest.Choice2;
	}

	public void Choose_1()
	{
		ResetIconPos();
		CurrentQuest.Choose(1);
		Change(UiState.Result);
	}
	public void Choose_2()
	{
		ResetIconPos();
		CurrentQuest.Choose(2);
		Change(UiState.Result);
	}

	void ResetIconPos()
	{
		currentIcon = 0;
		currentText = 0;
		foreach (GameObject obj in ResultIcons)
		{
			obj.GetComponent<RectTransform>().localPosition = hiddenPos;
		}
		foreach (Text obj in ResultText)
		{
			obj.GetComponent<RectTransform>().localPosition = hiddenPos;
		}

	}

	public void UpdateResult(string value)
	{
		ResultText[6].GetComponent<RectTransform>().localPosition = textPositions[6];
		ResultText[6].text = "" + value;
	}
	public void UpdateResult(string type, string value)
	{
		int num = 0;
		switch (type)
		{
			case "Rating":
				num = 0;
				break;
			case "Budget":
				num = 1;
				break;
			case "Project":
				num = 2;
				break;
			case "Environment":
				num = 3;
				break;
			case "Social":
				num = 4;
				break;
			case "Finance":
				num = 5;
				break;
		}
		ResultIcons[num].GetComponent<RectTransform>().localPosition = iconPositions[currentIcon];
		ResultText[num].GetComponent<RectTransform>().localPosition = textPositions[currentText];
		ResultText[num].text = "" + value;
		//get next position for icons and text
		currentIcon += 1;
		currentText += 1;
	}
	#endregion

	#region Project & Vote UI

	public void ProjectUI()
	{
		if (GameCanvas.enabled)
			Change(UiState.Projects);
		else
			Change(UiState.Game);
	}

	public void PlaceProject()
	{
		if (CameraControl.Instance.LastTouchedCell != null)
		{
			CellManager.Instance.NetworkCommunicator.ActivateProject("PlaceProject", CameraControl.Instance.LastTouchedCell.CellPos, LocalManager.Instance.RoleType, ProjectManager.Instance.SelectedProjectId);
			CellManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Budget", ProjectManager.Instance.Quests.GetBudgetInt(ProjectManager.Instance.SelectedProjectId));
			CellManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Rating", ProjectManager.Instance.Quests.GetRatingInt(ProjectManager.Instance.SelectedProjectId));
			GameUI();
		}
	}

	public void ShowPlacementCanvas()
	{
		Change(UiState.Placement);
	}

	public void EnableVoteUI()
	{
		Change(UiState.Vote);
	}

    public void ShowProjectDesignCanvas()
    {
        Change(UiState.DesignProject);
    }

	public void Vote_Choice1()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice1","", ProjectManager.Instance.SelectedProjectId);
		ProjectManager.Instance.SelectedProject.LocalVote = true;
		GameUI();
	}

	public void Vote_Choice2()
	{
		CellManager.Instance.NetworkCommunicator.Vote("Choice2", "", ProjectManager.Instance.SelectedProjectId);
		ProjectManager.Instance.SelectedProject.LocalVote = true;
		GameUI();
	}

	public void DisplayEventCanvas()
	{
		Change(UiState.Event);
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
		LocalManager.Instance.RoleType = "Environment";
		EventManager.TriggerEvent("EnvironmentMap");
		Invoke("GameUI", .1f);
	}

	public void ChooseFinance()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Finance");
		LocalManager.Instance.RoleType = "Finance";
		EventManager.TriggerEvent("FinanceMap");
		Invoke("GameUI", .1f);
	}

	public void ChooseSocial()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Social");
		LocalManager.Instance.RoleType = "Social";
		EventManager.TriggerEvent("SocialMap");
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
		Change(UiState.Role);
	}
	#endregion

	public void ResetMenus()
	{
		Change(UiState.Network);
	}

	public void DebugButton()
	{
		//Debug.Log("CLICK");
		//Vote_Choice1();
		ProjectManager.Instance.CreateRandomProject();
		//ProjectManager.Instance.AddProject();
		//ProjectManager.Instance.ProjectApproved(2);
	}
}