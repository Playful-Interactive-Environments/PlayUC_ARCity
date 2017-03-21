using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MGManager : AManager<MGManager> {

    public MGState CurrentMG = MGState.None;
    public enum MGState
    {
        None, Advertise, Pointer, Sort, Area
    }

    //main objects & vars
    public Camera MainCam;
    public GameObject MGCam;
    public GameObject MainCanvas;
    public GameObject MGCanvas;
    public GameObject ObjectContainer;
    public Text TimerText;
    public Text ScoreText;
    public Text WinStateText;
    public Text GameDescription;
    public int ProjectCsvId;
    //vars
    public float targetAspect = 9f/16f;
    private float scaleFactor = 32f;
    public float Height;
    public float Width;
    private float _currentTime;
    private float _timeLimit;
    public bool Started;
    private float _resetTime = 3f;
    //MiniGames
    public GameObject MG_1_GO;
    public MG_1 MG_1_Mng;
    public GameObject MG_2_GO;
    public MG_2 MG_2_Mng;
    public GameObject MG_3_GO;
    public MG_3 MG_3_Mng;


    void Start () {
        /*Find Cameras & Canvas
        MGCam = GameObject.Find("MGCam");
        MainCam = Camera.main;
        MG_1_GO = GameObject.Find("MG_1");
        MG_1_Mng = MG_1.Instance;
        MG_2_GO = GameObject.Find("MG_2");
        MG_2_Mng = MG_2.Instance;
        MG_3_GO = GameObject.Find("MG_3");
        MG_3_Mng = MG_3.Instance;
        MainCanvas = GameObject.Find("Canvas");
        MGCanvas = GameObject.Find("MGCanvas");
        ObjectContainer = GameObject.Find("ImageTarget");
         */

        //adjust play space & cam
        Camera cam = MGCam.GetComponent<Camera>();
        cam.backgroundColor = Color.white;
        cam.orthographicSize = 80f;
        Height = cam.orthographicSize * 2;
        Width = Height*cam.aspect;
        EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);

        Invoke("Init", .1f);
    }


    void Init()
    {
        ChangeState(MGState.None);
    }
    void Update () {
        TrackProgress();
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeState(MGState.Advertise);
        }
    }

    void TrackProgress()
    {
        if (Started)
        {
            switch (CurrentMG)
            {
                case MGState.Advertise:
                    //update UI
                    _currentTime += Time.deltaTime;
                    TimerText.text = Mathf.Round(_timeLimit - _currentTime) + "s";
                    ScoreText.text = "Audience: " + MG_2_Mng.VotersCollected + "/" + MG_2_Mng.VotersNeeded;
                    //check win/lose state
                    if (_currentTime >= _timeLimit)
                    {
                        StartCoroutine(EndMG("lose", _resetTime));
                    }

                    if (MG_2_Mng.VotersCollected >= MG_2_Mng.VotersNeeded)
                    {
                        CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Mg2Win", 0);
                        StartCoroutine(EndMG("win", _resetTime));
                    }
                    break;
                case MGState.Area:
                    _currentTime += Time.deltaTime;
                    TimerText.text = Mathf.Round(_timeLimit - _currentTime) + "s";
                    ScoreText.text = "Land: " + MG_3_Mng.CurrentPercent + "/" + MG_3_Mng.PercentNeeded + " %";

                    if (_currentTime >= _timeLimit)
                    {
                        StartCoroutine(EndMG("lose", _resetTime));
                    }
                    if (MG_3_Mng.CurrentPercent >= MG_3_Mng.PercentNeeded)
                    {
                        CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Mg3Win", 0);
                        StartCoroutine(EndMG("win", _resetTime));
                    }
                    break;
                case MGState.Sort:
                    _currentTime += Time.deltaTime;
                    TimerText.text = Mathf.Round(_timeLimit - _currentTime) + "s";
                    ScoreText.text = "Sorted: " + MG_1_Mng.CollectedDocs + "/" + MG_1_Mng.DocsNeeded;

                    if (_currentTime >= _timeLimit)
                    {
                        StartCoroutine(EndMG("lose", _resetTime));
                    }
                    if (MG_1_Mng.CollectedDocs >= MG_1_Mng.DocsNeeded)
                    {
                        CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "Mg1Win", 0);
                        StartCoroutine(EndMG("win", _resetTime));
                    }
                    break;
            }
        }
    }

    void ChangeState(MGState state)
    {
        //Reset all minigames and get proper text & variables
        CurrentMG = state;
        MainCam.gameObject.SetActive(false);
        ObjectContainer.SetActive(false);
        MainCanvas.SetActive(false);
        MGCanvas.SetActive(false);
        MGCam.SetActive(false);
        MG_1_GO.SetActive(false);
        MG_2_GO.SetActive(false);
        MG_3_GO.SetActive(false);
        MG_1_Mng.ResetGame();
        MG_2_Mng.ResetGame();
        MG_3_Mng.ResetGame();
        CameraControl.Instance.CurrentCam = MGCam.GetComponent<Camera>();
        WinStateText.text = "";
        _currentTime = 0;
        Started = true;
        switch (state)
        {
            case MGState.Advertise:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                _timeLimit = MG_2_Mng.TimeLimit;
                MG_2_Mng.StartCoroutine("InitGame");
                MG_2_GO.SetActive(true);
                GameDescription.text = "Drag the megaphone to attract voters to the stage!";
                CellManager.Instance.NetworkCommunicator.SetPlayerState(LevelManager.Instance.RoleType, "MiniGame");
                break;
            case MGState.Pointer:
                break;
            case MGState.Sort:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                _timeLimit = MG_1_Mng.TimeLimit;
                MG_1_GO.SetActive(true);
                MG_1_Mng.InitGame();
                GameDescription.text = "Sort the documents on the correct stack!";
                CellManager.Instance.NetworkCommunicator.SetPlayerState(LevelManager.Instance.RoleType, "MiniGame");
                break;
            case MGState.Area:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                _timeLimit = MG_3_Mng.TimeLimit;
                MG_3_GO.SetActive(true);
                MG_3_Mng.InitGame();
                GameDescription.text = "Draw a straight line to block off land for your project!";
                CellManager.Instance.NetworkCommunicator.SetPlayerState(LevelManager.Instance.RoleType, "MiniGame");
                break;
            case MGState.None:
                Started = false;
                ObjectContainer.SetActive(true);
                MainCam.gameObject.SetActive(true);
                MainCanvas.SetActive(true);
                CameraControl.Instance.CurrentCam = MainCam;
                if(CellManager.Instance!=null)
                    CellManager.Instance.NetworkCommunicator.SetPlayerState(LevelManager.Instance.RoleType, "Game");
                break;
        }
    }

    public IEnumerator EndMG(string state, float time)
    {
        Started = false;
        if (state == "win")
        {
            CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "MgTime", Mathf.RoundToInt(_currentTime));
            WinStateText.text = "You made it. Great job! Resuming in " + time + "s";
        }
        if (state == "lose")
        {
            CellManager.Instance.NetworkCommunicator.UpdateData(LevelManager.Instance.RoleType, "MgFail", 0);
            WinStateText.text = "You failed. Try again later. Resuming in " + time + "s";
        }
        switch (CurrentMG)
        {
            case MGState.Advertise:
                _currentTime = 0;
                MG_2_Mng.ResetGame();
                break;
            case MGState.Area:
                _currentTime = 0;
                MG_3_Mng.ResetGame();
                break;
            case MGState.Pointer:
                break;
            case MGState.Sort:
                _currentTime = 0;
                MG_1_Mng.ResetGame();
                break;
        }
        yield return new WaitForSeconds(time);
        if (state == "reset")
        {
            ChangeState(MGState.None);
            yield break;
        }

        if (state == "win")
        {
            ProjectManager.Instance.UnlockButton(ProjectCsvId);
        }
        ChangeState(MGState.None);
    }

    public void SwitchState(MGState state)
    {
        if (MainCam.gameObject.activeInHierarchy == false)
        {
            StartCoroutine(EndMG("reset", .1f));
        }
        else
        {
            ChangeState(state);
        }
    }

    public void BackButton()
    {
        SwitchState(MGState.None);
    }

    void NetworkDisconnect()
    {
        ChangeState(MGState.None);
    }
}