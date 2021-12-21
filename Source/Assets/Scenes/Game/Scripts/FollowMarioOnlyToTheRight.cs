using UnityEngine;

namespace Scenes.Game.Scripts
{
	/// <summary>
	/// A component for the camera to follow mario in a right side-scrolling manner.
	/// </summary>
	public class FollowMarioOnlyToTheRight : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D marioRigidbody2D;

		private void Update()
		{
			var pos = transform.position;
			if (marioRigidbody2D.velocity.x <= 0 || marioRigidbody2D.position.x <= pos.x) return;
			pos.x = marioRigidbody2D.position.x;
			transform.position = pos;
		}
	}
}