using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vuforia;


public class ValueManager : AManager<ValueManager>
{

	public float MapWidth = 297f;
	public float MapHeight = 207f;
	public float ScreenWidth;
	public float ScreenHeight;
    public float UiHeight;
    public float UiWidth;
	public static float xEast;
	public static float xWest;
	public static float yNorth;
	public static float ySouth;
	public Canvas MainCanvas;
	public EventSystem EventSystem;


	void Awake ()
	{
		ScreenWidth = Screen.width;
		ScreenHeight = Screen.height;
		xEast = 0 + MapWidth / 2;
		xWest = 0 - MapWidth / 2;
		yNorth = 0 + MapHeight / 2;
		ySouth = 0 - MapHeight / 2;
		EventSystem.pixelDragThreshold = Mathf.RoundToInt(20 * MainCanvas.scaleFactor);
        UiHeight = MGManager.Instance.MainCanvas.GetComponent<CanvasScaler>().referenceResolution.y;
        UiWidth = MGManager.Instance.MainCanvas.GetComponent<CanvasScaler>().referenceResolution.x;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
