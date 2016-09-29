using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GridPieceLogic : MonoBehaviour
{

	public enum HeatmapState
	{
		PlacementState, JobsState, PollutionState, TopographicMap
	}
	public int Count;
	public int JobsRate;
	public int PollutionRate;
	public float Height;
	private bool Topograhic;
	public bool Occupied;
	public HeatmapState CurrentState = HeatmapState.PollutionState;
	public Material[] Unemployment;
	public Material[] Pollution;
	public Material[] Placement;
	private Renderer _currentRenderer;
	public TextMesh PollText;
	public TextMesh JobsText;
	public ParticleSystem PollParticles;
	public ParticleSystem JobsParticles;


	void Start () {
		PollParticles.Stop();
		JobsParticles.Stop();
		_currentRenderer = GetComponent<Renderer>();
		_currentRenderer.material = Placement[0];
		EventManager.StartListening("PlacementMap", PlacementMap);
		EventManager.StartListening("JobsMap", JobsMap);
		EventManager.StartListening("PollutionMap", PollutionMap);
		EventManager.StartListening("TopographicMap", TopographicMap);
	}
	public void PlacementMap()
	{
		CurrentState = HeatmapState.PlacementState;
	}
	public void JobsMap()
	{
		CurrentState = HeatmapState.JobsState;
	}
	public void PollutionMap()
	{
		CurrentState = HeatmapState.PollutionState;
	}
	public void TopographicMap()
	{
		if (Topograhic)
		{
			Topograhic = false;
		}
		else
		{
			Topograhic = true;
		}

	}

	void Update ()
	{
		PollText.text = "Pollution: " + PollutionRate;
		JobsText.text = "Jobs: " + JobsRate;

		if (Topograhic)
		{
			transform.position = new Vector3(transform.position.x, Height / 10, transform.position.z);
			transform.localScale = new Vector3(transform.localScale.x, Height / 5, transform.localScale.z);
		}
		if (!Topograhic)
		{
			transform.position = new Vector3(transform.position.x, 1, transform.position.z);
			transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
		}
		switch (CurrentState)
		{
			case HeatmapState.PlacementState:
				Height = 1;
				PollText.gameObject.SetActive(true);
				JobsText.gameObject.SetActive(true);

				if (Occupied)
					_currentRenderer.material = Placement[1];
				if(!Occupied)
					_currentRenderer.material = Placement[0];
				break;
			case HeatmapState.JobsState:
				Height = JobsRate;
				if (JobsRate < 33)
					_currentRenderer.material = Unemployment[0];
				if (JobsRate >= 33 && JobsRate <= 66)
					_currentRenderer.material = Unemployment[1];
				if (JobsRate > 66)
					_currentRenderer.material = Unemployment[2];
				break;
			case HeatmapState.PollutionState:
				Height = PollutionRate;
				if (PollutionRate < 33)
					_currentRenderer.material = Pollution[0];
				if (PollutionRate >= 33 && PollutionRate <= 66)
					_currentRenderer.material = Pollution[1];
				if (PollutionRate > 66)
					_currentRenderer.material = Pollution[2];
				break;

		}
	}

	public void PolutionEffect(int amount, float time)
	{
			PollParticles.maxParticles = Mathf.Abs(amount);
			PollParticles.Emit(Mathf.Abs(amount));
	}
	public void UnemploymentEffect(int amount, float time)
	{
			JobsParticles.maxParticles = Mathf.Abs(amount);
			JobsParticles.Emit(Mathf.Abs(amount));
	}
}
