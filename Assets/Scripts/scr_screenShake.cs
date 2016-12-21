using UnityEngine;
using System.Collections;

public class scr_screenShake : MonoBehaviour {
	Vector3 startPosition;
	public float timer;

	//public GameObject boomerang1;
	//public GameObject boomerang2;


	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.Find ("Player1") != null) {
			//do nothing
		} else {
			Debug.Log ("P1 dead");

			timer = timer + Time.deltaTime;
				Invoke ("screenShake", 0f);
			}

		if (GameObject.Find ("Player2") != null) {
			//do nothing
		} else {
			Debug.Log ("P2 dead");

			timer = timer + Time.deltaTime;
				Invoke ("screenShake", 0f);
			}
		

	
		//when the boomerang hits something, it turns "startshake" to true. This checks the boomerang script to see if this has been triggered.
		/*if (boomerang1.gameObject.GetComponent<scr_boomerang>().startShake == true) {

			Debug.Log ("should be shaking");

			Vector3 shakeSideDirection = transform.right * Mathf.Sin (Time.time * 27f) * 0.1f;
			Vector3 shakeUpDirection = transform.up * Mathf.Sin (Time.time * 23f) * 0.1f;
			transform.position += shakeSideDirection + shakeUpDirection;

		} //else {//if (boomerang1.gameObject.GetComponent<scr_boomerang>().startShake == false || 
						//boomerang2.gameObject.GetComponent<scr_boomerang>().startShake == false){

			//Debug.Log ("Not shaking");
			//transform.position = startPosition;
		//}*/

	}

	void screenShake(){
		if (timer < 0.8f) {
			Vector3 shakeSideDirection = transform.right * Mathf.Sin (Time.time * 22f) * 0.4f;
			Vector3 shakeUpDirection = transform.up * Mathf.Sin (Time.time * 22f) * 0.4f;
			transform.position += shakeSideDirection + shakeUpDirection;
		} else {
			transform.position = startPosition;
		}
	}
}
