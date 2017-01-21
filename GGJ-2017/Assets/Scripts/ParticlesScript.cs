using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesScript : MonoBehaviour {

	public float lifetime = 2.0f;

	// Use this for initialization
	void Start () {
		var plane = GameObject.FindGameObjectsWithTag ("Plane")[0] as GameObject;
		this.GetComponent<ParticleSystem>().collision.SetPlane (0, plane.transform);
		Invoke ("DestroySelf", lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void DestroySelf() {
		Destroy (this.gameObject);
	}
}
