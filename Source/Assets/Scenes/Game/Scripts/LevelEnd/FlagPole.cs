using Scenes.Game.Scripts.Mario;
using UnityEngine;

namespace Scenes.Game.Scripts.LevelEnd
{
	public class FlagPole : MonoBehaviour
	{
		[SerializeField] private Flag flag;

		private void OnCollisionEnter2D(Collision2D other)
		{
			GetComponent<Collider2D>().enabled = false;
			var cutsceneController = other.gameObject.GetComponent<CutsceneController>();
			cutsceneController.LowerFlag = () => StartCoroutine(flag.LowerFlag());
			cutsceneController.enabled = true;
			GameManager.FlagPoleSlide((other.gameObject.transform.position.y - transform.position.y + 3) * 1.5f);
		}
	}
}