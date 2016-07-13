using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PedestrianMovement : MonoBehaviour {

	//junction the object should move to
	public GameObject targetLocation;

	//prefab that should be used for arrow
	public GameObject arrowPrefab;

	//if gyro controls should be used for this pedestrian
	public bool gyroControls = false;

	//if there is an selected arrow
	public bool arrowSelected;
	public bool pedestrianIsSelected = false;

	//Defines how far off the Vehicle is from the Road
	private float vehicleOffset = 0.15f;

	//time that the pedestrians need to walk from one junction to the next
	private float lerpTime = (1.0f/30f);

	//junction the object is currently on
	private GameObject currentJunction;

	//street the object should move on
	private GameObject targetStreet;

	//position of the street within the array of the junction
	private int streetPosition;

	//tells if object is already walking
	private bool walking = false;

	//tells if junction has already been found
	private bool foundJunction = false;

	//tells if street has already been found
	private bool foundStreet = false;

	//where the object is no within the bezier curve (the street)
	private Vector3 lastPoint;

	//where the object will move within the bezier curve (the street)
	private Vector3 currentPoint;

	//current status of movement
	private float startTime;
	private float currentLerpTime;

	//if object should take random streets
	private bool randomDirection = true;

	//Streetnetwork of pedestrian
	private GameObject streetNetwork;

	//old rotation pedestrian
	private float oldRotation;

	//directoin of gyro
	private float dir;

	//List of control arrows from this pedestrian
	public List<ArrowLogic> controlArrows;

	/*
	 * Searches for parent network
	 * Defines if there is already a target Junction, otherwise goes random directions
	 */

	void Start () {
		streetNetwork = this.transform.parent.gameObject;

		if (targetLocation == null) {
			randomDirection = true;
		} else {
			randomDirection	= false;
		}
	}

	void Update () {
		if (transform.parent.GetComponent<StreetNetwork> ().isWaypointSelected ()) {
			if (transform.parent.GetComponent<StreetNetwork>().isPedestrianSelected()) {
				transform.parent.GetComponent<StreetNetwork>().selectedPedestrian().GetComponent<PedestrianMovement>().targetLocation = transform.parent.GetComponent<StreetNetwork> ().selectedWaypoint ();
				randomDirection = false;
			}
		}

		if (!foundJunction) {
			DestroyArrows ();
			seekJunction ();
			if (targetLocation != null && currentJunction == targetLocation) {
				targetLocation = null;
				transform.parent.GetComponent<StreetNetwork> ().changeAllWayPointsToNormal ();
				pedestrianIsSelected = false;
			}
			foundJunction = true;
		}

		if (!foundStreet) {
			if (arrowSelected) {
				randomDirection = false;
				for (int i = 0; i < transform.childCount; i++) {
					if (transform.GetChild (i).CompareTag ("Arrow")) {
						if (transform.GetChild (i).GetComponent<ArrowLogic> ().isSelected == true) {
							targetStreet = transform.GetChild (i).GetComponent<ArrowLogic> ().arrowTarget;
							for (int j = 0; j < currentJunction.GetComponent<Junction> ().connectingStreets.Length; j++) {
								if (targetStreet == currentJunction.GetComponent<Junction> ().connectingStreets [j]) {
									streetPosition = j;
								}
							}
						}
					}
				}
				createControlArrows ();
			} else if (targetLocation == null) {
				randomDirection = true;
			}

			if (randomDirection) {
				seekRandomStreet ();

			} else if (!arrowSelected && targetLocation != null) {
				DestroyArrows ();
				seekStreetTargetLocation ();
			}
		
			if (randomDirection && !this.pedestrianIsSelected) {
				createControlArrows ();
			}

			foundStreet = true;
			arrowSelected = false;
		}

		if (gyroControls == true && SystemInfo.deviceType == DeviceType.Handheld) {
			firstPersonGyroController ();
		}

		if (!walking) {
			walkDownStreet ();
			walking = true;
		}

		lerpTime = (1.0f / 30f) * targetStreet.GetComponent<BezierCurve> ().length;
			
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}
			
		float perc = currentLerpTime / lerpTime;
		transform.position = Vector3.Lerp (lastPoint, currentPoint, perc);
		oldRotation = transform.rotation.eulerAngles.y;
		transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x, 180f - calcAngle (lastPoint.x, lastPoint.z, currentPoint.x, currentPoint.z), transform.rotation.eulerAngles.z));
		counterRotateArrows ();

		transform.Translate (vehicleOffset, 0f, 0f);
	}

	/*
	 * Looks for the closest junction and defines it as currentJunction
	 */

	void seekJunction() {
		for (int j = 0; j < streetNetwork.GetComponent<StreetNetwork>().allJunctions.Length; j++) {
			if ((streetNetwork.GetComponent<StreetNetwork>().allJunctions[j].gameObject.transform.position.x - 0.2 <= transform.position.x && streetNetwork.GetComponent<StreetNetwork>().allJunctions[j].gameObject.transform.position.x + 0.2 >= transform.position.x) &&
				(streetNetwork.GetComponent<StreetNetwork>().allJunctions[j].gameObject.transform.position.z - 0.2 <= transform.position.z && streetNetwork.GetComponent<StreetNetwork>().allJunctions[j].gameObject.transform.position.z + 0.2 >= transform.position.z)) {
				currentJunction = streetNetwork.GetComponent<StreetNetwork>().allJunctions[j].gameObject;
			}
		}
	}

	/*
	 * Looks through all the streets within the currentJunction and selects one randomly (will not choose the street from which the pedestrian came)
	 */

	void seekRandomStreet() {
		if (targetStreet != null) {
			streetPosition = (int)Random.Range (0, currentJunction.GetComponent<Junction> ().connectingStreets.Length);
			while (targetStreet == currentJunction.GetComponent<Junction> ().connectingStreets [streetPosition]) {
				streetPosition = (int)Random.Range (0, currentJunction.GetComponent<Junction> ().connectingStreets.Length);
			}
			targetStreet = currentJunction.GetComponent<Junction>().connectingStreets[streetPosition];
		} else {
			streetPosition = (int)Random.Range (0, currentJunction.GetComponent<Junction> ().connectingStreets.Length);
			targetStreet = currentJunction.GetComponent<Junction>().connectingStreets[streetPosition];
		}
	}

	/*
	 * Takes the streets of the currentJunction and compares the junctions at their endpoints to the target
	 * Whatever Street ends closer to the target is chosen
	 */

	void seekStreetTargetLocation() {
		Junction junctionComponent = currentJunction.GetComponent<Junction> ();
		float minDistance = 100f;
		GameObject oldTargetStreet = targetStreet;

		for (int i = 0; i < junctionComponent.connectingStreets.Length; i++) {

			//Defines if the street starts here (0) or ends here (bigger 0)
			//Important to find Junction at the end of the street (to compare distances)
			int endpoint;
			if (junctionComponent.connectingStreetPoint[i] == 0) {
				endpoint = junctionComponent.connectingStreets [i].GetComponent<BezierCurve> ().pointCount-1;
			} else {
				endpoint = 0;
			}

			Vector3 endpointPosition = currentJunction.GetComponent<Junction> ().connectingStreets [i].GetComponent<BezierCurve> ().GetAnchorPoints () [endpoint].position;
		
			float dist = Vector3.Distance (endpointPosition, targetLocation.transform.position);

			if (dist < minDistance && oldTargetStreet != junctionComponent.connectingStreets[i]) {
				minDistance = dist;
				targetStreet = junctionComponent.connectingStreets [i];
				streetPosition = i;
			}
		}
	}

	/*
	 * Creates the control arrows for a pedestrian object
	 * Rotates the arrows according to the streets of the next junction
	 * Saves all arrows into a list and sorts them
	 */

	void createControlArrows() {
		Junction nextJunction = findNextJunction ();
		for (int i = 0; i < nextJunction.connectingStreets.Length; i++) {
			if (nextJunction.connectingStreets [i] != targetStreet) {
				GameObject arrow = Instantiate(arrowPrefab, this.transform.position, arrowPrefab.transform.rotation) as GameObject;
				arrow.transform.SetParent (this.transform);
				arrow.transform.Rotate (new Vector3 (0, 0, calcAngle(nextJunction.transform.position.x, nextJunction.transform.position.z,  nextJunction.connectingStreets [i].GetComponent<BezierCurve> ().GetPointAt (0.5f).x,  nextJunction.connectingStreets [i].GetComponent<BezierCurve> ().GetPointAt (0.5f).z)));
				arrow.transform.Translate (0.6f, 0f, 0f);
				arrow.GetComponent<ArrowLogic> ().arrowTarget = nextJunction.connectingStreets [i];
				arrow.tag = "Arrow";
				controlArrows.Add (arrow.GetComponent<ArrowLogic>());
			}
		}
		controlArrows.Sort (SortByAngle);
	}

	/*
	 * Starts walking coroutine
	 */

	void walkDownStreet(){
		StartCoroutine (walkingMethod ());
	}

	/*
	 * Walks down a given Street
	 * --- Important ---
	 * Needs streetPosition within the junction to determine where it should start to walk
	 * Needs currentJunction
	 */

	IEnumerator walkingMethod() {
		BezierCurve randomStreetBezier = targetStreet.GetComponent<BezierCurve> ();
		int limit = randomStreetBezier.resolution + 1;
		float _res = randomStreetBezier.resolution;
		bool normal = true;

		if (currentJunction.GetComponent<Junction> ().connectingStreetPoint [streetPosition] != 0) {
			normal = false;
		}

		lastPoint = randomStreetBezier.GetAnchorPoints () [currentJunction.GetComponent<Junction>().connectingStreetPoint[streetPosition]].position;
		currentPoint = Vector3.zero;

		if (normal) {
			for (int i = 0; i < limit; i++) {
				currentPoint = randomStreetBezier.GetPointAt (i / _res);
				startTime = Time.time;
				yield return new WaitForSeconds (lerpTime);
				currentLerpTime = 0f;
				lastPoint = currentPoint;
			}
		} else {
			for (int i = limit - 1; i >= 0; i--) {
				currentPoint = randomStreetBezier.GetPointAt (i / _res);
				startTime = Time.time;
				yield return new WaitForSeconds (lerpTime);
				currentLerpTime = 0f;
				lastPoint = currentPoint;
			}
		}
	
		walking = false;
		foundStreet = false;
		foundJunction = false;
	}

	/*
	 * Finds the next junction if a street is already selected
	 */

	private Junction findNextJunction() {
		Vector3 nextJunctionPosition;
		GameObject nextJunction = null;
		if (currentJunction.GetComponent<Junction>().connectingStreetPoint[streetPosition] == 0) {
			nextJunctionPosition = targetStreet.GetComponent<BezierCurve> ().GetPointAt (1);
		} else {
			nextJunctionPosition = targetStreet.GetComponent<BezierCurve> ().GetPointAt (0);
		}

		Junction[] allJunctions = GameObject.FindObjectOfType<StreetNetwork> ().allJunctions;
		for (int i = 0; i < allJunctions.Length; i++) {
			if ((allJunctions [i].gameObject.transform.position.x - 0.1f <= nextJunctionPosition.x && allJunctions [i].gameObject.transform.position.x + 0.1f >= nextJunctionPosition.x) &&
			    (allJunctions [i].gameObject.transform.position.z - 0.1f <= nextJunctionPosition.z && allJunctions [i].gameObject.transform.position.z + 0.1f >= nextJunctionPosition.z)) {
				nextJunction = allJunctions [i].transform.gameObject;
			}
		}
			
		return nextJunction.GetComponent<Junction> ();
	}

	//Calculates the Angle between two points
	private float calcAngle(float x, float y, float x1, float y1) {
		float _angle = (float)Mathf.Rad2Deg*(Mathf.Atan2 (x1 - x, y - y1));
		return _angle;
	}

	/*
	 * If pedestrian is not selected, select him and show waypoints
	 * if waypoint gets selected, pedestrian will move to this
	 * 
	 * If pedestrian is selected, unselect him and hide all waypoints
	 */

	private void OnMouseDown() {
		DestroyArrows ();
		if (!pedestrianIsSelected) {
			streetNetwork.GetComponent<StreetNetwork> ().showWayPoints ();
			transform.parent.GetComponent<StreetNetwork> ().unselectAllPedestrians ();
			pedestrianIsSelected = true;
			randomDirection = true;
		} else {
			streetNetwork.GetComponent<StreetNetwork> ().hideWayPoints ();
			pedestrianIsSelected = false;
			createControlArrows ();
		}
	}

	//Destroys all current control arrows from this pedestrian
	private void DestroyArrows() {
		for (int j = 0; j < this.transform.childCount; j++) {
			if (this.transform.GetChild (j).CompareTag ("Arrow")) {
				Destroy (this.transform.GetChild(j).gameObject);
			}
		}
		controlArrows.Clear ();
	}

	/*
	 * When the parents gets rotated, the children (the arrows) have to be rotated back so they still look into the right direction
	 * Rotates the arrows back (takes old parent rotation for this)
	 */

	private void counterRotateArrows() {
		for (int i = 0; i < this.transform.childCount; i++) {
			if (this.transform.GetChild (i).CompareTag ("Arrow")) {
				GameObject child = this.transform.GetChild (i).gameObject;
				child.transform.RotateAround (this.transform.position, Vector3.up, oldRotation - transform.rotation.eulerAngles.y);
			}
		}
	}

	//Controlls the selected arrow with the gyroscope
	private void firstPersonGyroController() {
		dir=Input.acceleration.x + 0.5f;
		float arrowSegments = 1f / (float)controlArrows.Count;
		for (int i = 0; i < controlArrows.Count; i++) {
			if (i * arrowSegments < dir && i + 1 * arrowSegments > dir) {
				controlArrows[i].getsSelected();
			}
		}
	}

	//Sort Allgorithim to get Arrows into and consistend order (used for gyroscope controlls)
	/*
	 * At first searches for the angle opposite of the parent global angle (e. g. Parent Angle = 90° -> Opposite angle = 270° -> biggest angle is now 270°, everything
	 * that is bigger than 270° must be substracted by 360°)
	 * Then adjustes both comparable angles globally to the parent angle (e. g. in this case 271° should be smaller than 269° --> 271° - 360° to adjust it)
	 * Finally compaes the adjusted angles and defines which is smaller/bigger
	 */

	static int SortByAngle(ArrowLogic p1, ArrowLogic p2) {
		float mappingValue;
		float adjustedp1Y = p1.transform.rotation.eulerAngles.y;
		float adjustedp2Y = p2.transform.rotation.eulerAngles.y;

		if (p1.transform.parent.transform.rotation.eulerAngles.y > 180) {
			mappingValue = p1.transform.parent.transform.rotation.eulerAngles.y - 180;
		} else {
			mappingValue = p1.transform.parent.transform.rotation.eulerAngles.y + 180;
		}

		if (p1.transform.rotation.eulerAngles.y > mappingValue && mappingValue > 180) {
			adjustedp1Y -= 360;
		} else if (p1.transform.rotation.eulerAngles.y < mappingValue && mappingValue < 180) {
			adjustedp1Y += 360;
		}

		if (p2.transform.rotation.eulerAngles.y > mappingValue && mappingValue > 180) {
			adjustedp2Y -= 360;
		} else if (p2.transform.rotation.eulerAngles.y < mappingValue && mappingValue < 180) {
			adjustedp2Y += 360;
		}
			
		if (adjustedp1Y < adjustedp2Y) {
			return -1;
		} else if (adjustedp1Y > adjustedp2Y) {
			return 1;
		} else {
			return 0;
		}
	}
}