using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public float enemySpeedMultiply = 1.0f;
	public const float currentEnemySpeed = 50;
	public GameObject enemyPrefab;

	private System.Random generator;
	public float radius = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		enemySpeedMultiply = 1.0f + GameManager.level / 10.0f;
	}

	public void SpawnLevel(int enemyCount) {
		generator = new System.Random();
		for (int i = 0; i < enemyCount; i++) {
			var enemy = Instantiate(enemyPrefab) as GameObject;
			float angle = (float)generator.NextDouble () * 2.0f * Mathf.PI;
			enemy.transform.position = new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
			enemy.transform.LookAt(Vector3.zero);
			enemy.transform.localScale += new Vector3(GetRandomNumber(0.8, 1.2), GetRandomNumber(0.8, 1.5), 0);
		}
	}

	void SpawnSingleEnemy(float x, float z) {
		GameObject newEnemy = Instantiate (enemyPrefab) as GameObject;
		newEnemy.transform.position = new Vector3 (x, 0, z);
	}

	private float GetRandomNumber(double minimum, double maximum)
	{
		return (float)(generator.NextDouble() * (maximum - minimum) + minimum);
	}
}
