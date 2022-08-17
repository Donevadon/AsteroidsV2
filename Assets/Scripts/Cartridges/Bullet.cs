using System;
using GameLibrary;
using GameLibrary.EntityLibrary;
using UnityEngine;

namespace Cartridges
{
    public class Bullet : Cartridge
    {
        [SerializeField] private float _speed;
        private ObjectMovement Movement { get; set; }
        private ObjectRotation RotationObject { get; set; }

        private event Action ObjectMove;
        public override event Action<IEntity> EntityDeaded;

        public override float RadiusCollider => 0.074f;
        public override Sprite Image { get; }

        public override Entity Type => Entity.Bullet;

        public override System.Numerics.Vector3 Position { get => Movement.Position; set => Movement.SetPosition(value); }
        public override System.Numerics.Vector3 Rotation 
        {
            get => RotationObject.EulerAngles;
            set
            {
                RotationObject.SetRotation(value);
                transform.rotation = Quaternion.Euler(RotationObject.EulerAngles.Parse());
                Movement.Direction = transform.TransformDirection(Visualization.GetDirection()).Parse();
            }
        }

        private void Awake()
        {
            Initial();
        }

        private void FixedUpdate()
        {
            transform.position = Movement.Position.Parse();
        }

        private void Initial()
        {
            RotationObject = new ObjectRotation();
            Movement = new ObjectMovement(ref ObjectMove)
            {
                Speed = _speed,
                Acceleration = 1
            };
        }
        protected override void Timer(float time)
        {
            if (deadTime >= 1)
            {
                EntityDeaded(this);
                Destroy();
            }
            else deadTime += time;
        }

        public override void UpdateData()
        {
            ObjectMove();
            Timer(0.03f);
        }

        public override void Destroy()
        {
            GameSystem.Context.Send((x) =>
            {
                if (gameObject is null) return;
                Destroy(gameObject);
            }, null);
        }

        public override void OnCollision(IEntity foundEntity)
        {
            switch (foundEntity)
            {
                case IGameEntity gameEntity when gameEntity.Type != Entity.Player:
                    gameEntity.Dead();
                    EntityDeaded(this);
                    Destroy();
                    break;
            }
        }
    }
}
