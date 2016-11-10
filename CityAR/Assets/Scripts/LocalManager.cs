using UnityEngine;
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

		InvokeRepeating("Refresh", 0f, .1f);
	}

	void Update()
	{

	}
	void Refresh()
	{
		switch (RoleType)
		{
			case "Environment":
				UIManager.Instance.RatingText.text = "Rating: " + GlobalManager.Instance.EnvironmentPlayer.Rating;
				UIManager.Instance.BudgetText.text = "Budget: " + GlobalManager.Instance.EnvironmentPlayer.Budget;
				break;
			case "Social":
				UIManager.Instance.RatingText.text = "Rating: " + GlobalManager.Instance.SocialPlayer.Rating;
				UIManager.Instance.BudgetText.text = "Budget: " + GlobalManager.Instance.SocialPlayer.Budget;
				break;
			case"Finance":
				UIManager.Instance.RatingText.text = "Rating: " + GlobalManager.Instance.FinancePlayer.Rating;
				UIManager.Instance.BudgetText.text = "Budget: " + GlobalManager.Instance.FinancePlayer.Budget;
				break;
		}
	}
}
