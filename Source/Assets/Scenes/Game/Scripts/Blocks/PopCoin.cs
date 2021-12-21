using UnityEngine;

namespace Scenes.Game.Scripts.Blocks
{
	[RequireComponent(typeof(Animator))]
	public class PopCoin : MonoBehaviour, ISpecialBlockStrategy
	{
		#region Serialized fields

		[SerializeField] private GameObject coinPrefab;

		#endregion

		#region Public methods

		/// <summary>
		/// Pops a coin and adds it to the counter.
		/// </summary>
		public void CollisionBehaviour()
		{
			Destroy(Instantiate(coinPrefab, transform), 1);
			GameManager.AddCoin();
			AudioManager.Coin();
		}

		/// <summary>
		/// Does nothing.
		/// </summary>
		/// <param name="isGiant"></param>
		public void BlockActivated(bool isGiant)
		{
		}

		#endregion
	}
}