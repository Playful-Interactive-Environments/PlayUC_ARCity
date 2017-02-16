using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Globalization;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance = null;
	private CSVLeveling _csvLeveling;
	public string RoleType;
	public Image ForegroundImage;
	public Text ProgressText;
	public Text RankText;
	public int CurrentRank;
	private int _currentInfluence;
	private int _currentGoal;
	private int _prevGoal = 0;
	private float _valueGoal;
	private int _lastRank;
	private bool levelUnlocked;
	//LevelDisplay
	public GameObject LevelTemplate;
	public GridLayoutGroup GridGroup;
	public List<GameObject> LevelInstances = new List<GameObject>();

	public int Value
	{
		get
		{
			if (ForegroundImage != null)
				return (int)(ForegroundImage.fillAmount * _valueGoal);
			else
				return 0;
		}
		set
		{
			if (ForegroundImage != null)
			{
				ForegroundImage.fillAmount = value / _valueGoal;
			}
		}
	}

	void Awake ()
	{
		//Phone Bar
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		//Level Bar
		_csvLeveling = CSVLeveling.Instance;
		EventDispatcher.StartListening("NetworkDisconnect", NetworkDisconnect);

	}

	void NetworkDisconnect()
	{
		LevelInstances.Clear();
		CancelInvoke("UpdateProgress");
	}

	public void PushGrid()
	{
		float gridHeight = GridGroup.GetComponent<RectTransform>().sizeDelta.y;
		float cellSpacing = GridGroup.GetComponent<GridLayoutGroup>().cellSize.y + GridGroup.GetComponent<GridLayoutGroup>().spacing.y;
		Debug.Log(gridHeight + " " + cellSpacing);
		GridGroup.GetComponent<RectTransform>().localPosition = new Vector3(0, -gridHeight/2 - cellSpacing + CurrentRank * cellSpacing, 0);
	}

	void Update()
	{
	}

	public void UpdateProgress()
	{
		if(SaveStateManager.Instance != null && CurrentRank < 20)
		{
			//keep track of influence & rank
			_currentInfluence = SaveStateManager.Instance.GetInfluence(RoleType);
			CurrentRank = SaveStateManager.Instance.GetRank(RoleType);
			_currentGoal = ConvertString(_csvLeveling.Find_Rank(CurrentRank + 1).influencegoal);
			//update value variable for the progress bar
			_prevGoal = ConvertString(_csvLeveling.Find_Rank(CurrentRank).influencegoal);

			Value = _currentInfluence - _prevGoal;
			_valueGoal = _currentGoal - _prevGoal;

			//update text
			ProgressText.text = _currentInfluence + " / " + _currentGoal;
			RankText.text = CurrentRank + "";

			//check if new level reached
			if (_currentInfluence >= _currentGoal && CellManager.Instance.NetworkCommunicator !=null && !levelUnlocked)
			{
				CellManager.Instance.NetworkCommunicator.UpdateData(RoleType, "Rank", 1);
				_lastRank = CurrentRank;
				levelUnlocked = true;
			}
			if (_lastRank < CurrentRank)
				levelUnlocked = false;
		}
	    if (CurrentRank == 20)
	    {
            ProgressText.text = "WINNER!";

            Debug.Log("Trigger Mayor Game Over");
            CancelInvoke("UpdateProgress");

        }
    }

	public void CreateLevelTemplate()
	{
		InvokeRepeating("UpdateProgress", 0f, .3f);
		for (int i = 1; i <= _csvLeveling.rowList.Count-1; i++)
		{
			GridGroup = GameObject.Find("LevelLayout").GetComponent<GridLayoutGroup>();
			LevelTemplate = GameObject.Find("LevelTemplate");
			GameObject level = Instantiate(LevelTemplate, transform.position, Quaternion.identity) as GameObject;
			level.transform.parent = GridGroup.transform;
			level.transform.localScale = new Vector3(1, 1, 1);
			//_projectButton.GetComponent<Button>().onClick.AddListener(() => SelectProject());
			level.GetComponent<LevelDescription>().SetupLayout(i);
			LevelInstances.Add(level);
			GridGroup.GetComponent<RectTransform>().localPosition = new Vector3(0, -855f, 0);
		}
	}
	
	private int ConvertString(string input)
	{
		int parsedInt = 0;
		int.TryParse(input,NumberStyles.Any, null, out parsedInt);
		return parsedInt;
	}
}