using UnityEngine;
using System.Collections;

public class scr_boomerang : MonoBehaviour {

	Rigidbody rb;

	// Player who instantiated this projectile.
	public GameObject player;

	// Throw velocity indicated by player script.
	public float throwSpeed;

	// Currently moving at this speed
	float currentThrowSpeed;

	// How long it takes for the object to slow to a stop before turning.
	float throwLength = .4f;

	// Where the boomerang initially faces
	public Vector3 directionFacing;

	// How fast the return translation should be.
	float returnSpeed;

	AudioSource audio;

	public AudioClip crash;
	public AudioClip release;
	public AudioClip back;


	// Should the boomerang be returning to the player of origin?
	bool returnToPlayer = false;

	IEnumerator throwRoutine(){

		//Begin slowing after 3/5 of the duration
		yield return new WaitForSeconds(throwLength * .6f);
		//Gradual slow to stop.
		currentThrowSpeed /= 2f;

		//Slow to the stop at the remainder 1/5 of the timer on throwLength.
		yield return new WaitForSeconds (throwLength * .2f);
		currentThrowSpeed = 0f;

		//Pause at the end of throw.
		yield return new WaitForSeconds (throwLength * .2f);

		// Wait before returning
		yield return new WaitForSeconds (throwLength * .4f);
		returnToPlayer = true;
		audio.clip = back;
		audio.Play ();
		// Begin speeding up for return.
		currentThrowSpeed = returnSpeed / 4;

		yield return new WaitForSeconds (throwLength * .2f);
		returnToPlayer = true;
		// Full speed return.
		currentThrowSpeed = returnSpeed;
	

		//yield return StartCoroutine (flashRed());
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();

		// What direction the boomerang faces and will move in.
		transform.eulerAngles = directionFacing;

		currentThrowSpeed = throwSpeed;

		returnSpeed = throwSpeed / 300;

		audio = GetComponent <AudioSource> ();

	
		audio.clip = release;
		audio.Play ();

		// Initial throw velocity after being instantiated. Information taking from originating player's script.
		//rb.velocity = player.transform.forward * throwSpeed; 

		StartCoroutine (throwRoutine ());
	}
	
	// Update is called once per frame
	void Update () {

		if(returnToPlayer == false){
			rb.velocity = transform.forward *  -1f * currentThrowSpeed;// The *-1f is a really hacky solution.
		} else if (returnToPlayer == true) {
			transform.position = Vector3.Lerp (transform.position, player.transform.position, returnSpeed);
		}
	}

	void OnTriggerEnter (Collider collision){

		// Destroys players that didn't throw this boomerang.
		if (collision.gameObject != player && collision.tag == "Player") {
			Destroy (collision.gameObject);
			audio.clip = crash;
			audio.Play ();
		}

		// Destroys the boomerang if returned to player.
		if (collision.gameObject == player) {

			Destroy (gameObject);
			player.GetComponent<scr_player> ().boomerangThrown = false;
		}
		/*
		else if (collision.tag == "Boomerang") {
			Debug.Log ("Hit boomerang of opponent");
		} else if ( collision.tag != player.tag && (collision.tag == "PlayerOne" || collision.tag == "PlayerTwo") ) {
			player.GetComponent<scr_player> ().charge = 0;
			if (collision.tag == "PlayerOne") {
				Instantiate (p2WinText, Vector3.zero , Quaternion.identity);
			}
			else if (collision.tag == "PlayerTwo") {
				Instantiate (p1WinText, Vector3.zero , Quaternion.identity);
			}
			if (collision.gameObject.GetComponent<scr_player> ().dodging == false) {
				Destroy (collision.gameObject);
			}
		}*/
	}
}
