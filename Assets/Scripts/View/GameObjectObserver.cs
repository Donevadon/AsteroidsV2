using CoreEngine.Core;
using UnityEngine;
using GameObject = CoreEngine.Entities.GameObject;

namespace View
{
    public class GameObjectObserver : MonoBehaviour
    {
        [SerializeField] private ObjectType type;
        public ObjectType Type => type;
        private IPoolSetter _pool;

        public void Init(CoreEngine.Entities.GameObject obj, IPoolSetter pool)
        {
            _pool = pool;
            obj.PositionChanged += OnUpdatePosition;
            obj.RotationChanged += OnUpdateRotation;
            obj.Destroyed += OnEndObserve;
        }

        private void OnUpdatePosition(System.Numerics.Vector2 position)
        {
            transform.position = new Vector3(position.X, position.Y);
        }

        private void OnUpdateRotation(float angle)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void OnEndObserve(IObject sender)
        {
            var obj = sender as GameObject;
            obj.PositionChanged -= OnUpdatePosition;
            obj.RotationChanged -= OnUpdateRotation;
            obj.Destroyed -= OnEndObserve;
            _pool.Set(this);
        }
    }
}