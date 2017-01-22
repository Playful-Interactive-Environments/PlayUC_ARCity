﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : AManager<CameraControl>
{
    public Camera CurrentCam;
    RaycastHit hitCenter;
    private UIManager _uiManager;
    public bool CarryingObject;
    private GameObject _carriedObject;
    string objectTag;
    //public HexCell LastTouchedCell;
    public GameObject LastTouchedCell;


    void Start()
    {
        _uiManager = UIManager.Instance;
        CurrentCam = Camera.main;
    }

    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = CurrentCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1))
            {
               // _uiManager.DebugText.text = mouseHit.transform.name;
                if (mouseHit.transform.tag.Equals("Cell"))
                {
                    mouseHit.transform.gameObject.GetComponent<CellLogic>().TouchCell();
                    LastTouchedCell = mouseHit.transform.gameObject;
                    //LastTouchedCell = HexGrid.Instance.GetCell(mouseHit.point);
                    //HexGrid.Instance.TouchCell(mouseHit.point);
                }
                if (mouseHit.transform.name.Equals("Build"))
                {
                    //CellManager.Instance.NetworkCommunicator.SpawnObject(LastTouchedPos);
                }
                if (mouseHit.transform.name.Equals("Quest"))
                {
                    UIManager.Instance.QuestUI(mouseHit.transform.gameObject.GetComponent<Quest>());
                }
                if (mouseHit.transform.name.Equals("Project"))
                {
                   mouseHit.transform.gameObject.GetComponent<Project>().ShowProjectCanvas();
                }
            }
        }

#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray rayTouch = CurrentCam.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                   
                    if (Physics.Raycast(rayTouch, out hit, Mathf.Infinity) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
                    {

                        _uiManager.DebugText.text = hit.transform.name;
                        if (hit.transform.tag.Equals("Cell"))
                        {
                            hit.transform.gameObject.GetComponent<CellLogic>().TouchCell();
                            LastTouchedCell = hit.transform.gameObject;
                            //LastTouchedCell = HexGrid.Instance.GetCell(hit.point);
                            //HexGrid.Instance.TouchCell(hit.point);
                        }
                        if (hit.transform.name.Equals("Build"))
                        {
                            //CellManager.Instance.NetworkCommunicator.SpawnObject(LastTouchedPos);
                        }
                        if (hit.transform.name.Equals("Quest"))
                        {
                            UIManager.Instance.QuestUI(hit.transform.gameObject.GetComponent<Quest>());
                        }
                        if (hit.transform.name.Equals("Project"))
                        {
                            hit.transform.gameObject.GetComponent<Project>().ShowProjectCanvas();
                        }
                    }
                }
            }
        }
#endif
    }
}
