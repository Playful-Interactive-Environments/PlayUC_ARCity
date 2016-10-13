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
	public Canvas QuestCanvas;
	public Canvas ResultCanvas;
	public Button HeatmapButton;
	public Text DebugText;
	public Button Environment;
	public Button Finance;
	public Button Social;
	public Button Switch;
	public Text Title;
	public Text Content;
	public Text Choice1;
	public Text Choice2;
	public Text Choice3;
	public Text Result;

	void Start ()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		ResetMenus();
	}

	void Update ()
	{
		UpdateRoleButtons();
	}
	#region Game UI
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
	
	public void GameUI()
	{
		RoleCanvas.gameObject.SetActive(false);
		QuestCanvas.gameObject.SetActive(false);
		ResultCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(true);
		Switch.gameObject.SetActive(true);
	}
	#endregion

	#region Quest UI
	public void QuestUI()
	{
		if (GameCanvas.gameObject.activeInHierarchy)
		{
			QuestCanvas.gameObject.SetActive(true);
			GameCanvas.gameObject.SetActive(false);
			QuestManager.Instance.GetQuest();
		}
		else
		{
			QuestCanvas.gameObject.SetActive(false);
			GameCanvas.gameObject.SetActive(true);
		}
	}

	public void Choose_1()
	{
		QuestManager.Instance.GetResult(1);
		MakeChoice();
	}
	public void Choose_2()
	{
		QuestManager.Instance.GetResult(2);
		MakeChoice();
	}
	public void Choose_3()
	{
		QuestManager.Instance.GetResult(3);
		MakeChoice();
	}
	public void MakeChoice()
	{
		QuestCanvas.gameObject.SetActive(false);
		ResultCanvas.gameObject.SetActive(true);
	}
	public void Back()
	{
		ResultCanvas.gameObject.SetActive(false);
		GameCanvas.gameObject.SetActive(true);

	}
	#endregion


	#region Choose Roles

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
		HeatmapButton.onClick.AddListener(() => ToggleFinanceMap());
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
	public void RoleUI()
	{
		NetworkCanvas.gameObject.SetActive(false);
		RoleCanvas.gameObject.SetActive(true);
	}
	#endregion

	public void ResetMenus()
	{
		NetworkCanvas.gameObject.SetActive(true);
		GameCanvas.gameObject.SetActive(false);
		RoleCanvas.gameObject.SetActive(false);
		Switch.gameObject.SetActive(false);
		QuestCanvas.gameObject.SetActive(false);
		ResultCanvas.gameObject.SetActive(false);

	}
}
