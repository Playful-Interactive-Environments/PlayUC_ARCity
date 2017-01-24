using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MGManager : AManager<MGManager> {

    //main objects & vars
    public Camera MainCam;
    public GameObject MGCam;
    public GameObject MainCanvas;
    public GameObject MGCanvas;
    public Text TimerText;
    public Text ScoreText;
    public float Height;
    public float Width;
    private float _currentTime;
    private float _timeLimit = 20f;
    public bool Started;

    public enum MiniGame
    {
        None, Advertise, Pointer, Sorting, Garbage
    }

    public MiniGame CurrentMG = MiniGame.None;

    //Mini-Game 1 - Advertise Voters
    public GameObject MG_1;
    public List<Vector3> Waypoints = new List<Vector3>();
    public List<Vector3> StartPoints = new List<Vector3>();
    public GameObject Advertisement;
    public GameObject TargetStage;
    public GameObject VoterPrefab;
    private Vector3 startingPos;
    private int _votersNeeded = 20;
    public int VotersCollected;


    void Start () {
        //Find Objects
        MGCam = GameObject.Find("MGCam");
        MainCam = Camera.main;
        MG_1 = GameObject.Find("MG_1");
        MainCanvas = GameObject.Find("Canvas");
        MGCanvas = GameObject.Find("MGCanvas");
        Camera c = MGCam.GetComponent<Camera>();
        c.backgroundColor = Color.white;
        MainGame();
        ObjectPool.CreatePool(VoterPrefab, _votersNeeded);
        //adjust play space to screen res
        c.orthographicSize = Screen.width / 32;
        Height = c.orthographicSize;
        Width = c.orthographicSize;

    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
            DebugMG(MiniGame.Advertise);
        if (Started)
        {
            _currentTime += Time.deltaTime;
            TimerText.text = Mathf.Round(_timeLimit - _currentTime) + "s";
            ScoreText.text = "Voters: " + VotersCollected;
            if (_currentTime >= _timeLimit)
                EndMG();
            if(VotersCollected >= _votersNeeded)
                EndMG();
        }
    }
    void StartMG(MiniGame state)
    {
        Started = true;
        CurrentMG = state;
        switch (state)
        {
            case MiniGame.Advertise:
                InitAdvertiseVoters();
                break;
            case MiniGame.Garbage:
                break;
            case MiniGame.Pointer:
                break;
            case MiniGame.Sorting:
                break;
        }
    }
    
    void InitAdvertiseVoters()
    {

        StartPoints.Add(new Vector3(0, 0, 0));
        StartPoints.Add(new Vector3(-Height, 0, 0));
        StartPoints.Add(new Vector3(Height, 0, 0));
        StartPoints.Add(new Vector3(-Height, -Width, 0));
        StartPoints.Add(new Vector3(0, Width, 0));
        StartPoints.Add(new Vector3(0, -Width, 0));
        Advertisement.transform.position = new Vector3(MGCam.GetComponent<Camera>().pixelWidth, 0, 0);
        TargetStage.transform.position = new Vector3(0, -Height +5f, 0);

        int waypoints = 40;
        for (int i = 0; i <= waypoints; i++)
        {
            Waypoints.Add(new Vector3(Utilities.RandomFloat(-Width, Width), Utilities.RandomFloat(-Height, Height), 0));
        }

        for (int i = 0; i < _votersNeeded; i++)
        {
            startingPos = StartPoints[Utilities.RandomInt(0, StartPoints.Count)];
            ObjectPool.Spawn(VoterPrefab, MG_1.transform, startingPos, Quaternion.identity);
        }
    }

    public void EndMG()
    {
        switch (CurrentMG)
        {
            case MiniGame.Advertise:
                ObjectPool.RecycleAll(VoterPrefab);
                _currentTime = 0;
                VotersCollected = 0;
                StartPoints.Clear();
                Waypoints.Clear();
                Advertisement.GetComponent<Advertisement>().Reset();
                break;
            case MiniGame.Garbage:
                break;
            case MiniGame.Pointer:
                break;
            case MiniGame.Sorting:
                break;
        }
        Started = false;
    }

    public void DebugMG(MiniGame state)
    {
        if (MainCam.gameObject.activeInHierarchy == false)
        {
            MainGame();
            EndMG();
        }
        else
        {
            MiniGameCamera();
            StartMG(state);
        }
    }

    public void BackButton()
    {
        DebugMG(MiniGame.Advertise);    
    }

    void MiniGameCamera()
    {
        MainCam.gameObject.SetActive(false);
        MainCanvas.SetActive(false);
        MGCanvas.SetActive(true);
        MGCam.SetActive(true);
        MG_1.SetActive(true);
        CameraControl.Instance.CurrentCam = MGCam.GetComponent<Camera>();
    }

    void MainGame()
    {
        MainCam.gameObject.SetActive(true);
        MainCanvas.SetActive(true);
        MGCanvas.SetActive(false);
        MGCam.SetActive(false);
        MG_1.SetActive(false);
        CameraControl.Instance.CurrentCam = MainCam;
    }
}
