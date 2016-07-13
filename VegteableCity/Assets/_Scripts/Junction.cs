using UnityEngine;
using System.Collections;

public class Junction : MonoBehaviour {

	public GameObject[] connectingStreets;
	public int[] connectingStreetPoint;
	public bool isSelectedWayPoint;

	//Selects the clicked junction and hides all waypoints/junctions
	void OnMouseDown() {
		this.isSelectedWayPoint = true;
		transform.parent.GetComponent<StreetNetwork> ().hideWayPoints ();
	}
}
