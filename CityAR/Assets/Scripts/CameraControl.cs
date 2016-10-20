using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{

    RaycastHit hitCenter;
    private UIManager _uiManager;
    public bool CarryingObject;
    private GameObject _carriedObject;
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
                    _uiManager.DebugText.text = mouseHit.transform.name;
                }
                if (mouseHit.transform.name.Equals("Build"))
                {
                    CellManager.Instance.NetworkCommunicator.SpawnObject(mouseHit.point);
                    _uiManager.DebugText.text = mouseHit.transform.name;
                }
                if (mouseHit.transform.name.Equals("Show"))
                {
                    mouseHit.transform.GetComponentInParent<CellInterface>().ButtonClick();
                    _uiManager.DebugText.text = mouseHit.transform.name;
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

                        _uiManager.DebugText.text = hit.transform.name;

                    }
                }
            }
        }
#endif

    }
}
