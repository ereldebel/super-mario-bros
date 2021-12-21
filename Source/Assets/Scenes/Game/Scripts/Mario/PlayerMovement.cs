using System;
using UnityEngine;

namespace Scenes.Game.Scripts.Mario
{
	/// <summary>
	/// The component in charge of Mario's movement using the player input.
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(SpriteRenderer))]
	public class PlayerMovement : MonoBehaviour
	{
		private const float RegularGravity = 2f;
		private const float FallGravity = 4.5f;

		#region Serialized fields

		[SerializeField] private KeyCode rightKey = KeyCode.RightArrow;
		[SerializeField] private KeyCode leftKey = KeyCode.LeftArrow;
		[SerializeField] private KeyCode jumpKey = KeyCode.Space;
		[SerializeField] private float speed = 10;
		[SerializeField] private float jumpForce = 100;

		#endregion

		#region Private fields

		private Rigidbody2D _rigidbody2D;
		private SpriteRenderer _spriteRenderer, _giantSpriteRenderer, _fireballSpriteRenderer;
		private MarioManager _marioManager;

		private Vector2 _jumpVector = Vector2.up;
		private Vector2 _rightVector = Vector2.right;
		private Vector2 _leftVector = Vector2.left;

		private Direction _direction = Direction.Right;
		private Vector2 _currentMovement = Vector2.zero;
		private float _jumpStartTime;

		#endregion

		#region Public properties

		public Direction Direction
		{
			get => _direction;
			private set
			{
				if (_direction != value)
				{
					var newDir = !_spriteRenderer.flipX;
					_spriteRenderer.flipX = newDir;
					_giantSpriteRenderer.flipX = newDir;
					_fireballSpriteRenderer.flipX = newDir;
					_marioManager.CheckForSlip();
				}

				_direction = value;
			}
		}

		#endregion

		#region Function events

		private void Awake() => InitFields();

		private void Update()
		{
			_currentMovement = Vector2.zero;
			WalkByArrowInput();
			JumpOnKeyPress();
		}

		private void FixedUpdate()
		{
			_rigidbody2D.AddForce(_currentMovement);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Initializes the instance fields.
		/// </summary>
		private void InitFields()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_marioManager = GetComponent<MarioManager>();
			_giantSpriteRenderer = _marioManager.giant.GetComponent<SpriteRenderer>();
			_fireballSpriteRenderer = _marioManager.fireball.GetComponent<SpriteRenderer>();
			_rightVector *= speed;
			_leftVector *= speed;
			_jumpVector *= jumpForce;
		}

		/// <summary>
		/// Sets the walking direction of Mario by the input of the arrow keys.
		/// </summary>
		private void WalkByArrowInput()
		{
			if (Input.GetKey(leftKey))
				_currentMovement += _leftVector;
			if (Input.GetKey(rightKey))
				_currentMovement += _rightVector;
			if (_currentMovement == Vector2.zero) return;
			Direction = _currentMovement.normalized.x > 0 ? Direction.Right : Direction.Left;
		}

		/// <summary>
		/// Makes Mario jump by the input of the space bar. 
		/// </summary>
		private void JumpOnKeyPress()
		{
			var y = _rigidbody2D.velocity.y;
			if (Input.GetKeyDown(jumpKey) && _marioManager.Grounded)
			{
				AudioManager.Jump(_marioManager.IsGiant);
				_rigidbody2D.AddForce(_jumpVector, ForceMode2D.Impulse);
			}

			_rigidbody2D.gravityScale =
				!_marioManager.Grounded && (y < 0 || y > 0 && !Input.GetKey(jumpKey)) ? FallGravity : RegularGravity;
		}

		#endregion
	}
}