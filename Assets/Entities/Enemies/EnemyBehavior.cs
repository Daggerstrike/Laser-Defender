using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

	public GameObject laserPrefab;
	public int laserSpeed = -10;	// Must be negative in order to make lasers fire downward
	public float health = 150f;
	public float shotsPerSecond = 0.5f;

	public int pointValue = 100;
	private ScoreKeeper scoreKeeper;

	public AudioClip fireSound;
	public AudioClip deathSound;

	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Laser missile = collider.gameObject.GetComponent<Laser>();

		// Laser was fired and collides with enemy
		if(missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0)
				Die();
		}
	}

	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(pointValue);
	}

	void Fire() {
		GameObject missile = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void Update() {
		float probability = shotsPerSecond * Time.deltaTime;
		if(Random.value < probability)
			Fire();
	}
}