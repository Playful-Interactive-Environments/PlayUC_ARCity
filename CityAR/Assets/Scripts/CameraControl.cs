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
                UIManager.Instance.DebugText.text = mouseHit.transform.name;

                if (mouseHit.transform.name.Equals("HexMesh"))
                {
                    HexGrid.Instance.TouchCell(mouseHit.point);
                    UIManager.Instance.DebugText.text = mouseHit.transform.name;
                }
                if (mouseHit.transform.name.Equals("Build"))
                {
                    CellManager.Instance.NetworkCommunicator.SpawnObject(mouseHit.point);
                    UIManager.Instance.DebugText.text = mouseHit.transform.name;
                }
                if (mouseHit.transform.name.Equals("Show"))
                {
                    mouseHit.transform.GetComponentInParent<CellInterface>().ButtonClick();
                    UIManager.Instance.DebugText.text = mouseHit.transform.name;
                }
            }
        }

#if UNITY_ANDROID
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
}
