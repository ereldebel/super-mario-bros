using UnityEngine;

namespace Scenes.Game.Scripts.Mario.Fireballs
{
	public class ExplodeOnCollision : MonoBehaviour
	{
		/// <summary>
		/// Jumps on ground. Destroys self on collision with other. Hits hittable objects. 
		/// </summary>
		/// <param name="other"></param>
		private void OnCollisionEnter2D(Collision2D other)
		{
			var otherGameObject = other.gameObject;
			if (otherGameObject.CompareTag(GameManager.GroundTag)) return;
			otherGameObject.gameObject.GetComponent<IHittable>()?.TakeHit(other);
			Destroy(gameObject);
		}

		private void OnBecameInvisible() => Destroy(gameObject);
	}
}