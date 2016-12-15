using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChargingBar : MonoBehaviour {
	Image barimage;
	public Sprite bar;

	// Use this for initialization
	void Start () {
		barimage.sprite = bar;
		barimage.type = Image.Type.Filled;
		barimage.fillAmount = .5f;
	



	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
