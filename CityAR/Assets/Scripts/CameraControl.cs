using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : AManager<CameraControl>
{
    public GameObject ARCamera;
    public Camera MainCamera;
    public Camera MGCamera;
    public Camera CurrentCam;
    public GameObject LastTouchedCell;
    public Vector3 LastTouchedVector;
    private Ray cameraRay;
    private RaycastHit cameraHit;
    private float cameraDistance;
    public LayerMask cameraLayer;
    private int viewNum = 1;
    public bool CanTouch;
    void Start()
    {
        CurrentCam = MainCamera;
        CanTouch = true;
    }

    public Vector3 GetLastCell()
    {
        cameraRay = CurrentCam.ScreenPointToRay
       (new Vector3(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, 0));
        if (Physics.Raycast(cameraRay, out cameraHit, Mathf.Infinity, cameraLayer))
        {
            LastTouchedVector = cameraHit.point;
            if (cameraHit.transform.tag.Equals("Cell"))
            {
                LastTouchedCell = cameraHit.transform.gameObject;
                LastTouchedCell.GetComponent<CellLogic>().TouchCell();
            }
        }
        return LastTouchedVector;
    }

    void Update()
    {
        //animate transparent color
        TrackDistance();
        if (Input.GetMouseButtonDown(0) && CanTouch)
        {
            Ray mouseRay = CurrentCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(0))
            {
                if (mouseHit.transform.tag.Equals("Quest"))
                {
                    UIManager.Instance.QuestUI(mouseHit.transform.gameObject.GetComponent<Quest>());
                }
                if (mouseHit.transform.tag.Equals("Project"))
                {
                    mouseHit.transform.gameObject.GetComponent<Project>().ShowProjectCanvas();
                }
            }
        }
    }

    #region Distance Tracking
    public void ShowAll()
    {
        foreach (GameObject cell in CellGrid.Instance.GridCells)
        {
            CellInterface _interface = cell.GetComponent<CellInterface>();
            if (_interface.CurrentTextState != CellInterface.TextState.Changes)
            {
                _interface.ChangeCellDisplay(CellInterface.InterfaceState.Grey);
                _interface.ChangeCellText(CellInterface.TextState.Grey);
            }
        }
    }

    public void HideAll()
    {
        foreach (GameObject cell in CellGrid.Instance.GridCells)
        {
            CellInterface _interface = cell.GetComponent<CellInterface>();
            if (_interface.CurrentTextState != CellInterface.TextState.Changes)
            {
                cell.GetComponent<CellInterface>().ChangeCellDisplay(CellInterface.InterfaceState.Hide);
            }
        }
    }

    public void ShowDetails()
    {
        foreach (GameObject cell in CellGrid.Instance.GridCells)
        {
            CellInterface _interface = cell.GetComponent<CellInterface>();
            if (_interface.CurrentTextState != CellInterface.TextState.Changes)
            {
                _interface.ChangeCellDisplay(CellInterface.InterfaceState.White);
                _interface.ChangeCellText(CellInterface.TextState.White);
            }
        }
    }

    void TrackDistance()
    {
        cameraDistance = Vector3.Distance(ARCamera.transform.position,
                new Vector3(ARCamera.transform.position.x, 0, ARCamera.transform.position.z));
        //blend view 0 1 and 2
        if (ProjectManager.Instance != null)
        {
            if (cameraDistance < 100f && viewNum == 1)
            {
                ShowDetails();
                viewNum = 0;
            }
            if (cameraDistance >= 100f && cameraDistance <= 200f && (viewNum == 0) || (viewNum == 2))
            {
                viewNum = 1;
                ShowAll();
            }
            if (cameraDistance > 200f && viewNum == 1)
            {
                viewNum = 2;
                HideAll();
            }
        }
    }
#endregion
}

/*  
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
                       if (hit.transform.tag.Equals("Quest"))
                       {
                           UIManager.Instance.QuestUI(hit.transform.gameObject.GetComponent<Quest>());
                       }
                       if (hit.transform.tag.Equals("Project"))
                       {
                           hit.transform.gameObject.GetComponent<Project>().ShowProjectCanvas();
                       }
                   }
               }
           }
       }
#endif*/
