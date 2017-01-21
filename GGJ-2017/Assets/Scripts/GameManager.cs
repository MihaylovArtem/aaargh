using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public enum GameState {
		Playing,
		MainMenu,
		GameOver 
	}

	public static int level;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<RigidBod2D> ().AddForceAtPosition (Vector3.Forward, new Vector3 (0, 0, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}