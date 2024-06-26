using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JetMovement : MonoBehaviour
{
	public float speed = 50f; // U�a��n ileri h�z�
	public float rotationSpeed = 10f; // U�a��n d�nme h�z�
	public float maxRotationAngle = 45f; // Maksimum d�nme a��s�
	[SerializeField] Transform weaponTransform;
	public float attackRange = 600f; // Sald�r� menzili
	public GameObject bulletPrefab; // Kur�un prefab�
	[SerializeField] TextMeshProUGUI puanText;
	[SerializeField] TextMeshProUGUI canText;
	public int puan = 0;
	public int can = 100;
	[SerializeField] GameManager gm;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		gm = FindObjectOfType<GameManager>();
	}

	void Update()
	{
		MoveJet();
		RotateJet();
		if (Input.GetKey(KeyCode.Space)) Attack();
		UIUpdate();
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
		Ray ray = new Ray(weaponTransform.position, transform.forward);
		RaycastHit hitInfo;

		// D��man� vurdu�umuz yerde bir kur�un olu�tur
		Instantiate(bulletPrefab, weaponTransform.position, transform.rotation);

		// Raycast ile d��man� tespit et
		if (Physics.Raycast(ray, out hitInfo, attackRange))
		{
			// Raycast d��man� vurduysa ve vurdu�u �ey bir d��man ise sald�r� ger�ekle�tir
			if (hitInfo.collider.CompareTag("enemy"))
			{
				// Opsiyonel: D��man� yok etmek i�in
				Destroy(hitInfo.collider.gameObject);
				puan++;
				puanText.text = "Puan: " + puan.ToString();
				//print("vurdun");
			}
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "enemy")
		{
			can -= 20;
			canText.text = "Can: " + can.ToString();
			Destroy(collision.gameObject);
			if (can <= 0)
			{
				can = 0;
				canText.text = "Can: " + can.ToString();
				gm.GameOverPanel();
			}
		}
	}

	void UIUpdate()
	{
		canText.text = "Can: " + can.ToString();
		puanText.text = "Puan: " + puan.ToString();
	}
}
