using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : AManager<UIManager>
{
    public ProjectManager Projects;
    public Quest CurrentQuest;
    public enum UiState
    {
        Network, Role, Game, Projects, Placement, Quest, Result, GlobalState, Level, GameEnd
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
    public Canvas GameEndCanvas;

    [Header("Role Buttons")]
    public Button Environment;
    public Button Finance;
    public Button Social;
    public Text FinancePlayers;
    public Text SocialPlayers;
    public Text EnvironmentPlayers;

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
    Vector3 hiddenPos = new Vector3(-100, 1000, 0);

    [Header("PLAYER VARIABLES")]
    public GameObject PlayerVariables;
    public GameObject FinanceImage;
    public GameObject EnvironmentImage;
    public GameObject SocialImage;
    public GameObject RatingImage;
    public GameObject BudgetImage;
    public GameObject RoleHighlight;
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
    [Header("Game End Display Groups")]
    public GameObject EndStateStart;
    public GameObject GlobalEndState;
    public GameObject PlayerAchievements;
    public GameObject PlayerStats;
    [Header("Global Achievements")]
    public TextMeshProUGUI GameEndExtraText;
    public TextMeshProUGUI GameEndResult;
    public TextMeshProUGUI TimePlayedN;
    public TextMeshProUGUI SuccessfulProjectN;
    public TextMeshProUGUI TotalAddedValueN;
    public TextMeshProUGUI MostImprovedFieldN;
    public TextMeshProUGUI LeastImprovedFieldN;
    public Image GameEndResultImage;
    public Image MostImprovedFieldImage;
    public Image LeastImprovedFieldImage;
    [Header("Player Achievements")]
    public Image MostSuccessfulProjects;
    public Image MostMoneySpent;
    public Image HighestInfluence;
    public Image MostWinMiniGames;
    public Image LeastTimeMiniGames;
    public TextMeshProUGUI MostSuccessfulProjectsN;
    public TextMeshProUGUI MostMoneySpentN;
    public TextMeshProUGUI HighestInfluenceN;
    public TextMeshProUGUI MostWinMiniGamesN;
    public TextMeshProUGUI LeastTimeMiniGamesN;
    [Header("Personal Stats")]
    public TextMeshProUGUI ProjectsProposed;
    public TextMeshProUGUI ProjectsSuccessful;
    public TextMeshProUGUI ProjectsFailed;
    public TextMeshProUGUI ProjectsVotedApprove;
    public TextMeshProUGUI ProjectsVotedDeny;
    public TextMeshProUGUI QuestsCompleted;

    [Header("OTHER ELEMENTS")]
    public Button MenuButton;
    public Button GlobalStateButton;
    public Button GameEndPrevButton;
    public Button GameEndNextButton;
    public Button GameEndNextRestartButton;
    public GameObject AnimateText;
    public GameObject WaitingPlayers;
    [HideInInspector]
    public Vector2 BudgetTextPos;
    public Text RoleDescriptionText;
    public Text DebugText;
    public Text GameDebugText;
    public Text WaitingText;
    public Text FinDebugTxt;
    public Text SocDebugTxt;
    public Text EnvDebugTxt;

    public Sprite DefaultSprite;
    public Sprite ApproveSprite;
    public Sprite DenySprite;
    public Sprite ExclamationSprite;
    public Sprite QuestionSprite;
    public Sprite Player3_winSprite;
    public Sprite Player2_winSprite;
    public Sprite Player1_winSprite;
    public Sprite YouWin;
    private int[] savevalues = new int[5]; //array that stores values from previous update

    void Awake()
    {
        //Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 2;
    }
    void Start()
    {
        ResetMenus();
        InvokeRepeating("RefreshPlayerVars", .2f, .5f);
        GameEndNextRestartButton.gameObject.SetActive(false);
        ObjectPool.CreatePool(AnimateText, 5);
        BudgetTextPos = BudgetText.gameObject.GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        //UpdateRoleButtons();
        if (ProjectManager.Instance != null)
            Projects = ProjectManager.Instance;
    }

    void RefreshPlayerVars()
    {
        if (SaveStateManager.Instance != null)
        {
            if (savevalues[0] != SaveStateManager.Instance.GetBudget(LocalManager.Instance.RoleType))
                StartCoroutine(AnimateIcon(BudgetImage, .7f, .5f));
            if (savevalues[1] != SaveStateManager.Instance.GetInfluence(LocalManager.Instance.RoleType))
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
            savevalues[0] = SaveStateManager.Instance.GetBudget(LocalManager.Instance.RoleType);
            savevalues[1] = SaveStateManager.Instance.GetInfluence(LocalManager.Instance.RoleType);
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
        MenuButton.enabled = true;
        ProjectButton.gameObject.SetActive(false);
        ProjectButton.enabled = false;
        GlobalStateCanvas.gameObject.SetActive(true);
        GlobalStateCanvas.enabled = false;
        GlobalStateButton.gameObject.SetActive(false);
        GlobalStateButton.enabled = false;
        GameEndCanvas.gameObject.SetActive(false);
        GameEndCanvas.enabled = false;
        PlayerVariables.SetActive(true);
        CurrentState = state;
        switch (CurrentState)
        {
            case UiState.Network:
                NetworkCanvas.enabled = true;
                PlayerVariables.SetActive(false);
                if (NetMng.Instance.isNetworkActive)
                    LocalManager.Instance.NetworkCommunicator.SetPlayerState("Network");

                break;
            case UiState.Role:
                RoleCanvas.enabled = true;
                PlayerVariables.SetActive(false);
                MenuButton.gameObject.SetActive(false);
                if (NetMng.Instance.isNetworkActive)
                    LocalManager.Instance.NetworkCommunicator.SetPlayerState("Role");
                break;
            case UiState.Game:
                GameCanvas.enabled = true;
                ProjectButton.enabled = true;
                ProjectButton.gameObject.SetActive(true);
                GlobalStateButton.gameObject.SetActive(true);
                GlobalStateButton.enabled = true;
                LocalManager.Instance.NetworkCommunicator.SetPlayerState("Game");
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
            case UiState.Quest:
                LocalManager.Instance.NetworkCommunicator.SetPlayerState("Quest");
                QuestCanvas.enabled = true;
                break;
            case UiState.Result:
                ResultCanvas.enabled = true;
                break;
            case UiState.GlobalState:
                GlobalStateCanvas.enabled = true;
                GlobalStateButton.gameObject.SetActive(true);
                GlobalStateButton.enabled = true;
                break;
            case UiState.GameEnd:
                PlayerVariables.gameObject.SetActive(false);
                GameEndCanvas.gameObject.SetActive(true);
                GameEndCanvas.enabled = true;
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
        else if (NetworkCanvas.enabled && LocalManager.Instance.GameRunning)
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
            LocalManager.Instance.PushGrid();
        }
        else
        {
            GameUI();
        }
    }

    public void RestartApp()
    {
        NetMng.Instance.StopHosting();
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
            case Vars.Player1:
                num = 4;
                break;
            case Vars.Player2:
                num = 3;
                break;
            case Vars.Player3:
                num = 2;
                break;
            case Vars.MainValue1:
                num = 1;
                break;
            case Vars.MainValue2:
                num = 0;
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

        if (CameraControl.Instance.LastTouchedCell != null && ProjectManager.Instance.CurrentDummy.CanPlace &&
            SaveStateManager.Instance.GetBudget(LocalManager.Instance.RoleType) >=
            Mathf.Abs(ProjectManager.Instance.GetBudgetInt(ProjectManager.Instance.CurrentDummy.Id_CSV)))
        {
            PlacementText.text = "";

            LocalManager.Instance.NetworkCommunicator.ActivateProject
            ("CreateProject", CameraControl.Instance.LastTouchedCell.GetComponent<CellLogic>().CellId,
                ProjectManager.Instance.CurrentDummy.CurrentPos,
                ProjectManager.Instance.CurrentDummy.CurrentRot,
                LocalManager.Instance.RoleType,
                ProjectManager.Instance.CurrentDummy.Id_CSV);
            CancelPlacement();
            LocalManager.Instance.NetworkCommunicator.SetGlobalState(Vars.DiscussionStart);
        }
        else
        {
            PlacementText.text = "Not enough funding!";
        }
    }

    public void CancelPlacement()
    {
        if (ProjectManager.Instance.CurrentDummy != null)
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
    IEnumerator AnimateProjectButton()
    {
        yield return new WaitForSeconds(1f);
    }
    #endregion

    #region Choose Roles
    public void ChooseEnvironment()
    {
        LocalManager.Instance.NetworkCommunicator.TakeRole(Vars.Player3, "connect");
        LocalManager.Instance.RoleType = Vars.Player3;
        GameManager.Instance.TriggerGlobal(GameManager.Instance.GlobalState);
        EventDispatcher.TriggerEvent("EnvironmentMap");
        RoleDescriptionText.text = "\u2022 You are responsible for environment, green and open space, " +
                                   "transportation and mobility.\n\u2022 The value represents the number of open" +
                                   " spaces in your city.\n\u2022 The higher the amount, the higher the quality of air becomes.";
        LocalManager.Instance.CreateLevelTemplate();
        RoleHighlight.GetComponent<RectTransform>().anchoredPosition =
            EnvironmentImage.GetComponent<RectTransform>().anchoredPosition;
        Invoke("GameUI", .1f);
    }

    public void ChooseFinance()
    {
        LocalManager.Instance.NetworkCommunicator.TakeRole(Vars.Player1, "connect");
        LocalManager.Instance.RoleType = Vars.Player1;
        GameManager.Instance.TriggerGlobal(GameManager.Instance.GlobalState);
        EventDispatcher.TriggerEvent("FinanceMap");
        RoleDescriptionText.text = "\u2022 You are responsible for economy, real estate and industrial development." +
                                   "\n\u2022 The value represents value in Millions of Euros.\n\u2022" +
                                   " More financial projects incrase the wealth of your city.";
        LocalManager.Instance.CreateLevelTemplate();
        RoleHighlight.GetComponent<RectTransform>().anchoredPosition =
            FinanceImage.GetComponent<RectTransform>().anchoredPosition;
        Invoke("GameUI", .1f);
    }

    public void ChooseSocial()
    {
        LocalManager.Instance.NetworkCommunicator.TakeRole(Vars.Player2, "connect");
        LocalManager.Instance.RoleType = Vars.Player2;
        EventDispatcher.TriggerEvent("SocialMap");
        GameManager.Instance.TriggerGlobal(GameManager.Instance.GlobalState);
        RoleDescriptionText.text = "\u2022 You are responsible for social infrastructure and urban development." +
                                   "\n\u2022 The value represents the amount of employed people.\n\u2022 " +
                                   "More social projects increase employment and social stability.";
        LocalManager.Instance.CreateLevelTemplate();
        RoleHighlight.GetComponent<RectTransform>().anchoredPosition =
            SocialImage.GetComponent<RectTransform>().anchoredPosition;
        Invoke("GameUI", .1f);
    }

    void UpdateRoleButtons()
    {
        if (SaveStateManager.Instance != null)
        {
            if (SaveStateManager.Instance.GetTaken(Vars.Player3))
            {
                Environment.interactable = false;
            }
            else
            {
                Environment.interactable = true;
            }
            if (SaveStateManager.Instance.GetTaken(Vars.Player2))
            {
                Social.interactable = false;
            }
            else
            {
                Social.interactable = true;
            }
            if (SaveStateManager.Instance.GetTaken(Vars.Player1))
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


    #region GameEnd
    public void DisplayEndStates(string state)
    {
        EndStateStart.gameObject.SetActive(false);
        GlobalEndState.gameObject.SetActive(false);
        PlayerAchievements.gameObject.SetActive(false);
        PlayerStats.gameObject.SetActive(false);
        switch (state)
        {
            case "EndStateStart":
                EndStateStart.gameObject.SetActive(true);
                GameEndPrevButton.gameObject.SetActive(false);
                break;
            case "GlobalEndState":
                GameEndPrevButton.gameObject.SetActive(true);
                GlobalEndState.gameObject.SetActive(true);
                break;
            case "PlayerAchievements":
                GameEndNextButton.gameObject.SetActive(true);
                PlayerAchievements.gameObject.SetActive(true);
                break;
            case "PlayerStats":
                GameEndNextButton.gameObject.SetActive(false);
                PlayerStats.gameObject.SetActive(true);
                GameEndNextRestartButton.gameObject.SetActive(true);
                break;
        }
    }

    public void DisplayNext()
    {
        if (EndStateStart.activeInHierarchy)
        {
            DisplayEndStates("GlobalEndState");
            return;
        }

        if (GlobalEndState.activeInHierarchy)
        {
            DisplayEndStates("PlayerAchievements");
            return;
        }

        if (PlayerAchievements.activeInHierarchy)
        {
            DisplayEndStates("PlayerStats");
            return;
        }
    }

    public void DisplayPrev()
    {
        if (PlayerStats.activeInHierarchy)
        {
            DisplayEndStates("PlayerAchievements");
            return;
        }
        if (PlayerAchievements.activeInHierarchy)
        {
            DisplayEndStates("GlobalEndState");
            return;
        }
        if (GlobalEndState.activeInHierarchy)
        {
            DisplayEndStates("EndStateStart");
            return;
        }
    }

    #endregion
    public void CreateText(Color color, string txt, int size, float move, float anim, Vector2 animStartPos, Vector2 animEndPos)
    {
        GameObject textObject = ObjectPool.Spawn(AnimateText);
        textObject.GetComponent<AnimateText>().Init(color, txt, size, move, anim, animStartPos, animEndPos);
    }

    public void ResetMenus()
    {
        Change(UiState.Network);
        Finance.interactable = true;
        Social.interactable = true;
        Environment.interactable = true;
        HideProjectDisplay();
        HideDiscussionPanel();
    }

    IEnumerator AnimateIcon(GameObject icon, float scaleTo, float originalScale)
    {
        iTween.ScaleTo(icon, iTween.Hash("x", scaleTo, "y", scaleTo, "time", .2f));
        yield return new WaitForSeconds(.2f);
        iTween.ScaleTo(icon, iTween.Hash("x", originalScale, "y", originalScale, "time", .2f));
    }

    public void Debug_2()
    {
        MGManager.Instance.SwitchState(MGManager.MGState.Mg2);
    }

    public void Debug_3()
    {
        MGManager.Instance.SwitchState(MGManager.MGState.Mg3);
    }
    public void Debug_4()
    {
        GameManager.Instance.CurrentTime = Vars.Instance.GameEndTime;
    }
    public void Debug_1()
    {
        MGManager.Instance.SwitchState(MGManager.MGState.Mg1);
    }

    public void Debug()
    {
        //Debug.Log("CLICK");
        //Vote_Choice1();
        //EventManager.Instance.TriggerRandomEvent();
        LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, "Influence", 50);
        //MGManager.Instance.DebugMG(MGManager.MiniGame.Sorting);
        //ProjectManager.Instance.CreateRandomProject();
        //ProjectManager.Instance.AddProject();
        //ProjectManager.Instance.ProjectApproved(2);
    }
}