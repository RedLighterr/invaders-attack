using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetMovement : MonoBehaviour
{
	public float speed = 50f; // Uçaðýn ileri hýzý
	public float rotationSpeed = 10f; // Uçaðýn dönme hýzý
	public float maxRotationAngle = 1f; // Maksimum dönme açýsý

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
		// Uçaðý ileri doðru hareket ettir
		rb.velocity = rb.transform.forward * speed * Time.deltaTime * 100;
	}

	void RotateJet()
	{
		// Ekran merkezini referans al
		Vector3 mousePos = Input.mousePosition;
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;

		// Fare hareketlerini normalize et (-0.5, 0.5 aralýðýna getir)
		float horizontal = (mousePos.x / screenWidth - 0.5f);
		float vertical = (mousePos.y / screenHeight - 0.5f) / 2;

		// Maksimum dönme açýsý ile çarp
		float yaw = -horizontal * maxRotationAngle;
		float pitch = -vertical * maxRotationAngle;

		// Yeni rotasyonu hesapla
		Quaternion targetRotation = Quaternion.Euler(pitch, 0, yaw);

		// Uçaðý yavaþça hedef rotasyona döndür
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}
}
