using UnityEngine;
using static Scenes.Game.Scripts.CollisionDirection;

namespace Scenes.Game.Scripts.NPC
{
	/// <summary>
	/// Enables each of the given components when the collider collides with an object which is not the ground.
	/// </summary>
	public class EnableScriptsOnCollision : EnableScriptsWithTrigger
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (CollidedFromBelow(other.GetContact(0).normal)) return;
			EnableScriptsAndDestroySelf();
		}
	}
}