using UnityEngine;
using System.Collections;

public class scr_player : MonoBehaviour {

	// Which player is this?
	public int playerNumber;

	// Base movement speed.
	float baseMoveSpeed = 10f;
	float moveSpeed;

	Rigidbody rb;

	// Custom key inputs to designate for each player.
	public KeyCode myDodgeButton;
	public KeyCode myFireButton;

	//Keys for character movement to determine the direction the player is facing
	public KeyCode myMoveUp;
	public KeyCode myMoveDown;
	public KeyCode myMoveLeft;
	public KeyCode myMoveRight;

	AudioSource audio;

	public AudioClip retrieve;


	// Checks to see if boomerang is already thrown so the player doesn't have multiple projectiles out.
	public bool boomerangThrown = false;

	// To track where the boomerang is thrown from.
	public GameObject spawner;

	// The player specific boomerang.
	public GameObject boomerangPrefab;

	// Initial boomerang velocity.
	float throwStrength = 30f;

	// Current charge value.
	float charge=1f;

	// Multiplyer for charged boomerang velocity.
	float chargeMultiplier = 2f;

	// How long charging.
	float chargeTimer = 0f;

	void Start () {
		audio = GetComponent <AudioSource> ();
		// Set up initial movement speed.
		moveSpeed = baseMoveSpeed;

		rb = GetComponent<Rigidbody> ();
	}

	void Update(){

		// Key input to charge boomerang.
		if (Input.GetKey (myFireButton) && boomerangThrown == false) {

			// Slows movement during charge.
			moveSpeed = baseMoveSpeed / 2;

			// How long you need to charge before getting the powered up boomerang.
			float chargeThreshold = .75f;

			chargeTimer += Time.deltaTime;
			if (chargeTimer > chargeThreshold) {
				Debug.Log ("Charged");
				charge = chargeMultiplier;
			}
		}

		// Key input to throw boomerang.
		if (Input.GetKeyUp (myFireButton) && boomerangThrown == false) {

			// Movement reset after charge
			moveSpeed = baseMoveSpeed;

			boomerangThrown = true;

			// Resets charge on release of fire key.
			chargeTimer = 0f;

			GameObject boomerang = (GameObject)Instantiate (boomerangPrefab, spawner.transform.position, Quaternion.identity);
			boomerang.GetComponent<scr_boomerang> ().player = gameObject;
			boomerang.GetComponent<scr_boomerang> ().throwSpeed = throwStrength * charge;
			boomerang.GetComponent<scr_boomerang> ().directionFacing = transform.eulerAngles;

			// Resets charge.
			charge = 1f;
		}
	}

	void FixedUpdate () {

		// Movement control designated by specific player player axes.
		float inputX = 0f; // Compiling requires proxy number, hence the 0f.
		float inputY = 0f;

		if (playerNumber == 1) {
			inputX = Input.GetAxis ("HorizontalP1"); // Check in Edit > Project Settings > Input > Axes to change these.
			inputY = Input.GetAxis ("VerticalP1");
		} else if (playerNumber == 2) {
			inputX = Input.GetAxis ("HorizontalP2");
			inputY = Input.GetAxis ("VerticalP2");
		}

		faceDirection (inputX, inputY);

		// Movement keys used here alongside axes for a nice mix of digital and analog movement. Feels decent IMO.
		if (Input.GetKey (myMoveRight) || Input.GetKey (myMoveLeft) || Input.GetKey (myMoveUp) || Input.GetKey (myMoveDown)) {
			faceDirection (inputX, inputY);
			rb.velocity = transform.forward * -1f * moveSpeed; // Admittedly the * -1f is a hack.
		

		} else {
			rb.velocity = Vector3.zero;

		}




	}
		
	// Function turns the player to face the correct direction. Messy function, could be streamlined.
	float faceDirection(float x, float y){

		// How much the player needs to have turned on the Vertical and Horizontal axes to register the change.
		float turnThreshold = 0.1f;

		if (x > turnThreshold && (Mathf.Abs (y)) < turnThreshold) { // Moved east
			transform.eulerAngles = new Vector3 (0, 90);
		}
		if (x < -1f * turnThreshold && (Mathf.Abs (y)) < turnThreshold) { // Moved west
			transform.eulerAngles = new Vector3 (0, 270);
		}
		if((y > turnThreshold && (Mathf.Abs (x)) < turnThreshold)){ // Moved north
			transform.eulerAngles = new Vector3 (0, 0);
		}
		if((y < -1f * turnThreshold && (Mathf.Abs (x)) < turnThreshold)){ // Moved south
			transform.eulerAngles = new Vector3 (0, 180);
		}
		if (x > turnThreshold && y > turnThreshold) { // Moved northeast
			transform.eulerAngles = new Vector3 (0, 45);
		}
		if (x < -1f * turnThreshold && y > turnThreshold) { // Moved northwest
			transform.eulerAngles = new Vector3 (0, 315);
		}
		if (x < -1f * turnThreshold && y < -1f * turnThreshold) { // Moved southwest
			transform.eulerAngles = new Vector3 (0, 225);
		}
		if (x > turnThreshold && y < -1f * turnThreshold) { // Moved northeast
			transform.eulerAngles = new Vector3 (0, 135);
		}


		return x;
	}
}
