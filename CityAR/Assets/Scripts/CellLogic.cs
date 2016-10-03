using UnityEngine;
using System.Collections;

public class CellLogic : MonoBehaviour {


    public enum HeatmapState
    {
        PlacementState, JobsState, PollutionState, TopographicMap
    }
    HexCell _hexCell;
    HexGrid _hexGrid;
    private CellInterface _interface;
    public int Count;
    public int JobsRate;
    public int PollutionRate;
    public float Height;
    private bool Topograhic;
    public bool Occupied;
    public HeatmapState CurrentState = HeatmapState.PollutionState;
    int state;
    
    void Start()
    {
        _hexCell = GetComponent<HexCell>();
        _interface = GetComponent<CellInterface>();
        _hexGrid = HexGrid.Instance;
        _hexCell.color = _hexGrid.colors[0];
        EventManager.StartListening("PlacementMap", PlacementMap);
        EventManager.StartListening("JobsMap", JobsMap);
        EventManager.StartListening("PollutionMap", PollutionMap);
        EventManager.StartListening("TopographicMap", TopographicMap);
        
    }
    public void PlacementMap()
    {
        CurrentState = HeatmapState.PlacementState;
        _interface.CurrentState = CellInterface.InterfaceState.Default;
    }
    public void JobsMap()
    {
        CurrentState = HeatmapState.JobsState;
    }
    public void PollutionMap()
    {
        CurrentState = HeatmapState.PollutionState;

    }
    public void TopographicMap()
    {

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
            case HeatmapState.JobsState:
                if (JobsRate < 20)
                    state = 1;
                if (JobsRate >= 20 && JobsRate < 40)
                    state = 2;
                if (JobsRate >= 40 && JobsRate < 60)
                    state = 3;
                if (JobsRate >= 60 && JobsRate < 80)
                    state = 4;
                if (JobsRate >= 80)
                    state = 5;
                _hexCell.color = _hexGrid.colors[state];
                _hexCell.Elevation = state - 1;
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
            case HeatmapState.PollutionState:
                state = 0;
                _hexCell.Elevation = state;
                _hexCell.color = _hexGrid.colors[state];
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
        }
    }
}
