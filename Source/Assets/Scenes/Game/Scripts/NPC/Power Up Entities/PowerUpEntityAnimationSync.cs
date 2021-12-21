using UnityEngine;

namespace Scenes.Game.Scripts.NPC.Power_Up_Entities
{
	public class PowerUpEntityAnimationSync : EnableScriptsWithTrigger
	{
		private void Awake() => scripts = new MonoBehaviour[] {transform.parent.GetComponent<NPC>()};

		/// <summary>
		/// Starts NPC behaviour. Called from animation event.
		/// </summary>
		private void StartNPCBehaviour() => EnableScriptsAndDestroySelf();
	}
}