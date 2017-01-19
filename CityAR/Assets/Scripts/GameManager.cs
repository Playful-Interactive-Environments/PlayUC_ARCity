using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	public static GameManager Instance;
	

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		InvokeRepeating("CreateProject", 0f, 15f);

	}
	
	void Update () {
		
	}

	void CreateProject()
	{
		ProjectManager.Instance.GenerateRandomProject();
	}

	void CreateNewProjects()
	{
		
	}
}
