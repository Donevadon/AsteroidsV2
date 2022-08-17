using GameLibrary;
using System;
using Entities;

public class Debris : Asteroid
{
    public override event Action<IEntity> EntityDeaded;
    public override float RadiusCollider => 0.117f;
    public override Entity Type => Entity.Debris;
    private void Start()
    {
        Movement.Acceleration = 1;
    }

    public override void Dead()
    {
        EntityDeaded(this);
        Destroy();
    }
}

