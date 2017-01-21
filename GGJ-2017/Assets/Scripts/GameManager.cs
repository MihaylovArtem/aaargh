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
	public static int level;
	public static GameState gameState;
	public static float breathingTime;
	public static float damageByBullet = 10.0f;

	EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		gameState = GameState.MainMenu;
		breathingTime = 5.0f;
		CheckEnemies ();
		enemyManager = gameObject.GetComponent<EnemyManager> ();

		Invoke ("StartNewLevel", 2.0f);
	}

	void CheckEnemies() {
		if (gameState == GameState.Playing) {
			var enemies = GameObject.FindGameObjectsWithTag("Enemy");
			//Debug.Log (enemies.Length);
			if (enemies.Length == 0) {
				Breathe ();
			}
		}
		Invoke("CheckEnemies", 1.0f);
	}

	void Breathe() {
		Debug.Log ("Breathing");
		gameState = GameState.Breathing;
		//Здесь добавим какой-нибудь progress bar
		Invoke ("StartNewLevel", breathingTime);
	}

	void StartNewLevel() {
		enemyManager.SpawnLevel (5);
		gameState = GameState.Playing;
	}

	// Update is called once per frame
	void Update () {
		switch (gameState) {
		case GameState.MainMenu: {
				break;
			}
		case GameState.GameOver: {
				break;
			}
		case GameState.Breathing: {
				break;
			}
		case GameState.Playing: {
				break;
			}
		}
	}
}