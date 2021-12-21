using System;
using System.Collections;
using Scenes.Game.Scripts.Mario.Fireballs;
using Scenes.Game.Scripts.NPC.Power_Up_Entities;
using UnityEngine;
using static Scenes.Game.Scripts.PowerUp;

namespace Scenes.Game.Scripts.Mario
{
	/// <summary>
	/// The main Mario component. Manages his hits and power ups.
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(ThrowFireballs))]
	public class MarioManager : MonoBehaviour, IHittable
	{
		#region Serialized fields

		[SerializeField] public GameObject giant;
		[SerializeField] public GameObject fireball;
		[SerializeField] public GameObject immuneChild;
		[SerializeField] private InvincibilityAnimation invincibilityAnimation;
		[SerializeField] private float rayLength = 0.34836f;
		[SerializeField] private float rayWidth = 0.27f;

		#endregion

		#region Private fields

		private SpriteRenderer _spriteRenderer;
		private Rigidbody2D _rigidbody2D;
		private Collider2D _collider2d;
		private Animator _animator;
		private ThrowFireballs _fireballComponent;

		private bool _isGiant = false;
		private bool _invincible = false;
		private int _streak = 0;
		private int _livesLeft = 0;
		private int _layerMask;
		private Vector2 _hitJumpVector = new Vector2(0, 10);
		private const int SlipVelocityThreshold = 2;

		private const int StandAnimation = 0;
		private const int WalkAnimation = 1;
		private const int JumpAnimation = 2;

		private static readonly int State = Animator.StringToHash("State");
		private static readonly int Death = Animator.StringToHash("Death");
		private static readonly int Slip = Animator.StringToHash("Slip");
		private static readonly int GiantTransition = Animator.StringToHash("Giant Transition");

		#endregion

		#region Public properties

		public bool IsInvincible
		{
			get => _invincible;
			private set
			{
				_invincible = value;
				invincibilityAnimation.enabled = _invincible;
				if (_invincible)
					StartCoroutine(TimeInvincibility());
			}
		}

		public bool Grounded { get; private set; } = true;

		public bool IsGiant
		{
			get => _isGiant;
			private set
			{
				if (value)
					_isGiant = true;
				else if (_isGiant != value)
					ApplyPowerUp(None);
			}
		}

		#endregion

		#region Function events

		private void Awake() => InitFields();


		private void OnTriggerEnter2D(Collider2D other)
		{
			EquipPowerUp(other.gameObject.GetComponent<PowerUpIdentifier>());
		}

		private void Update()
		{
			var velocity = _rigidbody2D.velocity;
			CheckGrounded();
			if (!Grounded)
			{
				_animator.SetInteger(State, JumpAnimation);
				return;
			}

			if (velocity.x == 0)
			{
				_animator.SetInteger(State, StandAnimation);
				return;
			}

			_animator.SetInteger(State, WalkAnimation);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Cause Mario damage.
		/// </summary>
		/// <param name="other">Collision with damage giver.</param>
		public bool TakeHit(Collision2D other)
		{
			if (IsInvincible)
				return false;
			if (IsGiant)
			{
				AudioManager.TakeHit();
				IsGiant = false;
				StartCoroutine(TimedImmunity());
				return false;
			}

			GetComponent<Collider2D>().enabled = false;
			AudioManager.Death();
			_animator.SetTrigger(Death);
			_rigidbody2D.velocity = _hitJumpVector;
			GameManager.ResetGame();
			return true;
		}

		/// <summary>
		/// Plays slip animation if velocity is above threshold
		/// </summary>
		public void CheckForSlip()
		{
			if (Math.Abs(_rigidbody2D.velocity.x) > SlipVelocityThreshold)
				_animator.SetTrigger(Slip);
		}

		/// <summary>
		/// Registers enemy kill at game manager.
		/// </summary>
		public void EnemyKilled() => GameManager.EnemyKilled(_streak++);

		#endregion

		#region Coroutines

		private IEnumerator TimedImmunity()
		{
			immuneChild.SetActive(true);
			_collider2d.enabled = false;
			for (int i = 0; i < 10; ++i)
			{
				_spriteRenderer.enabled = false;
				yield return new WaitForSeconds(0.1f);
				_spriteRenderer.enabled = true;
				yield return new WaitForSeconds(0.1f);
			}

			immuneChild.SetActive(false);
			_collider2d.enabled = true;
		}

		private IEnumerator TimeInvincibility()
		{
			yield return new WaitForSeconds(15);
			IsInvincible = false;
		}

		private IEnumerator TransformationPositionFix(Action transformationEndAction)
		{
			_rigidbody2D.bodyType = RigidbodyType2D.Static;
			yield return new WaitForSeconds(15 / 12f);
			_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
			transformationEndAction();
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Updates the value of the Grounded property by checking Ground object below character with raycasts.
		/// </summary>
		private void CheckGrounded()
		{
			var offset = Vector3.right * rayWidth;
			var pos = transform.position;
			Grounded = Physics2D.Raycast(pos + offset, Vector2.down, rayLength, _layerMask).collider != null ||
			           Physics2D.Raycast(pos - offset, Vector2.down, rayLength, _layerMask).collider != null;
			if (Grounded)
				_streak = 0;
		}


		/// <summary>
		/// Initializes the class fields.
		/// </summary>
		private void InitFields()
		{
			_layerMask = LayerMask.GetMask("Ground");
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_collider2d = GetComponent<Collider2D>();
			_animator = GetComponent<Animator>();
			_fireballComponent = GetComponent<ThrowFireballs>();
		}

		/// <summary>
		/// Equips The collected PowerUp
		/// </summary>
		/// <param name="identifier"> identifier of the collected PowerUp.</param>
		private void EquipPowerUp(PowerUpIdentifier identifier)
		{
			if (identifier == null) return;
			GameManager.PowerUpCollected();
			ApplyPowerUp(identifier.PowerUp);
			identifier.Dispose();
		}

		/// <summary>
		/// Applies the current PowerUps' effects.
		/// </summary>
		/// <param name="powerUp">The current max power up.</param>
		private void ApplyPowerUp(PowerUp powerUp)
		{
			switch (powerUp)
			{
				case None:
					RegularMario(true);
					FireballMario(false);
					giant.SetActive(false);
					_isGiant = false;
					break;
				case Fireball:
					FireballMario(true);
					giant.SetActive(false);
					invincibilityAnimation.enabled = true;
					StartCoroutine(TransformationPositionFix(() => invincibilityAnimation.enabled = false));
					break;
				case Giant:
					_animator.SetTrigger(GiantTransition);
					AudioManager.CollectPowerUp(false);
					giant.SetActive(true);
					IsGiant = true;
					StartCoroutine(TransformationPositionFix(() => RegularMario(false)));
					break;
				case Invincible:
					AudioManager.Invincibility();
					IsInvincible = true;
					break;
				case OneUp:
					AudioManager.CollectPowerUp(true);
					++_livesLeft;
					break;
			}
		}

		private void RegularMario(bool flag)
		{
			_spriteRenderer.enabled = flag;
			_collider2d.enabled = flag;
		}

		private void FireballMario(bool flag)
		{
			AudioManager.CollectPowerUp(false);
			fireball.SetActive(flag);
			_fireballComponent.enabled = flag;
		}

		#endregion
	}
}