using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScore : MonoBehaviour {
	private static GameObject newHighScorePanel;
	private static GameObject UIPanel;

	void Start () {
		newHighScorePanel = GameObject.Find("New High Score Panel");
		UIPanel = GameObject.Find("UI Panel");
		HighScoreManager highScoreManager = new HighScoreManager();
		DisableNewHSPanel();		// Disable by default

		// If there is a new high score, enable the panel
		if(highScoreManager.CheckHighScores() != -1) {
			EnableNewHSPanel();
		}
	}

	// Used separate methods so that panel state can be changed from buttons if needed
	public static void EnableNewHSPanel() {
		newHighScorePanel.SetActive(true);
		UIPanel.SetActive(false);
	}

	public static void DisableNewHSPanel() {
		newHighScorePanel.SetActive(false);
		UIPanel.SetActive(true);
	}
}
