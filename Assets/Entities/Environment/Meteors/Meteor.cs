using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
	private float damage = 100f;
	private float health = 200f;

	public float GetDamage() {
		return damage;
	}

	public void Hit() {
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Laser missile = collider.gameObject.GetComponent<Laser>();

		// Laser was fired and collides with enemy
		if(missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0)
				Destroy(gameObject);
		}
	}
}
