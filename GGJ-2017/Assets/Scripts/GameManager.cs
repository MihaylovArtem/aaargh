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
	private int startEnemiesCount = 3;
	public GameObject newGameProgressBar;
	public UIScript uiScript;

    
    public SoundManager soundManager;
    EnemyManager enemyManager;

	// Use this for initialization
	void Start () {
		gameState = GameState.MainMenu;
		breathingTime = 5.0f;
		CheckEnemies ();
		enemyManager = gameObject.GetComponent<EnemyManager> ();
		highscore = PlayerPrefs.GetInt ("highscore");
        if (!soundManager.IsPlaying) soundManager.SetMenuMusic();
    }

	void CheckEnemies() {
		if (gameState == GameState.Playing) {
			var enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (enemies.Length == 0 && EnemyManager.allWavesCompleted) {
				if (level < 5) {
					Breathe ();
				} else {
					Win ();
				}
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
        soundManager.FadeInBattle();
	}

	void StartNewLevel() {
        if (level == 1)
        {
            soundManager.SetBattleMusic();
        }
        else
        {
            soundManager.FadeOutBattle();
        }
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
			StartCoroutine (enemyManager.SpawnLevel (3, level, 100, 0, 10f));
		}
	}

	static public void GameOver() {
		highscore = GameManager.level;
		GameManager.level = 1;
		PlayerPrefs.SetInt ("highscore", highscore);
		PlayerPrefs.Save ();
	}

	public void Win() {
		gameState = GameState.Win;
		Invoke ("GoToMainMenu", 3.0f);
	}

	void GoToMainMenu() {
		gameState = GameState.MainMenu;
        soundManager.SetMenuMusic();
    }

	// Update is called once per frame
	void Update () {
		progressBarTimer += Time.deltaTime;
		switch (gameState) {
		case GameState.MainMenu: {
                
				newGameProgressBar.SetActive (true);
				newGameProgressBar.transform.localScale = new Vector3 (newGameLoudnessTimer / 2.0f, 1, 1);
				if (AudioInput.MicLoudness > 0.3) {
					newGameLoudnessTimer += Time.deltaTime;
					Debug.Log (newGameLoudnessTimer);
					if (newGameLoudnessTimer > 2.0f) {
						newGameLoudnessTimer = 0.0f;
						newGameProgressBar.SetActive (false);
						newGameProgressBar.transform.localScale = new Vector3 (0, 1, 1);
						StartNewLevel (); 
					}
				}
				break;
			}
		case GameState.GameOver: {
				newGameProgressBar.SetActive (true);
				newGameProgressBar.transform.localScale = new Vector3 (newGameLoudnessTimer / 2.0f, 1, 1);
				if (AudioInput.MicLoudness > 0.3) {
					newGameLoudnessTimer += Time.deltaTime;
					Debug.Log (newGameLoudnessTimer);
					if (newGameLoudnessTimer > 2.0f) {
						newGameLoudnessTimer = 0.0f;
						newGameProgressBar.SetActive (false);
						newGameProgressBar.transform.localScale = new Vector3 (0, 1, 1);
						StartNewLevel (); 
					}
				}
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