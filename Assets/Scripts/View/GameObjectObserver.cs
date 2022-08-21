using CoreEngine.Core;
using UnityEngine;

namespace View
{
    public class GameObjectObserver : MonoBehaviour
    {
        [SerializeField] private ObjectType type;
        public ObjectType Type => type;
        private Vector3 _newPosition;
        private Quaternion _newRotation;
        private IPoolSetter Pool { get; set; }

        public void Init(CoreEngine.Entities.GameObject player, IPoolSetter pool)
        {
            player.PositionChanged += vector2 => _newPosition = new Vector2(vector2.X, vector2.Y);
            player.RotationChanged += angle => _newRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            Pool = pool;
            var currentTransform = transform;
            _newPosition = currentTransform.position;
            _newRotation = currentTransform.rotation;
            player.Destroyed += sender =>
            {
                Pool.Set(this);
            };
        }

        private void Update()
        {
            transform.position = _newPosition;
            transform.rotation = _newRotation;
        }
    }
}