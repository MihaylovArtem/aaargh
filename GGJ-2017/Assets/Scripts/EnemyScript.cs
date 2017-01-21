using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public float hitPoints;
	public float maxHitPoints;
	public float enemySingleMultiplier;
	public GameObject hpBar;
	private Rigidbody rigidbodyComponent;
	private System.Random generator;
	private int monsterType;

	private bool isBoss;

	// Use this for initialization
	void Start () {
		hitPoints = maxHitPoints;
		//Correct sprite
		isBoss = false;
		Vector3 force = -(gameObject.transform.position - new Vector3 (0, 0, 0));
		force.Normalize ();
		rigidbodyComponent = gameObject.GetComponent<Rigidbody> ();
		rigidbodyComponent.AddForce (enemySingleMultiplier * force*EnemyManager.currentEnemySpeed);
	}
	
	// Update is called once per frame
	void Update () {
		hpBar.transform.position = new Vector3 (gameObject.transform.position.x, hpBar.transform.position.y, gameObject.transform.position.z);
		hpBar.transform.localScale = new Vector3 (hitPoints / maxHitPoints, 0.1f, 0.1f);
		if (isBoss) {
			//Boss mechanics
		}
	}

	void MakeBoss() {
		isBoss = true;
		//Boss sprite;
		maxHitPoints = 1000.0f;
		hitPoints = 1000.0f;
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Bullet") {
			hitPoints -= GameManager.damageByBullet;
			if (hitPoints <= 0.0f) {
				rigidbodyComponent.AddForce (gameObject.transform.forward * enemySingleMultiplier * EnemyManager.currentEnemySpeed * -4);
				Invoke ("DestroySelf", 5.0f);
			} else {
				rigidbodyComponent.AddForce (gameObject.transform.forward * enemySingleMultiplier * EnemyManager.currentEnemySpeed * -3);
				Invoke ("ReturnPreviousForce", 0.3f);
			}
		}
	}

	void ReturnPreviousForce() {
		rigidbodyComponent.AddForce (gameObject.transform.forward * enemySingleMultiplier * EnemyManager.currentEnemySpeed*3);
	}

	void DestroySelf() {
		Destroy (gameObject);
	}

	public void RunAway() {
		rigidbodyComponent.AddForce (gameObject.transform.forward * enemySingleMultiplier * EnemyManager.currentEnemySpeed*-6);
		Invoke ("DestroySelf", 5.0f);
	}
}
