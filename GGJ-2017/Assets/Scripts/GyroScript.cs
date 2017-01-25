using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScript : MonoBehaviour {
	Vector3 angle = Vector3.zero;
	//TextMesh text; <-- I was using a text mesh to test displaying

	void Start() {
		//text = GetComponent<TextMesh>();
	}

	void Update() {
		angle.x = Input.acceleration.x * 360;
		angle.y = Input.acceleration.y * 360;
		angle.z = Input.acceleration.z * 360;
		transform.rotation = Quaternion.Euler(angle);
		//text.text = Input.acceleration.x.ToString();
	}
}