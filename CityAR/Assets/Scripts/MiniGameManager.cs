using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour {

    public Camera MainCam;
    public GameObject MiniGameCam;
    public GameObject MainCanvas;
    public GameObject MiniGameObjects;
    void Start () {
        MiniGameCam = GameObject.Find("MiniGameCam");
        MainCam = Camera.main;
        MainCanvas = GameObject.Find("Canvas");
        MiniGameObjects = GameObject.Find("MiniGameObjects");
        Camera c = MiniGameCam.GetComponent<Camera>();
        c.orthographicSize = Screen.currentResolution.width / 16;
        c.backgroundColor = Color.white;
        MainCamera();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (MainCam.gameObject.activeInHierarchy == false)
                MainCamera();
            else
                MiniGameCamera();
        }
    }

    void MiniGameCamera()
    {
        MainCam.gameObject.SetActive(false);
        MainCanvas.SetActive(false);
        MiniGameCam.SetActive(true);
        MiniGameObjects.SetActive(true);
        CameraControl.Instance.CurrentCam = MiniGameCam.GetComponent<Camera>();
    }

    void MainCamera()
    {
        MainCam.gameObject.SetActive(true);
        MainCanvas.SetActive(true);
        MiniGameCam.SetActive(false);
        MiniGameObjects.SetActive(false);
        CameraControl.Instance.CurrentCam = MainCam.GetComponentInChildren<Camera>();
    }
}
