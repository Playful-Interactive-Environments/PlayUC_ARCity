using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MGManager : AManager<MGManager> {

    public MiniGame CurrentMG = MiniGame.None;
    public enum MiniGame
    {
        None, Advertise, Pointer, Sorting, Area
    }

    //main objects & vars
    public Camera MainCam;
    public GameObject MGCam;
    public GameObject MainCanvas;
    public GameObject MGCanvas;
    public Text TimerText;
    public Text ScoreText;
    public Text WinStateText;

    public float Height;
    public float Width;
    private float _currentTime;
    private float _timeLimit;
    public bool Started;

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
        MGCam.GetComponent<Camera>().backgroundColor = Color.white;
        MGCam.GetComponent<Camera>().orthographicSize = Screen.width / 32;
        Height = MGCam.GetComponent<Camera>().orthographicSize * 2;
        Width = Height / Screen.height * Screen.width;
        MGStart(MiniGame.None);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
            DebugButton();
        if (Started)
        {
            _currentTime += Time.deltaTime;
            TimerText.text = Mathf.Round(_timeLimit - _currentTime) + "s";
            switch (CurrentMG)
            {
                case MiniGame.Advertise:
                    ScoreText.text = "Audience: " + MG_2_Mng.VotersCollected;
                    if (_currentTime >= _timeLimit)
                    {
                        WinStateText.text = "GAME LOST!";
                        EndMG();
                    }

                    if (MG_2_Mng.VotersCollected >= MG_2_Mng.VotersNeeded)
                    {
                        WinStateText.text = "TIME RAN OUT!";
                        EndMG();
                    }
                    break;
                case MiniGame.Area:
                    ScoreText.text = "Confiscated: " + MG_3_Mng.CurrentPercent + "/" + MG_3_Mng.PercentNeeded + " %";
                    if (_currentTime >= _timeLimit)
                    {
                        WinStateText.text = "TIME RAN OUT!";
                        EndMG();
                    }
                    if (MG_3_Mng.CurrentPercent >= MG_3_Mng.PercentNeeded)
                    {
                        WinStateText.text = "YOU WIN!";
                        EndMG();
                    }
                    break;
                case MiniGame.Sorting:
                    ScoreText.text = "Sorted: " + MG_1_Mng.CollectedDocs;
                    if (_currentTime >= _timeLimit)
                    {
                        WinStateText.text = "TIME RAN OUT!";
                        EndMG();
                    }
                    if (MG_3_Mng.CurrentPercent >= MG_3_Mng.PercentNeeded)
                    {
                        WinStateText.text = "YOU WIN!";
                        EndMG();
                    }
                    break;
            }
        }
    }

    void MGStart(MiniGame state)
    {
        Started = true;
        CurrentMG = state;
        MainCam.gameObject.SetActive(false);
        MainCanvas.SetActive(false);
        MGCanvas.SetActive(false);
        MGCam.SetActive(false);
        MG_1_GO.SetActive(false);
        MG_2_GO.SetActive(false);
        MG_3_GO.SetActive(false);
        CameraControl.Instance.CurrentCam = MGCam.GetComponent<Camera>();
        WinStateText.text = "";
        switch (state)
        {
            case MiniGame.Advertise:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                MG_2_Mng.SetVars(40, 20, 30);
                _timeLimit = MG_2_Mng.TimeLimit;
                MG_2_Mng.InitGame();
                MG_2_GO.SetActive(true);
                break;
            case MiniGame.Pointer:
                break;
            case MiniGame.Sorting:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                _timeLimit = MG_1_Mng.TimeLimit;
                MG_1_GO.SetActive(true);
                MG_1_Mng.InitGame();
                break;
            case MiniGame.Area:
                MGCanvas.SetActive(true);
                MGCam.SetActive(true);
                _timeLimit = MG_3_Mng.TimeLimit;
                MG_3_GO.SetActive(true);
                MG_3_Mng.InitGame();
                break;
            case MiniGame.None:
                MainCam.gameObject.SetActive(true);
                MainCanvas.SetActive(true);
                CameraControl.Instance.CurrentCam = MainCam;
                break;
        }
    }

    public void EndMG()
    {
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
            case MiniGame.Sorting:
                _currentTime = 0;
                MG_3_Mng.ResetGame();
                break;
        }
        Started = false;
    }

    public void DebugMG(MiniGame state)
    {
        if (MainCam.gameObject.activeInHierarchy == false)
        {
            EndMG();
            MGStart(MiniGame.None);
        }
        else
        {
            MGStart(state);
        }
    }
    
    public void DebugButton()
    {
        DebugMG(MiniGame.Sorting);    
    }
}