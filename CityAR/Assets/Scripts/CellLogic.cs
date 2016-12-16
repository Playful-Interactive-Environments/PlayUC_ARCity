using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

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
    private HeatmapState PreviousState = HeatmapState.PlacementState;
    private int state;
    public int OccupiedSlots;
    public List<int> Slots = new List<int>();
    private float offset = 15f;
     
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

    public Vector3 GetPositionOffset()
    {
        Vector3 vector = new Vector3();
        OccupiedSlots = GlobalManager.Instance.OccupiedList[_hexCell.CellId];
        if (OccupiedSlots == 1)
            vector = new Vector3(0, 0, 0);
        if (OccupiedSlots == 2)
            vector = new Vector3(offset, 0, 0);
        if (OccupiedSlots == 3)
            vector = new Vector3(-offset, 0, 0);
        if (OccupiedSlots == 4)
            vector = new Vector3(-offset, 0, offset);
        if (OccupiedSlots == 5)
            vector = new Vector3(offset, 0, offset);
        if (OccupiedSlots == 6)
            vector = new Vector3(-offset, 0, -offset);
        if (OccupiedSlots == 7)
            vector = new Vector3(0, 0, -offset);
        if (OccupiedSlots == 8)
            vector = new Vector3(0, 0, offset);
        if (OccupiedSlots == 9)
            vector = new Vector3(offset, 0, -offset);
        return vector;
    }
    public void PlacementMap()
    {
        CurrentState = PreviousState;
        //CurrentState = HeatmapState.PlacementState;
        _interface.CurrentState = CellInterface.InterfaceState.Default;
        GetComponent<CellInterface>().ResetCell();
    }
    public void SocialMap()
    {
        CurrentState = HeatmapState.SocialMap;
        PreviousState = CurrentState;
    }
    public void FinanceMap()
    {
        CurrentState = HeatmapState.FinanceMap;
        PreviousState = CurrentState;
    }
    public void EnvironmentMap()
    {
        CurrentState = HeatmapState.EnvironmentMap;
        PreviousState = CurrentState;
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
                //5 - terrible social state, civil unreast; 1 - great social state, happyness
            case HeatmapState.SocialMap:
                if (SocialRate < _chunkValue)
                    state = 5;
                if (SocialRate >= _chunkValue && SocialRate < _chunkValue * 2)
                    state = 4;
                if (SocialRate >= _chunkValue * 2 && SocialRate < _chunkValue * 3)
                    state = 3;
                if (SocialRate >= _chunkValue * 3 && SocialRate < _chunkValue * 4)
                    state = 2;
                if (SocialRate >= _chunkValue * 4)
                    state = 1;
                _hexCell.color = _hexGrid.colors[state];
                //_hexCell.Elevation = state - 1;
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
                //5- high polution; 1 - low polution
            case HeatmapState.EnvironmentMap:
                if (EnvironmentRate < _chunkValue)
                    state = 5;
                if (EnvironmentRate >= _chunkValue && EnvironmentRate < _chunkValue * 2)
                    state = 4;
                if (EnvironmentRate >= _chunkValue * 2 && EnvironmentRate < _chunkValue * 3)
                    state = 3;
                if (EnvironmentRate >= _chunkValue * 3 && EnvironmentRate < _chunkValue * 4)
                    state = 2;
                if (EnvironmentRate >= _chunkValue * 4)
                    state = 1;
                //_hexCell.Elevation = state - 1;
                _hexCell.color = _hexGrid.colors[state];
                _interface.CurrentState = CellInterface.InterfaceState.Default;
                break;
                //5- finance state worst 1 - finance state best
            case HeatmapState.FinanceMap:
                if (FinanceRate < _chunkValue)
                    state = 5;
                if (FinanceRate >= _chunkValue && FinanceRate < _chunkValue * 2)
                    state = 4;
                if (FinanceRate >= _chunkValue * 2 && FinanceRate < _chunkValue * 3)
                    state = 3;
                if (FinanceRate >= _chunkValue * 3 && FinanceRate < _chunkValue * 4)
                    state = 2;
                if (FinanceRate >= _chunkValue * 4)
                    state = 1;
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
