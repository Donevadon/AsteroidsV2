using GameLibrary;
using GameLibrary.EntityLibrary;
using System;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour, IGameEntity
{
    public abstract Entity Type { get; }
    public abstract System.Numerics.Vector3 Position { get; set; }
    public abstract System.Numerics.Vector3 Rotation { get; set; }

    public abstract float RadiusCollider { get; }

    public abstract event Action<IEntity> EntityDeaded;

    public abstract void Dead();

    public abstract void Destroy();

    public virtual void OnCollision(IEntity foundEntity)
    {
        
    }

    public abstract void UpdateData();
}
