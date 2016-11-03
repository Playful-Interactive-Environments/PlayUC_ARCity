using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Project : NetworkBehaviour
{

	[SyncVar]
	public string Title;
	[SyncVar]
	public string Description;
	[SyncVar]
	public int Rating;
	[SyncVar]
	public int Social;
	[SyncVar]
	public int Environment;
	[SyncVar]
	public int Finance;
	[SyncVar]
	public int Cost;
	[SyncVar]
	public int ProjectId;

	public HexCell Cell;

	public int EffectiveTime = 5;

	void Start () {
		transform.name = "Project";
		transform.parent = CellManager.Instance.ImageTarget.transform;
	}

	void Update () {
	
	}

	public void ProjectEffect()
	{
		CellManager.Instance.UpdateFinance(Cell.CellId, Finance);
		CellManager.Instance.UpdateSocial(Cell.CellId, Social);
		CellManager.Instance.UpdateEnvironment(Cell.CellId, Environment);
		
		if (EffectiveTime == 0)
		{
			Debug.Log(Cell.GetComponent<CellLogic>().GetVars());
			CancelInvoke("ProjectEffect");
		}
		EffectiveTime--;
	}

	public void ShowProjectInfo()
	{
		UIManager.Instance.ProjectInfo.text = QuestManager.Instance.GetProjectDescription(ProjectId);
		UIManager.Instance.ShowProjectInfo();
	}

	public void PlaceProject(Vector3 pos)
	{
		Cell = HexGrid.Instance.GetCell(pos);

		RoleManager.Instance.Budget += Cost;
		RoleManager.Instance.Rating += Rating;

		InvokeRepeating("ProjectEffect", 0f, 1f);
		Debug.Log(Cell.GetComponent<CellLogic>().GetVars());

	}
}
