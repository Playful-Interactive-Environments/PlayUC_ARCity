using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.Generic;
using System.Linq;

public class Advertisement : MonoBehaviour
{
	public GameObject Timer;
	public GameObject Icon;
	public GameObject Radius;
	private LineRenderer linerenderer;

	public List<Vector3> points;
	public int pointIndex;
	public int followIndex;
	private Vector3 _startingPos;
	private Vector3 _lastPointPos;
	
	private bool _started;
	private bool _dragging;
	private bool _released;

	public float AdvertisementActiveTime;
	public float CurrentPathTime;
	private const float DrawPathTime = 3.0f;
	private const float RepeatRate = 0.1f;

	void Start ()
	{
		Timer.gameObject.SetActive(false);
		linerenderer = GetComponent<LineRenderer>();

	}

	void Update () {
		if (_started)
		{
			_started = false;
			_dragging = true;
			InvokeRepeating("AddCurrentPosition", 0, RepeatRate);
		}
		if (_dragging)
		{
			//Track Time
			CurrentPathTime += Time.deltaTime;
			Timer.gameObject.SetActive(true);
			Timer.GetComponent<TextMesh>().text = Mathf.Round(DrawPathTime - CurrentPathTime) + "s";

			if (CurrentPathTime >= DrawPathTime)
			{
				Release();
			}
		}
		if (_released)
		{
			CurrentPathTime += Time.deltaTime;
			Timer.GetComponent<TextMesh>().text = Mathf.Round(CurrentPathTime) + "s";
			Radius.transform.Rotate(Vector3.forward, 40 * Time.deltaTime, 0);
			Vector3 nextWayPoint = points.ElementAt(followIndex);
			_lastPointPos = points[points.Count - 1];

			if (transform.position != nextWayPoint)
			{
				transform.position = Vector3.MoveTowards(transform.position, nextWayPoint, Time.deltaTime * 40f);
			}
			if (transform.position == _lastPointPos )
			{
				Reset();
				//Destroy(this.gameObject);|| CurrentPathTime >= AdvertisementActiveTime
			}
			if (transform.position == nextWayPoint)
			{
				followIndex ++;
			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Voter") && _released)
		{
			other.gameObject.GetComponent<Agent>().Capture();
			MG_2.Instance.VotersCollected += 1;
		}
	}
	private void AddCurrentPosition()
	{
			points.Add(transform.position);
			linerenderer.numPositions = points.Count;
			linerenderer.SetPositions(points.ToArray());
			pointIndex++;
	}

	public void StartDragging()
	{
		_startingPos = transform.position;
		_started = true;
	}

	public void Release()
	{
		transform.position = points[0];
		CancelInvoke("AddCurrentPosition");
		StartCoroutine("AdvertisementAnimation");
		GetComponent<Draggable>().enabled = false;
		_dragging = false;
		_released = true;
		linerenderer.enabled = false;
		CurrentPathTime = 0.0f;
	}

	public void Reset()
	{
		StopAllCoroutines();
		iTween.Stop();
		transform.position = _startingPos;
		_released = false;
		GetComponent<Draggable>().enabled = true;
		linerenderer.enabled = true;
		CurrentPathTime = 0;
		linerenderer.numPositions = 0;
		pointIndex = 0;
		followIndex = 0;
		points.Clear();

	}

	IEnumerator AdvertisementAnimation()
	{
		iTween.ScaleTo(Icon, iTween.Hash("scale", new Vector3(0.5f, 0.5f, 0.5f), "looptype", "pingpong"));
		yield return new WaitForSeconds(0.1f);
	}
}


