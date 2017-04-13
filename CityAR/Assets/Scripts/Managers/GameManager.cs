using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
[NetworkSettings(channel = 3, sendInterval = 0.1f)]
public class GameManager : NetworkBehaviour
{

    //states: "Game", "MiniGame", "NewStart", "DiscussionStart", "DiscussionEnd", "Occupied"} ;
    [SyncVar]
    public string EnvironmentState;
    [SyncVar]
    public string FinanceState;
    [SyncVar]
    public string SocialState;
    [SyncVar]
    public float CurrentTime;
    [SyncVar]
    public int ClientsConnected;

    private string MyState;
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
        CameraControl.Instance.Invoke("ShowAll",1f);
    }

    void LocalClientDisconnect()
    {
        if (MyState != "GameEnd")
        {
            ResetDiscussion();
        }
    }

    void ServerHandleDisconnect()
    {
        if (MyState != "GameEnd")
        {
            ResetDiscussion();
            if (isServer)
                LocalManager.Instance.NetworkCommunicator.SendEvent(Vars.ServerHandleDisconnect);
        }
    }

    public void ResetDiscussion()
    {
        UiM.HideProjectDisplay();
        UiM.HideDiscussionPanel();
        StopAllCoroutines();
    }

    void Update()
    {
        if (isServer && ClientsConnected >= Vars.Instance.MinPlayers)
        {
            CurrentTime += Time.deltaTime;
        }
        if (ClientsConnected < Vars.Instance.MinPlayers && UiM.CurrentState != UIManager.UiState.Network &&
            UiM.CurrentState != UIManager.UiState.Role)
        {
            UIManager.Instance.WaitingPlayers.SetActive(true);
            UIManager.Instance.WaitingText.text = "Waiting for players... " + ClientsConnected + "/3";
        }
        if (ClientsConnected >= Vars.Instance.MinPlayers)
        {
            UIManager.Instance.WaitingPlayers.SetActive(false);
        }
        UiM.TimeText.text = Utilities.DisplayTime(CurrentTime);
        lerpVal = Mathf.PingPong(Time.time, 1f) / 1f;
        placementMat.color = new Color(lerpVal, lerpVal, lerpVal, .5f);
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
            }
        }
    }

    void CalculateAchievements()
    {
        //End Game
        LocalManager.Instance.NetworkCommunicator.SetPlayerState(LocalManager.Instance.RoleType, "GameEnd");
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
        if (LocalManager.Instance.RoleType == Vars.Player1)
            MyState = FinanceState;
        if (LocalManager.Instance.RoleType == Vars.Player2)
            MyState = SocialState;
        if (LocalManager.Instance.RoleType == Vars.Player3)
            MyState = EnvironmentState;
        if (MyState == "GameEnd")
            StopAllCoroutines();
    }

    IEnumerator StartDiscussion()
    {
        //if user is occupied wait until they finish
        CheckMyState();
        while (MyState == "MiniGame")
        {
            CheckMyState();
            yield return new WaitForSeconds(.5f);
        }
        while (MyState == "Quest")
        {
            CheckMyState();
            yield return new WaitForSeconds(.5f);
        }
        EventDispatcher.TriggerEvent("StartDiscussion");
        yield return new WaitForSeconds(.1f);
        UIManager.Instance.Change(UIManager.UiState.Game);
        UiM.ShowProjectDisplay();
        UiM.ShowDiscussionPanel();
        UiM.ShowInfoScreen();
    }

    IEnumerator EndDiscussion()
    {
        yield return new WaitForSeconds(3f);
        UiM.HideProjectDisplay();
        UiM.HideDiscussionPanel();
        yield return new WaitForSeconds(1f);
        UiM.GameUI();
    }

    public void SetState(string player, string state)
    {
        if (isServer)
        {
            switch (player)
            {
                case Vars.Player1:
                    FinanceState = state;
                    break;
                case Vars.Player2:
                    SocialState = state;
                    break;
                case Vars.Player3:
                    EnvironmentState = state;
                    break;
            }
        }
        if (isClient)
        {
            switch (state)
            {
                case "DiscussionStart":
                    StartCoroutine(StartDiscussion());
                    break;
                case "DiscussionEnd":
                    StartCoroutine(EndDiscussion());
                    break;
            }
        }
    }
    #endregion
}
