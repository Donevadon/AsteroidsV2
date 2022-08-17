using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace GameLibrary.Engine
{
    public static class Ray
    {
        private static ICollider2D collider = new Collider();
        private static IDataUpdater dataUpdater = DataUpdater.GetInitialInstance();
        private static Thread Cast;
        public static void Raycast(IEntity callingEntity, Vector3 position,Vector3 direction,float distance,float size)
        {
            Cast = new Thread(() => StartRaycast(callingEntity,position, direction, distance, size));
            Cast.Start();
        }

        private static void StartRaycast(IEntity callingEntity, Vector3 position, Vector3 direction, float distance, float size)
        {
            Vector3 endPoint = position + (direction * distance);
            int countPoint = (int)Math.Ceiling(position.Distance(endPoint) / size);
            List<Vector3> points = new List<Vector3>();
            for (int i = 0; i < countPoint; i++)
            {
                position += (direction * size);
                points.Add(position);
            }
            collider.CheckRaycast(points.ToArray(), size, new List<IEntity>(dataUpdater.GetAllExcept(callingEntity)));
        }
    }
}
