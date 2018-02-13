using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour 
{
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 3f;
	public float spawnDelay = 0.5f;

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

	void SpawnUntilFull() {
		Transform freePosition = NextFreePosition();
		if(freePosition != null) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition() != null)
			Invoke("SpawnUntilFull", spawnDelay);
	}

	Transform NextFreePosition() {
		foreach(Transform childPositionGameObject in transform) {
			if(childPositionGameObject.childCount == 0)
				return childPositionGameObject;
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

	// Draws a box around the EnemyFormation in the editor
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new  Vector3(width, height, 0));
	}
}
