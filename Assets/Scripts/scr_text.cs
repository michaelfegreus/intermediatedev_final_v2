using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_text : MonoBehaviour {

	public GameObject boomerang1;
	public GameObject boomerang2;

	public Text victoryText;
	public Text restartText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (scr_boomerang.gameOver == true) {
			Debug.Log ("end");
			victoryText.text = "Victory!";
			restartText.text = "Press G to play again.";
		}

		if (Input.GetKeyDown (KeyCode.G)) {
			SceneManager.LoadScene (1);
		}

	}
}
