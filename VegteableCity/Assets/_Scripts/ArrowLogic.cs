using UnityEngine;
using System.Collections;

public class ArrowLogic : MonoBehaviour {

	//The street in which direction the arrow is looking
	public GameObject arrowTarget;

	//If the arrow is currently the selected one
	public bool isSelected;

	//Sprite for selected Arrow
	public Sprite selectedArrowSprite;

	//Sprite for unselected Arrow
	public Sprite unselectedArrowSprite;

	/*
	 * Sets a new arrow to unselected by default
	 * Changes sorting order so there is no problem with rendering
	 */

	void Start () {
		isSelected = false;
		transform.GetComponent<SpriteRenderer> ().sortingOrder = 1;
	}

	/*
	 * Happens if arrow gets clicked with mouse/touch
	 * At First iterates through all other child arrows and deselects them, also changes their color back to unselectedArrowSprite
	 * Then changes to the sprite to selectedArrowSprite and changes isSelected to true
	 */

	void OnMouseDown(){
		for (int i = 0; i < transform.parent.childCount; i++) {
			if (transform.parent.GetChild(i).CompareTag ("Arrow")) {
				transform.parent.GetChild (i).GetComponent<SpriteRenderer> ().sprite = unselectedArrowSprite;
				transform.parent.GetChild (i).GetComponent<ArrowLogic> ().isSelected = false;
			}
		}
		GetComponent<SpriteRenderer> ().sprite = selectedArrowSprite;
		transform.parent.GetComponent<PedestrianMovement> ().arrowSelected = true;
		this.isSelected = true;
	}

	/*
	 * Can be triggered from every other script (used in PedestrianMovement with the gyro-controlls)
	 * At First iterates through all other child arrows and deselects them, also changes their color back to unselectedArrowSprite
	 * Then changes to the sprite to selectedArrowSprite and changes isSelected to true
	 */

	public void getsSelected() {
		for (int i = 0; i < transform.parent.childCount; i++) {
			if (transform.parent.GetChild(i).CompareTag ("Arrow")) {
				transform.parent.GetChild (i).GetComponent<SpriteRenderer> ().sprite = unselectedArrowSprite;
				transform.parent.GetChild (i).GetComponent<ArrowLogic> ().isSelected = false;
			}
		}
		GetComponent<SpriteRenderer> ().sprite = selectedArrowSprite;
		transform.parent.GetComponent<PedestrianMovement> ().arrowSelected = true;
		this.isSelected = true;
	}
}
