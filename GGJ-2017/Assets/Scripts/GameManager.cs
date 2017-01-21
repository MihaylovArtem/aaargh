using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public enum GameState {
		Playing,
		MainMenu,
		GameOver,
		Breathing,
		Win
	}
	public static int level = 1;
	public static GameState gameState;
	public static float breathingTime;
	public static float damageByBullet = 10.0f;
	public static int highscore = 0;
	public float progressBarTime;
	public GameObject breathingObject;
	public GameObject progressBar;
	private float progressBarTimer = 0.0f;
	private float newGameLoudnessTimer = 0.0f;
	private int startEnemiesCount = 5;

	public UIScript uiScript;

	EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		gameState = GameState.MainMenu;
		breathingTime = 5.0f;
		CheckEnemies ();
		enemyManager = gameObject.GetComponent<EnemyManager> ();
		highscore = PlayerPrefs.GetInt ("highscore");
		//Вместо этого в апдейте будем проверять, что если крик 2 сек, то начинаем играть
		//Invoke ("StartNewLevel", 2.0f);
	}

	void CheckEnemies() {
		if (gameState == GameState.Playing) {
			var enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (enemies.Length == 0 && EnemyManager.allWavesCompleted) {
				Breathe ();
			}
		}
		Invoke("CheckEnemies", 1.0f);
	}

	void Breathe() {
		Debug.Log ("Breathing");
		progressBarTimer = 0.0f;
		gameState = GameState.Breathing;
		progressBarTime = breathingTime;
		progressBar.SetActive (true);
		breathingObject.SetActive (true);
		GameManager.level++;
		Invoke ("StartNewLevel", breathingTime);
	}

	void StartNewLevel() {
		progressBar.transform.localScale = new Vector3 (1, 1, 1);
		progressBar.SetActive (false);
		breathingObject.SetActive (false);
		gameState = GameState.Playing;
		uiScript.ShowWaveText ();
		if (level == 1 || level == 2 || level == 4) {
			StartCoroutine (enemyManager.SpawnLevel (startEnemiesCount + level, level, level + 1, 0, 6f + level));
		} else if (level == 3) {
			StartCoroutine (enemyManager.SpawnLevel (1, level, 20, 0, 0.5f));
		} else if (level == 5) {
			StartCoroutine (enemyManager.SpawnLevel (3, level, 100, 0, 3f));
		}
	}

	void GameOver() {
		highscore = GameManager.level;
		GameManager.level = 1;
		PlayerPrefs.SetInt ("highscore", highscore);
		PlayerPrefs.Save ();
	}

	// Update is called once per frame
	void Update () {
		progressBarTimer += Time.deltaTime;
		switch (gameState) {
		case GameState.MainMenu: {
				Debug.Log (AudioInput.MicLoudness);
				if (AudioInput.MicLoudness < 0.3) {
					newGameLoudnessTimer += Time.deltaTime;
				} else {
					if (newGameLoudnessTimer > 2.0f) {
						newGameLoudnessTimer = 0.0f;
						StartNewLevel (); 
					}
				}
				break;
			}
		case GameState.GameOver: {
				break;
			}
		case GameState.Breathing: {
				progressBar.transform.localScale = Vector3.Lerp (new Vector3 (1, 1, 1), new Vector3 (0, 1, 1), progressBarTimer / breathingTime);
				break;
			}
		case GameState.Playing: {
				break;
			}
		}
	}
}