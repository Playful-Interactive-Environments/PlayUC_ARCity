using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Word : MonoBehaviour
{

	public TextMesh WordText;
	public string Title;
	public string Type;

	private float currentSpeed;
	private float maxSpeed = 200f;
	private float nextTick;
	private Vector3 currentPos;
	private Vector3 lastPos;
	private Vector3 startPos;
	private Vector3 moveDirection;
	private Vector3 stackPos;
	private bool dropped;
	private bool collected;

	void Start ()
	{
		WordText = GetComponentInChildren<TextMesh>();
	}

	public void SetVars(Vector3 start, string title, string type)
	{
		Title = title;
		Type = type;
		transform.position = start;
		startPos = start;
	}

	void OnEnable()
	{
		iTween.Stop();
		transform.position = Vector3.zero;
		transform.localScale = new Vector3(1, 1, 1);
	}

	public void Grab()
	{
	    dropped = false;
	}

	public void Drop()
	{
		currentSpeed = maxSpeed;
		moveDirection = transform.position - lastPos;
		moveDirection = moveDirection.normalized;
		if(!collected)
			dropped = true;
	}

	void Update ()
	{
		WordText.text = "" + currentSpeed;
		if (dropped)
		{
			transform.Translate(moveDirection * Time.deltaTime * currentSpeed, Space.World);
			currentSpeed -= 10f;
			if (currentSpeed <= 0)
				dropped = false;
		}
		if (collected)
		{
			transform.position = Vector3.MoveTowards(transform.position, stackPos, 100f * Time.deltaTime);
		}
		
		if (Time.time > nextTick)
		{
			lastPos = transform.position;
			nextTick = Time.time + .1f;
		}
		if (Vector3.Distance(transform.position, startPos) > MGManager.Instance.Width)
			Reset();
	}

	void OnTriggerEnter(Collider other)
	{
		stackPos = other.gameObject.transform.position;
		collected = true;
		dropped = false;
		GetComponent<Draggable>().enabled = false;
		if (other.transform.name.Equals(Type))
		{
			MG_1.Instance.CollectedDocs += 1;
		}
		else
		{
			MG_1.Instance.CollectedDocs -= 1;
		}
		Invoke("Reset", 2f);
	}

	void Reset()
	{
		StopAllCoroutines();
		ObjectPool.Recycle(this.gameObject);
		GetComponent<Draggable>().enabled = true;
		dropped = false;
		collected = false;
		currentSpeed = 0;
		transform.position = Vector3.zero;
	}
}

/*
		dropPos = transform.position;
		var force = (dropPos - startPos);
		force = force.normalized;
				//iTween.ScaleTo(gameObject, iTween.Hash("x", 0f, "y", 0f, "time", 1.5f));

		force /= Mathf.Abs(Time.time - nextTick);
		rigidbody.AddForce(force*factor);
		Invoke("Reset", 2f);
 */
