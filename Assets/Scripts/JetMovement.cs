using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetMovement : MonoBehaviour
{
	public float speed = 50f; // U�a��n ileri h�z�
	public float rotationSpeed = 10f; // U�a��n d�nme h�z�
	public float maxRotationAngle = 1f; // Maksimum d�nme a��s�

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		MoveJet();
		RotateJet();
	}

	void MoveJet()
	{
		// U�a�� ileri do�ru hareket ettir
		rb.velocity = rb.transform.forward * speed * Time.deltaTime * 100;
	}

	void RotateJet()
	{
		// Ekran merkezini referans al
		Vector3 mousePos = Input.mousePosition;
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;

		// Fare hareketlerini normalize et (-0.5, 0.5 aral���na getir)
		float horizontal = (mousePos.x / screenWidth - 0.5f);
		float vertical = (mousePos.y / screenHeight - 0.5f) / 2;

		// Maksimum d�nme a��s� ile �arp
		float yaw = -horizontal * maxRotationAngle;
		float pitch = -vertical * maxRotationAngle;

		// Yeni rotasyonu hesapla
		Quaternion targetRotation = Quaternion.Euler(pitch, 0, yaw);

		// U�a�� yava��a hedef rotasyona d�nd�r
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}
}
