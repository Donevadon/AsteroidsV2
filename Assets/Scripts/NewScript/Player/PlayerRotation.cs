using System;
using System.Numerics;

namespace NewScript.Player
{
    public class PlayerRotation : IRotate
    {
        private Vector3 _rotation;
        private readonly Vector3 _direction;
        private readonly float _speed;

        public PlayerRotation(Vector3 startRotation, Vector3 direction, float speed)
        {
            _rotation = startRotation;
            _direction = direction;
            _speed = speed;
        }
        public void Rotate(float acceleration)
        {
            _rotation += _direction * acceleration * (_speed * 0.02f);

            if (_rotation.Z > 180)
            {
                var a =_rotation.Z - 180;
                _rotation.Z = -180 + a;
            }
            else if (_rotation.Z < -180)
            {
                var a = -180 - _rotation.Z;
                _rotation.Z = 180 - a;
            }
            
            RotationChanged?.Invoke(_rotation);
        }

        public event Action<Vector3> RotationChanged;
    }
}