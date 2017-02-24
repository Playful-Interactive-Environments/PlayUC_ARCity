using UnityEngine;
using System.Collections;
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

	void Awake ()
	{
		ScreenWidth = Screen.width;
		ScreenHeight = Screen.height;
		xEast = 0 + Instance.MapWidth / 2;
		xWest = 0 - Instance.MapWidth / 2;
		yNorth = 0 + Instance.MapHeight / 2;
		ySouth = 0 - Instance.MapHeight / 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
