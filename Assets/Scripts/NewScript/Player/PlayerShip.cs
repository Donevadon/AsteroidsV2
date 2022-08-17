using System;
using GameObject = NewScript.Core.GameObject;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace NewScript.Player
{
    public class PlayerShip : GameObject
    {
        private readonly IPlayerController _controller;
        private readonly IMovement _movement = new PlayerMovement(Vector2.Zero, Vector2.UnitY, 0.1f);
        private readonly IRotate _rotation = new PlayerRotation(Vector3.Zero, Vector3.UnitZ, 5);
        
        public event Action<Vector2> PositionChanged
        {
            add => _movement.PositionChanged += value;
            remove => _movement.PositionChanged -= value;
        }
        public event Action<Vector3> RotationChanged
        {
            add => _rotation.RotationChanged += value;
            remove => _rotation.RotationChanged -= value;
        }

        public PlayerShip(IPlayerController controller)
        {
            _controller = controller;

            _controller.Move += _movement.Move;
            _controller.Rotate += _rotation.Rotate;
            _rotation.RotationChanged += _movement.CalculateDirection;
        }

        public override void Update()
        {
        }
    }

    internal interface IRotate
    {
        void Rotate(float acceleration);
        public event Action<Vector3> RotationChanged;
    }

    internal interface IMovement
    {
        void Move(float acceleration);
        void CalculateDirection(Vector3 rotationZ);
        public event Action<Vector2> PositionChanged;
    }
}