using UnityEngine;

namespace Scenes.Game.Scripts.NPC
{
	/// <summary>
	/// An abstract class with a single method which enables each of the given components.
	/// </summary>
	public abstract class EnableScriptsWithTrigger : MonoBehaviour
	{
		[SerializeField] protected MonoBehaviour[] scripts;

		/// <summary>
		/// Enables each of the given components.
		/// </summary>
		protected void EnableScriptsAndDestroySelf()
		{
			foreach (var component in scripts)
				component.enabled = true;
			Destroy(this);
		}
	}
}