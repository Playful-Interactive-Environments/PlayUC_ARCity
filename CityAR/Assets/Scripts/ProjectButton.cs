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
		TitleText.text = ProjectManager.Instance.GetCSVTitle(id);
		DescriptionText.text = ProjectManager.Instance.GetCSVContent(id);
		FinanceText.text = "" + ProjectManager.Instance.GetCSVFinanceString(id);
		SocialText.text = "" + ProjectManager.Instance.GetCSVSocialString(id);
		EnvironmentText.text = "" + ProjectManager.Instance.GetCSVEnvironmentString(id);
		RatingText.text = "+" + ProjectManager.Instance.GetCSVInfluenceString(id);
		BudgetText.text = "" + ProjectManager.Instance.GetCSVBudgetString(id);
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
		GameObject gobj = Instantiate(DummyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		gobj.GetComponent<ProjectDummy>().Id_CSV = ProjectCSVId;
		gobj.GetComponent<ProjectDummy>().RepresentationId = ProjectManager.Instance.GetRepresentation(ProjectCSVId); ;

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
