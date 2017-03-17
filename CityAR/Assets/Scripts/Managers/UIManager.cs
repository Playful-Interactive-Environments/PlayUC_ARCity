using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : AManager<UIManager>
{
	public ProjectManager Projects;
    public Quest CurrentQuest;
	public enum UiState
	{
		Network, Role, Game, Projects, Placement, Quest, Result, GlobalState, Level
	}

	public UiState CurrentState;
    [Header("CANVAS STATES")]
    public Canvas GameCanvas;
	public Canvas NetworkCanvas;
	public Canvas RoleCanvas;
	public Canvas QuestCanvas;
	public Canvas ResultCanvas;
	public Canvas ProjectCanvas;
	public Canvas PlacementCanvas;
	public Canvas GlobalStateCanvas;
	public Canvas LevelCanvas;

    [Header("Role Buttons")]
    public Button Environment;
	public Button Finance;
	public Button Social;

    [Header("QUEST UI")]
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

    [Header("PLAYER VARIABLES")]
    public GameObject PlayerVariables;
	public GameObject FinanceImage;
	public GameObject EnvironmentImage;
	public GameObject SocialImage;
	public GameObject RatingImage;
	public GameObject BudgetImage;
    public Text RatingText;
	public Text BudgetText;
	public Text TimeText;
	public Text GlobalFinanceText;
	public Text GlobalEnvironmentText;
	public Text GlobalSocialText;

    [Header("PROJECTS")]
    public GameObject ProjectDisplay;
	public GameObject Discussion;
    public GameObject InfoScreen;
    public GameObject InfoIcon;
    public TextMeshProUGUI InfoText;
    public Text PlacementText;
	public Button ProjectButton;

    [Header("OTHER ELEMENTS")]
    public Button MenuButton;
    public Button GlobalStateButton;
	public Text RoleDescriptionText;
    public Text DebugText;
    public Sprite DefaultSprite;
    public Sprite ApproveSprite;
    public Sprite DenySprite;
    public Sprite ExclamationSprite;
    public Sprite QuestionSprite;
    private int[] savevalues = new int[5]; //array that stores values from previous update

    void Awake()
	{
		Application.targetFrameRate = 30;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	void Start ()
	{
		ResetMenus();
		InvokeRepeating("RefreshPlayerVars", .2f, .5f);
	}

	void Update ()
	{
		UpdateRoleButtons();
		if (ProjectManager.Instance != null)
			Projects = ProjectManager.Instance;
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
		PlacementCanvas.gameObject.SetActive(true);
		PlacementCanvas.enabled = false;    
		ProjectCanvas.gameObject.SetActive(true);
		ProjectCanvas.enabled = false;
		LevelCanvas.enabled = false;
		LevelCanvas.gameObject.SetActive(true);
		MenuButton.gameObject.SetActive(true);
		MenuButton.enabled = false;
		ProjectButton.gameObject.SetActive(false);
		ProjectButton.enabled = false;
		GlobalStateCanvas.gameObject.SetActive(true);
		GlobalStateCanvas.enabled = false;
		GlobalStateButton.gameObject.SetActive(false);
		GlobalStateButton.enabled = false;
		PlayerVariables.SetActive(true);
		CurrentState = state;
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
				GameCanvas.enabled = true;
				ProjectButton.enabled = true;
				ProjectButton.gameObject.SetActive(true);
				MenuButton.enabled = true;
				GlobalStateButton.gameObject.SetActive(true);
				GlobalStateButton.enabled = true;
				CellManager.Instance.NetworkCommunicator.SetPlayerState(LevelManager.Instance.RoleType, "Game");
				break;
			case UiState.Level:
				LevelCanvas.enabled = true;
				break;
			case UiState.Projects:
				ProjectCanvas.enabled = true;
				ProjectButton.enabled = true;
				ProjectButton.gameObject.SetActive(true);
				break;
			case UiState.Placement:
				PlacementCanvas.enabled = true;
				break;
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

	public void GameUI()
	{
		Change(UiState.Game);
	}

	public void LevelUI()
	{
		if (LevelCanvas.enabled == false)
		{
			Change(UiState.Level);
			LevelManager.Instance.PushGrid();
		}
		else
		{
			GameUI();
		}
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
		Choice1.text = "" + CurrentQuest.Choice1;
		Choice2.text = "" + CurrentQuest.Choice2;
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
		ResultText[5].GetComponent<RectTransform>().localPosition = textPositions[3];
		ResultText[5].text = "" + value;
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
			case "Environment":
				num = 2;
				break;
			case "Social":
				num = 3;
				break;
			case "Finance":
				num = 4;
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
		if (CameraControl.Instance.LastTouchedCell != null && ProjectManager.Instance.CurrentDummy.CanPlace)
		{
			CellManager.Instance.NetworkCommunicator.ActivateProject
				("CreateProject", CameraControl.Instance.LastTouchedCell.GetComponent<CellLogic>().CellId,
				ProjectManager.Instance.CurrentDummy.CurrentPos, 
				ProjectManager.Instance.CurrentDummy.CurrentRot, 
				LevelManager.Instance.RoleType, 
				ProjectManager.Instance.CurrentDummy.Id_CSV);
			CancelPlacement();

			CellManager.Instance.NetworkCommunicator.SetPlayerState(LevelManager.Instance.RoleType, "DiscussionStart");
		}
	}

	public void CancelPlacement()
	{
		if(ProjectManager.Instance.CurrentDummy != null)
			ProjectManager.Instance.CurrentDummy.DestroySelf();
		GameUI();
	}

	public void ShowPlacementCanvas()
	{
		Change(UiState.Placement);
	}

	public void ShowProjectDisplay()
	{
		ProjectDisplay.GetComponent<ProjectInfo>().ProjectCSVId = ProjectManager.Instance.SelectedProject.ID_Spawn;
		ProjectDisplay.GetComponent<Animator>().SetBool("Show", true);
	}

	public void HideProjectDisplay()
	{
		ProjectDisplay.GetComponent<Animator>().SetBool("Show", false);
	}

	public void ShowDiscussionPanel()
	{
		DiscussionManager.Instance.SetupDiscussion(ProjectManager.Instance.SelectedProject.ID_Spawn);
		Discussion.GetComponent<Animator>().SetBool("Show", true);
	}

	public void HideDiscussionPanel()
	{
		Discussion.GetComponent<Animator>().SetBool("Show", false);
		DiscussionManager.Instance.Reset();
	}

    public void ShowInfoScreen()
    {
        StartCoroutine(AnimateInfoScreen());
    }

    IEnumerator AnimateInfoScreen()
    {
        InfoScreen.GetComponent<Animator>().SetBool("Show", true);
        yield return new WaitForSeconds(5f);
        InfoScreen.GetComponent<Animator>().SetBool("Show", false);
    }
    #endregion

    #region Choose Roles
    public void ChooseEnvironment()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Environment");
		LevelManager.Instance.RoleType = "Environment";
		EventDispatcher.TriggerEvent("EnvironmentMap");
		RoleDescriptionText.text = "\u2022 You are responsible for environment, green and open space, transportation and mobility.\n\u2022 The value represents the number of open spaces in your city.\n\u2022 The higher the amount, the higher the quality of air becomes.";
		Invoke("GameUI", .1f);
		LevelManager.Instance.CreateLevelTemplate();

	}

	public void ChooseFinance()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Finance");
		LevelManager.Instance.RoleType = "Finance";
		EventDispatcher.TriggerEvent("FinanceMap");
		RoleDescriptionText.text = "\u2022 You are responsible for economy, real estate and industrial development.\n\u2022 The value represents value in Millions of Euros.\n\u2022 More financial projects incrase the wealth of your city.";
		Invoke("GameUI", .1f);
		LevelManager.Instance.CreateLevelTemplate();
	}

	public void ChooseSocial()
	{
		CellManager.Instance.NetworkCommunicator.TakeRole("Social");
		LevelManager.Instance.RoleType = "Social";
		EventDispatcher.TriggerEvent("SocialMap");
		RoleDescriptionText.text = "\u2022 You are responsible for social infrastructure and urban development.\n\u2022 The value represents the amount of employed people.\n\u2022 More social projects increase employment and social stability.";
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

	public void Debug_2()
	{
		MGManager.Instance.SwitchState(MGManager.MGState.Advertise);
	}
	public void Debug_3()
	{
		MGManager.Instance.SwitchState(MGManager.MGState.Area);
	}
	public void Debug_1()
	{
		MGManager.Instance.SwitchState(MGManager.MGState.Sort);
	}

	public void Debug()
	{
		//Debug.Log("CLICK");
		//Vote_Choice1();
		//EventManager.Instance.TriggerRandomEvent();
		CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Influence", 50);
		//MGManager.Instance.DebugMG(MGManager.MiniGame.Sorting);
		//ProjectManager.Instance.CreateRandomProject();
		//ProjectManager.Instance.AddProject();
		//ProjectManager.Instance.ProjectApproved(2);
	}
}