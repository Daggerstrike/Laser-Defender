using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//public class HighScoreManager : MonoBehaviour {
public class HighScoreManager {

	private string highScoreFileName = "scores.json";
	private HighScoreData highScores;
	private string filePath;

	public HighScoreManager() {
		filePath = Path.Combine(Application.streamingAssetsPath, highScoreFileName);
	}

	// Loads in Json data
	public void LoadHighScores() {
		if(File.Exists(filePath)) {
			string dataAsJson = File.ReadAllText(filePath);
			highScores = JsonUtility.FromJson<HighScoreData>(dataAsJson);
		}

		else
			Debug.LogError("Cannot load game data");
	}

	// Checks to see if new high score
	public int CheckHighScores() {
		LoadHighScores();
		for(int i=0; i<highScores.scores.Length; i++) {
			if(ScoreKeeper.score > highScores.scores[i].score)
				return i;
		}
		return -1;
	}

	// Saves the high score if it's higher than one on the high score board
	public void SaveHighScores(string initials) {
		int index = CheckHighScores();
		if(index != -1) {
			// Shift all current high scores back one space
			for(int j=highScores.scores.Length-1; j>index; j--) {
				highScores.scores[j].name = highScores.scores[j-1].name;
				highScores.scores[j].score = highScores.scores[j-1].score;
			}

			// Replace high score at position with new data
			highScores.scores[index].name = initials;
			highScores.scores[index].score = ScoreKeeper.score;
			highScores.scores[index].position = index+1;

			string json = JsonUtility.ToJson(highScores);
			File.WriteAllText(filePath, json);
		}

		else {
			Debug.LogError("Cannot save game data");
		}
	}
}
