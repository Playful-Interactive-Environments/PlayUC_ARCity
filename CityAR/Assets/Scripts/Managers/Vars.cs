using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vars : AManager<Vars>
{
    [Header("Cell Starting Values")]
    public int CellTotalVal;
    public float SingleCellMaxVal; //4 heatmap steps!

    [Header("Player Variables")]
    public int StartingBudget;
    public int MinPlayers;

    [Header("Game End Goals")]
    public float GameEndTime;
    public int UtopiaRate;
    public int MayorLevel;
    
    [Header("Mini Game Settings")]
    public float MiniGameTime = 30;
    public float[] Mg1_SpawnTimes =  {3, 4, 5};
    public int[] Mg1_DocsNeeded = {4, 3, 3};

    [Header("Player Roles")]
    public const string Player1 = "Finance";
    public const string Player2 = "Social";
    public const string Player3 = "Environment";
    public const string MainValue1 = "Budget";
    public const string MainValue2 = "Influence";

    [Header("Voting Strings")]
    public const string Approved = "Approved";
    public const string Denied = "Denied";
    public const string Choice1 = "Choice1";
    public const string Choice2 = "Choice2";
    public const string ResultChoice1 = "Result_Choice1";
    public const string ResultChoice2 = "Result_Choice2";

    [Header("MgStrings")]
    public const string Mg1 = "Sort";
    public const string Mg2 = "Advertise";
    public const string Mg3 = "Area";
    public const string NoMg = "None";

    [Header("Event Messages")]
    public const string LocalClientDisconnect = "LocalClientDisconnect";
    public const string ServerHandleDisconnect = "ServerHandleDisconnect";
}
