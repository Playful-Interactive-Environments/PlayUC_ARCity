﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocalManager : MonoBehaviour
{
	public static LocalManager Instance = null;
	public string RoleType;

	void Awake () {
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

		InvokeRepeating("Refresh", 0f, 1f);
	}

	void Update()
	{

	}
    
	void Refresh()
	{
        if (GlobalManager.Instance != null)
        {
            UIManager.Instance.RatingText.text = "Rating: " + GlobalManager.Instance.GetRating(RoleType);
            UIManager.Instance.BudgetText.text = "Budget: " + GlobalManager.Instance.GetBudget(RoleType);
        }
    }
}
