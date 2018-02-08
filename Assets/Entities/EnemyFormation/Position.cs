using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour 
{
	void OnDrawGizmos()
	{
		// Draws a sphere around the EnemyPosition object in Unity
		// This allows us to see enemy spawns even when the object is not selected in the editor
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}
