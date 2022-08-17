using System;
using System.Numerics;

namespace GameLibrary.EntityLibrary
{
    public class ObjectRotation
    {
        private float _rotation;

        public Vector3 EulerAngles { get;private set; }
        private Vector3 Direction { get; set; }
        private Vector3 Position { get; set; }
        public float? Rotation
        {
            set
            {
                if (value.HasValue)
                {
                    if (value.Value <= -1) _rotation = -1;
                    else if (value.Value >= 1) _rotation = 1;
                    else _rotation = value.Value;
                }
                else throw new Exception("Не допустимо значение Null во вращении корабля");
            }
        }
        public float Speed { get; set; }

        public event Action<Vector3> Euler_Updated;
        public ObjectRotation(Vector3 direction,ref Action Object_Rotate)
        {
            Direction = direction;
            Object_Rotate += Rotate;
        }
        public ObjectRotation()
        {

        }
        public ObjectRotation(Vector3 direction, 
            ref Action<Vector3,bool> Object_Rotate,
            Action<Action<Vector3>> UpdatePosition_Delegate,
            Action<Action<Vector3>> UpdateDirection_Delegate)
        {
            Direction = direction;
            Object_Rotate += RotateForTarget;
            UpdatePosition_Delegate(UpdatePosition);
            UpdateDirection_Delegate(UpdateDirection);
        }
        private void UpdateDirection(Vector3 direction) 
        {
            Direction = direction;
        }
        private void UpdatePosition(Vector3 position) 
        {
            Position = position;
        }
        public void SetRotation(Vector3 euler)
        {
            EulerAngles = euler;
        }
        public void RotateForTarget(Vector3 target,bool is3D)
        {
            Vector3 vector = target + (Direction * 10) - Position;
            float rotationZ = (float)(Math.Atan2(vector.X, vector.Y) * (360 / (Math.PI * 2)));
            if (is3D) EulerAngles = new Vector3(-rotationZ - 90, -90, 90);
            else EulerAngles = new Vector3(0,0,-rotationZ);
            InvokeUpdateEvent();
        }
        private void Rotate()
        {
            EulerAngles += Direction * _rotation * Speed;
            InvokeUpdateEvent();
        }

        private void InvokeUpdateEvent()
        {
            Euler_Updated?.Invoke(EulerAngles);
        }
    }
}