using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetMovement : MonoBehaviour
{
	public float speed = 50f; // U�a��n ileri h�z�
	public float rotationSpeed = 10f; // U�a��n d�nme h�z�
	public float maxRotationAngle = 45f; // Maksimum d�nme a��s�
	[SerializeField] Transform weaponTransform;
	public float attackRange = 600f; // Sald�r� menzili
	public GameObject bulletPrefab; // Kur�un prefab�

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
		// Klavye girdilerinden d�nme hareketini al
		float horizontalRotation = Input.GetAxis("Horizontal") * -rotationSpeed * Time.deltaTime * 10;

		// Yeni rotasyonu hesapla ve u�a�� d�nd�r
		float verticalRotation = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime * 10;

		Quaternion deltaRotation = Quaternion.Euler(verticalRotation, 0, horizontalRotation);
		rb.MoveRotation(rb.rotation * deltaRotation);
	}

	void Attack()
	{
		// U�a��n �n�nden bir ray olu�tur
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hitInfo;

		// Raycast ile d��man� tespit et
		if (Physics.Raycast(ray, out hitInfo, attackRange))
		{
			// Raycast d��man� vurduysa ve vurdu�u �ey bir d��man ise sald�r� ger�ekle�tir
			if (hitInfo.collider.CompareTag("Enemy"))
			{
				// D��man� vurdu�umuz yerde bir kur�un olu�tur
				Instantiate(bulletPrefab, hitInfo.point, Quaternion.identity);
				// Opsiyonel: D��man� yok etmek i�in
				Destroy(hitInfo.collider.gameObject);
			}
		}
	}
}
