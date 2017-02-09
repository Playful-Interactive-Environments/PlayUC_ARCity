using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Agent : MonoBehaviour {

    public enum VoterState
    {
        Walking, Captured
    }

    public enum MovementPattern
    {
        Random, RightLeft, LeftRight, Downtop, TopDown, Circle, RandomArea
    }

    public MovementPattern currentPattern = MovementPattern.Random;
    private VoterState currentState = VoterState.Walking;
    public Vector3 _nextWayPointPosition;
    private int nextWaypoint;
    private List<Vector3> Waypoints = new List<Vector3>();

    private Vector3 _moveDirection;
    private Vector3 _startingPos;
    private float angle;
    private float speed = 15f;

    public float xEast; //max
    public float xWest; // min
    public float yNorth; //max
    public float ySouth; //min
    private float Height;
    private float Width;

    void Start ()
    {

    }

    public void SetWaypoints(float xE, float xW, float yN, float yS)
    {
        //agents in MG-3
        xEast = xE;
        xWest = xW;
        yNorth = yN;
        ySouth = yS;
        ResetWaypoints();
        for (int i = 0; i <= 40f; i++)
        {
            Vector3 waypoint = new Vector3(Utilities.RandomFloat(xWest, xEast), Utilities.RandomFloat(yNorth, ySouth), 0);
            Waypoints.Add(waypoint);
        }
        GetNextPoint();
    }

    public void SetWaypoints(float w, float h, MovementPattern pattern)
    {
        //agents in MG-2
        Width = w;
        Height = h;
        ResetWaypoints();
        currentPattern = pattern;
        //define movement pattern
        switch (currentPattern)
        {
            case MovementPattern.Random:
                for (int i = 0; i <= 40f; i++)
                {
                    Vector3 waypoint = new Vector3(Utilities.RandomFloat(-Width/2, Width/2), Utilities.RandomFloat(-Height/2, Height/2), 0);
                    Waypoints.Add(waypoint);
                }
                speed = Random.Range(10f, 30f);
                break;
            case MovementPattern.RightLeft:
                float b = Random.Range(2, 7);
                Waypoints.Add(new Vector3(-Width/2, Height / b + Random.Range(-10, 10), 0));
                Waypoints.Add(new Vector3(Width/2, Height / b + Random.Range(-10, 10), 0));
                speed = Random.Range(25f,35f);
                break;
            case MovementPattern.LeftRight:
                float a = Random.Range(2, 7);
                Waypoints.Add(new Vector3(Width / 2, Height / a + Random.Range(-10, 10), 0));
                Waypoints.Add(new Vector3(-Width / 2, Height / a + Random.Range(-10, 10), 0));
                speed = Random.Range(25f, 35f);
                break;
            case MovementPattern.TopDown:
                float d = Random.Range(2, 7);
                Waypoints.Add(new Vector3(Width / d + Random.Range(-10, 10), Height/2, 0));
                Waypoints.Add(new Vector3(Width / d + Random.Range(-10, 10), -Height/2, 0));
                speed = Random.Range(20f, 30f);
                break;
            case MovementPattern.Downtop:
                float c = Random.Range(2, 7);
                Waypoints.Add(new Vector3(-Width / c + Random.Range(-10, 10), -Height / 2, 0));
                Waypoints.Add(new Vector3(-Width / c + Random.Range(-10, 10), Height / 2, 0));
                speed = Random.Range(20f, 30f);
                break;
            case MovementPattern.Circle:
                Vector3 centerPos = new Vector3(Random.Range(-Width/2, Width/2), Random.Range(-Height/2, Height/2), 0);
                float radius = Random.Range(Width/10, Width/5);
                for (float i = Random.Range(-10, 10); i <= 10; i++)
                {
                    float anAngle = i / 10 * 360;
                    Vector3 aPosOnCircle = new Vector3(
                        centerPos.x + radius * Mathf.Sin(anAngle * Mathf.Deg2Rad),
                        centerPos.y + radius * Mathf.Cos(anAngle * Mathf.Deg2Rad),
                        centerPos.z
                        );
                    Waypoints.Add(aPosOnCircle);
                }
                speed = Random.Range(5f, 12f);
                break;
        }
        GetNextPoint();
        transform.position = _nextWayPointPosition;
    }

    public void ResetWaypoints()
    {
        Waypoints.Clear();
        currentState = VoterState.Walking;
    }

    void Update () {
        switch (currentState)
        {
            case VoterState.Walking:
                Search();
                Walk();
                break;
            case VoterState.Captured:
                Walk();
                break;
        }
    }

    void Search()
    {
        if (transform.position == _nextWayPointPosition)
        {
            // Loop through all points nextWayPoint = (nextWayPoint + 1) % agent.wayPoints.Length; or randomize:
            GetNextPoint();
        }
    }

    void Walk()
    {
        if (_nextWayPointPosition != transform.position)
        {
            _moveDirection = (transform.position - _startingPos);
            transform.position = Vector3.MoveTowards(transform.position, _nextWayPointPosition, speed * Time.deltaTime);
        }
        if (_moveDirection != Vector3.zero)
        {
            angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    void GetNextPoint()
    {
        switch (currentPattern)
        {
            case MovementPattern.Random:
                nextWaypoint = Utilities.RandomInt(0, Waypoints.Count);
                _nextWayPointPosition = Waypoints[nextWaypoint];
                break;
            case MovementPattern.RightLeft:
                nextWaypoint = (nextWaypoint + 1 )% Waypoints.Count;
                _nextWayPointPosition = Waypoints[nextWaypoint];
                break;
            case MovementPattern.TopDown:
                nextWaypoint = (nextWaypoint + 1) % Waypoints.Count;
                _nextWayPointPosition = Waypoints[nextWaypoint];
                break;
            case MovementPattern.LeftRight:
                nextWaypoint = (nextWaypoint + 1) % Waypoints.Count;
                _nextWayPointPosition = Waypoints[nextWaypoint];
                break;
            case MovementPattern.Downtop:
                nextWaypoint = (nextWaypoint + 1) % Waypoints.Count;
                _nextWayPointPosition = Waypoints[nextWaypoint];
                break;
            case MovementPattern.Circle:
                nextWaypoint = (nextWaypoint + 1) % Waypoints.Count;
                _nextWayPointPosition = Waypoints[nextWaypoint];
                break;
        }
        _startingPos = transform.position;
    }

    public void Capture()
    {
        _nextWayPointPosition = MG_2.Instance.TargetStage.transform.position;
        _startingPos = transform.position;
        currentState = VoterState.Captured;
    }
}