using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosuarController : MonoBehaviour {

	public GameObject wavePrefab;
	[HideInInspector]
	float waveSpeed = 20f;
	[HideInInspector]
	float minVolume = 0.05f;
	[HideInInspector]
	float maxVolume = 0.6f;
	[HideInInspector]
	float maxShootDelay = 1.0f;
	[HideInInspector]
	float maxShootDelayDivider = 5.0f;
	private float timer = 0.0f;

	public GameObject hands;
	public GameObject body;

	bool isStarted;
	public Vector3 startingPoint;
	public Vector3 gamePoint;

	public GameObject camera;

	public GameManager gameManager;


	// Use this for initialization
	void Start () {
		gamePoint = new Vector3( camera.transform.position.x, 0, camera.transform.position.z);
		startingPoint = transform.position;
		SetStarted (false);
	}

	public void SetStarted(bool started) {
		isStarted = started;
		Invoke ("HideBodyparts", 1);
	}

	void HideBodyparts() {
		hands.transform.localScale = !isStarted ? Vector3.zero : Vector3.one; 
		body.transform.localScale = isStarted ? Vector3.zero : Vector3.one; 
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		var currentLoudness = AudioInput.MicLoudness;
		var modifiedLoudness = currentLoudness / maxVolume;
		var currentDelay = maxShootDelay / (maxShootDelayDivider * modifiedLoudness);
		var message = "Loudness = " + currentLoudness.ToString ("00.00") + ", modifiedLoudness = " + modifiedLoudness.ToString ("00.00") + 
			", currentDelay = " + currentDelay.ToString ("00.00") + 
			", timer = " + timer.ToString ("00.00");
		//Debug.Log (message);
		if (modifiedLoudness > minVolume) {
			if (timer > currentDelay) {
				timer = 0;
				ShootWave ();
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			ShootWave ();
		}
		if (isStarted) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, camera.transform.rotation.eulerAngles.y, 0));
			if (body.transform.rotation.eulerAngles.y < 180) {
				body.transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.FromToRotation (transform.position, startingPoint), Time.deltaTime * 5);
			} else {
				body.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));
			}
		} else {
			if (body.transform.rotation.eulerAngles.y > 0) {
				body.transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.FromToRotation (transform.position, gamePoint), Time.deltaTime * 5);
			} else {
				body.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			}
		}
		var distance = Vector3.Distance (transform.position, gamePoint);
		if (isStarted) {
			if (distance > 0.5f) {
				transform.position = Vector3.MoveTowards (transform.position, gamePoint, Time.deltaTime * 5);
			}
		} else {
			transform.position = Vector3.MoveTowards (transform.position, startingPoint, Time.deltaTime * 5);
		}
	}

	void ShootWave() {
		if (GameManager.gameState == GameManager.GameState.Playing) {
			var cloneWave = Instantiate (wavePrefab) as GameObject;
			cloneWave.transform.position = new Vector3 (camera.transform.position.x, camera.transform.position.y - 0.8f, camera.transform.position.z);
			cloneWave.transform.rotation = Quaternion.Euler (new Vector3 (transform.rotation.eulerAngles.x - 100, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 90));
			cloneWave.GetComponent <Rigidbody> ().velocity = transform.forward * waveSpeed;
		}
		//Debug.Log (waveSpeed.ToString ());
	}

	void AllRunAway() {
		var currentEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in currentEnemies) {
			enemy.GetComponent<EnemyScript> ().RunAway ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy" && GameManager.gameState == GameManager.GameState.Playing) {
			AllRunAway ();
			gameManager.GameOver ();
		}
	}
}
