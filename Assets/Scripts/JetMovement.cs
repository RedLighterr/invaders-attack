using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetMovement : MonoBehaviour
{
	public float speed = 50f; // Uçaðýn ileri hýzý
	public float rotationSpeed = 10f; // Uçaðýn dönme hýzý
	public float maxRotationAngle = 45f; // Maksimum dönme açýsý
	[SerializeField] Transform weaponTransform;
	public float attackRange = 600f; // Saldýrý menzili
	public GameObject bulletPrefab; // Kurþun prefabý

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		MoveJet();
		RotateJet();
		if (Input.GetKey(KeyCode.Space)) Attack();
	}

	void MoveJet()
	{
		// Klavye girdilerinden ileriye hareket et
		rb.velocity = transform.forward * speed * Time.deltaTime * 100;
	}

	void RotateJet()
	{
		// Klavye girdilerinden dönme hareketini al
		float horizontalRotation = Input.GetAxis("Horizontal") * -rotationSpeed * Time.deltaTime * 10;

		// Yeni rotasyonu hesapla ve uçaðý döndür
		float verticalRotation = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime * 10;

		Quaternion deltaRotation = Quaternion.Euler(verticalRotation, 0, horizontalRotation);
		rb.MoveRotation(rb.rotation * deltaRotation);
	}

	void Attack()
	{
		// Uçaðýn önünden bir ray oluþtur
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;

		// Raycast ile düþmaný tespit et
		if (Physics.Raycast(ray, out hitInfo, attackRange))
		{
			// Raycast düþmaný vurduysa ve vurduðu þey bir düþman ise saldýrý gerçekleþtir
			if (hitInfo.collider.CompareTag("Enemy"))
			{
				// Düþmaný vurduðumuz yerde bir kurþun oluþtur
				Instantiate(bulletPrefab, hitInfo.point, Quaternion.identity);
				// Opsiyonel: Düþmaný yok etmek için
				Destroy(hitInfo.collider.gameObject);
			}
		}
	}
}
