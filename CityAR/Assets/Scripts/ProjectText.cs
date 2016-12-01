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

	void Update()
	{

	}

	public void SetText(int id)
	{
		Debug.Log("CreateButton" + id);

		TitleText.text = QuestManager.Instance.GetTitle(id);
		DescriptionText.text = QuestManager.Instance.GetContent(id);
		FinanceText.text = "" + QuestManager.Instance.GetFinanceString(id);
		SocialText.text = "" + QuestManager.Instance.GetSocialString(id);
		EnvironmentText.text = "" + QuestManager.Instance.GetEnvironmentString(id);
		RatingText.text = "" + QuestManager.Instance.GetRatingString(id);
		BudgetText.text = "" + QuestManager.Instance.GetBudgetString(id);

	}
}
