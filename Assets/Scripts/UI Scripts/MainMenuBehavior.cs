using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
	public void playButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		lockCursor();
	}

	public void quitButton()
	{
		Application.Quit();
	}

	public void resetButton()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		lockCursor();
	}

	private void lockCursor()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
