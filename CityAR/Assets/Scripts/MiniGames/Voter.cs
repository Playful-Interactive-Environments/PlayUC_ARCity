using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Voter : MonoBehaviour {

	public enum VoterState
	{
		Walking, Captured
	}

	private VoterState currentState = VoterState.Walking;
	private MGManager _mgManager;
	public Vector3 _nextWayPointPosition;
	private Vector3 _moveDirection;
	private Vector3 _startingPos;
	private float angle;
	private float speed = 20f;

	void Start ()
	{
		_mgManager = MGManager.Instance;
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
			_moveDirection = transform.position - _startingPos;
			transform.position = Vector3.MoveTowards(transform.position, _nextWayPointPosition, speed * Time.deltaTime);
		}

		if (_moveDirection != Vector3.zero)
		{
			angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	void GetRandomPoint()
	{
		int i = Utilities.RandomInt(0, _mgManager.Waypoints.Count);
		_nextWayPointPosition = _mgManager.Waypoints[i];
	}

	public void Capture()
	{
	    _nextWayPointPosition = MGManager.Instance.TargetStage.transform.position;
		currentState = VoterState.Captured;
	}

}
