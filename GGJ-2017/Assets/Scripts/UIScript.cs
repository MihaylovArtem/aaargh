using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public GameObject shoutToPlay;
	public Text waveText;
	public GameObject waveTextObject;
	public GameObject arghTextObject;
	public Text arghText;
	public Text gameOverText;
	public GameObject gameOverTextObject;

	public float gameOverTimer = 10.0f;

	// Use this for initialization
	void Start () {
		
	}

	void Update() {
		gameOverTimer -= Time.deltaTime;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GameManager.gameState == GameManager.GameState.MainMenu) {
			shoutToPlay.SetActive (true);
			waveTextObject.SetActive (false);
			arghTextObject.SetActive (true);
			gameOverTextObject.SetActive (false);
			string hs = "A";
			for (int i=1; i < GameManager.highscore; i++) {
				hs += "a";
			}
			hs += "rgh!";
			arghText.text = hs;
		}

		if (GameManager.gameState == GameManager.GameState.Playing) {
			shoutToPlay.SetActive (false);
			arghTextObject.SetActive (false);
			waveTextObject.SetActive (true);
			gameOverTextObject.SetActive (false);
			waveText.text = "Wave " + GameManager.level;

			var alpha = waveText.color.a - (Time.deltaTime / 2000f);
			if (alpha < 0) {
				alpha = 0;
			}
			//Debug.Log (alpha.ToString ());
			waveText.color -= new Color(waveText.color.r, waveText.color.g, waveText.color.b, alpha);
		}

		if (GameManager.gameState == GameManager.GameState.GameOver) {
			shoutToPlay.SetActive (false);
			arghTextObject.SetActive (false);
			waveTextObject.SetActive (false);
			gameOverTextObject.SetActive (true);
			gameOverText.text = "You lost your cake :(\nShout to start again!\nYou will go to main menu in " +
				Mathf.Round (gameOverTimer).ToString ();
			if (gameOverTimer <= 0.0f) {
				gameOverTimer = 10.0f;
				GameManager.gameState = GameManager.GameState.MainMenu;
			}
		}
	}

	public void ShowWaveText() {
		waveText.color = new Color(waveText.color.r, waveText.color.g, waveText.color.b, 1);
	}
}
