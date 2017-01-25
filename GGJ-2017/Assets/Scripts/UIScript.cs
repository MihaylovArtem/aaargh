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
	public GameObject winTextObject;
	public GameObject aboutTextObject;
	public GameManager gameManager;
	public Button vrButton;
	public Sprite vrOnImage;
	public Sprite vrOffImage;
	public GameObject vrOnCamera;
	public GameObject vrOffCamera;

	public float gameOverTimer = 10.0f;
	public static bool vrEnabled = false;

	// Use this for initialization
	void Start () {
		
	}

	void Update() {
		if (GameManager.gameState == GameManager.GameState.GameOver) {
			gameOverTimer -= Time.deltaTime;
		} else {
			gameOverTimer = 10.0f;
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GameManager.gameState == GameManager.GameState.MainMenu) {
			shoutToPlay.SetActive (true);
			waveTextObject.SetActive (false);
			winTextObject.SetActive (false);
			arghTextObject.SetActive (true);
			gameOverTextObject.SetActive (false);
			aboutTextObject.SetActive (true);
			string hs = "A";
			for (int i=1; i < GameManager.highscore; i++) {
				hs += "a";
			}
			hs += "rgh!";
			arghText.text = hs;
		}

		if (GameManager.gameState == GameManager.GameState.Playing) {
			shoutToPlay.SetActive (false);
			winTextObject.SetActive (false);
			arghTextObject.SetActive (false);
			waveTextObject.SetActive (true);
			gameOverTextObject.SetActive (false);
			aboutTextObject.SetActive (false);
			waveText.text = "Wave " + GameManager.level;
		}

		if (GameManager.gameState == GameManager.GameState.GameOver) {
			shoutToPlay.SetActive (false);
			winTextObject.SetActive (false);
			arghTextObject.SetActive (false);
			waveTextObject.SetActive (false);
			gameOverTextObject.SetActive (true);
			aboutTextObject.SetActive (true);
			gameOverText.text = "You lost your cake :(\nShout to start again!\n\nYou will go to main menu in " +
				Mathf.Round (gameOverTimer).ToString ();
			if (gameOverTimer <= 0.0f) {
				gameOverTimer = 10.0f;
				gameManager.GoToMainMenu ();
			}
		}

		if (GameManager.gameState == GameManager.GameState.Win) {
			shoutToPlay.SetActive (false);
			winTextObject.SetActive (true);
			arghTextObject.SetActive (false);
			waveTextObject.SetActive (false);
			gameOverTextObject.SetActive (false);
			aboutTextObject.SetActive (true);
		}
		vrButton.image.sprite = vrEnabled ? vrOnImage : vrOffImage;
	}

	public void vrButtonTapped() {
		vrEnabled = !vrEnabled;

		vrOnCamera.SetActive (vrEnabled);
		vrOffCamera.SetActive (!vrEnabled);
	}
}
