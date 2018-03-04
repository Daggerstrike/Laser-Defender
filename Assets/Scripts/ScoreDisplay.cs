using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles display of the score in text objects
public class ScoreDisplay : MonoBehaviour {
	public bool newHighScorePanel;		// Set in inspector

	void Start() {
		if(newHighScorePanel)
			FormatNewHighScore();
		else
			FormatScore();
	}

	// Formats the score as it appears on the New High Score Panel of the Game Over scene
	void FormatNewHighScore() {
		Text myText = GetComponent<Text>();
		myText.text = ScoreKeeper.score.ToString();
	}

	// Formats the score on the Game Over scene
	void FormatScore() {
		Text myText = GetComponent<Text>();
		myText.text = string.Format("Score: {0}", ScoreKeeper.score.ToString());
	}
}
