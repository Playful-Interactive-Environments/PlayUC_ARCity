using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Agent : MonoBehaviour {

	public enum VoterState
	{
		Walking, Captured
	}

	private VoterState currentState = VoterState.Walking;
	public Vector3 _nextWayPointPosition;
	private List<Vector3> Waypoints = new List<Vector3>();

	private Vector3 _moveDirection;
	private Vector3 _startingPos;
	private float angle;
	private float speed = 15f;

	public float xEast; //max
	public float xWest; // min
	public float yNorth; //max
	public float ySouth; //min
	private float randomX;
	private float randomY;
	private float tChange = 0;
    private float prevangle;

	void Start ()
	{
	}

	public void ResetWaypoints()
	{
		Waypoints.Clear();
		Invoke("GetRandomPoint",.1f);
	}

	public void AddWaypoint(Vector3 point)
	{
		Waypoints.Add(point);
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
			GetRandomPoint();
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

    void GetRandomPoint()
	{
		int i = Utilities.RandomInt(0, Waypoints.Count);
		_nextWayPointPosition = Waypoints[i];
        _startingPos = transform.position;
    }

	public void Capture()
	{
		_nextWayPointPosition = MG_2.Instance.TargetStage.transform.position;
		currentState = VoterState.Captured;
	}
}

/*
		if (Time.time >= tChange)
		{
			randomX = Random.Range(xEast, xWest);
			randomY = Random.Range(ySouth, yNorth);
			_nextWayPointPosition = new Vector3(randomX, randomY);
			angle = Vector3.Angle(transform.position, _nextWayPointPosition);
			tChange = Time.time + Utilities.RandomFloat(.5f, 1.5f);
		}
		transform.Translate(new Vector3(randomX, randomY, 0) * speed *Time.deltaTime);
		
		transform.Rotate(0, 0, iTween.FloatUpdate(transform.position.z, angle, .5f), Space.World);

		if (transform.position.x <= xWest || transform.position.x >= xEast)
		{
			randomX = -randomX;
		}
		if (transform.position.y <= ySouth || transform.position.y >= yNorth)
		{
			randomY = -randomY;
		}

			if (changePos)
		{
			randomX = Random.Range(xEast, xWest);
			randomY = Random.Range(ySouth, yNorth);
			_nextWayPointPosition = new Vector3(randomX, randomY);
			angle = Vector3.Angle(transform.position, _nextWayPointPosition);
			changePos = false;
		}
		if (transform.position == _nextWayPointPosition)
			changePos = true;
		transform.Translate(new Vector3(randomX, randomY, 0) *moveSpeed*Time.deltaTime);
		transform.Rotate(0, 0, iTween.FloatUpdate(transform.position.z, angle, rotSpeed), Space.World);
		if (transform.position.x <= xWest || transform.position.x >= xEast)
		{
			changePos = true;
		}
		if (transform.position.y <= ySouth || transform.position.y >= yNorth)
		{
			changePos = true;
		}
			public void SetBorders(float xE, float xW, float yN, float yS)
	{
		xEast = xE;
		xWest = xW;
		yNorth = yN;
		ySouth = yS;
	}
	 */
