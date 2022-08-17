using System;
using System.Numerics;
using System.Threading;

namespace GameLibrary.EntityLibrary
{
    public class ObjectMovement : ObjectScene
    {
        private float _acceleration;

        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; set; }
        public float Speed { get; set; }
        /// <summary>
        /// Задать ускорение от 0 до 1
        /// </summary>
        public float? Acceleration
        { 
            set 
            {
                if (value.HasValue)
                {
                    if (value.Value <= 0) _acceleration = 0;
                    else if (value.Value >= 1) _acceleration = 1;
                    else _acceleration = value.Value;
                }
                else throw new Exception("Не допустимо значение Null в ускорении корабля");
            } 
        }

        public event Action<Vector3> Position_Updated;
        public event Action<Vector3> Direction_Updated;

        public ObjectMovement(Vector3 direction,ref Action Object_Move)
        {
            Direction = direction;
            Object_Move += Move;
        }
        public ObjectMovement() { }
        public ObjectMovement(ref Action Object_Move)
        {
            Object_Move += Move;
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
            InvokeUpdate();
            
        }
        private void Move()
        {
            Position = CheckBorder(Position);
            Position += (Direction * _acceleration * (Speed * 0.02f));
            InvokeUpdate();
            Direction_Updated?.Invoke(Direction);
        }

        private void InvokeUpdate() 
        {
            
            Position_Updated?.Invoke(Position);
        }
    }
}