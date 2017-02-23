using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectButton : MonoBehaviour {

	public Text TitleText;
	public Text DescriptionText;
	public Text FinanceText;
	public Text SocialText;
	public Text EnvironmentText;
	public Text RatingText;
	public Text BudgetText;
	public Text CooldownText;
	public Text MGText;
	public Image CooldownCover;
	public int ProjectCSVId;
	private string miniGame;
	private float cdTime;
	private float currentTime;
	private bool cdStarted;
	public GameObject DummyPrefab;


	void Start()
	{
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);
		ResetButton();
	}

	void Update()
	{
		if (cdStarted)
		{
			currentTime += Time.deltaTime;
			CooldownText.text = "" + Mathf.Round(cdTime - currentTime);
			if (currentTime >= cdTime)
			{
				ResetButton();
			}
		}
	}

	public void SetupProjectButton(int id)
	{
		ProjectCSVId = id;
		TitleText.text = ProjectManager.Instance.GetTitle(id);
		DescriptionText.text = ProjectManager.Instance.GetContent(id);
		FinanceText.text = "" + ProjectManager.Instance.GetFinanceString(id);
		SocialText.text = "" + ProjectManager.Instance.GetSocialString(id);
		EnvironmentText.text = "" + ProjectManager.Instance.GetEnvironmentString(id);
		RatingText.text = "+" + ProjectManager.Instance.GetInfluenceString(id);
		BudgetText.text = "" + ProjectManager.Instance.GetBudgetString(id);
		miniGame = ProjectManager.Instance.GetMiniGame(id);
		switch (miniGame)
		{
			case "None":
				MGText.text = "No Tasks.";
				break;
			case "Sort":
				MGText.text = "Task: Bureucracy";
				break;
			case "Advertise":
				MGText.text = "Task: Campaigning";
				break;
			case "Area":
				MGText.text = "Task: Planning";
				break;
			default:
				MGText.text = "No Tasks.";
				break;
		}
		cdTime = ProjectManager.Instance.GetCooldown(id);
	}

	public void SelectProject()
	{
		SpawnDummy();
		switch (miniGame)
		{
			case "None":
				UIManager.Instance.ShowPlacementCanvas();
				break;
			case "Sort":
				MGManager.Instance.SwitchState(MGManager.MGState.Sort);
				break;
			case "Advertise":
				MGManager.Instance.SwitchState(MGManager.MGState.Advertise);
				break;
			case "Area":
				MGManager.Instance.SwitchState(MGManager.MGState.Area);
				break;
		}
	}

	void SpawnDummy()
	{
		ProjectManager.Instance.SelectedCSV = ProjectCSVId;
		GameObject gobj = Instantiate(DummyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		ProjectManager.Instance.CurrentDummy = gobj;

	}

	public void ActivateCooldown()
	{
		GetComponent<Button>().interactable = false;
		CooldownText.gameObject.SetActive(true);
		CooldownCover.gameObject.SetActive(true);
		cdStarted = true;
	}

	void ResetButton()
	{
		cdStarted = false;
		currentTime = 0;
		GetComponent<Button>().interactable = true;
		CooldownText.gameObject.SetActive(false);
		CooldownCover.gameObject.SetActive(false);

	}
	void NetworkDisconnect()
	{
		if (transform.name != "ProjectTemplate" && transform.name != "ProjectInfo")
			Destroy(gameObject);
	}
}
