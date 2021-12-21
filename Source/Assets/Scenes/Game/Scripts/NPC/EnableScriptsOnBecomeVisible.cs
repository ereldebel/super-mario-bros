namespace Scenes.Game.Scripts.NPC
{
	/// <summary>
	/// Enables each of the given components when they enter the camera frame.
	/// </summary>
	public class EnableScriptsOnBecomeVisible : EnableScriptsWithTrigger
	{
		private void OnBecameVisible() => EnableScriptsAndDestroySelf();
	}
}