using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public float enemySpeedMultiply = 1.0f;
	public const float currentEnemySpeed = 50;
	public GameObject enemyPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		enemySpeedMultiply = 1.0f + GameManager.level / 10.0f;
	}

	public void SpawnLevel(int enemyCount) {
		for (int i=-4; i<0; i++) {
			SpawnSingleEnemy (4 * i, 10);
		}
	}

	void SpawnSingleEnemy(float x, float z) {
		GameObject newEnemy = Instantiate (enemyPrefab) as GameObject;
		newEnemy.transform.position = new Vector3 (x, 0, z);
	}
}
