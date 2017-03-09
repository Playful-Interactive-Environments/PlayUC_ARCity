using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.Generic;
using System.Linq;

public class Advertisement : MonoBehaviour
{
	public GameObject Icon;
	public GameObject Radius;
	private LineRenderer linerenderer;

	public List<Vector3> points;
	public int pointIndex;
	public int followIndex;
	private Vector3 _lastPointPos;
	
	private bool _started;
	private bool _dragging;
	private bool _released;

	public float AdvertisementActiveTime;
	private const float RepeatRate = 0.1f;
	private float distanceSum;
	private float maxDistance = 75f;

	void Start ()
	{
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
			if (distanceSum >= maxDistance)
			{
				//Release();
			}
		}
		if (_released)
		{
			Radius.transform.Rotate(Vector3.forward, 40 * Time.deltaTime, 0);
			Vector3 nextWayPoint = points.ElementAt(followIndex);
			MG_2.Instance.TimeSpent += Time.deltaTime;
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
			//StartCoroutine(AdvertisementAnimation());
		}
	}

	private void AddCurrentPosition()
	{
		if(points.Count > 0)
			distanceSum += Vector3.Distance(points[points.Count - 1], transform.position);
		points.Add(transform.position);
		linerenderer.numPositions = points.Count;
		linerenderer.SetPositions(points.ToArray());
		pointIndex++;
	}

	public void StartDragging()
	{
		_started = true;
	}

	public void Release()
	{
		transform.position = points[0];
		CancelInvoke("AddCurrentPosition");
		GetComponent<Draggable>().enabled = false;
		_dragging = false;
		_released = true;
		linerenderer.enabled = false;
		distanceSum = 0;
	}

	public void Reset()
	{
		StopAllCoroutines();
		iTween.Stop();
		if (points.Count > 0)
			transform.position = points[points.Count-1]; // set to last pos
		_released = false;
		GetComponent<Draggable>().enabled = true;
		linerenderer.enabled = true;
		linerenderer.numPositions = 0;
		pointIndex = 0;
		followIndex = 0;
		points.Clear();
	}

	IEnumerator AdvertisementAnimation()
	{
		iTween.ScaleTo(Icon, iTween.Hash("scale", new Vector3(0.5f, 0.5f, 0.5f)));
		iTween.ScaleTo(Radius, iTween.Hash("scale", new Vector3(0.5f, 0.5f, 0.5f)));
		yield return new WaitForSeconds(0.1f);
		iTween.ScaleTo(Icon, iTween.Hash("scale", new Vector3(2f, 2f, 1f)));
		iTween.ScaleTo(Radius, iTween.Hash("scale", new Vector3(1.5f, 1.5f, 1.5f)));
	}
}


