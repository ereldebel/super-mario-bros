using UnityEngine;

namespace Scenes.Game.Scripts.NPC.Enemies
{
	/// <summary>
	/// A Koopa Troopa (The turtle enemy) enemy component.
	/// In charge of it's movement, Mario hits and self destruction if out of frame.
	/// </summary>
	public class KoopaTroopa : Goomba
	{
		[SerializeField] private GameObject shellPrefab;

		/// <summary>
		/// Takes a hit. Dies and instantiates it's shell in it's place.
		/// </summary>
		/// <param name="other">Collision.</param>
		/// <returns>True if died. always true.</returns>
		public override bool TakeHit(Collision2D other)
		{
			Instantiate(shellPrefab, transform.position - new Vector3(0, 0.281f, 0), Quaternion.identity);
			Destroy(gameObject);
			return true;
		}
	}
}