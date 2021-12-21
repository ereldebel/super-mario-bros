using UnityEngine;

namespace Scenes.Game.Scripts.Blocks
{
	public class PopPowerUp : MonoBehaviour, ISpecialBlockStrategy
	{
		#region Serialized fields

		[SerializeField] private GameObject giantPowerUpEntity;
		[SerializeField] private GameObject fireballPowerUpEntity;
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
			var powerUp = isGiant ? fireballPowerUpEntity : giantPowerUpEntity;
			var instantiationPosition = transform.position + new Vector3(0, blockSize, 0);
			Instantiate(powerUp, instantiationPosition, Quaternion.identity, transform);
		}

		#endregion
	}
}