using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vars : AManager<Vars>
{
    [Header("Player Roles")]
    public const string Player1 = "Finance";
    public const string Player2 = "Social";
    public const string Player3 = "Environment";

    [Header("Cell Starting Values")]
    public int CellTotalVal;
    public float SingleCellMaxVal; //4 heatmap steps!

    [Header("Player Variables")]
    public int StartingBudget;

    [Header("Game End Triggers")]
    public float GameEndTime;
    public int UtopiaRate;
    public int MayorLevel;

    [Header("Event Names")]
    public string GameEndTrigger;
    public string DiscussionStart;
    public string DiscussionEnd;
    
    [Header("Mini Game Settings")]
    public float MiniGameTime = 30;
}
