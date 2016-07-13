using UnityEngine;
using System.Collections;

public class StreetNetwork : MonoBehaviour {

	public Junction[] allJunctions;
	public PedestrianMovement[] allPedestrians;
	private BezierCurve[] allBezierCurves;
	public Material standardJunctionMaterial;
	public Material selectedJunctionMaterial;

	void Start () {
		//Array with all Junctions
		allJunctions = Object.FindObjectsOfType<Junction> ();

		//Array with all Pedestrians
		allPedestrians = Object.FindObjectsOfType<PedestrianMovement> ();

		//Array with all Streets
		allBezierCurves = Object.FindObjectsOfType<BezierCurve> ();

		//defines how many streets are ending in the junction
		for (int i = 0; i < allJunctions.Length; i++) {
			int counter = 0;
			for (int j = 0; j < allBezierCurves.Length; j++) {
				for (int k = 0; k < allBezierCurves [j].GetAnchorPoints ().Length; k++) {
					if ((allJunctions [i].transform.position.x >= allBezierCurves [j].GetAnchorPoints () [k].transform.position.x - 0.2f && allJunctions [i].transform.position.x <= allBezierCurves [j].GetAnchorPoints () [k].transform.position.x + 0.2f) &&
						(allJunctions [i].transform.position.z >= allBezierCurves [j].GetAnchorPoints () [k].transform.position.z - 0.2f && allJunctions [i].transform.position.z <= allBezierCurves [j].GetAnchorPoints () [k].transform.position.z + 0.2f)) {
						counter++;
					}
				}
			}
			allJunctions [i].connectingStreets = new GameObject[counter];
		}

		//fills the array of streets
		for (int i = 0; i < allJunctions.Length; i++) {
			int counter = 0;
			for (int j = 0; j < allBezierCurves.Length; j++) {
				for (int k = 0; k < allBezierCurves [j].GetAnchorPoints ().Length; k++) {
					if ((allJunctions [i].transform.position.x >= allBezierCurves [j].GetAnchorPoints () [k].transform.position.x - 0.2f && allJunctions [i].transform.position.x <= allBezierCurves [j].GetAnchorPoints () [k].transform.position.x + 0.2f) &&
					    (allJunctions [i].transform.position.z >= allBezierCurves [j].GetAnchorPoints () [k].transform.position.z - 0.2f && allJunctions [i].transform.position.z <= allBezierCurves [j].GetAnchorPoints () [k].transform.position.z + 0.2f)) {
						allJunctions [i].connectingStreets [counter] = allBezierCurves [j].GetAnchorPoints () [k].transform.parent.gameObject;
						counter++;
					}
				}
			}
		}

		//fills out the array with the street start points
		for (int j = 0; j < allJunctions.Length; j++) {
			allJunctions[j].connectingStreetPoint = new int[allJunctions[j].connectingStreets.Length];
			for (int i = 0; i < allJunctions[j].connectingStreets.Length; i++) {
				int numberOfPoints = allJunctions[j].connectingStreets[i].GetComponent<BezierCurve> ().pointCount;
				BezierPoint[] bezierPointArray = new BezierPoint[numberOfPoints];
				bezierPointArray = allJunctions[j].connectingStreets [i].GetComponent<BezierCurve> ().GetAnchorPoints ();

				for (int k = 0; k < bezierPointArray.Length; k++) {
					if ((bezierPointArray [k].position.x + 0.2 >= allJunctions[j].transform.position.x && bezierPointArray [k].position.x - 0.2 <= allJunctions[j].transform.position.x) &&
						(bezierPointArray [k].position.z + 0.2 >= allJunctions[j].transform.position.z && bezierPointArray [k].position.z - 0.2 <= allJunctions[j].transform.position.z)) {
						allJunctions [j].connectingStreetPoint [i] = k;
					}
				}
			}
		}

		//Readjusts all nearby streets to position of the junction
		for (int i = 0; i < allJunctions.Length; i++) {
			for (int j = 0; j < allJunctions [i].connectingStreets.Length; j++) {
				if ((allJunctions [i].connectingStreets [j].GetComponent<BezierCurve> ().GetAnchorPoints () [allJunctions[i].connectingStreetPoint[j]].transform.position.x <= allJunctions[i].transform.position.x + 0.2f &&
					allJunctions [i].connectingStreets [j].GetComponent<BezierCurve> ().GetAnchorPoints () [allJunctions[i].connectingStreetPoint[j]].transform.position.x >= allJunctions[i].transform.position.x - 0.2f) &&
					(allJunctions [i].connectingStreets [j].GetComponent<BezierCurve> ().GetAnchorPoints () [allJunctions[i].connectingStreetPoint[j]].transform.position.z <= allJunctions[i].transform.position.z + 0.2f &&
						allJunctions [i].connectingStreets [j].GetComponent<BezierCurve> ().GetAnchorPoints () [allJunctions[i].connectingStreetPoint[j]].transform.position.z >= allJunctions[i].transform.position.z - 0.2f)) {
					allJunctions [i].connectingStreets [j].GetComponent<BezierCurve> ().GetAnchorPoints () [allJunctions [i].connectingStreetPoint [j]].transform.position = new Vector3(allJunctions [i].transform.position.x, allJunctions [i].connectingStreets [j].GetComponent<BezierCurve> ().GetAnchorPoints () [allJunctions [i].connectingStreetPoint [j]].transform.position.y, allJunctions [i].transform.position.z);
				}
			}
		}

		hideWayPoints ();
	}

	//makes every Waypoint/Junction visible
	public void showWayPoints() {
		for (int i = 0; i < allJunctions.Length; i++) {
			allJunctions[i].gameObject.GetComponent<MeshRenderer> ().enabled = true;
		}
	}

	//makes every Waypoint/Junction invisible
	public void hideWayPoints() {
		for (int i = 0; i < allJunctions.Length; i++) {
			allJunctions[i].gameObject.GetComponent<MeshRenderer> ().enabled = false;
		}
	}

	//unselects all Waypoints/Junctions and changes their appearance to standard
	public void changeAllWayPointsToNormal() {
		for (int i = 0; i < allJunctions.Length; i++) {
			allJunctions [i].GetComponent<MeshRenderer> ().material = standardJunctionMaterial;
			allJunctions [i].isSelectedWayPoint = false;
		}
	}

	//returns if any Waypoint/Junction is currently selected
	public bool isWaypointSelected() {
		for (int i = 0; i < allJunctions.Length; i++) {
			if (allJunctions [i].isSelectedWayPoint) {
				return true;
			}
		}
		return false;
	}

	//returns currently selected Waypoint/Junction
	public GameObject selectedWaypoint() {
		for (int i = 0; i < allJunctions.Length; i++) {
			if (allJunctions [i].isSelectedWayPoint) {
				return allJunctions[i].gameObject;
			}
		}
		return null;
	}

	//returns if any pedestrians is currently selected
	public bool isPedestrianSelected() {
		for (int i = 0; i < allPedestrians.Length; i++) {
			if (allPedestrians[i].pedestrianIsSelected) {
				return true;
			}
		}
		return false;
	}

	//tells currently selected pedestrian
	public GameObject selectedPedestrian() {
		for (int i = 0; i < allPedestrians.Length; i++) {
			if (allPedestrians[i].pedestrianIsSelected) {
				return allPedestrians[i].gameObject;
			}
		}
		return null;
	}

	//unselects all Pedestrians
	public void unselectAllPedestrians() {
		for (int i = 0; i < allPedestrians.Length; i++) {
			allPedestrians [i].pedestrianIsSelected = false;
		}
	}
}
