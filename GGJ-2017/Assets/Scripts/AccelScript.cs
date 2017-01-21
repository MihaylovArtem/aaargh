using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AccelScript : MonoBehaviour {

	public Text accelText;
	public Text accelText2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		accelText.text = "x:" + Input.acceleration.x.ToString () + " y:" + Input.acceleration.y.ToString () + " z:" + Input.acceleration.z.ToString ();
		accelText2.text = Input.acceleration.magnitude.ToString ();
	}
}
