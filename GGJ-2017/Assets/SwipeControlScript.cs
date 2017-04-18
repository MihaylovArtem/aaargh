using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControlScript : MonoBehaviour {

	private Quaternion localRotation; // 
	private float xSpeed = 250.0f; // ajustable speed from Inspector in Unity editor
	private float zSpeed = 50.0f; // ajustable speed from Inspector in Unity editor

	// Use this for initialization
	void Start () 
	{
		// copy the rotation of the object itself into a buffer
		localRotation = transform.rotation;
	}


	void Update() // runs 60 fps or so
	{
		// find speed based on delta
		float curXSpeed = Time.deltaTime * xSpeed;
		float curZSpeed = -Time.deltaTime * zSpeed;
		if (Mathf.Abs (Input.acceleration.z) > 0.1f) {
			var accel = (Input.acceleration.z - Mathf.Sign (Input.acceleration.z)*0.1f) / 4f * 10f * curZSpeed;
			if (Mathf.Abs (Input.acceleration.z) > 0.5f) {
				accel = curZSpeed * Mathf.Sign (Input.acceleration.z);
			}
			localRotation.eulerAngles += new Vector3 (accel, 0, 0);
			if ((localRotation.eulerAngles.x > 75.0f && localRotation.eulerAngles.x < 90.0f)) {
				localRotation.eulerAngles = new Vector3 (75.0f, localRotation.eulerAngles.y, 0);
			} else if (localRotation.eulerAngles.x < 285.0f && localRotation.eulerAngles.x > 260.0f) {
				localRotation.eulerAngles = new Vector3 (285.0f, localRotation.eulerAngles.y, 0);

			}
		}
		if (Mathf.Abs (Input.acceleration.x) > 0.1f){
			var accel = (Input.acceleration.x - Mathf.Sign (Input.acceleration.x)*0.1f) / 4f * 10f * curXSpeed;
			if (Mathf.Abs (Input.acceleration.x) > 0.5f) {
				accel = curXSpeed * Mathf.Sign (Input.acceleration.x);
			}
			localRotation.eulerAngles += new Vector3(0, accel, 0);
		}


		// then rotate this object accordingly to the new angle
		transform.rotation = localRotation;

	}
}