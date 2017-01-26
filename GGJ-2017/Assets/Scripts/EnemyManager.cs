using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	
	public static float currentEnemySpeed;
	public const float startEnemySpeed = 100f;
	public GameObject enemy1Prefab;
	public GameObject enemy2Prefab;
	public GameObject enemy3Prefab;
	public GameObject enemy4Prefab;
	public GameObject bossPrefab;

	private System.Random generator;
	public float radius = 100f;
	static public bool allWavesCompleted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator SpawnLevel(int enemyCount, int level, int waves, int currentWave, float delay) {
		allWavesCompleted = false;
		currentEnemySpeed = startEnemySpeed * (1 + (level - 1) / 10.0f);
		generator = new System.Random ();
		List<GameObject> monstersPool = new List<GameObject> ();
		monstersPool.Add (enemy1Prefab);
		if (level >= 2) {
			monstersPool.Add (enemy2Prefab);
		}
		if (level >= 3) {
			monstersPool.Add (enemy3Prefab);
		}
		if (level >= 4) {
			monstersPool.Add (enemy4Prefab);
		}
		if (level == 5 && currentWave == 0) {
			var enemy = Instantiate (bossPrefab) as GameObject;
			float angle = (float)generator.NextDouble () * 2.0f * Mathf.PI;
			enemy.transform.position = new Vector3 (Mathf.Cos (angle) * radius, 0f, Mathf.Sin (angle) * radius);
			enemy.transform.LookAt (Vector3.zero);
			enemy.transform.localScale += new Vector3 (GetRandomNumber (0.8, 1.2), GetRandomNumber (0.8, 1.5), 0);
		} else {
			if ((level == 5 && GameObject.Find ("Boss(Clone)")) || (level != 5)) {
				for (int i = 0; i < enemyCount; i++) {
					var index = Random.Range (0, monstersPool.Count);
					var enemy = Instantiate (monstersPool [index]) as GameObject;
					float angle = (float)generator.NextDouble () * 2.0f * Mathf.PI;
					enemy.transform.position = new Vector3 (Mathf.Cos (angle) * radius, 0f, Mathf.Sin (angle) * radius);
					enemy.transform.LookAt (Vector3.zero);
					enemy.transform.localScale += new Vector3 (GetRandomNumber (0.8, 1.2), GetRandomNumber (0.8, 1.5), 0);
				}
			} else {
				allWavesCompleted = true;
			}
		}
		yield return new WaitForSeconds (delay);
		if (currentWave != waves && GameManager.gameState == GameManager.GameState.Playing) {
			Debug.Log ("Called wave " + (currentWave + 1).ToString ());
			StartCoroutine (SpawnLevel (enemyCount, level, waves, currentWave + 1, delay));
		} else {
			allWavesCompleted = true;
		}
	}

	private float GetRandomNumber(double minimum, double maximum)
	{
		return (float)(generator.NextDouble() * (maximum - minimum) + minimum);
	}
}
