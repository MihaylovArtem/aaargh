﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	float hitPoints = 100.0f;


	// Use this for initialization
	void Start () {
		Vector3 force = (gameObject.transform.position - new Vector3 (0, 0, 0));
		force.Normalize ();
		gameObject.GetComponent<Rigidbody> ().AddForce (-50*force);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Bullet") {
			hitPoints -= GameManager.damageByBullet;
			Debug.Log (hitPoints);
			if (hitPoints <= 0.0f) {
				//Runaway, runaway baby!
			}
		}
	}
}
