using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] GameObject gameOverPanel;

	public void GameOverPanel()
	{
		gameOverPanel.SetActive(!gameOverPanel.activeSelf);
	}

	public void RestartButton()
	{
		SceneManager.LoadScene(1);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene(0);
	}
}
