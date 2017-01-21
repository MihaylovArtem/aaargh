using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosuarController : MonoBehaviour {

	public GameObject wavePrefab;
	public float waveSpeed = 10f;
	public float minVolume = 0.3f;
	public float maxVolume = 1.0f;
	private float maxShootDelay = 4.0f;
	private float maxShootDelayDivider = 8.0f;
	private float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		
//		var currentDelay = maxShootDelay/(maxShootDelayDivider * )

//		if timer

		if (Input.GetKeyDown(KeyCode.Space)) {
			var cloneWave = Instantiate (wavePrefab) as GameObject;
			cloneWave.transform.position = this.transform.position;
			cloneWave.GetComponent <Rigidbody> ().velocity = this.transform.forward * waveSpeed;
		}
	}
}
