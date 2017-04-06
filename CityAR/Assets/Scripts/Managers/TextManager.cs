using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : AManager<TextManager>
{
    [Header("Main Menu")]
    public Text Host;
    public Text Connect;
    public Text Search;
    public Text Restart;
    public Text Stop;
    public Text Menu;
    [Header("Roles")]
    public Text ChooseRole;
    public Text Player1_Role;
    public Text Player2_Role;
    public Text Player3_Role;
    [Header("Discussion")]
    public Text Discussion_Description;
    public string InfoTextVoted;
    public string InfoTextProposed;

    [Header("MiniGames")]
    public string Mg1_Description;
    public string Mg2_Description;
    public string Mg3_Description;
    public string Mg1_Goal;
    public string Mg2_Goal;
    public string Mg3_Goal;
    public string Mg_win;
    public string Mg_lose;

    [Header("Game End")]
    public TextMeshProUGUI Congratulations;
    public string TimeWinText;
    public string UtopiaWinText;
    public string MayorWinText;
    public string MayorAnnounceText;
    public TextMeshProUGUI TimePlayed;
    public TextMeshProUGUI SuccessfulProjects;
    public TextMeshProUGUI TotalAddValue;
    public TextMeshProUGUI MostImprovedVal;
    public TextMeshProUGUI LeastImprovedVal;

    [Header("Player Achievements")]
    public TextMeshProUGUI MostSuccessfulPro;
    public TextMeshProUGUI MostMoneySpent;
    public TextMeshProUGUI HighestInfluence;
    public TextMeshProUGUI MostWinsMg;
    public TextMeshProUGUI FastestMg;

    [Header("Personal Stats")]
    public TextMeshProUGUI ProjectsProposed;
    public TextMeshProUGUI ProjectsSuccessful;
    public TextMeshProUGUI ProjectsFailed;
    public TextMeshProUGUI ProjectsVotedApprove;
    public TextMeshProUGUI ProjectsVotedDenied;
    public TextMeshProUGUI QuestsCompleted;

    public string CurrentLanguage;
    public CSVLocalization Languages;

    void Start()
    {
        Languages = CSVLocalization.Instance;
        Invoke("ChooseEnglish", .1f);
    }

    void Update()
    {

    }

    public void ChooseEnglish()
    {
        CurrentLanguage = "english";
        GetWords(CurrentLanguage);

        CSVQuests.Instance.LoadLanguage("english");
        if (QuestManager.Instance != null)
            QuestManager.Instance.Invoke("LoadLanguage", .1f);

        CSVProjects.Instance.LoadLanguage("english");
        if (ProjectManager.Instance != null)
            ProjectManager.Instance.Invoke("LoadLanguage", .1f);
    }

    public void ChooseGerman()
    {
        CurrentLanguage = "german";

        GetWords(CurrentLanguage);

        CSVQuests.Instance.LoadLanguage("german");
        if (QuestManager.Instance != null)
            QuestManager.Instance.Invoke("LoadLanguage", .1f);

        CSVProjects.Instance.LoadLanguage("german");
        if (ProjectManager.Instance != null)
            ProjectManager.Instance.Invoke("LoadLanguage", .1f);
    }

    public void ChooseFrench()
    {
        CurrentLanguage = "french";
        GetWords(CurrentLanguage);
    }

    public void GetWords(string language)
    {
        //Main Menu
        Host.text = Languages.GetWord("mm_host", language);
        Connect.text = Languages.GetWord("mm_connect", language);
        Search.text = Languages.GetWord("mm_search", language);
        Restart.text = Languages.GetWord("mm_restart", language);
        Stop.text = Languages.GetWord("mm_stop", language);
        Menu.text = Languages.GetWord("mm_menu", language);

        //Roles
        ChooseRole.text = Languages.GetWord("role_choose", language);
        Player1_Role.text = Languages.GetWord("role_player1", language);
        Player2_Role.text = Languages.GetWord("role_player2", language);
        Player3_Role.text = Languages.GetWord("role_player3", language);

        //Discussion
        Discussion_Description.text = Languages.GetWord("disc_descr", language);
        InfoTextVoted = Languages.GetWord("disc_info_voted", language);
        InfoTextProposed = Languages.GetWord("disc_info_proposed", language);

        //Mini Games
        Mg1_Description = Languages.GetWord("Mg1_Description", language);
        Mg2_Description = Languages.GetWord("Mg2_Description", language);
        Mg3_Description = Languages.GetWord("Mg3_Description", language);
        Mg1_Goal = Languages.GetWord("Mg1_Goal", language);
        Mg2_Goal = Languages.GetWord("Mg2_Goal", language);
        Mg3_Goal = Languages.GetWord("Mg3_Goal", language);
        Mg_win = Languages.GetWord("Mg_win", language);
        Mg_lose = Languages.GetWord("Mg_lose", language);

        //Game End
        TimeWinText = Languages.GetWord("end_timewin", language);
        UtopiaWinText = Languages.GetWord("end_utopiawin", language);
        MayorWinText = Languages.GetWord("end_mayorwin", language);
        MayorAnnounceText = Languages.GetWord("end_mayourannounce", language);

        Congratulations.text = Languages.GetWord("end_congrats", language);
        TimePlayed.text = Languages.GetWord("end_timeplayed", language);
        SuccessfulProjects.text = Languages.GetWord("end_successfulprojects", language);
        TotalAddValue.text = Languages.GetWord("end_totaladdvalue", language);
        MostImprovedVal.text = Languages.GetWord("end_mostimprovedfield", language);
        LeastImprovedVal.text = Languages.GetWord("end_leastimprovedfield", language);

        //Player Achievements
        MostSuccessfulPro.text = Languages.GetWord("end_mostsuccessfulpro", language);
        MostMoneySpent.text = Languages.GetWord("end_mostmoneyspent", language);
        HighestInfluence.text = Languages.GetWord("end_highestinfluence", language);
        MostWinsMg.text = Languages.GetWord("end_mostwinsmg", language);
        FastestMg.text = Languages.GetWord("end_fastestmg", language);

        //Personal Stats
        ProjectsProposed.text = Languages.GetWord("end_projectsproposed", language);
        ProjectsSuccessful.text = Languages.GetWord("end_projectssuccessful", language);
        ProjectsFailed.text = Languages.GetWord("end_projectsfailed", language);
        ProjectsVotedApprove.text = Languages.GetWord("end_projectsapproved", language);
        ProjectsVotedDenied.text = Languages.GetWord("end_projectsdenied", language);
        QuestsCompleted.text = Languages.GetWord("end_questscompleted", language);
    }
}
