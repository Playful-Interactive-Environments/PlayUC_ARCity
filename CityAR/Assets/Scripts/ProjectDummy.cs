using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectDummy : MonoBehaviour {

	private Material placementMat;
	public Material blockedMat;
	private Renderer[] allRenderers;
    public GameObject[] FinanceRepr;
    public GameObject[] SocialRepr;
    public GameObject[] EnvironmentRepr;
    private GameObject representation;
	public GameObject Arrow;
	public int RepresentationId;
	public Vector3 CurrentRot;
	public Vector3 CurrentPos;
	private float yRot;
	public bool CanPlace;
	public int Id_CSV;

	void Start () {
		transform.parent = LocalManager.Instance.ImageTarget.transform;
		ProjectManager.Instance.CurrentDummy = this;
        placementMat = GameManager.Instance.placementMat;
        CreateRepresentation();
        EventDispatcher.StartListening("StartDiscussion", StartDiscussion);
	}

	void StartDiscussion()
	{
	   DestroySelf();
	}

	void Update () {

		if (CameraControl.Instance.MainCamera.gameObject.activeInHierarchy)
		{
			transform.position = CameraControl.Instance.GetLastCell();
		}
		yRot += Time.deltaTime * 50f;
		CurrentRot = new Vector3(0, yRot, 0);
		representation.transform.localEulerAngles = CurrentRot;
		CurrentPos = transform.position;
		if (CanPlace)
			UIManager.Instance.PlacementText.text = "";
		if (!CanPlace)
			UIManager.Instance.PlacementText.text = "Mg3 occupied!";
	}

	public void CreateRepresentation()
	{
        //create 3d representation
        switch (LocalManager.Instance.RoleType)
        {
            case Vars.Player1:
                representation = Instantiate(FinanceRepr[RepresentationId], transform.position, Quaternion.identity);
                break;
            case Vars.Player2:
                representation = Instantiate(SocialRepr[RepresentationId], transform.position, Quaternion.identity);
                break;
            case Vars.Player3:
                representation = Instantiate(EnvironmentRepr[RepresentationId], transform.position, Quaternion.identity);
                break;
        }
		representation.transform.parent = transform;
		representation.transform.localScale = new Vector3(.5f, .5f, .5f);
		allRenderers = representation.GetComponentsInChildren<Renderer>();
		Arrow.transform.parent = representation.transform;
		Free();
	    CameraControl.Instance.CanTouch = false;
	}

	public void Blocked()
	{
		foreach (Renderer child in allRenderers)
		{
			child.material = blockedMat;
		}
		CanPlace = false;
	}

	public void Free()
	{
		foreach (Renderer child in allRenderers)
		{
			child.material = placementMat;
		}
		CanPlace = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.transform.tag.Equals("Project") || other.gameObject.transform.tag.Equals("Quest"))
			Blocked();
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.transform.tag.Equals("Project") || other.gameObject.transform.tag.Equals("Quest"))
			Free();
	}

	public void DestroySelf()
	{
		//reset placement text & color of cells
		UIManager.Instance.PlacementText.text = "";
	    CameraControl.Instance.CanTouch = true;
		foreach (GameObject cell in CellGrid.Instance.GridCells)
		{
			cell.GetComponent<CellLogic>().Default();
			cell.GetComponent<CellInterface>().ChangeCellText(CellInterface.TextState.Grey);
		}
		Destroy(gameObject);
	}
}
