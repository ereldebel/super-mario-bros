using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class M_JumpOnKeyPress : MonoBehaviour
{
	[SerializeField] private KeyCode jumpKey = KeyCode.Space;
	[SerializeField] private float jumpForce = 100;
	[SerializeField] private float maxJumpDuration;

	private Rigidbody2D _rigidbody2D;
	private Vector2 _currentMovement = Vector2.zero;
	private Vector2 _jump = Vector2.up;
	private bool _grounded = true;
	private float _jumpStartTime;

	private void Awake()
	{
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		_jump *= jumpForce;
	}

	private void Update()
	{
		_currentMovement = Vector2.zero;
		if (!Input.GetKey(jumpKey)) return;
		if (_grounded)
		{
			_grounded = false;
			_jumpStartTime = Time.time;
		}
		else if (Time.time >= _jumpStartTime + maxJumpDuration)
			return;

		_currentMovement += _jump;
	}


	private void OnCollisionEnter2D(Collision2D other)
	{
		_grounded = true;
	}

	private void FixedUpdate()
	{
		_rigidbody2D.AddForce(_currentMovement);
	}
}