using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public GameObject laserPrefab;
	public GameObject shieldPrefab;
	private GameObject shieldImage;

	public int laserSpeed = 10;
	private float health = 200f;
	public float fireRate = 1f;
	public float speed = 10.0f; 
	float globalTime = 0f;

	public float padding = 1.0f;
	float xmin;
	float xmax;

	public AudioClip fireSound;
	public AudioClip deathSound;
	public AudioClip shieldUp;
	public AudioClip shieldDown;
	public AudioClip speedBoostOn;
	public AudioClip speedBoostOff;

	private LifeCounter lifeCounter;

	void Start() {
		// Used to determine boundaries for the game space where the player can move
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;

		// Get LifeCounter so that we can inc/dec lives for certain events
		lifeCounter = GameObject.Find("Lives").GetComponent<LifeCounter>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Laser missile = collider.gameObject.GetComponent<Laser>();
		Meteor meteor = collider.gameObject.GetComponent<Meteor>();
		Powerup powerup = collider.gameObject.GetComponent<Powerup>();

		// Laser was fired and collides with player
		if(missile) {
			// If shield is hit, negate the damage and destroy the missile
			if(Powerup.GetShield()) {
				ShieldDown();
				missile.Hit();
			}
			// Else take damage
			else {
				health -= missile.GetDamage();
				missile.Hit();
				HealthCheck();
			}
		}

		// Meteor collides with player
		if(meteor) {
			// If shield is hit, negate the damage and destroy the meteor
			if(Powerup.GetShield()) {
				ShieldDown();
				meteor.Hit();
			}
			// Else take damage
			else {
				health -= meteor.GetDamage();
				meteor.Hit();
				HealthCheck();
			}
		}

		// If player collects a shield powerup
		if(powerup && collider.gameObject.name.Contains("shieldPowerup"))
			ShieldUp(powerup);

		// If player collects a speed powerup
		if(powerup && collider.gameObject.name.Contains("speedPowerup"))
			SpeedBoostOn(powerup);
	}

	void HealthCheck() {
		if(health <= 0) {
			ResetHealth();
			Die();
		}
	}

	void ResetHealth() {
		health = 200f;
	}

	void ShieldUp(Powerup powerup) {
		// Only display shield image if there isn't one active already (prevents duplicates)
		if(!Powerup.GetShield())
			shieldImage = Instantiate(shieldPrefab, transform.position, Quaternion.identity) as GameObject;
		Powerup.SetShield(true);
		AudioSource.PlayClipAtPoint(shieldUp, transform.position);
		powerup.Collected();
	}

	void ShieldDown() {
		Destroy(shieldImage);
		Powerup.SetShield(false);
		AudioSource.PlayClipAtPoint(shieldDown, transform.position);
	}

	void SpeedBoostOn(Powerup powerup) {
		// Player cannot stack speed boosts
		if(Powerup.GetSpeedBoost() != true) {
			Powerup.SetSpeedBoost(true);
			globalTime = Time.time;
			speed *= 2;
			fireRate /= 2;
			AudioSource.PlayClipAtPoint(speedBoostOn, transform.position);
		}
		powerup.Collected();
	}

	void SpeedBoostOff() {
		Powerup.SetSpeedBoost(false);
		speed /= 2;
		fireRate *= 2;
		AudioSource.PlayClipAtPoint(speedBoostOff, transform.position);
	}

	void Fire() {
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject beam = Instantiate(laserPrefab, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Powerup.SetSpeedBoost(false);
		Powerup.SetShield(false);
		lifeCounter.LoseLife();	// LoseLife handles the transitions and whatnot
		Destroy(gameObject);	// Destroy the player GameObject
	}

	void Update() {
		// Restrict player to game space
		float newx = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newx, transform.position.y, transform.position.z);

		// Move left and right
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.position += Vector3.left * speed * Time.deltaTime;
		else if(Input.GetKey(KeyCode.RightArrow))
			transform.position += Vector3.right * speed * Time.deltaTime;

		// If space is held down, fire lasers at a set rate
		// "Fire" refers to the method above via string
		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, fireRate);
		}

		// If space is released, cease firing lasers
		if(Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}

		// If speed boost expired, set speed and fire rate back to normal
		if (Powerup.GetSpeedBoost() && Time.time - globalTime >= 20f) {
			SpeedBoostOff();
		}

		// Set position of shield to be the same as the player
		if(Powerup.GetShield())
			shieldImage.transform.position = transform.position;
	}
}
