using System;
using System.Collections.Generic;
using System.Linq;
using CoreEngine.Core;
using CoreEngine.Entities.Objects.Factory;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace View
{
    public class UnityPool : MonoBehaviour, IObjectPool, IFragmentsFactory, IBulletFactory, IPoolSetter
    {
        [SerializeField] private Controller _controller;
        private CoreEngineForUnity _core;
        private IObjectPool _pool;
        private Dictionary<Type, Queue<GameObjectObserver>> _queue;
        private Dictionary<Type, GameObjectObserver> _proto;
        private readonly Queue<Action> _spawns = new();
        private readonly Queue<GameObjectObserver> _setter = new();

        private void Awake()
        {
            var objects = Resources.LoadAll<GameObjectObserver>("");

            _queue = objects.ToDictionary(observer => observer.GetType(), _ => new Queue<GameObjectObserver>());
            _proto = objects.ToDictionary(observer => observer.GetType());

            _core = new CoreEngineForUnity(this);
            _pool = new DefaultObjectFactory(_core, _controller);
            _core.Start();
        }

        public IObject GetPlayer(Vector2 position, IBulletFactory factory)
        {
            var player = _pool.GetPlayer(position, factory);

            _spawns.Enqueue(() =>
            {
                var observer = GetObserver<Player>(position);
                observer.Init(player as CoreEngine.Entities.GameObject, this);
            });
            
            return player;
        }

        public IObject GetAsteroid(Vector2 vector2, IFragmentsFactory factory)
        {
            var asteroid = _pool.GetAsteroid(vector2, factory);

            _spawns.Enqueue(() =>
            {
                var observer = GetObserver<Asteroid>(vector2);
                observer.Init(asteroid as CoreEngine.Entities.GameObject, this);
            });

            return asteroid;
        }
        
        private T GetObserver<T>(Vector2 position)
            where T : GameObjectObserver
        {
            if (_queue[typeof(T)].TryDequeue(out var observer))
            {
                observer.transform.position = new Vector3(position.X, position.Y);
                observer.gameObject.SetActive(true);
            }
            else
            {
                observer = Instantiate(_proto[typeof(T)], new Vector3(position.X, position.Y), Quaternion.identity);
            }

            return observer as T;
        }

        private void Update()
        {
            if (_spawns.TryDequeue(out var action))
            {
                action.Invoke();
            }

            if (_setter.TryDequeue(out var observer))
            {
                observer.gameObject.SetActive(false);
                _queue[observer.GetType()].Enqueue(observer);
            }
        }

        public IObject GetSmallAsteroid(Vector2 position)
        {
            var fragment = _pool as IFragmentsFactory;
            var asteroid = fragment?.GetSmallAsteroid(position);

            _spawns.Enqueue(() =>
            {
                var observer = GetObserver<SmallAsteroid>(position);
                observer.Init(asteroid as CoreEngine.Entities.GameObject, this);
            });

            return asteroid;
        }

        public IObject GetBullet(Vector2 position, System.Numerics.Vector3 direction)
        {
            var factory = _pool as IBulletFactory;
            var bullet = factory?.GetBullet(position, direction);

            _spawns.Enqueue(() =>
            {
                var observer = GetObserver<Bullet>(position);
                observer.Init(bullet as CoreEngine.Entities.GameObject, this);
            });

            return bullet;
        }

        public void Set(GameObjectObserver observer)
        {
            _setter.Enqueue(observer);
        }
    }
}