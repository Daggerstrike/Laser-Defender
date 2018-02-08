using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// General script for lasers
public class Laser : MonoBehaviour {
	public float damage = 100f;

	public float GetDamage() {
		return damage;
	}

	public void Hit() {
		Destroy(gameObject);
	}
}
