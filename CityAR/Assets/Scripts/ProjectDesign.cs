using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectDesign : MonoBehaviour {

	public InputField TitleInput;
	public InputField DescriptionInput;
	public Text FinanceText;
	public Text EnvironmentText;
	public Text SocialText;
	public Text RatingText;
	public Text BudgetText;
	public Text ComponentsText;
	public GameObject FinanceImage;
	public GameObject EnvironmentImage;
	public GameObject SocialImage;
	public GameObject RatingImage;
	public GameObject BudgetImage;
	public GameObject AddValueImage;
	public GameObject SubtractValueImage;
	public GameObject ComponentImage;
	public Text FinanceTierText;
	public Text EnvironmentTierText;
	public Text SocialTierText;
	public Text BudgetTierText;
	public Text RatingTierText;
	public int Id;
	public string Title;
	public string Content;
	public int Environment;
	public int Social;
	public int Finance;
	public int Budget;
	public int Influence;

	public int MaxComponents = 5;
	private int AddedValue = 6;
	private int RemovedValue = 1;

	//tiers
	List<int> financeTier = new List<int>();
	List<int> socialTier = new List<int>();
	List<int> environmentTier = new List<int>();
	List<int> budgetTier = new List<int>();
	List<int> ratingTier = new List<int>();

	void Start () {
		InvokeRepeating("UpdateVars", 1f, .5f);
	}
	
	void UpdateVars()
	{
		EnvironmentText.text = "" + FormatText(Environment);
		FinanceText.text = "" + FormatText(Finance);
		SocialText.text = "" + FormatText(Social);
		RatingText.text = "" + FormatText(Influence);
		BudgetText.text = "" + FormatText(Budget);
		Title = TitleInput.text;
		Content = DescriptionInput.text;
		ComponentsText.text = "" + MaxComponents;
		BudgetTierText.text = "" + budgetTier.Count;
		RatingTierText.text = "" + ratingTier.Count;
		FinanceTierText.text = "" + financeTier.Count;
		EnvironmentTierText.text = "" + environmentTier.Count;
		SocialTierText.text = "" + socialTier.Count;
		
	}

	public void AddValue(Draggable.DraggableType type)
	{
		if (MaxComponents == 0)
			return;
		StartCoroutine(AnimateIcon(AddValueImage, .7f, 1f));
		StartCoroutine(AnimateIcon(ComponentImage, .7f, 1f));
		switch (type)
		{
			case Draggable.DraggableType.Environment:
				environmentTier.Add(1);
				Environment += (AddedValue - environmentTier.Count);
				Budget -= (RemovedValue + environmentTier.Count) * 100;
				StartCoroutine(AnimateIcon(EnvironmentImage, .7f, .5f));
				break;
			case Draggable.DraggableType.Finance:
				financeTier.Add(1);
				Finance += (AddedValue - financeTier.Count);
				Environment -= (RemovedValue + financeTier.Count);
				StartCoroutine(AnimateIcon(FinanceImage, .7f, .5f));
				break;
			case Draggable.DraggableType.Social:
				socialTier.Add(1);
				Social += (AddedValue - socialTier.Count);
				Finance -= (RemovedValue + socialTier.Count);
				StartCoroutine(AnimateIcon(SocialImage, .7f, .5f));
				break;
			case Draggable.DraggableType.Rating:
				ratingTier.Add(1);
				Influence += (AddedValue - ratingTier.Count);
				Social -= (RemovedValue + ratingTier.Count);
				StartCoroutine(AnimateIcon(RatingImage, .7f, .5f));
				break;
			case Draggable.DraggableType.Budget:
				budgetTier.Add(1);
				Budget += (AddedValue - budgetTier.Count)*100;
				Influence -= (RemovedValue + budgetTier.Count);
				StartCoroutine(AnimateIcon(BudgetImage, .7f, .5f));
				break;
		}
		MaxComponents -= 1;
	}

	public void SubtractValue(Draggable.DraggableType type)
	{
		if (MaxComponents == 5)
			return;
		StartCoroutine(AnimateIcon(SubtractValueImage, .7f, 1f));
		StartCoroutine(AnimateIcon(ComponentImage, 1.2f, 1f));
		switch (type)
		{
			case Draggable.DraggableType.Environment:
				if (environmentTier.Count == 0)
					return;
				Environment -= (AddedValue - environmentTier.Count);
				Budget += (RemovedValue + environmentTier.Count) * 100;
				environmentTier.Remove(1);
				StartCoroutine(AnimateIcon(EnvironmentImage, .7f, .5f));
				break;
			case Draggable.DraggableType.Finance:
				if (financeTier.Count == 0)
					return;
				Finance -= (AddedValue - financeTier.Count);
				Environment += (RemovedValue + financeTier.Count);
				financeTier.Remove(1);
				StartCoroutine(AnimateIcon(FinanceImage, .7f, .5f));
				break;
			case Draggable.DraggableType.Social:
				if (socialTier.Count == 0)
					return;
				Social -= (AddedValue - socialTier.Count);
				Finance += (RemovedValue + socialTier.Count);
				socialTier.Remove(1);
				StartCoroutine(AnimateIcon(SocialImage, .7f, .5f));
				break;
			case Draggable.DraggableType.Rating:
				if (ratingTier.Count == 0)
					return;
				Influence -= (AddedValue - ratingTier.Count);
				Social += (RemovedValue + ratingTier.Count);
				ratingTier.Remove(1);
				StartCoroutine(AnimateIcon(RatingImage, .7f, .5f));
				break;
			case Draggable.DraggableType.Budget:
				if (budgetTier.Count == 0)
					return;
				Budget -= (AddedValue - budgetTier.Count) * 100;
				Influence += (RemovedValue + budgetTier.Count);
				budgetTier.Remove(1);
				StartCoroutine(AnimateIcon(BudgetImage, .7f, .5f));
				break;
		}
		MaxComponents += 1;
	}

	string FormatText(int num)
	{
		string text = "";
		if (num > 0)
			text = "+" + num;
		if (num < 0)
			text = "" + num;
		if (num == 0)
			text = "" + num;
		return text;
	}

	IEnumerator AnimateIcon(GameObject icon, float start, float finish)
	{
		iTween.ScaleTo(icon, iTween.Hash("x", start, "y", start, "time", .2f));
		yield return new WaitForSeconds(.2f);
		iTween.ScaleTo(icon, iTween.Hash("x", finish, "y", finish, "time", .2f));
	}

	public void Reset()
	{
		Id = ProjectManager.Instance.CSVProjects.rowList.Count + 1;
		Debug.Log(Id);
		//CellManager.Instance.NetworkCommunicator.CreatePlayerProject(Id, Title, Content, Environment, Social, Finance, Budget, Influence);
		Invoke("SpawnProject", 1f);
	}

	public void SpawnProject()
	{
		//ProjectManager.Instance.UnlockProject(Id);
		UIManager.Instance.ProjectUI();
		financeTier.Clear();
		environmentTier.Clear();
		socialTier.Clear();
		budgetTier.Clear();
		ratingTier.Clear();
		Finance = 0;
		Social = 0;
		Environment = 0;
		Budget = 0;
		Influence = 0;
		MaxComponents = 5;
	}
}
