using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls enemy actions
public class EnemyBehavior : MonoBehaviour {

	public GameObject laserPrefab;
	private int laserSpeed = -10;	// Must be negative in order to make lasers fire downward
	private float health = 200f;
	private float shotsPerSecond = 0.5f;

	private int pointValue = 100;
	private ScoreKeeper scoreKeeper;
	private LifeCounter lifeCounter;

	public AudioClip fireSound;
	public AudioClip deathSound;

	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		lifeCounter = GameObject.Find("Lives").GetComponent<LifeCounter>();
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

	void Fire() {
		GameObject missile = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(pointValue);

		// Since lives are based on score, I must add lives in the EnemyBehavior script rather than PlayerController
		// It has to be done here, since this is where score is changed
		if(ScoreKeeper.score % 1000 == 0 && ScoreKeeper.score != 0)
			lifeCounter.GainLife();
	}

	// Randomly fire lasers 
	void Update() {
		float probability = shotsPerSecond * Time.deltaTime;
		if(Random.value < probability)
			Fire();
	}
}