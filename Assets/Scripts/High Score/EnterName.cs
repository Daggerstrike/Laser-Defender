using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles name entry for the New High Score Panel of the Game Over scene
public class EnterName : MonoBehaviour {
	private char[] nameField;
	private string text;
	private int alphaPosition;
	private int index;
	private static GameObject panel;

	void Start() {
		nameField = new char[] {'A', 'A', 'A'};
		alphaPosition = 1;
		index = 0;
	}

	void Update () {
		Text myText = GetComponent<Text>();

		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
			&& alphaPosition < 26) {
			alphaPosition += 1;
			nameField[index] = NumberToChar(alphaPosition);
		}

		if((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
			&& alphaPosition > 1) {
			alphaPosition -= 1;
			nameField[index] = NumberToChar(alphaPosition);
		}

		if((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			&& index < nameField.Length - 1) {
			index += 1;
			alphaPosition = CharToNumber(nameField[index]);
		}

		if((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
			&& index > 0) {
			index -= 1;
			alphaPosition = CharToNumber(nameField[index]);
		}

		for(int i=0; i<nameField.Length; i++)
			text = text + nameField[i].ToString();
		myText.text = text;

		// Submit initals and save to high score board
		// Disables the panel and returns to ordinary game over scene
		if(Input.GetKeyDown(KeyCode.Return)) {
			HighScoreManager highScoreManager = new HighScoreManager();
			highScoreManager.SaveHighScores(text);
			NewHighScore.DisableNewHSPanel();
		}

		text = "";	// Reset text variable after displaying it
					// Stuck this at the bottom so that text won't reset before hitting enter
	}

	// Converts position in alphabet to character
	private char NumberToChar(int number) {
		number += 64;	// Add 64 to get ASCII value of letter
		char letter = (char)number;
		return letter;
	}

	// Converts character to position in alphabet
	private int CharToNumber(char letter) {
		int index = char.ToUpper(letter) - 64;
		return index;
	}
}