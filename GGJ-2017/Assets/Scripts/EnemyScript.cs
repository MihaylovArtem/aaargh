using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	float hitPoints;
	const float maxHitPoints = 100.0f;
	public GameObject hpBar;
	private Rigidbody rigidbodyComponent;

	// Use this for initialization
	void Start () {
		Vector3 force = -(gameObject.transform.position - new Vector3 (0, 0, 0));
		force.Normalize ();
		hitPoints = maxHitPoints;
		rigidbodyComponent = gameObject.GetComponent<Rigidbody> ();
		rigidbodyComponent.AddForce (force*EnemyManager.currentEnemySpeed);
	}
	
	// Update is called once per frame
	void Update () {
		hpBar.transform.position = new Vector3 (gameObject.transform.position.x, hpBar.transform.position.y, gameObject.transform.position.z);
		hpBar.transform.localScale = new Vector3 (hitPoints / maxHitPoints, 0.1f, 0.1f);
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Bullet") {
			hitPoints -= GameManager.damageByBullet;
			Debug.Log (hitPoints);
			if (hitPoints <= 0.0f) {
				//Runaway, runaway baby!
				DestroySelf ();
			} else {
				rigidbodyComponent.AddForce (gameObject.transform.forward * EnemyManager.currentEnemySpeed * -3);
				Invoke ("ReturnPreviousForce", 0.3f);
			}
		}
	}

	void ReturnPreviousForce() {
		rigidbodyComponent.AddForce (gameObject.transform.forward * EnemyManager.currentEnemySpeed*3);
	}

	void DestroySelf() {
		Destroy (gameObject);
	}
}
