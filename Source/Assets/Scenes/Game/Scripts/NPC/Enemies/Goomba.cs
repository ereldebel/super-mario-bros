using UnityEngine;
using static Scenes.Game.Scripts.CollisionDirection;

namespace Scenes.Game.Scripts.NPC.Enemies
{
	/// <summary>
	/// A basic enemy (goomba) component.
	/// In charge of it's movement, Mario hits and self destruction if out of frame.
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Animator))]
	public class Goomba : NPC
	{
		#region Private methods

		private Collider2D _collider2D;
		private Animator _animator;
		private static readonly int Squashed = Animator.StringToHash("Squashed");

		#endregion

		#region Function Events

		protected override void Awake()
		{
			base.Awake();
			_collider2D = GetComponent<Collider2D>();
			_animator = GetComponent<Animator>();
		}

		#endregion

		#region Public and Protected methods

		/// <summary>
		/// Cause this enemy damage.
		/// </summary>
		/// <param name="other">Collision with damage giver.</param>
		/// <returns>true if killed. false otherwise.</returns>
		public override bool TakeHit(Collision2D other)
		{
			if (CollidedFromBelow(other.GetContact(0).normal))
			{
				_animator.SetTrigger(Squashed);
				Destroy(gameObject, 1);
				Destroy(this);
				Destroy(Rigidbody2D);
			}
			else
			{
				SpriteRenderer.flipY = true;
				Rigidbody2D.velocity = new Vector2(0, 4);
			}

			_collider2D.enabled = false;
			return true;
		}

		/// <summary>
		/// The behaviour of this enemy type on collision enter.
		///
		/// If collided with mario and not from above, hit him.
		/// If collided with something other than mario, turn around.
		/// </summary>
		/// <param name="other">object collided with.</param>
		protected override void CollisionBehaviour(Collision2D other)
		{
			if (other.gameObject.CompareTag(GameManager.MarioTag))
			{
				if (!CollidedFromAbove(other.GetContact(0).normal))
					other.gameObject.GetComponent<IHittable>().TakeHit(other);
				return;
			}

			base.CollisionBehaviour(other);
		}

		#endregion
	}
}