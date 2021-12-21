using System.Collections;
using UnityEngine;

namespace Scenes.Game.Scripts
{
	/// <summary>
	/// Component responsible for all audio in the game.
	/// </summary>
	public class AudioManager : MonoBehaviour
	{
		#region Serialized fields

		[SerializeField] private AudioClip[] clips;

		#endregion

		#region Private fields

		private static AudioManager _shared;
		private AudioSource _audio;

		#endregion

		#region Function events

		private void Awake()
		{
			_shared = this;
			_audio = GetComponent<AudioSource>();
		}

		#endregion

		#region Public methods

		public static void Death()
		{
			_shared._audio.Stop();
			_shared._audio.PlayOneShot(_shared.clips[0]);
		}

		public static void PowerUpAppear() => _shared._audio.PlayOneShot(_shared.clips[1]);

		public static void CollectPowerUp(bool isOneUp)
		{
			if (isOneUp)
				_shared._audio.PlayOneShot(_shared.clips[3]);
			else
				_shared._audio.PlayOneShot(_shared.clips[2]);
		}

		public static void Invincibility()
		{
			_shared.StartCoroutine(StopThemeFor(_shared.clips[4].length - 3.8f));
			_shared._audio.PlayOneShot(_shared.clips[4]);
		}

		private static IEnumerator StopThemeFor(float time)
		{
			_shared._audio.Stop();
			yield return new WaitForSeconds(time);
			_shared._audio.Stop();
			_shared._audio.Play();
		}

		public static void Jump(bool isGiant)
		{
			if (isGiant)
				_shared._audio.PlayOneShot(_shared.clips[6]);
			else
				_shared._audio.PlayOneShot(_shared.clips[5]);
		}

		public static void EnemyHop() => _shared._audio.PlayOneShot(_shared.clips[7]);

		public static void BrickBump() => _shared._audio.PlayOneShot(_shared.clips[8]);

		public static void BreakBrick() => _shared._audio.PlayOneShot(_shared.clips[9]);

		public static void Fireball() => _shared._audio.PlayOneShot(_shared.clips[10]);

		public static void FlagpoleSlide() => _shared._audio.PlayOneShot(_shared.clips[11]);

		public static void StageClear()
		{
			_shared._audio.Stop();
			_shared._audio.clip = _shared.clips[12];
			_shared._audio.loop = false;
			_shared._audio.Play();
		}

		public static void Coin() => _shared._audio.PlayOneShot(_shared.clips[13]);

		public static void TakeHit() => _shared._audio.PlayOneShot(_shared.clips[14]);

		#endregion
	}
}