using UnityEngine;
using System.Collections;

public class CellLogic : MonoBehaviour {


    public enum HeatmapState
    {
        PlacementState, SocialMap, EnvironmentMap, FinanceMap, CellSelected
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
    private float _chunkValue; // based on max value, must be dividable by 5 due to 5 heatmap steps
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
        GetComponent<CellInterface>().ResetCell();
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

    public void CellSelected()
    {
        CurrentState = HeatmapState.CellSelected;
        GetComponent<CellInterface>().DisplayCell();
    }
    void ActivateMenu()
    {
        Debug.Log("Get here");
        _interface.CurrentState = CellInterface.InterfaceState.Menu;
    }

    void Update()
    {
        if(GlobalManager.Instance!=null)
            _chunkValue = GlobalManager.Instance.CellMaxValue / 5;
        switch (CurrentState)
        {
            case HeatmapState.PlacementState:
                state = 0;
               // _hexCell.Elevation = state;
                _hexCell.color = _hexGrid.colors[state];
                break;
            case HeatmapState.CellSelected:
                state = 6;
                //_hexCell.Elevation = 0;
                _hexCell.color = _hexGrid.colors[state];
                break;
            case HeatmapState.SocialMap:
                if (SocialRate < _chunkValue)
                    state = 1;
                if (SocialRate >= _chunkValue && SocialRate < _chunkValue * 2)
                    state = 2;
                if (SocialRate >= _chunkValue * 2 && SocialRate < _chunkValue * 3)
                    state = 3;
                if (SocialRate >= _chunkValue * 3 && SocialRate < _chunkValue * 4)
                    state = 4;
                if (SocialRate >= _chunkValue * 4)
                    state = 5;
                _hexCell.color = _hexGrid.colors[state];
                //_hexCell.Elevation = state - 1;
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
            case HeatmapState.EnvironmentMap:
                if (EnvironmentRate < _chunkValue)
                    state = 1;
                if (EnvironmentRate >= _chunkValue && EnvironmentRate < _chunkValue * 2)
                    state = 2;
                if (EnvironmentRate >= _chunkValue * 2 && EnvironmentRate < _chunkValue * 3)
                    state = 3;
                if (EnvironmentRate >= _chunkValue * 3 && EnvironmentRate < _chunkValue * 4)
                    state = 4;
                if (EnvironmentRate >= _chunkValue * 4)
                    state = 5;
                //_hexCell.Elevation = state - 1;
                _hexCell.color = _hexGrid.colors[state];
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
            case HeatmapState.FinanceMap:
                if (FinanceRate < _chunkValue)
                    state = 1;
                if (FinanceRate >= _chunkValue && FinanceRate < _chunkValue * 2)
                    state = 2;
                if (FinanceRate >= _chunkValue * 2 && FinanceRate < _chunkValue * 3)
                    state = 3;
                if (FinanceRate >= _chunkValue * 3 && FinanceRate < _chunkValue * 4)
                    state = 4;
                if (FinanceRate >= _chunkValue * 4)
                    state = 5;
                //_hexCell.Elevation = state - 1;
                _hexCell.color = _hexGrid.colors[state];
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
        }
    }

    public string GetVars()
    {
        string vars = " Environment " + EnvironmentRate + " Social " + SocialRate  + " Finance "+ FinanceRate;
        return vars;
    }
}
