using System;
using GameLibrary;
using GameLibrary.EntityLibrary;
using UnityEngine;

namespace Entities
{
    public class Asteroid : GameEntity
    {
        [SerializeField] private float _speed;
        protected ObjectMovement Movement { get; set; }
        protected ObjectRotation RotationObject { get; set; }
        public override Entity Type => Entity.Asteroid;

        public override System.Numerics.Vector3 Position
        {
            get => Movement.Position;
            set => Movement.SetPosition(value);
        }

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

        public override float RadiusCollider => 0.35f;

        public event Action Object_Move;
        public override event Action<IEntity> EntityDeaded;

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
            Movement = new ObjectMovement(ref Object_Move);
            Movement.Speed = _speed;
            Movement.Acceleration = 1;
        }

        private void CreateDebris()
        {
            IEntity debris;
            for (int i = 0; i < UnityEngine.Random.Range(2, 5); i++)
            {
                debris = GameSystem.GetInstance().Spawner
                    .SpawnEntity(Entity.Debris, Position, Visualization.GetRandomEuler().Parse(), null);
                debris.EntityDeaded += EntityDeaded;
            }
        }

        public override void Dead()
        {
            GameSystem.Context.Send((x) => CreateDebris(), null);
            EntityDeaded(this);
            Destroy();
        }

        public override void UpdateData()
        {
            Object_Move();
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
                case IGameEntity {Type: Entity.Player} gameEntity:
                    gameEntity.Dead();
                    break;
            }
        }
    }
}