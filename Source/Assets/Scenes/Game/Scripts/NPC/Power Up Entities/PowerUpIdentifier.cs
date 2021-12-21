using UnityEngine;

namespace Scenes.Game.Scripts.NPC.Power_Up_Entities
{
	/// <summary>
	/// Keeps an identifier for a PowerUp game object.
	/// </summary>
	public class PowerUpIdentifier : MonoBehaviour
	{
		[SerializeField] private PowerUp powerUp;
		public PowerUp PowerUp => powerUp;
		public void Dispose() => Destroy(transform.parent.gameObject);
	}
}