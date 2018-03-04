using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour {

	void OnGUI() {
		string timePassed;

		if(Time.timeScale != 0)
			timePassed = ((int)(1.0f / Time.smoothDeltaTime)).ToString();
		else
			timePassed = "0";
		GUI.Label(new Rect(0, 0, 100, 100), timePassed);
	}
}
