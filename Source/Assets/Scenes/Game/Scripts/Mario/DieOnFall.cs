using UnityEngine;

namespace Scenes.Game.Scripts.Mario
{
	public class DieOnFall: MonoBehaviour
	{
		private void OnBecameInvisible()
		{
			if (transform.position.y < -5)
				GameManager.ResetGame();
		}
	}
}