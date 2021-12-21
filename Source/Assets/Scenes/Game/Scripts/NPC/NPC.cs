using System;
using UnityEngine;
using static Scenes.Game.Scripts.CollisionDirection;

namespace Scenes.Game.Scripts.NPC
{
	/// <summary>
	/// An NPC component.
	/// In charge of it's movement and self destruction if out of frame.
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Collider2D))]
	public class NPC : MonoBehaviour, IHittable
	{
		#region Serialized fields

		[SerializeField] public Direction initialWalkingDirection = Direction.Right;
		[SerializeField] private float speed = 10;

		#endregion

		#region Protected and Private Fields and properties

		protected Rigidbody2D Rigidbody2D;
		protected SpriteRenderer SpriteRenderer;

		private float _currentMovement = 1;

		protected float CurrentMovement
		{
			set
			{
				if (_currentMovement * value < 0)
					SpriteRenderer.flipX = !SpriteRenderer.flipX;
				_currentMovement = value;
			}
			get => _currentMovement;
		}

		#endregion

		#region Function methods

		protected virtual void Awake() => InitFields();

		private void OnCollisionEnter2D(Collision2D other) => CollisionBehaviour(other);

		private void FixedUpdate()
		{
			var velocity = Rigidbody2D.velocity;
			velocity.x = _currentMovement;
			Rigidbody2D.velocity = velocity;
		}

		private void OnBecameInvisible() => DestroySelfIfNeeded();

		#endregion

		#region Public and Protected methods

		/// <summary>
		/// Changes the movement direction away from the collision.
		/// </summary>
		/// <param name="other">Collision</param>
		/// <returns>False.</returns>
		public virtual bool TakeHit(Collision2D other)
		{
			var velocity = Math.Abs(CurrentMovement);
			CurrentMovement = other.GetContact(0).normal.x > 0 ? -velocity : velocity;
			return false;
		}

		/// <summary>
		/// The behaviour of this enemy type on collision enter.
		///
		/// If collided with mario and not from above, hit him.
		/// If collided with something other than mario, turn around.
		/// </summary>
		/// <param name="other">object collided with.</param>
		protected virtual void CollisionBehaviour(Collision2D other)
		{
			var normal = other.GetContact(0).normal;
			if (CollidedFromSide(normal) && normal.x * CurrentMovement < 0)
				CurrentMovement = -CurrentMovement;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// If exited the camera frame form the left or from the bottom - destroys self.
		/// </summary>
		private void DestroySelfIfNeeded()
		{
			if (transform.position.x < GameManager.CameraPosition.x ||
			    transform.position.y < -5)
				Destroy(gameObject);
		}

		/// <summary>
		/// Initializes the instance fields.
		/// </summary>
		private void InitFields()
		{
			Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
			SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			_currentMovement *= speed;
			if (initialWalkingDirection == Direction.Left)
				CurrentMovement = -CurrentMovement;
		}

		#endregion
	}
}