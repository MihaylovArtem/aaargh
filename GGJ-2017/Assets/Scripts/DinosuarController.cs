using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosuarController : MonoBehaviour {

	public GameObject wavePrefab;
	public float waveSpeed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			var cloneWave = Instantiate (wavePrefab) as GameObject;
			cloneWave.transform.position = this.transform.position;
			cloneWave.GetComponent <Rigidbody> ().velocity = this.transform.forward * waveSpeed;
		}
	}
}
