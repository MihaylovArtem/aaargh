using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {

	public GameObject particles;
	public float lifetime = 1.0f;

	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf", lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3(Time.deltaTime*2, Time.deltaTime*5, 0);
	}

	void DestroySelf() {
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Plane" || other.tag == "Enemy") {
			var particlesClone = Instantiate (particles) as GameObject;
			particlesClone.transform.position = this.transform.position;
			DestroySelf ();
		}
	}
}
