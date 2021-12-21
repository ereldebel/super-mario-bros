using System;
using UnityEngine;

namespace Scenes.Game.Scripts
{
    public static class CollisionDirection
    {
        /// <param name="normal">The normal of a collision.</param>
        /// <returns>True if the collision came from above.</returns>
        public static bool CollidedFromAbove(Vector2 normal) => -normal.y > Math.Abs(normal.x);
        
        /// <param name="normal">The normal of a collision.</param>
        /// <returns>True if the collision came from below.</returns>
        public static bool CollidedFromBelow(Vector2 normal) => normal.y > Math.Abs(normal.x);
        
        
        /// <param name="normal">The normal of a collision.</param>
        /// <returns>True if the collision came from left or right.</returns>
        public static bool CollidedFromSide(Vector2 normal) => Math.Abs(normal.x) > Math.Abs(normal.y);
    }
}
