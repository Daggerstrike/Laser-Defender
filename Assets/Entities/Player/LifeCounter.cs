using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour {

	public static int lives = 0;
	private Text myText;

	private PlayerSpawn playerSpawn;

	void Start () {
		myText = GetComponent<Text>();
		playerSpawn = GameObject.Find("PlayerSpawn").GetComponent<PlayerSpawn>();
	}

	// This is where death management is taken care of
	public void LoseLife() {
		lives -= 1;
		myText.text = string.Format("Lives: {0}", lives.ToString());
		if(lives >= 0)
			StartCoroutine(RespawnPlayer());
		else { 
			LifeCounter.Reset();
			GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadNextLevel();
		}
	}

	// Respawns a new player
	IEnumerator RespawnPlayer() {
		yield return new WaitForSeconds(2.0f);
		playerSpawn.Respawn();
		yield return new WaitForSeconds(2.0f);
		playerSpawn.EnableCollisions();
	}

	public void GainLife() {
		if(lives < 3) {
			lives += 1;
			myText.text = string.Format("Lives: {0}", lives.ToString());
		}
	}

	public int GetLives() {
		return lives;
	}

	public static void Reset() {
		lives = 0;
	}

}
