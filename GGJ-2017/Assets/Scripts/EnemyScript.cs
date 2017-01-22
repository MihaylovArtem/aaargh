using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private int hitPoints;
	public int maxHitPoints;
	public float enemySingleMultiplier;
	public GameObject hpBar;
	private Rigidbody rigidbodyComponent;
	private System.Random generator;
	private int monsterType;
	private float startingXScale = 1f;
	private Vector3 forwardVector;
	float rotateTime = 1.0f;
	float timer = 0.5f;
	float direction = 1;
	float rotationSpeed = 90f;

    public float stepVolume = 0.2f;
	private bool isBoss;
    public AudioSource fxAudio;

    // Use this for initialization
    void Start () {
		hitPoints = maxHitPoints;
		//Correct sprite
		isBoss = false;
		Vector3 force = -(gameObject.transform.position - new Vector3 (0, 0, 0));
		force.Normalize ();
		rigidbodyComponent = gameObject.GetComponent<Rigidbody> ();
		rigidbodyComponent.AddForce (enemySingleMultiplier * force*EnemyManager.currentEnemySpeed);
		startingXScale = hpBar.transform.localScale.x;
		forwardVector = transform.forward;

        fxAudio.pitch = Random.Range(0.8f, 1.2f);
		float randomDelay = Random.Range (0, 2f);
		float randomVolume = Random.Range (0.2f, 0.3f);
		Invoke ("StartAudio", randomDelay );
    }

	void StartAudio() {
		fxAudio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gameState == GameManager.GameState.Playing) {
			timer += Time.deltaTime;
			if (timer > rotateTime) {
				timer = 0;
				direction *= -1;
			}
			transform.Rotate (new Vector3 (0, direction * rotationSpeed * Time.deltaTime, 0));
		}
		hpBar.transform.position = new Vector3 (gameObject.transform.position.x, hpBar.transform.position.y, gameObject.transform.position.z);
		var barScale = 0f;
		if (hitPoints > 0) {
			
			barScale = (float)hitPoints / (float)maxHitPoints * startingXScale;
		} 
		hpBar.transform.localScale = new Vector3 (barScale, 0.1f, 0.1f);

		if (isBoss) {
			//Boss mechanics
		}
	}

	void MakeBoss() {
		isBoss = true;
		//Boss sprite;
		maxHitPoints = 1000;
		hitPoints = 1000;
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Bullet") {
			hitPoints -= GameManager.damageByBullet;
			rigidbodyComponent.AddForce (forwardVector * enemySingleMultiplier * EnemyManager.currentEnemySpeed * -3);
			Invoke ("ReturnPreviousForce", 0.3f);
			if (hitPoints <= 0) {
				hitPoints = 0;
				rigidbodyComponent.AddForce (forwardVector * enemySingleMultiplier * EnemyManager.currentEnemySpeed * -4);
				Invoke ("DestroySelf", 5.0f);
			} 
		}
	}

	void ReturnPreviousForce() {
		rigidbodyComponent.AddForce (forwardVector * enemySingleMultiplier * EnemyManager.currentEnemySpeed*3);
	}

	void DestroySelf() {
        fxAudio.Stop();
        Destroy (gameObject);
	}

	public void RunAway() {
		rigidbodyComponent.AddForce (new Vector3(1,0,0) * enemySingleMultiplier * EnemyManager.currentEnemySpeed*-6);
		transform.LookAt ((new Vector3(1,0,0) ) * enemySingleMultiplier * EnemyManager.currentEnemySpeed*-6);
		Invoke ("DestroySelf", 5.0f);
	}
}
