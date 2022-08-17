using GameLibrary;
using GameLibrary.EntityLibrary;
using System;
using UnityEngine;

public class Alien : GameEntity
{
    [SerializeField] private float _speedMove;
    private Ship player;
    private ShipControl ShipControl { get; set; }

    public override Entity Type => Entity.Alien;

    public override System.Numerics.Vector3 Position 
    {
        get => ShipControl.Movement.Position;
        set
        {
            ShipControl.Movement.SetPosition(value);
            transform.position = ShipControl.Movement.Position.Parse();
        }
    }
    public override System.Numerics.Vector3 Rotation 
    {
        get => ShipControl.Rotation.EulerAngles;
        set
        {
            ShipControl.Rotation.SetRotation(value);
            transform.rotation = Quaternion.Euler(ShipControl.Rotation.EulerAngles.Parse());
            ShipControl.Movement.Direction = transform.TransformDirection(Visualization.GetDirection()).Parse();
        }
    }

    public override float RadiusCollider => 0.15f;

    private event Action Ship_Move;
    private event Action<System.Numerics.Vector3, bool> Ship_Rotate;
    public override event Action<IEntity> EntityDeaded;

    private void Awake()
    {
        InitialShip();
    }

    private void Start()
    {
        ShipControl.Movement.Acceleration = 1;
        player = FindObjectOfType<Ship>();
    }

    private void FixedUpdate()
    {
        transform.position = ShipControl.Movement.Position.Parse();
        transform.rotation = Quaternion.Euler(ShipControl.Rotation.EulerAngles.Parse());
        ShipControl.Movement.Direction = transform.TransformDirection(Visualization.GetDirection()).Parse();
    }

    private void Move()
    {
        Ship_Move();
    }

    private void InitialShip()
    {
        ShipControl = new ShipControl(
            transform.TransformDirection(Visualization.GetDirection()).Parse(),
            ref Ship_Move,
            Visualization.GetRotateVector().Parse(),
            ref Ship_Rotate);
        InitialMovement();
    }

    private void InitialMovement()
    {
        ShipControl.Movement.Speed = _speedMove;
    }

    private void Rotate()
    {
        Ship_Rotate(player.Position,Visualization.is3D);
    }

    public override void Dead()
    {
        EntityDeaded(this);
        Destroy();
    }

    public override void UpdateData()
    {
        Move();
        Rotate();
    }

    public override void Destroy()
    {
        GameSystem.Context.Send((x) =>
            {
                if (gameObject is null) return;
                Destroy(gameObject);
            },null);
    }

    public override void OnCollision(IEntity foundEntity)
    {
        switch (foundEntity)
        {
            case IGameEntity gameEntity when gameEntity.Type == Entity.Player:
                gameEntity.Dead();
                Dead();
                break;
        }
    }
}
