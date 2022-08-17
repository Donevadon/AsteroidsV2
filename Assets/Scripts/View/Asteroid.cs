using UnityEngine;

namespace View
{
    public class Asteroid : MonoBehaviour
    {
        private Vector3 _newPosition;
        private Quaternion _newRotation;

        public void Init(CoreEngine.Core.Asteroid asteroid)
        {
            asteroid.PositionChanged += vector2 => _newPosition = new Vector2(vector2.X, vector2.Y);
            asteroid.RotationChanged += vector3 => _newRotation = Quaternion.Euler(new Vector3(vector3.X, vector3.Y, vector3.Z));
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