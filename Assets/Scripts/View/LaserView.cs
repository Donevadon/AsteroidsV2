using System;
using CoreEngine.Core;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace View
{
    public class LaserView : MonoBehaviour
    {
        private LineRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();
        }

        public void Init(IObject laser, float angle, Vector2 size)
        {
            laser.Destroyed += DisableLaser;
            var direction = GetDirection(angle);
            EnableLaser(new Vector3(laser.Position.X, laser.Position.Y), direction, size);
        }

        private void DisableLaser(IObject obj)
        {
            _renderer.positionCount = 0;
            obj.Destroyed -= DisableLaser;
        }

        private void EnableLaser(Vector3 position, Vector3 direction, Vector2 size)
        {
            _renderer.positionCount = 2;
            _renderer.widthMultiplier = size.Y;
            _renderer.SetPositions(new []
            {
                position,
                direction * size.X + position
            });
        }

        private static Vector3 GetDirection(float angle) => new((float) Math.Cos(Math.PI / 180 * angle), (float) Math.Sin(Math.PI / 180 * angle));
    }
}
