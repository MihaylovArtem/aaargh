using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {

	public GameObject particles;
	public float lifetime = 2.0f;

	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf", lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DestroySelf() {
		Destroy (this.gameObject);
	}

	void OnCollisionEnter(Collision collision) {
		var particlesClone = Instantiate (particles) as GameObject;
		particlesClone.transform.position = this.transform.position;
		DestroySelf ();
	}
}
