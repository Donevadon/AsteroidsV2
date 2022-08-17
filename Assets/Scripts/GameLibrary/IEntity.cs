using System;
using System.Numerics;

namespace GameLibrary
{
    public interface IEntity
    {
        float RadiusCollider { get; }
        Vector3 Position { get; set; }
        Vector3 Rotation { get; set; }
        Entity Type { get; }
        event Action<IEntity> EntityDeaded;
        void UpdateData();
        void Destroy();
        void OnCollision(IEntity foundEntity);
    }
}
