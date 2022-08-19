using CoreEngine.Core;
using UnityEngine;

namespace View
{
    public class GameObjectObserver : MonoBehaviour
    {
        private Vector3 _newPosition;
        private Quaternion _newRotation;
        private bool _isDestroy;
        protected IObjectPool Pool { get; private set; }

        public void Init(CoreEngine.Entities.GameObject player, IObjectPool pool)
        {
            player.PositionChanged += vector2 => _newPosition = new Vector2(vector2.X, vector2.Y);
            player.RotationChanged += vector3 => _newRotation = Quaternion.Euler(new Vector3(vector3.X, vector3.Y, vector3.Z));
            Pool = pool;
            var currentTransform = transform;
            _newPosition = currentTransform.position;
            _newRotation = currentTransform.rotation;
            player.Destroyed += () =>
            {
                var poolSetter = Pool as IPoolSetter;
                poolSetter?.Set(this);
            };
        }

        private void Update()
        {
            transform.position = _newPosition;
            transform.rotation = _newRotation;
            if (_isDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

    public interface IPoolSetter
    {
        void Set(GameObjectObserver gameObjectObserver);
    }
}