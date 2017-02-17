using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MGManager : AManager<MGManager> {

    public MiniGame CurrentMG = MiniGame.None;
    public enum MiniGame
    {
        None, Advertise, Pointer, Sort, Area
    }

    //main objects & vars
    public Camera MainCam;
    public GameObject MGCam;
    public GameObject MainCanvas;
    public GameObject MGCanvas;
    public Text TimerText;
    public Text ScoreText;
    public Text WinStateText;
    public Text GameDescription;
    //vars
    public float targetAspect = 9f/16f;
    private float scaleFactor = 32f;
    public float Height;
    public float Width;
    private float _currentTime;
    private float _timeLimit;
    public bool Started;
    private float _resetTime = 3f;
    private float timer;
    //MiniGames
    public GameObject MG_1_GO;
    public MG_1 MG_1_Mng;
    public GameObject MG_2_GO;
    public MG_2 MG_2_Mng;
    public GameObject MG_3_GO;
    public MG_3 MG_3_Mng;


    void Start () {
        //Find Cameras & Canvas
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
        StartMG(MiniGame.None);
    }
    void Update () {
        TrackProgress();
    }

    void TrackProgress()
    {
        if (Started)
        {
            switch (CurrentMG)
            {
                case MiniGame.Advertise:
                    //update UI
                    _currentTime = MG_2_Mng.TimeSpent;
                    TimerText.text = Mathf.Round(_timeLimit - _currentTime) + "s";
                    ScoreText.text = "Audience: " + MG_2_Mng.VotersCollected + "/" + MG_2_Mng.VotersNeeded;
                    //check win/lose state
                    if (_currentTime >= _timeLimit)
                    {
                        StartCoroutine(EndMG("lose", _resetTime));
                    }

                    if (MG_2_Mng.VotersCollected >= MG_2_Mng.VotersNeeded)
                    {
                        StartCoroutine(EndMG("win", _resetTime));
                    }
                    break;
                case MiniGame.Area:
                    _currentTime += Time.deltaTime;
                    TimerText.text = Mathf.Round(_timeLimit - _currentTime) + "s";
                    ScoreText.text = "Land: " + MG_3_Mng.CurrentPercent + "/" + MG_3_Mng.PercentNeeded + " %";

                    if (_currentTime >= _timeLimit)
                    {
                        StartCoroutine(EndMG("lose", _resetTime));
                    }
                    if (MG_3_Mng.CurrentPercent >= MG_3_Mng.PercentNeeded)
                    {
                        WinStateText.text = "You made it! Good job!";
                        StartCoroutine(EndMG("win", _resetTime));
                    }
                    break;
                case MiniGame.Sort:
                    _currentTime += Time.deltaTime;
                    TimerText.text = Mathf.Round(_timeLimit - _currentTime) + "s";
                    ScoreText.text = "Sorted: " + MG_1_Mng.CollectedDocs + "/" + MG_1_Mng.DocsNeeded;

                    if (_currentTime >= _timeLimit)
                    {
                        StartCoroutine(EndMG("lose", _resetTime));
                    }
                    if (MG_1_Mng.CollectedDocs >= MG_1_Mng.DocsNeeded)
                    {
                        StartCoroutine(EndMG("win", _resetTime));
                    }
                    break;
            }
        }
    }

    void StartMG(MiniGame state)
    {
        //Reset all minigames and get proper text & variables
        CurrentMG = state;
        MainCam.gameObject.SetActive(false);
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
            case MiniGame.Advertise:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                _timeLimit = MG_2_Mng.TimeLimit;
                MG_2_Mng.StartCoroutine("InitGame");
                MG_2_GO.SetActive(true);
                GameDescription.text = "Drag the megaphone to attract voters to the stage!";
                break;
            case MiniGame.Pointer:
                break;
            case MiniGame.Sort:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                _timeLimit = MG_1_Mng.TimeLimit;
                MG_1_GO.SetActive(true);
                MG_1_Mng.InitGame();
                GameDescription.text = "Sort the documents on the correct stack!";
                break;
            case MiniGame.Area:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                _timeLimit = MG_3_Mng.TimeLimit;
                MG_3_GO.SetActive(true);
                MG_3_Mng.InitGame();
                GameDescription.text = "Draw a straight line to block off land for your project!";
                break;
            case MiniGame.None:
                Started = false;
                MainCam.gameObject.SetActive(true);
                MainCanvas.SetActive(true);
                CameraControl.Instance.CurrentCam = MainCam;
                break;
        }
    }

    public IEnumerator EndMG(string state, float time)
    {

        if (state == "win")
        {
            WinStateText.text = "You made it. Great job! Resuming in " + time + "s";
        }
        if (state == "lose")
        {
            WinStateText.text = "You failed. Try again later. Resuming in " + time + "s";
        }
        switch (CurrentMG)
        {
            case MiniGame.Advertise:
                _currentTime = 0;
                MG_2_Mng.ResetGame();
                break;
            case MiniGame.Area:
                _currentTime = 0;
                MG_3_Mng.ResetGame();
                break;
            case MiniGame.Pointer:
                break;
            case MiniGame.Sort:
                _currentTime = 0;
                MG_1_Mng.ResetGame();
                break;
        }
        Started = false;
        yield return new WaitForSeconds(time);
        if (state == "reset")
        {
            StartMG(MiniGame.None);
            UIManager.Instance.GameUI();
            yield break;
        }
        timer = 0;
        if (state == "win")
            UIManager.Instance.ShowPlacementCanvas();
        if (state == "lose")
            UIManager.Instance.GameUI();
        StartMG(MiniGame.None);
    }

    public void SwitchState(MiniGame state)
    {
        if (MainCam.gameObject.activeInHierarchy == false)
        {
            StartCoroutine(EndMG("reset", .1f));
        }
        else
        {
            StartMG(state);
        }
    }

    public void BackButton()
    {
        SwitchState(MiniGame.None);
    }

    void NetworkDisconnect()
    {
        StartMG(MiniGame.None);
    }
}