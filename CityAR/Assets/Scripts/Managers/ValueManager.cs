using UnityEngine;
using System.Collections;
using Vuforia;

public class ValueManager : AManager<ValueManager>
{

    public float MapWidth = 297f;
    public float MapHeight = 207f;
    public float ScreenWidth;
    public float ScreenHeight;
	void Start ()
	{
	    ScreenWidth = Screen.width;
	    ScreenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
