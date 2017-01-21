using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public enum GameState {
		Playing,
		MainMenu,
		GameOver,
		Breathing
	}
	public static int level = 1;
	public static GameState gameState;
	public static float breathingTime;
	public static float damageByBullet = 10.0f;
	public static int highscore = 0;
	public float progressBarTime;
	public GameObject breathingObject;
	public GameObject progressBar;
	private float progressBarTimer = 0;
	private int startEnemiesCount = 3;

	EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		gameState = GameState.MainMenu;
		breathingTime = 5.0f;
		CheckEnemies ();
		enemyManager = gameObject.GetComponent<EnemyManager> ();
		highscore = PlayerPrefs.GetInt ("highscore");
		//Вместо этого в апдейте будем проверять, что если крик 2 сек, то начинаем играть
		Invoke ("StartNewLevel", 2.0f);
	}

	void CheckEnemies() {
		if (gameState == GameState.Playing) {
			var enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (enemies.Length == 0) {
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
		enemyManager.SpawnLevel (startEnemiesCount + GameManager.level / 2);
		gameState = GameState.Playing;
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