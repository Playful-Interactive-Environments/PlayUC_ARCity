using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Vuforia;
using Image = UnityEngine.UI.Image;


public class UIManager : AManager<UIManager>
{
	public enum HeatmapState
	{
		PlacementState, JobsState, PollutionState
	}

	public HeatmapState CurrentState = HeatmapState.PollutionState;
	public HeatmapState LastState;

	#region GameUI
	public Canvas GameCanvas;
	public Button Select;
	public Button Drop;
	public Button UnemploymentButton;
	public Button PollutionButton;
	public Button TopographicButton;
	public Button ClearButton;
	public Text DebugText;
	public Image Pointer;
	#endregion
	#region NetworkUI
	public Canvas NetworkCanvas;

	#endregion
	public Button Switch;


	void Start () {

		NetworkCanvas.gameObject.SetActive(true);
		GameCanvas.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		Pointer.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0f);
		DebugText.transform.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f - 100f, 0f);
	}

	public void ToggleUnemploymentMap()
	{
        UnemploymentButton.interactable = false;
        PollutionButton.interactable = true;
        EventManager.TriggerEvent("JobsMap");
        Invoke("RefreshGrid", .01f);
	}
	public void TogglePollutionMap()
	{
        PollutionButton.interactable = false;
        UnemploymentButton.interactable = true;
        EventManager.TriggerEvent("PollutionMap");
        Invoke("RefreshGrid", .01f);
	}

	public void ToggleClearMap()
	{
        UnemploymentButton.interactable = true;
        PollutionButton.interactable = true;
        EventManager.TriggerEvent("PlacementMap");
        Invoke("RefreshGrid", .01f);
	}

	void RefreshGrid()
	{
		HexGrid.Instance.Refresh();
	}
	public void SwitchUI()
	{
		if (NetworkCanvas.gameObject.activeInHierarchy == false)
		{
			NetworkCanvas.gameObject.SetActive(true);
			GameCanvas.gameObject.SetActive(false);
		}
		else
		{
			NetworkCanvas.gameObject.SetActive(false);
			GameCanvas.gameObject.SetActive(true);
		}
	}

	public void Spawn()
	{
		if (ObjectManager.Instance != null)
		{
			ObjectManager.Instance.SpawnNewObject();
		}
	}
}
