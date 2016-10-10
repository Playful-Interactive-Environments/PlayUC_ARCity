using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;


public class UIManager : AManager<UIManager>
{
    


    public Canvas GameCanvas;
    public Canvas NetworkCanvas;
    public Canvas RoleCanvas;
    public Canvas EventCanvas;
    public Button HeatmapButton;
    public Text DebugText;
    public Button Environment;
    public Button Finance;
    public Button Social;
    public Button Switch;

    public void ChooseEnvironment()
    {
        CellManager.Instance.NetworkCommunicator.TakeRole("Environment");
        RoleManager.Instance.CurrentRole = RoleManager.RoleType.Environment;
        HeatmapButton.onClick.RemoveAllListeners();
        HeatmapButton.onClick.AddListener(() => ToggleEnvironmentMap());
        HeatmapButton.GetComponentInChildren<Text>().text = "Environment";
        Invoke("GameUI", .1f);
    }

    public void ChooseFinance()
    {
        CellManager.Instance.NetworkCommunicator.TakeRole("Finance");
        RoleManager.Instance.CurrentRole = RoleManager.RoleType.Finance;
        HeatmapButton.onClick.RemoveAllListeners();
        HeatmapButton.onClick.AddListener(() =>  ToggleFinanceMap() );
        HeatmapButton.GetComponentInChildren<Text>().text = "Finance";
        Invoke("GameUI", .1f);
    }

    public void ChooseSocial()
    {
        CellManager.Instance.NetworkCommunicator.TakeRole("Social");
        HeatmapButton.onClick.RemoveAllListeners();
        HeatmapButton.onClick.AddListener(() => ToggleSocialMap());
        HeatmapButton.GetComponentInChildren<Text>().text = "Social";
        RoleManager.Instance.CurrentRole = RoleManager.RoleType.Social;
        Invoke("GameUI", .1f);
    }
    void Start ()
    {
        ResetMenus();
    }

    void Update ()
    {
        UpdateRoleButtons();
    }

	public void ToggleSocialMap()
	{
        EventManager.TriggerEvent("SocialMap");
        Invoke("RefreshGrid", .01f);
        Debug.Log("SocialMap");
	}
	public void ToggleEnvironmentMap()
	{
        EventManager.TriggerEvent("EnvironmentMap");
        Invoke("RefreshGrid", .01f);
        Debug.Log("EnvironmentMap");
    }

    public void ToggleFinanceMap()
	{
        EventManager.TriggerEvent("FinanceMap");
        Invoke("RefreshGrid", .01f);
        Debug.Log("FinanceMap");    
    }

    public void RefreshGrid()
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

    public void RoleUI()
    {
        NetworkCanvas.gameObject.SetActive(false);
        RoleCanvas.gameObject.SetActive(true);
    }
    public void GameUI()
    {
        RoleCanvas.gameObject.SetActive(false);
        GameCanvas.gameObject.SetActive(true);
        Switch.gameObject.SetActive(true);
    }

    public void EventUI()
    {
        if (GameCanvas.gameObject.activeInHierarchy)
        {
            EventCanvas.gameObject.SetActive(true);
            GameCanvas.gameObject.SetActive(false);
        }
        else
        {
            EventCanvas.gameObject.SetActive(false);
            GameCanvas.gameObject.SetActive(true);
        }
    }
    void UpdateRoleButtons()
    {
        if (RoleManager.Instance != null)
        {
            if (RoleManager.Instance.Environment)
            {
                Environment.interactable = false;
            }
            else
            {
                Environment.interactable = true;
            }
            if (RoleManager.Instance.Social)
            {
                Social.interactable = false;
            }
            else
            {
                Social.interactable = true;
            }
            if (RoleManager.Instance.Finance)
            {
                Finance.interactable = false;
            }
            else
            {
                Finance.interactable = true;
            }
        }

    }

    public void ResetMenus()
    {
        NetworkCanvas.gameObject.SetActive(true);
        GameCanvas.gameObject.SetActive(false);
        RoleCanvas.gameObject.SetActive(false);
        Switch.gameObject.SetActive(false);
        EventCanvas.gameObject.SetActive(false);
    }
}
