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
	public string TimeWinText;
	public string UtopiaWinText;
	public string MayorWinText;
	public string MayorAnnounceText;

    public CSVLocalization Languages;
    
	void Start ()
	{
	    Languages = CSVLocalization.Instance;
        Invoke("ChooseEnglish",.1f);
	}
	
	void Update () {
		
	}


    public void ChooseEnglish()
    {
        GetWords("english");
    }

    public void ChooseGerman()
    {
        GetWords("german");
    }

    public void ChooseFrench()
    {
        GetWords("dutch");
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
        Mg_win= Languages.GetWord("Mg_win", language);
        Mg_lose = Languages.GetWord("Mg_lose", language);

        //Game End
        TimeWinText = Languages.GetWord("TimeWin", language);
        UtopiaWinText = Languages.GetWord("UtopiaWin", language);
        MayorWinText = Languages.GetWord("MayorWin", language);
        MayorAnnounceText = Languages.GetWord("MayorAnnounce", language);
    }
}
