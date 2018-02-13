using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns an instance of the player character
public class PlayerSpawn : MonoBehaviour {

	public GameObject playerPrefab;
	private GameObject player;
	public float width = 10f;
	public float height = 5f;

	void Start () {
		InitialSpawn();
	}

	public void InitialSpawn() {
		foreach (Transform child in transform) {
			player = Instantiate(playerPrefab, child.transform.position, Quaternion.identity) as GameObject;
			gameObject.GetComponent<Animator>().enabled = false;	// Turn off animation for inital spawn
			player.transform.parent = child;
		}
	}

	// Seperate function for respawn since the Animator needs to be enabled
	public void Respawn() {
		foreach (Transform child in transform) {
			player = Instantiate(playerPrefab, child.transform.position, Quaternion.identity) as GameObject;
			player.GetComponent<PolygonCollider2D>().enabled = false;
			gameObject.GetComponent<Animator>().enabled = true;		// Re-enable the animation for respawn
			gameObject.GetComponent<Animator>().Play("RespawnAnimation", -1, 0f);
			player.transform.parent = child;
		}
	}

	// Re-enable collisions. This was disabled temporarily in Respawn to prevent collisions from happening during animation
	public void EnableCollisions() {
		player.GetComponent<PolygonCollider2D>().enabled = true;
	}

	// Draws a box around the PlayerSpawn in the editor
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new  Vector3(width, height, 0));
	}
}
