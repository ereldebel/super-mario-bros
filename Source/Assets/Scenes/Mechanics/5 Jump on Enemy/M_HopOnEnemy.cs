using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_HopOnEnemy : MonoBehaviour
{
	[SerializeField] private float hopForce = 100;

	private Rigidbody2D _rigidbody2D;
	private Vector2 _hop = Vector2.up;

	private void Awake()
	{
		_rigidbody2D = gameObject.transform.parent.GetComponent<Rigidbody2D>();
		_hop *= hopForce;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (!other.gameObject.CompareTag("Enemy")) return;
		_rigidbody2D.AddForce(_hop);
		Destroy(other.gameObject);
	}
}