using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDummy : MonoBehaviour {

	public Material transparentAnimated;
	private float lerpVal;
	private Renderer[] allRenderers;
	public GameObject[] BuildingSets;
	private GameObject representation;
	public int RepresentationId;
	public Vector3 CurrentRot;
	public Vector3 CurrentPos;
	private float yRot;
	void Start () {
		transform.parent = CellManager.Instance.ImageTarget.transform;
		RepresentationId = ProjectManager.Instance.GetRepresentation(ProjectManager.Instance.SelectedCSV);
		CreateRepresentation();
	}

	void Update () {
		lerpVal = Mathf.PingPong(Time.time, 1f) / 1f;
		transparentAnimated.color = new Color(lerpVal, lerpVal, lerpVal, .5f);
		if (CameraControl.Instance.MainCamera.gameObject.activeInHierarchy)
		{
		    transform.position = CameraControl.Instance.GetLastCell();
		}
		yRot += Time.deltaTime * 50f;
		CurrentRot = new Vector3(0, yRot, 0);
		representation.transform.localEulerAngles = CurrentRot;
	    CurrentPos = transform.position;
	}

	public void CreateRepresentation()
	{
		//create 3d representation
		representation = Instantiate(BuildingSets[RepresentationId], transform.position, Quaternion.identity);
		representation.transform.parent = transform;
		representation.transform.localScale = new Vector3(.5f, .5f, .5f);
		allRenderers = representation.GetComponentsInChildren<Renderer>();
		foreach (Renderer child in allRenderers)
		{
			child.material = transparentAnimated;
		}
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}
}
