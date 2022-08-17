using System;
using NewScript.Player;
using UnityEngine;

namespace View
{
    public class Player : MonoBehaviour
    {
        public Controller playerController;
        private Vector3 _newPosition;
        private Quaternion _newRotation;

        public void Init(PlayerShip player)
        {
            player.PositionChanged += vector2 => _newPosition = new Vector2(vector2.X, vector2.Y);
            player.RotationChanged += vector3 => _newRotation = Quaternion.Euler(new Vector3(vector3.X, vector3.Y, vector3.Z));
            var currentTransform = transform;
            _newPosition = currentTransform.position;
            _newRotation = currentTransform.rotation;
        }

        private void Update()
        {
            transform.position = _newPosition;
            transform.rotation = _newRotation;
        }
    }
}