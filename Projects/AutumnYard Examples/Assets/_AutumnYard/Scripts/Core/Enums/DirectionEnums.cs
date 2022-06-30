using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard
{
    public enum SideHorizontal { Left, Right }

    public enum DirectionFourAxis { Left, Right, Up, Down }

    public enum SideVertical { Bottom, Top }


    public static class DirectionEnumsExtensions
    {
        public static DirectionFourAxis Opposite(this DirectionFourAxis direction)
        {
            return (int)direction < 2 ? direction + 2 : direction - 2;
        }

        public static Vector2 Vector(this DirectionFourAxis direction)
        {
            switch (direction)
            {
                case DirectionFourAxis.Left: return Vector2.left;
                case DirectionFourAxis.Right: return Vector2.right;
                case DirectionFourAxis.Up: return Vector2.up;
                default:
                case DirectionFourAxis.Down: return Vector2.down;
            }
        }
    }

}