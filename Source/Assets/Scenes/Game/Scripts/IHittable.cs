using UnityEngine;

namespace Scenes.Game.Scripts
{
	/// <summary>
	/// Interface of a component that can receive a hit for it's GameObject. 
	/// </summary>
	public interface IHittable
	{
		/// <summary>
		/// Cause the GameObject damage.
		/// </summary>
		/// <param name="other">Collision with damage giver.</param>
		bool TakeHit(Collision2D other);
	}
}