using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTable : MonoBehaviour {

	public GUIStyle guiStyle = new GUIStyle();
	private HighScore[] scores;
	public Font myFont;

	void Start() {
		HighScoreManager highScoreManager = new HighScoreManager();
		highScoreManager.LoadHighScores();
		scores = highScoreManager.GetHighScores();
	}

	void OnGUI() {
		// Quick and dirty way to adjust font relative to screen size
		int baseWidth = 800;
		guiStyle.fontSize = 30 * (Screen.width/baseWidth);

		guiStyle.font = myFont;
		guiStyle.normal.textColor = Color.magenta;
		MakeTable(scores, 3);
	}

	private void MakeTable(HighScore[] highScores, int numOfDividers) {
		float widthOfCell = (float)Screen.width / (float)numOfDividers;

		// Determines starting position of table on screen
		GUILayout.BeginArea(new Rect(Screen.width/8, Screen.width/4, Screen.width, Screen.height));

		// Fills in the rows of the table
		for(int i=0; i<highScores.Length; i++) {
			GUILayout.BeginHorizontal();
			GUILayout.Label(highScores[i].position.ToString(), guiStyle, GUILayout.Width(widthOfCell));
			GUILayout.Label(highScores[i].score.ToString(), guiStyle, GUILayout.Width(widthOfCell));
			GUILayout.Label(highScores[i].name.ToString(), guiStyle, GUILayout.Width(widthOfCell));
			GUILayout.EndHorizontal();
		}

		GUILayout.EndArea();
	}
}
