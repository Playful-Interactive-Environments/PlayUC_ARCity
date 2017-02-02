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
	public int ProjectCSVId;

	void Start()
	{

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
		RatingText.text = "" + ProjectManager.Instance.GetInfluenceString(id);
		BudgetText.text = "" + ProjectManager.Instance.GetBudgetString(id);
	}

	public void SelectProject()
	{
		ProjectManager.Instance.SelectedProjectId = ProjectCSVId;
		UIManager.Instance.ShowPlacementCanvas();
	}
}
