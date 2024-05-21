using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 100f; // Mermi h�z�

	void Update()
	{
		// Mermiyi ileri do�ru hareket ettir
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	// E�er mermi belirli bir menzile ula��rsa yok edilmesi i�in
	void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
	}
}
