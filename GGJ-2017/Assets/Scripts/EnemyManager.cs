using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public float enemySpeedMultiply = 1.0f;
	public const float currentEnemySpeed = 50;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		enemySpeedMultiply = 1.0f + GameManager.level / 10.0f;
	}

	void SpawnLevel(int enemyCount) {
		
	}

	void SpawnSingleEnemy() {
		
	}
}
