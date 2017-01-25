using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControlScript : MonoBehaviour {

	Vector3 initTouch;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1)
		{
			// GET TOUCH 0
			Touch touch0 = Input.GetTouch(0);

			// APPLY ROTATION
			if (touch0.phase == TouchPhase.Moved)
			{
				transform.Rotate(touch0.deltaPosition.y/5f, touch0.deltaPosition.x/-5f, 0f);
				transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
			}

		}
	}
}
