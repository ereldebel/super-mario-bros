using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ExplodeOnCollision : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D other)
	{
		var otherGameObject = other.gameObject;
		if (otherGameObject.CompareTag("Enemy"))
			Destroy(otherGameObject);
		else if (otherGameObject.CompareTag("Ground")) return;
		Destroy(gameObject);
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
