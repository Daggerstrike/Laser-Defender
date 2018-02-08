using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public GameObject laserPrefab;
	public int laserSpeed = 10;
	public float fireRate = 0.2f;
	public float health = 150f;

	public float speed = 40.0f; 
	public float padding = 1.0f;

	float xmin;
	float xmax;

	public AudioClip fireSound;
	public AudioClip deathSound;

	void Start () {
		// Used to determine boundaries for the game space where the player can move
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	void Fire() {
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject beam = Instantiate(laserPrefab, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.position += Vector3.left * speed * Time.deltaTime;
		else if(Input.GetKey(KeyCode.RightArrow))
			transform.position += Vector3.right * speed * Time.deltaTime;

		// Restrict player to game space
		float newx = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newx, transform.position.y, transform.position.z);

		// If space is held down, fire lasers at a set rate
		// "Fire" refers to the method above via string
		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, fireRate);
		}

		// If space is released, cease firing lasers
		if(Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Laser missile = collider.gameObject.GetComponent<Laser>();

		// Laser was fired and collides with player
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
		GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadNextLevel();	
	}
}
