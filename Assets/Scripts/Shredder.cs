using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys lasers fired by player if they go out of bounds
public class Shredder : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		Destroy(collider.gameObject);
	}
}
