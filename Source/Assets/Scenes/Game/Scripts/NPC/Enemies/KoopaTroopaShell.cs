using System;
using UnityEngine;
using static Scenes.Game.Scripts.CollisionDirection;

namespace Scenes.Game.Scripts.NPC.Enemies
{
	/// <summary>
	/// A Koopa Troopa Armor (The turtle's Armor) component.
	/// In charge of it's movement, Mario and enemy hits and self destruction if out of frame.
	/// </summary>
	public class KoopaTroopaShell : Goomba
	{
		#region Public and protected methods

		/// <summary>
		/// Takes a hit. Changes movement direction to be away from collision.
		/// </summary>
		/// <param name="other">Collision.</param>
		/// <returns>True if died. Always false.</returns>
		public override bool TakeHit(Collision2D other)
		{
			var velocity = Math.Abs(CurrentMovement);
			CurrentMovement = other.GetContact(0).normal.x > 0 ? -velocity : velocity;
			return false;
		}

		/// <summary>
		/// Hits Hittable objects on collision. Changes direction on other side collisions.
		/// </summary>
		/// <param name="other">Collision.</param>
		protected override void CollisionBehaviour(Collision2D other)
		{
			var normal = other.GetContact(0).normal;
			if (!CollidedFromSide(other.GetContact(0).normal))
				return;
			var hittable = other.gameObject.GetComponent<IHittable>();
			if (hittable != null)
				other.gameObject.GetComponent<IHittable>().TakeHit(other);
			else
				CurrentMovement = -CurrentMovement;
		}

		#endregion
	}
}