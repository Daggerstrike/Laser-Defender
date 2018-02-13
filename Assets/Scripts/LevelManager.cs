using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour 
{
	// SceneManager takes Build Settings index as an argument
	public void LoadLevel(int index) {
		SceneManager.LoadScene(index);
	}

	public void QuitRequest() {
		Application.Quit();
	}

	public void LoadNextLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}