using System;
using System.Numerics;

namespace GameLibrary.Engine
{
    public static class Expansions
    {
        public static float Distance(this Vector3 vector,Vector3 point)
        {
            return (float)Math.Sqrt(Math.Pow((vector.X - point.X), 2) + Math.Pow((vector.Y - point.Y), 2));
        }

    }
}
