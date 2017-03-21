using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Vuforia;


public class ValueManager : AManager<ValueManager>
{

	public float MapWidth = 297f;
	public float MapHeight = 207f;
	public float ScreenWidth;
	public float ScreenHeight;
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
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
