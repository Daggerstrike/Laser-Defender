using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

	private float spawnRate = 1.0f;
	float globalTime = 0f;
	private int objectSpeed = -5;

	public GameObject shieldPowerupPrefab;
	public GameObject speedPowerupPrefab;
	public GameObject meteorPrefab1;
	public GameObject meteorPrefab2;
	public GameObject meteorPrefab3;
	public GameObject meteorPrefab4;
	List <GameObject> prefabList = new List <GameObject>();

	void Start () {
		// Adds prefabs to list
		prefabList.Add(meteorPrefab1);
		prefabList.Add(meteorPrefab2);
		prefabList.Add(meteorPrefab3);
		prefabList.Add(meteorPrefab4);
		prefabList.Add(shieldPowerupPrefab);
		prefabList.Add(speedPowerupPrefab);

		InvokeRepeating("Spawn", 2f, spawnRate);
	}

	void Spawn() {

		int prefabIndex;

		// Every tenth object spawned in will be a powerup
		if(Time.time - globalTime >= 20f) {
			globalTime = Time.time;
			prefabIndex = Random.Range(4, 6);
		}

		// Everything else will be a meteor
		else
			prefabIndex = Random.Range(0, 4);

		GameObject objectSpawn = Instantiate(prefabList[prefabIndex], new Vector3(Random.Range(-6f, 6f), 8, 0), Quaternion.identity) as GameObject;
		objectSpawn.GetComponent<Rigidbody2D>().velocity = new Vector3(0, objectSpeed, 0);
	}
}
