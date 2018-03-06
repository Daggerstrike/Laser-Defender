using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour 
{
	public GameObject enemyPrefab;
	public GameObject enemyGhostPrefab;

	private float width = 10f;
	private float height = 5f;
	private float speed = 5f;
	private float spawnDelay = 0.5f;

	private bool movingRight = false;
	private float xmax;
	private float xmin;
	
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
		xmax = rightBoundary.x;
		xmin = leftBoundary.x;
		SpawnUntilFull();
	}

	void Update () {
		if (movingRight)
			transform.position += Vector3.right * speed * Time.deltaTime;
		else
			transform.position += Vector3.left * speed * Time.deltaTime;

		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		// Flip direction of travel if boundaries hit
		if(leftEdgeOfFormation < xmin)
			movingRight = true;
		else if(rightEdgeOfFormation > xmax) 
			movingRight = false;

		if(AllMembersDead()) {
			SpawnUntilFull();
		}
	}

	// Spawn enemies one at a time
	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition();
		if(freePosition) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
			GameObject enemyGhost = Instantiate(enemyGhostPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemyGhost.transform.parent = freePosition;
		}
		if(NextFreePosition())
			Invoke("SpawnUntilFull", spawnDelay);
		else if(!NextFreePosition())
			DestroyGhosts();
	}

	Transform NextFreePosition() {
		foreach(Transform child in transform) {
			if(child.childCount == 0)
				return child;
		}
		return null;
	}

	// If all enemies are dead return true; else return false
	bool AllMembersDead() {
		foreach(Transform childPositionGameObject in transform) {
			if(childPositionGameObject.childCount > 0)
				return false;
		}
		return true;
	}

	// Enemy ghosts are just empty GameObjects that indicate where an enemy will spawn
	// This prevents players from spawning enemies in an endless loop
	void DestroyGhosts() {
		foreach(Transform child in transform)
			foreach(Transform grandChild in child)
				if(grandChild.name == "EnemyGhost(Clone)")
					Destroy(grandChild.gameObject);
	}

	// Draws a box around the EnemyFormation in the editor
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new  Vector3(width, height, 0));
	}
}
