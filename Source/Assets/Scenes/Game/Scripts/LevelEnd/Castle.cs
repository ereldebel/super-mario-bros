using UnityEngine;

namespace Scenes.Game.Scripts.LevelEnd
{
	public class Castle : MonoBehaviour
	{
		private Animator _animator;
		private static readonly int Property = Animator.StringToHash("Raise Flag");

		private void Awake() => _animator = GetComponent<Animator>();

		private void OnCollisionEnter2D(Collision2D collision)
		{
			_animator.SetTrigger(Property);
			collision.gameObject.SetActive(false);
			GameManager.ResetGame(5);
		}
	}
}