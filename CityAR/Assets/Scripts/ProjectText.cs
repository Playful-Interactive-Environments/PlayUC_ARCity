using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectText : MonoBehaviour {

	public Text TitleText;
	public Text DescriptionText;
	public Text FinanceText;
	public Text SocialText;
	public Text EnvironmentText;
	public Text RatingText;
	public Text BudgetText;

	void Start()
	{
    }

    public void SetText(int id)
	{
		Debug.Log("CreateButton" + id);

		TitleText.text = ProjectManager.Instance.GetTitle(id);
		DescriptionText.text = ProjectManager.Instance.GetContent(id);
		FinanceText.text = "" + ProjectManager.Instance.GetFinanceString(id);
		SocialText.text = "" + ProjectManager.Instance.GetSocialString(id);
		EnvironmentText.text = "" + ProjectManager.Instance.GetEnvironmentString(id);
		RatingText.text = "" + ProjectManager.Instance.GetRatingString(id);
		BudgetText.text = "" + ProjectManager.Instance.GetBudgetString(id);

	}
}
