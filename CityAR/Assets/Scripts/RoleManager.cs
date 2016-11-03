using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class RoleManager : NetworkBehaviour
{
	public static RoleManager Instance = null;
	public NetworkCommunicator EnvironmentPlayer;
	public NetworkCommunicator SocialPlayer;
	public NetworkCommunicator FinancePlayer;

	[SyncVar]
	public bool Environment;
	[SyncVar]
	public bool Finance;
	[SyncVar]
	public bool Social;
	public string RoleType;

	public int Rating;
	public int Budget;

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
		UIManager.Instance.RatingText.text = "Rating: " + Rating;
		UIManager.Instance.BudgetText.text = "Budget: " + Budget;
	}

	void Refresh()
	{
		if (isServer)
		{
			if (EnvironmentPlayer == null)
				Environment = false;
			if (SocialPlayer == null)
				Social = false;
			if (FinancePlayer == null)
				Finance = false;
		}
	}
}
