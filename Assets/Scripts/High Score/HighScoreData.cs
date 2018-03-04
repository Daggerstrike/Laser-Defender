using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Container for holding the high scores.
   This is needed for managing data for Json serialization. */
[System.Serializable]
public class HighScoreData {
	public HighScore[] scores;

	public HighScore getScore(int index) {
		return scores[index];
	}
}
