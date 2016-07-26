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
