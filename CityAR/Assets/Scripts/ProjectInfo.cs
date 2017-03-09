using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectInfo : MonoBehaviour {

	public Text TitleText;
	public Text DescriptionText;
	public Text FinanceText;
	public Text SocialText;
	public Text EnvironmentText;
	public Text InfluenceText;
	public Text BudgetText;
	public Text MGText;
	public int ProjectCSVId;
	private string miniGame;

	void Start()
	{
	}

	void Update()
	{

	}

    public void UpdateText(int id)
    {
        ProjectCSVId = id;
        TitleText.text = ProjectManager.Instance.SelectedProject.Title;
        DescriptionText.text = ProjectManager.Instance.SelectedProject.Description;
        FinanceText.text = "" + ProjectManager.Instance.SelectedProject.Finance;
        SocialText.text = "" + ProjectManager.Instance.SelectedProject.Social;
        EnvironmentText.text = "" + ProjectManager.Instance.SelectedProject.Environment;
        InfluenceText.text = "+" + ProjectManager.Instance.SelectedProject.Influence;
        BudgetText.text = "" + ProjectManager.Instance.SelectedProject.Budget;
    }

	public void SetProjectInfo(int id)
	{
        UpdateText(id);
	    miniGame = ProjectManager.Instance.SelectedProject.MiniGame;
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
		}	}

	public void SelectProject()
	{
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

}
