using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float spawnInterval = 3f;
	public float spawnDistance = 50f;
	private Transform playerTransform;

	void Start()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
	}

	void SpawnEnemy()
	{
		float x_min = spawnDistance - 25, x_max = spawnDistance + 25;
		float y_min = -10, y_max = 10;
		float z_min = spawnDistance - 25, z_max = spawnDistance + 25;

		Vector3 randomPos = new Vector3(
			playerTransform.position.x + Random.Range(x_min, x_max), 
			playerTransform.position.y + Random.Range(y_min, y_max), 
			playerTransform.position.z + Random.Range(z_min, z_max)
			);

		Vector3 spawnPosition = playerTransform.position + randomPos;
		spawnPosition += new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
		Instantiate(enemyPrefab, spawnPosition, new Quaternion(0 ,180 , 0, 0));
	}
}
