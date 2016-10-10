using UnityEngine;
using System.Collections;

public class CellLogic : MonoBehaviour {


    public enum HeatmapState
    {
        PlacementState, SocialMap, EnvironmentMap, FinanceMap
    }
    HexCell _hexCell;
    HexGrid _hexGrid;
    private CellInterface _interface;
    public int Count;
    public int SocialRate;
    public int EnvironmentRate;
    public int FinanceRate;
    public float Height;
    private bool Topograhic;

    public HeatmapState CurrentState = HeatmapState.PlacementState;
    int state;
    
    void Start()
    {
        _hexCell = GetComponent<HexCell>();
        _interface = GetComponent<CellInterface>();
        _hexGrid = HexGrid.Instance;
        _hexCell.color = _hexGrid.colors[0];
        EventManager.StartListening("PlacementMap", PlacementMap);
        EventManager.StartListening("SocialMap", SocialMap);
        EventManager.StartListening("EnvironmentMap", EnvironmentMap);
        EventManager.StartListening("FinanceMap", FinanceMap);

    }
    public void PlacementMap()
    {
        CurrentState = HeatmapState.PlacementState;
        _interface.CurrentState = CellInterface.InterfaceState.Default;
    }
    public void SocialMap()
    {
        CurrentState = HeatmapState.SocialMap;
    }
    public void FinanceMap()
    {
        CurrentState = HeatmapState.FinanceMap;
    }
    public void EnvironmentMap()
    {
        CurrentState = HeatmapState.EnvironmentMap;
    }
    void ActivateMenu()
    {
        Debug.Log("Get here");
        _interface.CurrentState = CellInterface.InterfaceState.Menu;
    }

    void Update()
    {
        switch (CurrentState)
        {
            case HeatmapState.PlacementState:
                state = 0;
                _hexCell.Elevation = state;
                _hexCell.color = _hexGrid.colors[state];
                break;
            case HeatmapState.SocialMap:
                if (SocialRate < 20)
                    state = 1;
                if (SocialRate >= 20 && SocialRate < 40)
                    state = 2;
                if (SocialRate >= 40 && SocialRate < 60)
                    state = 3;
                if (SocialRate >= 60 && SocialRate < 80)
                    state = 4;
                if (SocialRate >= 80)
                    state = 5;
                _hexCell.color = _hexGrid.colors[state];
                _hexCell.Elevation = state - 1;
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
            case HeatmapState.EnvironmentMap:
                if (EnvironmentRate < 20)
                    state = 1;
                if (EnvironmentRate >= 20 && EnvironmentRate < 40)
                    state = 2;
                if (EnvironmentRate >= 40 && EnvironmentRate < 60)
                    state = 3;
                if (EnvironmentRate >= 60 && EnvironmentRate < 80)
                    state = 4;
                if (EnvironmentRate >= 80)
                    state = 5;
                _hexCell.Elevation = state - 1;
                _hexCell.color = _hexGrid.colors[state];
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
            case HeatmapState.FinanceMap:
                if (FinanceRate < 20)
                    state = 1;
                if (FinanceRate >= 20 && FinanceRate < 40)
                    state = 2;
                if (FinanceRate >= 40 && FinanceRate < 60)
                    state = 3;
                if (FinanceRate >= 60 && FinanceRate < 80)
                    state = 4;
                if (FinanceRate >= 80)
                    state = 5;
                _hexCell.Elevation = state - 1;
                _hexCell.color = _hexGrid.colors[state];
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
        }
    }
}
