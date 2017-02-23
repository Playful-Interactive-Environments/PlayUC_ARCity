using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CellLogic : MonoBehaviour {

    public enum HeatmapState
    {
        PlacementState, SocialMap, EnvironmentMap, FinanceMap, CellSelected
    }
    //HexCell _hexCell;
    //HexGrid _hexGrid;
    private CellInterface _interface;
    public int Count;
    public int SocialRate;
    public int EnvironmentRate;
    public int FinanceRate;
    public float Height;
    private bool Topograhic;
    private float _chunkValue; // based on max value, must be dividable by 4 due to 4 heatmap steps
    public HeatmapState CurrentState = HeatmapState.PlacementState;
    private HeatmapState PreviousState = HeatmapState.PlacementState;
    private int state;
    public int OccupiedSlots;
    public List<int> Slots = new List<int>();
    private float offset = 20f;
    private Renderer _renderer;
    public int CellId;
    public Material[] colors;
     
    void Start()
    {
        _interface = GetComponent<CellInterface>();
        _renderer = GetComponent<Renderer>();
        _renderer.material = colors[0];
        EventDispatcher.StartListening("PlacementMap", PlacementMap);
        EventDispatcher.StartListening("SocialMap", SocialMap);
        EventDispatcher.StartListening("EnvironmentMap", EnvironmentMap);
        EventDispatcher.StartListening("FinanceMap", FinanceMap);
    }

    public Vector3 GetPositionOffset()
    {
        Vector3 vector = new Vector3();
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
        //_interface.ChangeCellDisplay(CellInterface.InterfaceState.Default);
       // GetComponent<CellInterface>().ResetCell();
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
       // GetComponent<CellInterface>().DisplayCell();
    }
    void ActivateMenu()
    {
        //_interface.ChangeCellDisplay(CellInterface.InterfaceState.Menu);
    }

    void Update()
    {
        if(SaveStateManager.Instance!=null)
            _chunkValue = SaveStateManager.Instance.CellMaxValue / 4;
        switch (CurrentState)
        {
            case HeatmapState.PlacementState:
                state = 0;
                _renderer.material = colors[state];
                break;
            case HeatmapState.CellSelected:
                state = 5;
                _renderer.material = colors[state];
                break;
                //4 - terrible social state, civil unrest; 1 - great social state, happyness
            case HeatmapState.SocialMap:
                if (SocialRate < _chunkValue)
                    state = 4;
                if (SocialRate >= _chunkValue  && SocialRate < _chunkValue * 2)
                    state = 3;
                if (SocialRate >= _chunkValue * 2 && SocialRate < _chunkValue * 3)
                    state = 2;
                if (SocialRate >= _chunkValue * 3)
                    state = 1;
                _renderer.material = colors[state];
                //_interface.ChangeCellDisplay(CellInterface.InterfaceState.Default);

                break;
                //4- high polution; 1 - low polution
            case HeatmapState.EnvironmentMap:
                if (EnvironmentRate < _chunkValue)
                    state = 4;
                if (EnvironmentRate >= _chunkValue && EnvironmentRate < _chunkValue * 2)
                    state = 3;
                if (EnvironmentRate >= _chunkValue * 2 && EnvironmentRate < _chunkValue * 3)
                    state = 2;
                if (EnvironmentRate >= _chunkValue * 3)
                    state = 1;
                _renderer.material = colors[state];
               // _interface.ChangeCellDisplay(CellInterface.InterfaceState.Default);
                break;
                //4- finance state worst 1 - finance state best
            case HeatmapState.FinanceMap:
                if (FinanceRate < _chunkValue)
                    state = 4;
                if (FinanceRate >= _chunkValue && FinanceRate < _chunkValue * 2)
                    state = 3;
                if (FinanceRate >= _chunkValue * 2 && FinanceRate < _chunkValue * 3)
                    state = 2;
                if (FinanceRate >= _chunkValue * 3)
                    state = 1;
                _renderer.material = colors[state];
                //_interface.ChangeCellDisplay(CellInterface.InterfaceState.Default);
                break;
        }
    }

    public string GetVars()
    {
        string vars = " Environment " + EnvironmentRate + " Social " + SocialRate  + " Finance "+ FinanceRate;
        return vars;
    }

    public void TouchCell()
    {
        foreach (GameObject cell in CellGrid.Instance.GridCells)
        {
            cell.GetComponent<CellLogic>().PlacementMap();
        }
        CellSelected();
        //EventDispatcher.TriggerEvent("PlacementMap");
        //Invoke("CellSelected", .05f);
    }
}
