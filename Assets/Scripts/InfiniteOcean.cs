using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteOcean : MonoBehaviour
{
	public GameObject oceanTilePrefab;
	public int oceanGridSize = 3;
	public float tileSize = 350f;

	private Transform playerTransform;
	private Vector3 startPos;

	private GameObject[,] oceanTiles;

	void Start()
	{
		playerTransform = Camera.main.transform;
		startPos = playerTransform.position;

		oceanTiles = new GameObject[oceanGridSize, oceanGridSize];
		for (int x = 0; x < oceanGridSize; x++)
		{
			for (int z = 0; z < oceanGridSize; z++)
			{
				Vector3 pos = new Vector3(
					startPos.x + (x - oceanGridSize / 2) * tileSize,
					-5,
					startPos.z + (z - oceanGridSize / 2) * tileSize
				);
				oceanTiles[x, z] = Instantiate(oceanTilePrefab, pos, Quaternion.identity);
			}
		}

		// InvokeRepeating ile UpdateOceanTiles fonksiyonunu belirli aralýklarla çaðýr
		InvokeRepeating("UpdateOceanTiles", 0f, 1f); // Ýlk çaðrýyý 0 saniye sonra yap, ardýndan her saniye tekrarla
	}

	void UpdateOceanTiles()
	{
		for (int x = 0; x < oceanGridSize; x++)
		{
			for (int z = 0; z < oceanGridSize; z++)
			{
				Vector3 tilePos = oceanTiles[x, z].transform.position;
				Vector3 offset = tilePos - playerTransform.position;

				if (offset.z <= -tileSize)
				{
					tilePos.z += tileSize * oceanGridSize;
					oceanTiles[x, z].transform.position = tilePos;
				}
				else if (offset.z > tileSize)
				{
					tilePos.z -= tileSize * oceanGridSize;
					oceanTiles[x, z].transform.position = tilePos;
				}
				if (offset.x <= -tileSize)
				{
					tilePos.x += tileSize * oceanGridSize;
					oceanTiles[x, z].transform.position = tilePos;
				}
				else if (offset.x > tileSize)
				{
					tilePos.x -= tileSize * oceanGridSize;
					oceanTiles[x, z].transform.position = tilePos;
				}
			}
		}
	}
}
