using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	public static bool speedBoostOn;
	public static bool shieldOn;

	// If powerup collected, destroy instance of it
	public void Collected() {
		Destroy(gameObject);
	}

	//----------------SHIELD--------------------
	public static void SetShield(bool up) {
		if(up)
			shieldOn = true;
		else
			shieldOn = false;
	}

	public static bool GetShield() {
		return shieldOn;
	}

	//---------------SPEED BOOST---------------
	public static void SetSpeedBoost(bool up) {
		if(up)
			speedBoostOn = true;
		else
			speedBoostOn = false;
	}

	public static bool GetSpeedBoost() {
		return speedBoostOn;
	}
}
