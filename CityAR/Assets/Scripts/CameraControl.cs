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
    private float tChange;

    void Start()
    {
        CurrentCam = MainCamera;
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
        if (Input.GetMouseButtonDown(0))
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
                _interface.CurrentTextState = CellInterface.TextState.Grey;
            }
        }
        foreach (Project project in FindObjectsOfType<Project>())
        {
            project.TransparentOff();
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
        foreach (Project project in FindObjectsOfType<Project>())
        {
                project.TransparentOff();
                project.HideLogo();
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
                _interface.CurrentTextState = CellInterface.TextState.White;
            }
        }
        foreach (Project project in FindObjectsOfType<Project>())
        {
            //project.TransparentOn();
        }
    }

    void TrackDistance()
    {
        cameraDistance = Vector3.Distance(ARCamera.transform.position,
                new Vector3(ARCamera.transform.position.x, 0, ARCamera.transform.position.z));
        UIManager.Instance.DebugText.text = cameraDistance + "";
        tChange += Time.deltaTime;

        if (tChange > .2f && ProjectManager.Instance != null)
        {
            if (cameraDistance < 100f)
            {
                ShowDetails();
            }
            if (cameraDistance >= 100f && cameraDistance <= 200f)
            {
                ShowAll();
            }
            if (cameraDistance > 200f)
            {
                HideAll();
            }
            tChange = 0;
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
