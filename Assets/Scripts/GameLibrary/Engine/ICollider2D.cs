using System.Collections.Generic;
using System.Numerics;

namespace GameLibrary.Engine
{
    interface ICollider2D
    {
        void CheckCollider(IEntity entity,params IEntity[] entities);
        void CheckRaycast(Vector3[] points, float size, List<IEntity> entities);
    }
}
