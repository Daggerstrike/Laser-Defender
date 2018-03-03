using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

	private static bool currentlyPaused = false;
	private static GameObject pausePanel;

	void Start() {
		pausePanel = GameObject.Find("Pause Panel");
		pausePanel.SetActive(false);	// Disable pause panel by default
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape) && !currentlyPaused)
			PauseGame();
	}

	public void PauseGame() {
		Time.timeScale = 0;
		currentlyPaused = true;
		pausePanel.SetActive(true);
	}

	public void UnpauseGame() { 
		Time.timeScale = 1;
		currentlyPaused = false;
		pausePanel.SetActive(false);
	}
}
