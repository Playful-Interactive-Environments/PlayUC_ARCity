using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 0.2f)]
public class GameManager : NetworkBehaviour
{

    //states: "Game", "MiniGame", "NewStart", "DiscussionStart", "DiscussionEnd", "Occupied"} ;
    [SyncVar]
    public float CurrentTime;
    [SyncVar]
    public int ClientsConnected;
    public string GlobalState;
    [SyncVar]
    public int SocialPlayers;
    [SyncVar]
    public int FinancePlayers;
    [SyncVar]
    public int EnvironmentPlayers;

    public string MyState;
    private int currentEvent;
    public float eventTime;
    public static GameManager Instance;
    private UIManager UiM;
    private SaveStateManager SaveData;
    //animate placement mat
    private float lerpVal;
    public Material placementMat;



    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        EventDispatcher.StartListening(Vars.LocalClientDisconnect, LocalClientDisconnect);
        EventDispatcher.StartListening(Vars.ServerHandleDisconnect, ServerHandleDisconnect);
        InvokeRepeating("CheckWinState", 0, 1f);
        UiM = UIManager.Instance;
        SaveData = SaveStateManager.Instance;
        LocalManager.Instance.GameRunning = true;
        CameraControl.Instance.Invoke("ShowAll", 1f);
    }

    void Update()
    {
        if (isServer && ClientsConnected >= Vars.Instance.MinPlayers)
        {
            CurrentTime += Time.deltaTime;
        }
        if (ClientsConnected < 0)
        {
            ClientsConnected = 0;
        }
        UIManager.Instance.GameDebugText.text = "\n" + GlobalState;

        UiM.FinancePlayers.text = "" + FinancePlayers;
        UiM.SocialPlayers.text = "" + SocialPlayers;
        UiM.EnvironmentPlayers.text = "" + EnvironmentPlayers;

        UiM.TimeText.text = Utilities.DisplayTime(CurrentTime);
        lerpVal = Mathf.PingPong(Time.time, 1f) / 1f;
        placementMat.color = new Color(lerpVal, lerpVal, lerpVal, .5f);
    }

    void LocalClientDisconnect()
    {
        if (MyState != "GameEnd")
        {
            GlobalState = Vars.DiscussionEnd;
        }
    }

    void ServerHandleDisconnect()
    {
        if (MyState != "GameEnd")
        {
            if (isServer)
            {
                LocalManager.Instance.NetworkCommunicator.SetGlobalState(Vars.DiscussionEnd);
            }
        }
    }

    #region GameEnd
    void CheckWinState()
    {
        //TIME END
        if (CurrentTime >= Vars.Instance.GameEndTime)
        {
            UiM.GameEndResult.text = TextManager.Instance.TimeWinText;
            UiM.GameEndResultImage.sprite = UiM.YouWin;
            CalculateAchievements();
        }
        //UTOPIA END
        if (CellManager.Instance.CurrentSocialGlobal >= Vars.Instance.UtopiaRate &&
            CellManager.Instance.CurrentEnvironmentGlobal >= Vars.Instance.UtopiaRate &&
            CellManager.Instance.CurrentFinanceGlobal >= Vars.Instance.UtopiaRate)
        {
            UiM.GameEndResult.text = TextManager.Instance.UtopiaWinText;
            UiM.GameEndResultImage.sprite = UiM.YouWin;
            CalculateAchievements();
        }
        //MAYOR END
        /*
        foreach (SaveStateManager.PlayerStats player in SaveData.Players)
        {
            if (player.Rank == Vars.Instance.MayorLevel)
            {
                UiM.GameEndResult.text = TextManager.Instance.MayorWinText;
                UiM.GameEndExtraText.text = TextManager.Instance.MayorAnnounceText;

                if (player.Player == LocalManager.Instance.RoleType)
                {
                    UiM.GameEndResultImage.sprite = UiM.YouWin;
                }
                else
                {
                    switch (player.Player)
                    {
                        case Vars.Player1:
                            UiM.GameEndResultImage.sprite = UiM.Player1_winSprite;
                            break;
                        case Vars.Player2:
                            UiM.GameEndResultImage.sprite = UiM.Player2_winSprite;
                            break;
                        case Vars.Player3:
                            UiM.GameEndResultImage.sprite = UiM.Player3_winSprite;
                            break;
                    }
                }
                CalculateAchievements();
            
        }}*/
    }

    void CalculateAchievements()
    {
        //End Game
        LocalManager.Instance.NetworkCommunicator.SetPlayerState("GameEnd");
        EventDispatcher.TriggerEvent("GameEnd");
        CancelInvoke("CheckWinState");
        StopAllCoroutines();
        LocalManager.Instance.GameRunning = false;
        UiM.DisplayEndStates("EndStateStart");
        UiM.Change(UIManager.UiState.GameEnd);
        //Calculate Global End State Vars
        UiM.TimePlayedN.text = Utilities.DisplayTime(CurrentTime);
        UiM.SuccessfulProjectN.text = "" + SaveData.GetAllSucessful("SuccessfulProjectN");
        UiM.TotalAddedValueN.text = "" + CellManager.Instance.GetTotalAddedValue();
        UiM.MostImprovedFieldN.text = "" + CellManager.Instance.GetMostImprovedValue();
        UiM.LeastImprovedFieldN.text = "" + CellManager.Instance.GetLeastImprovedValue();
        CellManager.Instance.GetValueImages();
        //Calculate Player Achievements
        SaveData.CalculateAchievement("MostSuccessfulProjects");
        SaveData.CalculateAchievement("MostMoneySpent");
        SaveData.CalculateAchievement("HighestInfluence");
        SaveData.CalculateAchievement("MostWinMiniGames");
        SaveData.CalculateAchievement("LeastTimeMiniGames");
        //Calculate Personal Stats
        SaveData.CalculatePersonalStats();
    }
    #endregion

    #region Discussion State
    void CheckMyState()
    {
        if (MyState == "GameEnd")
            StopAllCoroutines();
    }

    IEnumerator StartDiscussion()
    {
        //if user is occupied wait until they finish
        while (MyState != "Game")
        {
            CheckMyState();
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.1f);
        UIManager.Instance.Change(UIManager.UiState.Game);
        UiM.ShowProjectDisplay();
        UiM.ShowDiscussionPanel();
        UiM.ShowInfoScreen();
    }

    IEnumerator EndDiscussion()
    {
        StopCoroutine(StartDiscussion());
        while (MyState != "Game")
        {
            CheckMyState();
            yield return new WaitForSeconds(.1f);
        }
        UiM.GameUI();
        yield return new WaitForSeconds(1f);
        UiM.HideProjectDisplay();
        UiM.HideDiscussionPanel();
    }

    public void SetGlobalState(string state)
    {
        GlobalState = state;
        TriggerGlobal(state);
    }

    public void TriggerGlobal(string global)
    {
        switch (global)
        {
            case Vars.DiscussionStart:
                StartCoroutine(StartDiscussion());
                break;
            case Vars.DiscussionEnd:
                StartCoroutine(EndDiscussion());
                break;
        }
    }
    #endregion
}