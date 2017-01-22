using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawrAnimationScript : MonoBehaviour {

	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	SpriteRenderer renderer;


	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent <SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (AudioInput.MicLoudness <= 0.2) {
			renderer.sprite = sprite1;
		} else if (AudioInput.MicLoudness <= 0.4) {
			renderer.sprite = sprite2;
		} else if (AudioInput.MicLoudness <= 0.6) {
			renderer.sprite = sprite3;
		} else {
			renderer.sprite = sprite4;
		}
	}
}
