using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Game.Scripts
{
	public class GameManager : MonoBehaviour
	{
		#region Serialized fields

		[SerializeField] private TextMeshProUGUI coinsUI;
		[SerializeField] private TextMeshProUGUI pointsUI;
		[SerializeField] private TextMeshProUGUI timeUI;

		#endregion

		#region Constants

		public const string GroundTag = "Ground";
		public const string EnemyTag = "Enemy";
		public const string MarioTag = "Mario";
		private const int MaxGameTime = 400;
		private const int GameResetDelay = 3;

		#endregion

		#region Private fields

		private static GameManager _shared;
		private Transform _cameraTransform;

		private static Vector3 _lastPos = Vector3.zero;
		private int _coins = 0;
		private int _points = 0;
		private int _timePassed = 0;

		#endregion

		#region Public static properties

		public static Vector3 CameraPosition
		{
			get
			{
				if (_lastPos != Vector3.zero)
					return _lastPos;
				return _shared._cameraTransform == null
					? _shared.transform.position
					: _shared._cameraTransform.position;
			}
		}

		#endregion

		#region Private static properties

		private static int Coins
		{
			get => _shared._coins;
			set
			{
				_shared._coins = value;
				_shared.coinsUI.text = value > 9 ? $" x{value}" : $" x0{value}";
			}
		}

		private static int Points
		{
			get => _shared._points;
			set
			{
				_shared._points = value;
				var stringValue = value.ToString();
				var pointsPrefix = "00000".Substring(stringValue.Length);
				_shared.pointsUI.text = pointsPrefix + stringValue;
			}
		}

		private static int TimePassed
		{
			get => _shared._timePassed;
			set
			{
				_shared._timePassed = value;
				_shared.timeUI.text = (MaxGameTime - value).ToString();
			}
		}

		#endregion

		#region Function events

		private void Awake() => InitFields();

		private void Update() => UpdateGameTime();

		private void OnDestroy() => _lastPos = transform.position;

		#endregion

		#region Public static methods

		/// <summary>
		/// Resets the game.
		/// </summary>
		public static void ResetGame() => _shared.ResetAfterDelay(GameResetDelay);

		/// <summary>
		/// Resets the game with the requested delay.
		/// </summary>
		/// <param name="delay">Requested delay in seconds.</param>
		public static void ResetGame(float delay) => _shared.ResetAfterDelay(delay);

		/// <summary>
		/// Register a PowerUp collection.
		/// </summary>
		public static void PowerUpCollected() => Points += 1000;

		/// <summary>
		/// Register a coin collection.
		/// </summary>
		public static void AddCoin()
		{
			++Coins;
			Points += 200;
		}

		/// <summary>
		/// Register an enemy kill.
		/// </summary>
		/// <param name="streak">The current kill streak.</param>
		public static void EnemyKilled(int streak) => Points += 100 * (int) Math.Pow(2, streak);

		/// <summary>
		/// Register a flagpole slide
		/// </summary>
		/// <param name="height">The starting height of the slide.</param>
		public static void FlagPoleSlide(float height) => Points += 100 * (1 + (int) height);

		#endregion

		#region Private methods and coroutines

		/// <summary>
		/// Resets the game with a delay.
		/// </summary>
		/// <param name="delay">Delay in seconds.</param>
		private void ResetAfterDelay(float delay) => StartCoroutine(ResetAfterDelayCoroutine(delay));

		/// <summary>
		/// Initializes the class fields.
		/// </summary>
		private void InitFields()
		{
			_shared = this;
			var cam = Camera.main;
			_cameraTransform = cam == null ? null : cam.transform;
		}

		/// <summary>
		/// Updates the game time and resets game at time end.
		/// </summary>
		private static void UpdateGameTime()
		{
			if (Time.time >= TimePassed)
				TimePassed = (int) Time.time;
			if (TimePassed == MaxGameTime)
				ResetGame();
		}

		#endregion

		#region Coroutines

		private static IEnumerator ResetAfterDelayCoroutine(float time)
		{
			yield return new WaitForSeconds(time);
			SceneManager.LoadSceneAsync(0);
		}

		#endregion
	}
}