using GameLibrary;
using GameLibrary.EntityLibrary;
using GameLibrary.EntityLibrary.WeaponLibrary;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Корабль игрока
/// </summary>
public class Ship : GameEntity
{
    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedRotate;
    private ShipControl ShipControl { get; set; }
    private event Action Ship_Move;
    private event Action Ship_Rotate;
    public override Entity Type { get;} = Entity.Player;
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

    public override float RadiusCollider => 0.215f;

    public override event Action<IEntity> EntityDeaded;

    private void Awake()
    {
        InitialShip();
        InitialGuns();
    }

    private void InitialGuns()
    {
        ShipControl.GunManager.ShipGuns = new Weapon[]
            {
                new WeaponComponent(
                    new CartridgeLoader(),
                    Weapons.BulletWearon,
                    0.3f),
                new WeaponComponent(
                    new CartridgeLoader(),
                    Weapons.LazerWeapon,
                    2,
                    0.2f,
                    0)
            };
    }

    private void InitialShip()
    {
        ShipControl = new ShipControl(
            transform.TransformDirection(Visualization.GetDirection()).Parse(),
            ref Ship_Move,
            Visualization.GetRotateVector().Parse(),
            ref Ship_Rotate);

        ShipControl.Movement.Speed = _speedMove;
        ShipControl.Rotation.Speed = _speedRotate;
    }

    private void FixedUpdate()
    {
        ShipControl.Movement.Acceleration = Input.GetAxis("Vertical");
        ShipControl.Rotation.Rotation = Input.GetAxis("Horizontal");
        UpdatingGraphicsEngine();
        Shoot();
    }

    private void UpdatingGraphicsEngine()
    {
        transform.position = ShipControl.Movement.Position.Parse();
        transform.rotation = Quaternion.Euler(ShipControl.Rotation.EulerAngles.Parse());
        ShipControl.Movement.Direction = transform.TransformDirection(Visualization.GetDirection()).Parse();
    }

    private void Move()
    {
        Ship_Move.Invoke();
    }
    private void Rotate()
    {
        Ship_Rotate.Invoke();
    }
    private void Shoot()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ShipControl.GunManager.Shoot(
                Weapons.BulletWearon);
        }
        if (Input.GetMouseButton(1))
        {
            ShipControl.GunManager.Shoot(
                Weapons.LazerWeapon);
        }
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
        ShipControl.GunsRechargeAndAddCartridge(0.02f);
    }

    public override void Destroy()
    {
        GameSystem.Context.Send((x) =>
        {
            if (gameObject is null) return;
            Destroy(gameObject);
        }, null);
    }
}
