using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ProjectInfo : MonoBehaviour
{

    public Text TitleText;
    public Text DescriptionText;
    public TextMeshProUGUI FinAreaText;
    public TextMeshProUGUI SocAreaText;
    public TextMeshProUGUI EnvAreaText;
    public TextMeshProUGUI FinChangeText;
    public TextMeshProUGUI SocChangeText;
    public TextMeshProUGUI EnvChangeText;
    public Text InfluenceText;
    public Text BudgetText;
    public int ProjectCSVId;
    private Project selectedProject;

    void Start()
    {

    }

    void Update()
    {
        if (ProjectManager.Instance != null)
        {
            if (ProjectManager.Instance.SelectedProject != null)
                UpdateText();
        }
    }

    public void UpdateText()
    {
        selectedProject = ProjectManager.Instance.SelectedProject;
        TitleText.text = selectedProject.Title;
        DescriptionText.text = selectedProject.Description;


        float fin = CellGrid.Instance.GetCellLogic(selectedProject.CellId).FinanceRate;
        float soc = CellGrid.Instance.GetCellLogic(selectedProject.CellId).SocialRate;
        float env = CellGrid.Instance.GetCellLogic(selectedProject.CellId).EnvironmentRate;

        float finPro = selectedProject.Finance;
        float socPro = selectedProject.Social;
        float envPro = selectedProject.Environment;
        FinAreaText.text = "" + fin;
        SocAreaText.text = "" + soc;
        EnvAreaText.text = "" + env;
        if (finPro >= 0)
            FinChangeText.text = "</color>" + " <color=green><b>+" + finPro + "</color></b>";
        if (finPro < 0)
            FinChangeText.text = "</color>" + " <color=red><b>" + finPro + "</color></b>";
        if (socPro >= 0)
            SocChangeText.text = "</color>" + " <color=green><b>+" + socPro + "</color></b>";
        if (socPro < 0)
            SocChangeText.text = "</color>" + " <color=red><b>" + socPro + "</color></b>";
        if (envPro >= 0)
            EnvChangeText.text = "</color>" + " <color=green><b>+" + envPro + "</color></b>";
        if (envPro < 0)
            EnvChangeText.text = "</color>" + " <color=red><b>" + envPro + "</color></b>";


        InfluenceText.text = "+" + selectedProject.Influence;
        BudgetText.text = "" + selectedProject.Budget;
        switch (selectedProject.ProjectOwner)
        {
            case Vars.Player1:
                GetComponent<Image>().color = new Color32(0, 113, 188, 255);
                break;
            case Vars.Player2:
                GetComponent<Image>().color = new Color32(251, 176, 59, 255);
                break;
            case Vars.Player3:
                GetComponent<Image>().color = new Color32(166, 199, 56, 255);
                break;
        }
    }
}
