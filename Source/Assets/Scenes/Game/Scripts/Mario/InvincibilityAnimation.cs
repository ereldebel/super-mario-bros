using UnityEngine;

namespace Scenes.Game.Scripts.Mario
{
	public class InvincibilityAnimation : MonoBehaviour
	{
		#region Serialized fields

		[SerializeField] private int intervals = 4;

		#endregion

		#region Private fields

		private SpriteRenderer[] _spriteRenderers;
		private float _startTime;
		private int _color, _intervalCounter = 0;

		private static readonly Color[] Colors =
			{new Color(0, 110, 255), Color.cyan, new Color(86, 255, 0), new Color(255, 246, 0)};

		#endregion


		#region Function events

		private void Awake() => InitFields();

		private void OnEnable()
		{
			_startTime = Time.time;
			_color = 0;
		}

		private void FixedUpdate()
		{
			var localIntervals = this.intervals;
			++_intervalCounter;
			if (Time.time - _startTime > 12)
				localIntervals *= 2;
			if (_intervalCounter != localIntervals) return;
			_intervalCounter = 0;
			ChangeColor(Colors[_color]);
			_color = ++_color % Colors.Length;
		}

		private void OnDisable() => ChangeColor(Color.white);

		#endregion

		#region Private methods

		/// <summary>
		/// Initializes the class fields.
		/// </summary>
		private void InitFields()
		{
			var marioManager = GetComponent<MarioManager>();
			_spriteRenderers = new SpriteRenderer[]
			{
				GetComponent<SpriteRenderer>(),
				marioManager.giant.GetComponent<SpriteRenderer>(),
				marioManager.fireball.GetComponent<SpriteRenderer>()
			};
		}

		/// <summary>
		/// Changes the color of all sprites to the given color.
		/// </summary>
		/// <param name="color">Color to change to.</param>
		private void ChangeColor(Color color)
		{
			foreach (var spriteRenderer in _spriteRenderers)
				spriteRenderer.color = color;
		}

		#endregion
	}
}