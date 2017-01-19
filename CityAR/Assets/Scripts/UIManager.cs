using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : AManager<UIManager>
{
	public ProjectManager Projects;
	//Switch UI ELEMENTS
	public enum UiState
	{
		Network, Role, Game, Projects, DesignProject, Placement, Notifications,
		NotificationResult, Vote, Quest, Result, GlobalState, EventDisplay, EventResult, Level
	}

	public UiState CurrentState;
	//CANVAS
	public Canvas GameCanvas;
	public Canvas NetworkCanvas;
	public Canvas RoleCanvas;
	public Canvas QuestCanvas;
	public Canvas ResultCanvas;
	public Canvas NotificationResult;
	public Canvas ProjectCanvas;
	public Canvas ProjectDesignCanvas;
	public Canvas VoteCanvas;
	public Canvas NotificationCanvas;
	public Canvas PlacementCanvas;
	public Canvas GlobalStateCanvas;
	public Canvas EventDisplay;
	public Canvas EventResult;
	public Canvas LevelCanvas;
	public Text DebugText;
	//CHOOSE ROLE
	public Button Environment;
	public Button Finance;
	public Button Social;
	//GAME CANVAS
	public GameObject PlayerVariables;
	public Button MenuButton;
	public Button NotificationButton;
	public Button LevelButton;
	public Text RatingText;
	public Text BudgetText;
	public Text TimeText;
	public Text GlobalFinanceText;
	public Text GlobalEnvironmentText;
	public Text GlobalSocialText;
	public GameObject FinanceImage;
	public GameObject EnvironmentImage;
	public GameObject SocialImage;
	public GameObject RatingImage;
	public GameObject BudgetImage;
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
	//GLOBAL STATE CANVAS
	public Button GlobalStateButton;
	public Text RoleDescriptionText;
	private int[] savevalues = new int[5]; //array that stores values from previous update
	//EVENT DISPLAY CANVAS
	public Text EventTitle;
	public Text EventContent;
	public Button Event_Choice1;
	public Button Event_Choice2;
	public GameObject EventVars;
	//EVENT RESULT CANVAS
	public Text EventResultText;
	//current event progress text in GameUI
	public Text Event_CurrentProgress;
	public Text Event_TimeLeft;
	public bool EventProgressEnabled;



	void Start ()
	{
		ResetMenus();
		InvokeRepeating("RefreshPlayerVars", .2f, .5f);
		EventDispatcher.StartListening("HelpMayor", HelpMayor);
		EventDispatcher.StartListening("Crisis", Crisis);
		EventDispatcher.StartListening("DesignProject", DesignProject);

	}

	#region Event UI
	public void ShowEventResult()
	{
		Change(UiState.EventResult);
	}

	public void SetEventText(string title, string content, string button1, string button2)
	{
		EventVars.SetActive(false);

		EventTitle.text = title;
		EventContent.text = content;
		Event_Choice1.GetComponentInChildren<Text>().text = button1;
		Event_Choice2.GetComponentInChildren<Text>().text = button2;

		Event_Choice1.gameObject.SetActive(button1 != "");
		Event_Choice2.gameObject.SetActive(button2 != "");

		Change(UiState.EventDisplay);
	}
	//Help Mayor Event & Button
	void HelpMayor()
	{
		Event_Choice1.onClick.AddListener(() => AcceptEvent());
		EventVars.SetActive(true);
	}

	void AcceptEvent()
	{
		EventProgressEnabled = true;
		EventManager.Instance.CurrentEventScript.HelpMayorEvent = true;
		GameUI();
	}
	//Crisis Event & Button
	void Crisis()
	{
		Debug.Log("Crisis");
		Event_Choice1.onClick.AddListener(() => Crisis_Choice1());
		Event_Choice2.onClick.AddListener(() => Crisis_Choice2());

	}
	void Crisis_Choice1()
	{
		GameUI();
	}

	void Crisis_Choice2()
	{
		GameUI();
	}

	public void ShowEvent()
	{   
		if(EventDisplay.enabled == false)
			Change(UiState.EventDisplay);
	}
	//Design project Event & Button
	void DesignProject()
	{
		Event_Choice1.onClick.AddListener(() => AcceptDesignProject());
	}

	void AcceptDesignProject()
	{
		Change(UiState.DesignProject);

	}
	#endregion

	void Update ()
	{
		UpdateRoleButtons();
		if (ProjectManager.Instance != null)
		{
			Projects = ProjectManager.Instance;
		}
	}

	void RefreshPlayerVars()
	{
		if (SaveStateManager.Instance != null)
		{
			if (savevalues[0] != SaveStateManager.Instance.GetBudget(LevelManager.Instance.RoleType))
				StartCoroutine(AnimateIcon(BudgetImage, .7f, .5f));
			if (savevalues[1] != SaveStateManager.Instance.GetInfluence(LevelManager.Instance.RoleType))
				StartCoroutine(AnimateIcon(RatingImage, 1.2f, 1f));
			if (savevalues[2] != CellManager.Instance.CurrentFinanceGlobal)
				StartCoroutine(AnimateIcon(FinanceImage, .7f, .5f));
			if (savevalues[3] != CellManager.Instance.CurrentSocialGlobal)
				StartCoroutine(AnimateIcon(SocialImage, .7f, .5f));
			if (savevalues[4] != CellManager.Instance.CurrentEnvironmentGlobal)
				StartCoroutine(AnimateIcon(EnvironmentImage, .7f, .5f));

			BudgetText.text = "" + savevalues[0];
			GlobalFinanceText.text = "" + savevalues[2];
			GlobalSocialText.text = "" + savevalues[3];
			GlobalEnvironmentText.text = "" + savevalues[4];

			savevalues[0] = SaveStateManager.Instance.GetBudget(LevelManager.Instance.RoleType);
			savevalues[1] = SaveStateManager.Instance.GetInfluence(LevelManager.Instance.RoleType);
			savevalues[2] = CellManager.Instance.CurrentFinanceGlobal;
			savevalues[3] = CellManager.Instance.CurrentSocialGlobal;
			savevalues[4] = CellManager.Instance.CurrentEnvironmentGlobal;
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
		NotificationResult.gameObject.SetActive(true);
		NotificationResult.enabled = false;
		ProjectCanvas.gameObject.SetActive(true);
		ProjectCanvas.enabled = false;
		LevelCanvas.enabled = false;
		LevelCanvas.gameObject.SetActive(true);
		LevelButton.gameObject.SetActive(false);
		LevelButton.enabled = false;
		ProjectDesignCanvas.gameObject.SetActive(true);
		ProjectDesignCanvas.enabled = false;
		MenuButton.gameObject.SetActive(true);
		MenuButton.enabled = false;
		NotificationButton.gameObject.SetActive(false);
		NotificationButton.enabled = false;
		ProjectButton.gameObject.SetActive(false);
		ProjectButton.enabled = false;
		GlobalStateCanvas.gameObject.SetActive(true);
		GlobalStateCanvas.enabled = false;
		GlobalStateButton.gameObject.SetActive(false);
		GlobalStateButton.enabled = false;
		CurrentState = state;
		PlayerVariables.SetActive(true);
		EventDisplay.gameObject.SetActive(true);
		EventDisplay.enabled = false;
		EventResult.gameObject.SetActive(true);
		EventResult.enabled = false;
		switch (CurrentState)
		{
			case UiState.Network:
				NetworkCanvas.enabled = true;
				MenuButton.enabled = true;
				PlayerVariables.SetActive(false);
				break;
			case UiState.Role:
				RoleCanvas.enabled = true;
				PlayerVariables.SetActive(false);
				break;
			case UiState.Game:
				if(EventProgressEnabled)
					Event_CurrentProgress.gameObject.SetActive(true);
				if(!EventProgressEnabled)
					Event_CurrentProgress.gameObject.SetActive(false);
				GameCanvas.enabled = true;
				ProjectButton.enabled = true;
				ProjectButton.gameObject.SetActive(true);
				NotificationButton.enabled = true;
				NotificationButton.gameObject.SetActive(true);
				LevelButton.gameObject.SetActive(true);
				LevelButton.enabled = true;
				MenuButton.enabled = true;
				GlobalStateButton.gameObject.SetActive(true);
				GlobalStateButton.enabled = true;
				break;
			case UiState.Level:
				LevelCanvas.enabled = true;
				LevelButton.gameObject.SetActive(true);
				LevelButton.enabled = true;
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
			case UiState.NotificationResult:
				NotificationResult.enabled = true;
				break;
			case UiState.Quest:
				QuestCanvas.enabled = true;
				break;
			case UiState.Result:
				ResultCanvas.enabled = true;
				break;
			case UiState.GlobalState:
				GlobalStateCanvas.enabled = true;
				GlobalStateButton.gameObject.SetActive(true);
				GlobalStateButton.enabled = true;
				MenuButton.enabled = true;
				break;
			case UiState.EventDisplay:
				EventDisplay.enabled = true;
				break;
			case UiState.EventResult:
				EventResult.enabled = true;
				break;
		}
	}
	#region Game UI
	public void GlobalState()
	{
		if (GameCanvas.enabled == true)
		{
			Change(UiState.GlobalState);
		}
		else
		{
			GameUI();
		}
	}

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

	public void LevelUI()
	{
		if (LevelCanvas.enabled == false)
			Change(UiState.Level);
		else
			GameUI();
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
		ProjectButton.image.color = Color.white;
		if (GameCanvas.enabled)
			Change(UiState.Projects);
		else
			Change(UiState.Game);
	}

	public void PlaceProject()
	{
		if (CameraControl.Instance.LastTouchedCell != null)
		{
			CellManager.Instance.NetworkCommunicator.ActivateProject("PlaceProject", CameraControl.Instance.LastTouchedCell.GetComponent<CellLogic>().CellId, LevelManager.Instance.RoleType, ProjectManager.Instance.SelectedProjectId);
			CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Budget", ProjectManager.Instance.GetBudgetInt(ProjectManager.Instance.SelectedProjectId));
			CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Rating", ProjectManager.Instance.GetInfluenceInt(ProjectManager.Instance.SelectedProjectId));
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

	public void DisplayNotificationResult()
	{
		Change(UiState.NotificationResult);
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
		LevelManager.Instance.RoleType = "Environment";
		EventDispatcher.TriggerEvent("EnvironmentMap");
		RoleDescriptionText.text = "You are responsible of environment, green and open space, transportation and mobility";
		Invoke("GameUI", .1f);
		LevelManager.Instance.CreateLevelTemplate();

	}

	public void ChooseFinance()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Finance");
		LevelManager.Instance.RoleType = "Finance";
		EventDispatcher.TriggerEvent("FinanceMap");
		RoleDescriptionText.text = "You are responsible of economy, real estate and industrial development.";
		Invoke("GameUI", .1f);
		LevelManager.Instance.CreateLevelTemplate();
	}

	public void ChooseSocial()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Social");
		LevelManager.Instance.RoleType = "Social";
		EventDispatcher.TriggerEvent("SocialMap");
		RoleDescriptionText.text = "You are responsible of social infrastructure and urban development.";
		LevelManager.Instance.CreateLevelTemplate();
		Invoke("GameUI", .1f);
	}
	
	void UpdateRoleButtons()
	{
		if (SaveStateManager.Instance != null)
		{
			if (SaveStateManager.Instance.GetTaken("Environment"))
			{
				Environment.interactable = false;
			}
			else
			{
				Environment.interactable = true;
			}
			if (SaveStateManager.Instance.GetTaken("Social"))
			{
				Social.interactable = false;
			}
			else
			{
				Social.interactable = true;
			}
			if (SaveStateManager.Instance.GetTaken("Finance"))
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

	IEnumerator AnimateIcon(GameObject icon, float scaleTo, float originalScale)
	{
		iTween.ScaleTo(icon, iTween.Hash("x", scaleTo, "y", scaleTo, "time", .2f));
		yield return new WaitForSeconds(.2f);
		iTween.ScaleTo(icon, iTween.Hash("x", originalScale, "y", originalScale, "time", .2f));
	}

	public void DebugButton()
	{
		//Debug.Log("CLICK");
		//Vote_Choice1();
		//EventManager.Instance.TriggerRandomEvent();
		CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Influence", 10);

		ProjectManager.Instance.CreateRandomProject();
		//ProjectManager.Instance.AddProject();
		//ProjectManager.Instance.ProjectApproved(2);
	}
}