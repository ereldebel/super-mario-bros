using Scenes.Game.Scripts.Mario;
using UnityEngine;
using static Scenes.Game.Scripts.CollisionDirection;

namespace Scenes.Game.Scripts.Blocks
{
	public class BlockHit : MonoBehaviour
	{
		#region Private and protected fields

		[SerializeField] private GameObject debrisPrefab;
		protected Animator Animator;

		private SpriteRenderer _spriteRenderer;
		private static readonly int BlockHitTrigger = Animator.StringToHash("Block Hit");
		private float _placedYPosition;

		#endregion

		#region Function Events

		protected virtual void Awake() => InitFields();

		protected virtual void OnCollisionEnter2D(Collision2D other)
		{
			CheckHit(other);
			if (!CollidedFromBelow(other.GetContact(0).normal)) return;
			CollisionBehaviour(other);
		}

		#endregion


		#region Private and protected methods

		/// <summary>
		/// On collision with mario if he is a giant, break, else bump.
		/// </summary>
		/// <param name="other">Collision</param>
		private void CollisionBehaviour(Collision2D other)
		{
			var marioManager = other.gameObject.GetComponent<MarioManager>();
			if (marioManager == null) return;
			if (marioManager.IsGiant)
			{
				AudioManager.BreakBrick();
				CreateDebris();
				_spriteRenderer.enabled = false;
				Destroy(transform.parent.gameObject, 15f / 60f);
			}
			else
				AudioManager.BrickBump();

			Animator.SetTrigger(BlockHitTrigger);
		}

		/// <summary>
		/// Initialize the class fields.
		/// </summary>
		private void InitFields()
		{
			Animator = gameObject.GetComponent<Animator>();
			_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			_placedYPosition = transform.position.y;
		}

		/// <summary>
		/// Creates 4 debris objects all going different directions with a 2 second lifespan.
		/// </summary>
		private void CreateDebris()
		{
			for (int i = -1; i <= 1; i += 2)
			for (int j = -1; j <= 1; j += 2)
			{
				var debris = Instantiate(debrisPrefab, transform.position + new Vector3(i, j, 0) * 0.335f,
					Quaternion.identity);
				debris.GetComponent<Rigidbody2D>().velocity = new Vector2(i, i);
				Destroy(debris, 2);
				var sprite = debris.GetComponent<SpriteRenderer>();
				if (i == 1)
					sprite.flipX = true;
				if (j == 1)
					sprite.flipY = true;
			}
		}

		/// <summary>
		/// Hits object if collided from above and the block is mid-bump.
		/// </summary>
		/// <param name="other">Collision.</param>
		protected void CheckHit(Collision2D other)
		{
			if (CollidedFromAbove(other.GetContact(0).normal) && _placedYPosition < transform.position.y)
				other.gameObject.GetComponent<IHittable>()?.TakeHit(other);
		}

		#endregion
	}
}