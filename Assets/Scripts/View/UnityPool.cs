using System;
using System.Collections.Generic;
using System.Linq;
using CoreEngine.Core;
using CoreEngine.Core.Configurations;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects;
using CoreEngine.Entities.Objects.Factory;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace View
{
    public class UnityPool : MonoBehaviour, IObjectPool, IFragmentsFactory, IAmmunitionFactory, IPoolSetter
    {
        [SerializeField] private Controller controller;
        [SerializeField] private GameOptions options;
        [SerializeField] private Metric metric;
        private CoreEngineForUnity _core;
        private IObjectPool _pool;
        private IAmmunitionFactory _ammunition;
        private IFragmentsFactory _fragments;
        private Dictionary<ObjectType, Queue<GameObjectObserver>> _queue;
        private Dictionary<ObjectType, GameObjectObserver> _proto;

        private void Awake()
        {
            var objects = Resources.LoadAll<GameObjectObserver>("");

            _queue = objects.ToDictionary(observer => observer.Type, _ => new Queue<GameObjectObserver>());
            _proto = objects.ToDictionary(observer => observer.Type);

            _core = new CoreEngineForUnity(this, options.options);
            _pool = new DefaultObjectFactory(_core, controller, metric);
            _ammunition = new BulletFactory(_core);
            _fragments = new SmallAsteroidFactory(_core);
            _core.Start();
        }

        public IObject GetPlayer(PlayerModel model)
        {
            var player = _pool.GetPlayer(model);
            var observer = GetObserver(ObjectType.Player, model.MoveOptions.Position);
            observer.Init(player as CoreEngine.Entities.GameObject, this);

            return player;
        }

        public IObject GetAsteroid(AsteroidModel model)
        {
            var asteroid = _pool.GetAsteroid(model);
            var observer = GetObserver(ObjectType.Asteroid, model.MoveOptions.Position);
            observer.Init(asteroid as CoreEngine.Entities.GameObject, this);

            return asteroid;
        }

        public IObject GetAlien(AlienModel model)
        {
            var asteroid = _pool.GetAlien(model);
            var observer = GetObserver(ObjectType.Alien,model.MoveOptions.Position);
            observer.Init(asteroid as CoreEngine.Entities.GameObject, this);

            return asteroid;
        }

        private GameObjectObserver GetObserver(ObjectType type, Vector2 position)
        {
            if (_queue[type].TryDequeue(out var observer))
            {
                observer.transform.position = new Vector3(position.X, position.Y);
                observer.gameObject.SetActive(true);
            }
            else
            {
                observer = Instantiate(_proto[type], new Vector3(position.X, position.Y), Quaternion.identity);
            }

            return observer;
        }

        private void Update()
        {
            _core.UpdateFrame(Time.deltaTime);
        }

        public IObject GetSmallAsteroid(FragmentAsteroidModel model)
        {
            var asteroid = _fragments.GetSmallAsteroid(model);
            var observer = GetObserver(ObjectType.SmallAsteroid, model.MoveOption.Position);
            observer.Init(asteroid as CoreEngine.Entities.GameObject, this);

            return asteroid;
        }

        public IObject GetAmmo(MoveOptions moveOptions)
        {
            var bullet = _ammunition.GetAmmo(moveOptions);
            var observer = GetObserver(ObjectType.Bullet, moveOptions.Position);
            observer.Init(bullet as CoreEngine.Entities.GameObject, this);

            return bullet;
        }

        public void Set(GameObjectObserver observer)
        {
            _queue[observer.Type].Enqueue(observer);
            observer.gameObject.SetActive(false);
        }
    }
}