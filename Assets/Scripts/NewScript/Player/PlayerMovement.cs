using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace NewScript.Player
{
    public class PlayerMovement : IMovement
    {
        private Vector2 _position;
        public Vector2 Direction { get; set; }
        public void CalculateDirection(Vector3 rotationZ)
        {
            var z = rotationZ.Z;

            Direction = new Vector2(1 * (float)Math.Cos(Math.PI / 180 * z), 1 * (float) Math.Sin(Math.PI / 180 * z));
            Debug.Log("Direction: " + Direction);
        }

        public event Action<Vector2> PositionChanged;

        private readonly float _speed;

        public PlayerMovement(Vector2 startPosition, Vector2 direction, float speed)
        {
            _position = startPosition;
            Direction = direction;
            _speed = speed;
        }
        
        public void Move(float acceleration)
        {
            _position += (Direction * acceleration * (_speed * 0.02f));
            
            PositionChanged?.Invoke(_position);
        }
    }
}