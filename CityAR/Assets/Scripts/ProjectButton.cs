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
    public Image CooldownCover;
	public int ProjectCSVId;
    private float cdTime;
    private float currentTime;
    private bool cdStarted;

    void Start()
	{
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
		Debug.Log("CreateButton" + id);
		ProjectCSVId = id;
		TitleText.text = ProjectManager.Instance.GetTitle(id);
		DescriptionText.text = ProjectManager.Instance.GetContent(id);
		FinanceText.text = "" + ProjectManager.Instance.GetFinanceString(id);
		SocialText.text = "" + ProjectManager.Instance.GetSocialString(id);
		EnvironmentText.text = "" + ProjectManager.Instance.GetEnvironmentString(id);
		RatingText.text = "+" + ProjectManager.Instance.GetInfluenceString(id);
		BudgetText.text = "" + ProjectManager.Instance.GetBudgetString(id);
	    cdTime = ProjectManager.Instance.GetCooldown(id);
	}

	public void SelectProject()
	{
		ProjectManager.Instance.SelectedProjectId = ProjectCSVId;
		UIManager.Instance.ShowPlacementCanvas();
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
}
