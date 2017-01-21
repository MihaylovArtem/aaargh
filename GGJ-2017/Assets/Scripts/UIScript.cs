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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GameManager.gameState == GameManager.GameState.MainMenu) {
			shoutToPlay.SetActive (true);
			waveTextObject.SetActive (false);
			arghTextObject.SetActive (true);
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
			waveText.text = "Wave " + GameManager.level;

			var alpha = waveText.color.a - (Time.deltaTime / 2000f);
			if (alpha < 0) {
				alpha = 0;
			}
			Debug.Log (alpha.ToString ());
			waveText.color -= new Color(waveText.color.r, waveText.color.g, waveText.color.b, alpha);
		}
	}

	public void ShowWaveText() {
		waveText.color = new Color(waveText.color.r, waveText.color.g, waveText.color.b, 1);
	}
}
