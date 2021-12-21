using Scenes.Game.Scripts.Mario;
using UnityEngine;
using static Scenes.Game.Scripts.CollisionDirection;

namespace Scenes.Game.Scripts.Blocks
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(ISpecialBlockStrategy))]
	public class SpecialBlock : BlockHit
	{
		#region Serialized fields

		[SerializeField] private int numberOfUses = 1;

		#endregion

		#region Public fields

		private ISpecialBlockStrategy _strategy;
		private bool _isGiant;
		private static readonly int BlockHit = Animator.StringToHash("Block Hit");
		private static readonly int Used = Animator.StringToHash("Used");

		#endregion


		#region Function events

		protected override void Awake()
		{
			base.Awake();
			_strategy = GetComponent<ISpecialBlockStrategy>();
		}

		protected override void OnCollisionEnter2D(Collision2D other)
		{
			CheckHit(other);
			if (numberOfUses <= 0) return;
			if (!CollidedFromBelow(other.GetContact(0).normal)) return;
			BlockHitFromBelow(other.gameObject);
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Checks if hit mario and if he's a giant. Initiates bump animation and calls collision behaviour.  
		/// </summary>
		/// <param name="other">Object Collided with.</param>
		private void BlockHitFromBelow(GameObject other)
		{
			Animator.SetTrigger(BlockHit);
			var marioManager = other.GetComponent<MarioManager>();
			if (marioManager == null)
				return;
			_isGiant = marioManager.IsGiant;
			_strategy.CollisionBehaviour();
		}

		/// <summary>
		/// Calls the post collision actions. Called by animation event.
		/// </summary>
		private void PostHitBehaviour()
		{
			if (numberOfUses <= 0) return;
			--numberOfUses;
			_strategy.BlockActivated(_isGiant);
			if (numberOfUses == 0)
				Animator.SetBool(Used, true);
		}

		#endregion
	}
}