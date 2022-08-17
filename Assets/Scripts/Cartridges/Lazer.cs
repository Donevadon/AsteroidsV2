using System;
using GameLibrary;
using GameLibrary.EntityLibrary;
using UnityEngine;

namespace Cartridges
{
    [RequireComponent(typeof(LineRenderer))]
    public class Lazer : Cartridge
    {
        [SerializeField]private float distance;
        [SerializeField] private bool is3D;
        private Vector3[] linePoints = new Vector3[2];
        private LineRenderer lineRenderer;

        public override event Action<IEntity> EntityDeaded;
        private ObjectMovement Movement { get; set; } = new ObjectMovement();
        private ObjectRotation RotationObject { get; set; } = new ObjectRotation();

        public override Sprite Image { get; }

        public override Entity Type => Entity.Lazer;

        public override System.Numerics.Vector3 Position
        {
            get => Movement.Position;
            set
            {
                Movement.SetPosition(value);
                transform.position = Movement.Position.Parse();
            }
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

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            Attack();
        }

        private void DrawLine(Vector3 vector)
        {
            linePoints[0] = transform.position;
            linePoints[1] = transform.TransformDirection(vector) * distance;
            lineRenderer.SetPositions(linePoints);
        }

        private void Attack()
        {
            DrawLine(Visualization.GetDirection());
            GameSystem.Raycast(this,Position,Movement.Direction,15,0.1f);
        }
        public override void UpdateData()
        {
            Timer(0.5f);
        }

        protected override void Timer(float time)
        {
            if (deadTime >= 1)
            {
                Destroy();
            }
            else deadTime += time;
        }

        public override void Destroy()
        {
            EntityDeaded(this);
            GameSystem.Context.Send((x) =>
            {
                if (gameObject is null) return;
                Destroy(gameObject);
            }, null);

        }
    }
}
