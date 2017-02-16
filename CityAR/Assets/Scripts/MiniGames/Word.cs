using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;

public class Word : MonoBehaviour
{
	public TextMesh WordText;
	public string Title;
	public string Type;

	private float currentSpeed;
	private float maxSpeed = 240f;
	private float nextTick;
	private Vector3 currentPos;
	private Vector3 lastPos;
	private Vector3 startPos;
	private Vector3 moveDirection;
	private Vector3 stackPos;
	private float distance;
	private bool dropped;
	private bool collected;
    public GameObject Representation;

	void Start ()
	{
		WordText = Representation.GetComponentInChildren<TextMesh>();
	}

	public void SetVars(Vector3 start, string title, string type)
	{
		Title = SplitText(title, 15);
		Type = type;
		transform.position = start;
		startPos = start;
		WordText.text = Title;
	}

	void OnEnable()
	{
        //order words so they stack on top of each other
        Representation.transform.position = new Vector3(0,0, MG_1.Instance.zLayer);
	    GetComponent<BoxCollider>().center = new Vector3(0, 0, MG_1.Instance.zLayer);
        MG_1.Instance.zLayer--;
        iTween.Stop();
		transform.position = MG_1.Instance.StartingPos;
		transform.localScale = new Vector3(1, 1, 1);
	}

	public void Grab()
	{
		dropped = false;
	}

	public void Drag(Vector3 dragPos)
	{
		transform.position = Vector3.Lerp(transform.position, dragPos, Time.deltaTime * 10f);
	}
	public void Drop()
	{
		currentSpeed = maxSpeed;
		moveDirection = transform.position - lastPos;
		distance = Vector3.Distance(transform.position, lastPos);
		moveDirection = moveDirection.normalized;
		if(!collected)
			dropped = true;
	}

	void Update ()
	{
		if (dropped)
		{
			transform.Translate(moveDirection * Time.deltaTime * currentSpeed, Space.World);
			currentSpeed -= 8;
			if (currentSpeed <= 0)
				dropped = false;
		}
		if (collected)
		{
			transform.position = Vector3.Lerp(transform.position, stackPos, Time.deltaTime * 10f);
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
		GetComponent<Draggable>().enabled = true;
		dropped = false;
		collected = false;
		currentSpeed = 0;
		transform.position = Vector3.zero;
		ObjectPool.Recycle(this.gameObject);
	}

	string SplitText(string text, int charSize)
	{
		return text.Replace(" ", Environment.NewLine);
	}
}
/*       StringBuilder sb = new StringBuilder(text);
		int spaces = 0;
		int length = sb.Length;
		for (int i = 0; i < length; i++)
		{
			if (spaces % charSize == 0)
			{
				sb.Insert(i, Environment.NewLine);
			}
			spaces++;
		}
*/
