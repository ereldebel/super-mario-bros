using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class M_MoveAndTurnOnCollision : MonoBehaviour
{
	[SerializeField] private float speed = 10;

	private Vector2 _currentMovement = Vector2.right;
	private Rigidbody2D _rigidbody2D;
	private bool _useVelocity;


	private void Awake()
	{
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		_currentMovement *= speed;
	}

	private void FixedUpdate()
	{
		if (_useVelocity)
			_rigidbody2D.velocity = _currentMovement;
		else
			_rigidbody2D.AddForce(_currentMovement);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
			_useVelocity = true;
		if (other.gameObject.CompareTag("Wall"))
			_currentMovement = -_currentMovement;
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
			_useVelocity = false;
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}