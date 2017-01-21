using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 force = (gameObject.transform.position - new Vector3 (0, 0, 0));
		force.Normalize ();
		gameObject.GetComponent<Rigidbody> ().AddForce (-100*force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
