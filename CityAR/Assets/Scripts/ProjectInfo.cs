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
    private Project selectedProject;

	void Start()
	{
	}

	void Update()
	{
	    if (ProjectManager.Instance != null)
	    {
	        if(ProjectManager.Instance.SelectedProject !=null)
                UpdateText();
        }
    }

    public void UpdateText()
    {
        selectedProject = ProjectManager.Instance.SelectedProject;
        TitleText.text = selectedProject.Title;
        DescriptionText.text = selectedProject.Description;
        FinanceText.text = "" + selectedProject.Finance;
        SocialText.text = "" + selectedProject.Social;
        EnvironmentText.text = "" + selectedProject.Environment;
        InfluenceText.text = "+" + selectedProject.Influence;
        BudgetText.text = "" + selectedProject.Budget;
        miniGame = selectedProject.MiniGame;
        switch (miniGame)
        {
            case "None":
                MGText.text = "No Tasks.";
                break;
            case "Mg1":
                MGText.text = "Task: Bureucracy";
                break;
            case "Mg2":
                MGText.text = "Task: Campaigning";
                break;
            case "Mg3":
                MGText.text = "Task: Planning";
                break;
            default:
                MGText.text = "No Tasks.";
                break;
        }
    }
}
