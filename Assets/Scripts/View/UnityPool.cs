using System;
using System.Collections.Generic;
using System.Linq;
using CoreEngine.Core;
using CoreEngine.Player;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace View
{
    public class UnityPool : MonoBehaviour, IObjectPool
    {
        [SerializeField] private Controller _controller;
        private CoreEngineForUnity _core;
        private IObjectPool _pool;
        private Dictionary<Type, Queue<GameObjectObserver>> _queue;
        private Dictionary<Type, GameObjectObserver> _proto;
        private readonly Queue<Action> _spawns = new();

        private void Awake()
        {
            var objects = Resources.LoadAll<GameObjectObserver>("");

            _queue = objects.ToDictionary(observer => observer.GetType(), _ => new Queue<GameObjectObserver>());
            _proto = objects.ToDictionary(observer => observer.GetType());

            _core = new CoreEngineForUnity(this);
            _pool = new ObjectPool(_core, _controller);
            _core.Start();
        }

        public CoreEngine.Core.GameObject GetPlayer(Vector2 position)
        {
            var player = _pool.GetPlayer(position);

            _spawns.Enqueue(() =>
            {
                var observer = Instantiate(_proto[typeof(Player)], Vector3.zero, Quaternion.identity);
                observer.Init(player as PlayerShip);
            });
            
            return player;
        }

        public CoreEngine.Core.GameObject GetAsteroid(Vector2 vector2)
        {
            var asteroid = _pool.GetAsteroid(vector2);

            _spawns.Enqueue(() =>
            {
                var observer = Instantiate(_proto[typeof(Asteroid)], new Vector3(vector2.X, vector2.Y), Quaternion.identity);
                observer.Init(asteroid as CoreEngine.Core.Asteroid);
            });

            return asteroid;
        }

        private void Update()
        {
            if (_spawns.Count > 0)
            {
                _spawns.Dequeue().Invoke();
            }
        }
    }
}