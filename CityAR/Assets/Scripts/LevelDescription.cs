using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class LevelDescription : MonoBehaviour
{

    public int ThisLevel;
    public CSVLeveling levelData;
    public Text RankText;
    public Text UnlockTitle;
    public string textToParse;
    private string BonusType;
    private string BonusValue;
    int parsedValue;
    private string[] splitString;
    private bool levelUnlocked;
    private bool bonusUnlocked;

    void Start()
    {
        levelData = CSVLeveling.Instance;
        GetComponent<Image>().color = Color.grey;
        EventDispatcher.StartListening(Vars.LocalClientDisconnect, NetworkDisconnect);
    }

    void NetworkDisconnect()
    {
        if (transform.name != "LevelTemplate")
            Destroy(gameObject);
    }

    void Update()
    {
        if (ThisLevel <= LocalManager.Instance.CurrentRank && !levelUnlocked)
        {
            GetComponent<Image>().color = Color.white;
            if (BonusType == "Event" && !bonusUnlocked)
            {
                //EventManager.Instance.TriggerEvent(BonusValue);
            }
            if (BonusType == Vars.MainValue1 && !bonusUnlocked)
            {
                if (NetworkingManager.Instance.isNetworkActive)
                {
                    LocalManager.Instance.NetworkCommunicator.UpdateData(LocalManager.Instance.RoleType, Vars.MainValue1, ConvertString(BonusValue));
                    UIManager.Instance.CreateText(Color.green, BonusValue, 50, .5f, 2f, new Vector2(UIManager.Instance.BudgetTextPos.x, UIManager.Instance.BudgetTextPos.y), new Vector2(UIManager.Instance.BudgetTextPos.x, 0));
                }
            }
            if (BonusType == "Project")
            {
                int projectId = 0;
                switch (LocalManager.Instance.RoleType)
                {

                    case Vars.Player3:
                        projectId = ConvertString(levelData.GetEnvironmentPlayer(ThisLevel));
                        break;
                    case Vars.Player2:
                        projectId = ConvertString(levelData.GetSocialPlayer(ThisLevel));
                        break;
                    case Vars.Player1:
                        projectId = ConvertString(levelData.GetFinancePlayer(ThisLevel));
                        break;
                }
                ProjectManager.Instance.UnlockProject(projectId);
            }
            levelUnlocked = true;
        }
    }

    public void SetupLayout(int i)
    {

        if (LocalManager.Instance.CurrentRank > 0)
        {
            bonusUnlocked = true;
        }
        ThisLevel = i;
        RankText.text = "" + ThisLevel;
        textToParse = levelData.GetUnlock(i);
        ExtractData();
    }

    public void ExtractData()
    {
        splitString = textToParse.Split('/');
        //SaveStateManager.Instance.LogEvent("PLAYER: " + LocalManager.Instance.RoleType + " QUEST: " + Title + " CHOICE: " + Choice1 + " RESULT:" + Result1 + " EFFECT: " + Effect1);

        for (int i = 0; i < splitString.Length; i++)
        {
            //even members are the names. save them and get corresponding values
            if (i % 2 == 0)
            {
                UnlockTitle.text = splitString[i];
                BonusType = splitString[i];
            }
            //odd members are values. parse the value and act depending on the already saved name
            if (i % 2 != 0)
            {
                int.TryParse(splitString[i], NumberStyles.AllowLeadingSign, null, out parsedValue);
                UnlockTitle.text += " " + splitString[i];
                BonusValue = splitString[i];
            }
        }
    }

    private int ConvertString(string input)
    {
        int parsedInt = 0;
        int.TryParse(input, NumberStyles.Any, null, out parsedInt);
        return parsedInt;
    }
}
