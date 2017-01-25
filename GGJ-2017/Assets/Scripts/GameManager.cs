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
	public DinosuarController playerScript;
	public static int level = 1;
	public static GameState gameState;
	public static float breathingTime;
	public static int damageByBullet = 10;
	public static int highscore = 0;
	public float progressBarTime;
	public GameObject breathingObject;
	public GameObject breathingObject2;
	public GameObject breathingObject3;
	public GameObject progressBar;
	public GameObject progressBar2;
	public GameObject progressBar3;
	private float progressBarTimer = 0.0f;
	private float newGameLoudnessTimer = 0.0f;
	private int startEnemiesCount = 3;
	public GameObject newGameProgressBar;
	public GameObject newGameProgressBar2;
	public GameObject newGameProgressBar3;
	public UIScript uiScript;
	public AdsManager adsManager;

    
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
		progressBar2.SetActive (true);
		progressBar3.SetActive (true);
		breathingObject.SetActive (true);
		breathingObject2.SetActive (true);
		breathingObject3.SetActive (true);
		GameManager.level++;
		Invoke ("StartNewLevel", breathingTime);
        soundManager.FadeInBattle();
	}

	void StartNewLevel() {
        if (level == 1)
		{
			adsManager.HideBanner ();
            soundManager.SetBattleMusic();
        }
        else
        {
            soundManager.FadeOutBattle();
        }
		progressBar.transform.localScale = new Vector3 (1, 1, 1);
		progressBar2.transform.localScale = new Vector3 (1, 1, 1);
		progressBar3.transform.localScale = new Vector3 (1, 1, 1);
		progressBar.SetActive (false);
		progressBar2.SetActive (false);
		progressBar3.SetActive (false);
		breathingObject.SetActive (false);
		breathingObject2.SetActive (false);
		breathingObject3.SetActive (false);
		gameState = GameState.Playing;
		if (level == 1 || level == 2 || level == 4) {
			StartCoroutine (enemyManager.SpawnLevel (startEnemiesCount + level, level, level + 1, 0, 6f + level));
		} else if (level == 3) {
			StartCoroutine (enemyManager.SpawnLevel (1, level, 20, 0, 0.5f));
		} else if (level == 5) {
			StartCoroutine (enemyManager.SpawnLevel (3, level, 100, 0, 10f));
		}
		if (level == 1) {
			playerScript.SetStarted (true);
		}
	}

	public void GameOver() {
		adsManager.ShowBanner ();
		gameState = GameState.GameOver;
		highscore = GameManager.level;
		GameManager.level = 1;
		PlayerPrefs.SetInt ("highscore", highscore);
		PlayerPrefs.Save ();
		Invoke ("ShowInterstitial", 2.0f);
	}

	public void ShowInterstitial() {
		adsManager.ShowInterstitial ();
	}

	public void Win() {
		gameState = GameState.Win;
		Invoke ("GoToMainMenu", 3.0f);
	}

	public void GoToMainMenu() {
		adsManager.ShowBanner ();
		gameState = GameState.MainMenu;
        soundManager.SetMenuMusic();
	}

	// Update is called once per frame
	void Update () {
		progressBarTimer += Time.deltaTime;
		switch (gameState) {
		case GameState.MainMenu: {
                
				newGameProgressBar.SetActive (true);
				newGameProgressBar2.SetActive (true);
				newGameProgressBar3.SetActive (true);
				newGameProgressBar.transform.localScale = new Vector3 (newGameLoudnessTimer / 1.0f, 1, 1);
				newGameProgressBar2.transform.localScale = new Vector3 (newGameLoudnessTimer / 1.0f, 1, 1);
				newGameProgressBar3.transform.localScale = new Vector3 (newGameLoudnessTimer / 1.0f, 1, 1);
				if (AudioInput.MicLoudness > 0.3) {
					newGameLoudnessTimer += Time.deltaTime*3;
					Debug.Log (newGameLoudnessTimer);
					if (newGameLoudnessTimer > 1.0f) {
						newGameLoudnessTimer = 0.0f;
						newGameProgressBar.SetActive (false);
						newGameProgressBar2.SetActive (false);
						newGameProgressBar3.SetActive (false);
						newGameProgressBar.transform.localScale = new Vector3 (0, 1, 1);
						newGameProgressBar2.transform.localScale = new Vector3 (0, 1, 1);
						newGameProgressBar3.transform.localScale = new Vector3 (0, 1, 1);
						StartNewLevel (); 
					}
				}
				break;
			}
		case GameState.GameOver: {
				newGameProgressBar.SetActive (true);
				newGameProgressBar2.SetActive (true);
				newGameProgressBar3.SetActive (true);
				newGameProgressBar.transform.localScale = new Vector3 (newGameLoudnessTimer / 1.0f, 1, 1);
				newGameProgressBar2.transform.localScale = new Vector3 (newGameLoudnessTimer / 1.0f, 1, 1);
				newGameProgressBar3.transform.localScale = new Vector3 (newGameLoudnessTimer / 1.0f, 1, 1);
				if (AudioInput.MicLoudness > 0.3) {
					newGameLoudnessTimer += Time.deltaTime*3;
					Debug.Log (newGameLoudnessTimer);
					if (newGameLoudnessTimer > 1.0f) {
						newGameLoudnessTimer = 0.0f;
						newGameProgressBar.SetActive (false);
						newGameProgressBar2.SetActive (false);
						newGameProgressBar3.SetActive (false);
						newGameProgressBar.transform.localScale = new Vector3 (0, 1, 1);
						newGameProgressBar2.transform.localScale = new Vector3 (0, 1, 1);
						newGameProgressBar3.transform.localScale = new Vector3 (0, 1, 1);
						StartNewLevel (); 
					}
				}
				break;
			}
		case GameState.Breathing: {
				progressBar.transform.localScale = Vector3.Lerp (new Vector3 (1, 1, 1), new Vector3 (0, 1, 1), progressBarTimer / breathingTime);
				progressBar2.transform.localScale = Vector3.Lerp (new Vector3 (1, 1, 1), new Vector3 (0, 1, 1), progressBarTimer / breathingTime);
				progressBar3.transform.localScale = Vector3.Lerp (new Vector3 (1, 1, 1), new Vector3 (0, 1, 1), progressBarTimer / breathingTime);
				break;
			}
		case GameState.Playing: {
                    break;
			}
		}
	}
}