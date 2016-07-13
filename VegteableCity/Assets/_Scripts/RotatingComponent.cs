using UnityEngine;
using System.Collections;

public class RotatingComponent : MonoBehaviour {
	float i = 1f;

	//Rotate the object (used for junctions)
	void Update () {
		i++;
		Quaternion newAngle = Quaternion.Euler(30f, i, 45f);
		this.transform.rotation = newAngle;
	}
}
