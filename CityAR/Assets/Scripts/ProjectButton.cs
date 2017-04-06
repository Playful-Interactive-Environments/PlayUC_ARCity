using UnityEngine;
using System.Collections;
using TMPro;
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
	public Image BlockedCover;
	public int ProjectCSVId;
	private string miniGame;
	private float cdTime;
	private float currentTime;
	private bool cdStarted;
	public GameObject DummyPrefab;
	public Button SpawnDummyButton;
	public Button PlayMgButton;


	void Start()
	{
		EventDispatcher.StartListening(Vars.LocalClientDisconnect, NetworkDisconnect);
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
		cdTime = ProjectManager.Instance.GetCooldown(id);
		//cdTime = 1f;

		SetupInteractiveArea();
	}

	void SetupInteractiveArea()
	{
		CooldownText.gameObject.SetActive(false);
		BlockedCover.gameObject.SetActive(false);
		switch (miniGame)
		{
			case Vars.NoMg:
				MGText.gameObject.SetActive(false);
				PlayMgButton.gameObject.SetActive(false);
				MGText.text = "No Tasks.";
				break;
			case Vars.Mg1:
				PlayMgButton.gameObject.SetActive(true);
				MGText.gameObject.SetActive(true);
				MGText.text = "Complete Task to Unlock:\n" + "<color=red>Sorting Documents</color>";
				break;
			case Vars.Mg2:
                PlayMgButton.gameObject.SetActive(true);
				MGText.gameObject.SetActive(true);
				MGText.text = "Complete Task to Unlock:\n" + "<color=red>Gathering Supporters</color>";
				break;
			case Vars.Mg3:
                PlayMgButton.gameObject.SetActive(true);
				MGText.gameObject.SetActive(true);
				MGText.text = "Complete Task to Unlock:\n" + "<color=red>Urban Zoning</color>";
				break;
			default:
				BlockedCover.gameObject.SetActive(false);
				PlayMgButton.gameObject.SetActive(false);
				MGText.gameObject.SetActive(false);
				MGText.text = "";
				break;
		}
	}

	public void PlayMiniGame()
	{
		MGManager.Instance.ProjectCsvId = ProjectCSVId;
		switch (miniGame)
		{
			case Vars.Mg1:
                MGManager.Instance.SwitchState(MGManager.MGState.Mg1);
				break;
			case Vars.Mg2:
                MGManager.Instance.SwitchState(MGManager.MGState.Mg2);
				break;
			case Vars.Mg3:
                MGManager.Instance.SwitchState(MGManager.MGState.Mg3);
				break;
		}
	}

	public void UnlockProject()
	{
		PlayMgButton.gameObject.SetActive(false);
		BlockedCover.gameObject.SetActive(false);
		MGText.gameObject.SetActive(false);
	}

	public void SpawnDummy()
	{
		GameObject gobj = Instantiate(DummyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		gobj.GetComponent<ProjectDummy>().Id_CSV = ProjectCSVId;
		gobj.GetComponent<ProjectDummy>().RepresentationId = ProjectManager.Instance.GetRepresentation(ProjectCSVId); ;
		UIManager.Instance.ShowPlacementCanvas();
	}

	public void LockProject()
	{
		PlayMgButton.gameObject.SetActive(false);
		CooldownText.gameObject.SetActive(true);
		BlockedCover.gameObject.SetActive(true);
		MGText.gameObject.SetActive(false);
		cdStarted = true;
	}

	void ResetButton()
	{
		cdStarted = false;
		currentTime = 0;
		SetupInteractiveArea();
	}

	void NetworkDisconnect()
	{
		if (transform.name != "ProjectTemplate" && transform.name != "ProjectDisplay")
			Destroy(gameObject);
	}
}
