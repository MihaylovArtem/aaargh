using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	private float hitPoints;
	public float maxHitPoints;
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
    private Random audioPitchGenerator = new Random();
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
        fxAudio.volume = stepVolume;
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
		hpBar.transform.localScale = new Vector3 (hitPoints / maxHitPoints * startingXScale, 0.1f, 0.1f);
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
			if (hitPoints <= 0.0f) {
				hitPoints = 0;
				rigidbodyComponent.AddForce (forwardVector * enemySingleMultiplier * EnemyManager.currentEnemySpeed * -4);
				Invoke ("DestroySelf", 5.0f);
			} else {
				hitPoints -= GameManager.damageByBullet;
				rigidbodyComponent.AddForce (forwardVector * enemySingleMultiplier * EnemyManager.currentEnemySpeed * -3);
				Invoke ("ReturnPreviousForce", 0.3f);
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
