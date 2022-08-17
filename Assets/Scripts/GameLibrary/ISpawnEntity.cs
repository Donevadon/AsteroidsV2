using System;
using System.Numerics;

namespace GameLibrary
{
    public interface ISpawnEntity
    {
        IEntity SpawnEntity(Entity entity, Vector3 position, Vector3 euler, Action<IEntity> DeadHandler);
    }
}
