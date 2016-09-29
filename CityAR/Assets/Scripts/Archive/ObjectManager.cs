using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ObjectManager : NetworkBehaviour {

	public GameObject House;
	public GameObject ImageTarget;
	public GameObject LocalPlayerObject;
	public static ObjectManager Instance = null;
	public SyncListInt UnemploymentRates = new SyncListInt();
	public SyncListInt PollutionRates = new SyncListInt();
	public SyncListBool OccupiedGrid = new SyncListBool();

	void Start () {
		//Check if instance already exists
		if (Instance == null)

			//if not, set instance to this
			Instance = this;

		//If instance already exists and it's not this:
		else if (Instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
		if (isServer)
		{
			for (int i = 0; i < GridArray.Instance.Count; i++)
			{
				UnemploymentRates.Add(Random.Range(0,100));
				PollutionRates.Add(Random.Range(0,100));
				OccupiedGrid.Add(false);

			}
		}
		InvokeRepeating("UpdateGridVariables", 0f, 0.5f);
		ImageTarget = GameObject.Find("ImageTarget");

	}

	void UpdateGridVariables()
	{
		GridArray.Instance.UpdateGridVars(UnemploymentRates, PollutionRates, OccupiedGrid);
	}

	void Update () {
		
	}

	public void UpdateOccupiedVar(int grid, bool value)
	{
			OccupiedGrid[grid] = value;
	}
	public void UpdateUnemploymentVar(int grid, int value)
	{	
			UnemploymentRates[grid] += value;
	}
	public void UpdatePollutionVar(int grid, int value)
	{
			PollutionRates[grid] += value;
	}
	public void SpawnNewObject()
	{
			LocalPlayerObject.GetComponent<MessageCommunicator>().SpawnObject();
	}
}
