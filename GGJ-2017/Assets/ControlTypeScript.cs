using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTypeScript : MonoBehaviour {

	public GameObject noGyroPanel;
	public GameObject vrButton;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<GyroScript> ().enabled = SystemInfo.supportsGyroscope;

		gameObject.GetComponent<SwipeControlScript> ().enabled = !SystemInfo.supportsGyroscope;

		noGyroPanel.SetActive (!SystemInfo.supportsGyroscope);
		vrButton.SetActive (SystemInfo.supportsGyroscope);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
