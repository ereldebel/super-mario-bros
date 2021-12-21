namespace Scenes.Game.Scripts.Blocks
{
	public interface ISpecialBlockStrategy
	{
		/// <summary>
		/// Actions to perform on collision.
		/// </summary>
		void CollisionBehaviour();
		
		/// <summary>
		/// Actions to perform after block bump.
		/// </summary>
		/// <param name="isGiant"> True if mario is a giant.</param>
		void BlockActivated(bool isGiant);
	}
}