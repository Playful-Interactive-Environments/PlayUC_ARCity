using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class LocalManager : MonoBehaviour
{
	public static LocalManager Instance = null;
	public string RoleType; 

	void Awake ()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		Application.targetFrameRate = 30;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	void Update()
	{

	}
	

}