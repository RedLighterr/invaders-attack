using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float speed = 5f;
	private Transform playerTransform;

	void Start()
	{
		playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update()
	{
		Vector3 direction = playerTransform.position - transform.position - new Vector3(0 , 0, 15);
		direction.Normalize();
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
		transform.LookAt(playerTransform.position);
	}
}
