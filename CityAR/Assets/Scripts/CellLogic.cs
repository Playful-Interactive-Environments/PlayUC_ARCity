using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CellLogic : MonoBehaviour {

    public enum HeatmapState
    {
        Default, SocialMap, EnvironmentMap, FinanceMap, ProjectPlacement
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
    public HeatmapState CurrentState = HeatmapState.Default;
    private HeatmapState PreviousState = HeatmapState.Default;
    private int state;
    private Renderer _renderer;
    public int CellId;
    public Material[] colors;
    public GameObject CellRepresentation;
     
    void Start()
    {
        _interface = GetComponent<CellInterface>();
        _renderer = CellRepresentation.GetComponent<Renderer>();
        _renderer.material = colors[0];
        EventDispatcher.StartListening("Grey", Default);
        EventDispatcher.StartListening("SocialMap", SocialMap);
        EventDispatcher.StartListening("EnvironmentMap", EnvironmentMap);
        EventDispatcher.StartListening("FinanceMap", FinanceMap);
        float _cubeX = ValueManager.Instance.MapWidth / CellGrid.Instance.Columns;
        float _cubeZ = ValueManager.Instance.MapHeight / CellGrid.Instance.Rows;
        CellRepresentation.transform.localScale = new Vector3(_cubeX, .1f, _cubeZ);
        GetComponent<BoxCollider>().size = new Vector3(_cubeX, 1f, _cubeZ);
    }

    public void Default()
    {
        _interface.ChangeCellText(CellInterface.TextState.Grey);
        CurrentState = PreviousState;
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

    public void ProjectPlacement()
    {
        CurrentState = HeatmapState.ProjectPlacement;
        _interface.ChangeCellText(CellInterface.TextState.Changes);
        _interface.ChangeCellDisplay(CellInterface.InterfaceState.White);
    }

    void Update()
    {
        if(SaveStateManager.Instance!=null)
            _chunkValue = Vars.Instance.SingleCellMaxVal / 4;
        switch (CurrentState)
        {
            case HeatmapState.Default:
                state = 0;
                _renderer.material = colors[state];
                break;
            case HeatmapState.ProjectPlacement:
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
        CellGrid.Instance.DefaultState();
        ProjectPlacement();
    }
}
