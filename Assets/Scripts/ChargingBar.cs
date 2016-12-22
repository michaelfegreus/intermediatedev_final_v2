using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChargingBar : MonoBehaviour {
	public Image barimage;
	public Sprite bar;

	// Use this for initialization
	void Start () {
//		barimage.sprite = bar;
//		barimage.type = Image.Type.Filled;
		barimage.fillAmount = .5f;
	



	
	}

	void Update () {
	
	}
}
