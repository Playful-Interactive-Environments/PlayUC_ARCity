using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    RaycastHit hitCenter;
    private UIManager _uiManager;
    public bool CarryingObject;
    private GameObject _carriedObject;
    private GridPieceLogic _gridLogic;
    private MessageCommunicator _messageCommunicator;
    string objectTag;
    void Start()
    {

        _uiManager = UIManager.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity))
            {
                HexGrid.Instance.TouchCell(mouseHit.point);
                if (mouseHit.transform.tag.Equals("GridPiece"))
                {
                    Debug.Log(mouseHit.transform.name);
                }
            }
        }

#if UNITY_ANDROID
        Ray rayCenter = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2.0f, Screen.height/2.0f, 0f));
        if (ObjectManager.Instance != null && ObjectManager.Instance.LocalPlayerObject != null)
            _messageCommunicator = ObjectManager.Instance.LocalPlayerObject.GetComponent<MessageCommunicator>();

        if (Physics.Raycast(rayCenter, out hitCenter, Mathf.Infinity))
        {
            objectTag = hitCenter.transform.tag;

            if (objectTag == "Object")
            {
                //_uiManager.DebugText.text = hitCenter.transform.tag;
                if(!CarryingObject)
                    _carriedObject = hitCenter.transform.gameObject;
            }
            if (objectTag == "GridPiece")
            {
                _gridLogic = hitCenter.transform.gameObject.GetComponent<GridPieceLogic>();
               // _uiManager.DebugText.text = "P:" + _gridLogic.PollutionRate + " U: " + _gridLogic.UnemploymentRate; 
                if(CarryingObject)
                    _carriedObject.transform.position = hitCenter.transform.position;
            }
        }

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Ray rayTouch = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                   
                    if (Physics.Raycast(rayTouch, out hit, Mathf.Infinity))
                    {
                        
                        UIManager.Instance.DebugText.text = hit.transform.name;
                        
                    }
                }
               
            }
        }
#endif

    }
    
    public void Grab()
    {
        if (!CarryingObject && objectTag == "Object")
        {
            _uiManager.EnterPlacementState();
            CarryingObject = true;
            _messageCommunicator.ObjectTaken(_carriedObject);
        }
    }
    public void Drop()
    {
        if (!_gridLogic.Occupied && CarryingObject)
        {
            //Invoke("SendMessage", .1f);
            _messageCommunicator.ObjectPlaced(_carriedObject, _gridLogic.gameObject);
            CarryingObject = false;
            _carriedObject = null;
            _uiManager.ExitPlacementState();
        }        
    }

    void SendMessage()
    {

    }

}
