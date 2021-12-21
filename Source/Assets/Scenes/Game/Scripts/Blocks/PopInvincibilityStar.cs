using UnityEngine;

namespace Scenes.Game.Scripts.Blocks
{
	public class PopInvincibilityStar : MonoBehaviour, ISpecialBlockStrategy
	{
		#region Serialized fields

		[SerializeField] private GameObject invinciblePowerUpEntity;
		[SerializeField] private float blockSize = 0.67f;

		#endregion

		#region Public methods

		/// <summary>
		/// Plays PowerUp audio.
		/// </summary>
		public void CollisionBehaviour() => AudioManager.PowerUpAppear();

		/// <summary>
		/// Creates new PowerUp entity.
		/// </summary>
		/// <param name="isGiant">True if mario is a giant.</param>
		public void BlockActivated(bool isGiant)
		{
			var instantiationPosition = transform.position + new Vector3(0, blockSize, 0);
			Instantiate(invinciblePowerUpEntity, instantiationPosition, Quaternion.identity, transform);
		}

		#endregion
	}
}