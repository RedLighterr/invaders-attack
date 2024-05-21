using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed = 100f; // Mermi hýzý

	void Update()
	{
		// Mermiyi ileri doðru hareket ettir
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	// Eðer mermi belirli bir menzile ulaþýrsa yok edilmesi için
	void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
	}
}
