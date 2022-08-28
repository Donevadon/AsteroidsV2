using System;
using System.Collections.Generic;
using System.Linq;
using CoreEngine.Core;
using CoreEngine.Core.Models;
using CoreEngine.Entities.Objects.Factory;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Core = CoreEngine.Core.CoreEngine;
using GameObject = CoreEngine.Entities.GameObject;

namespace View
{
    public class UnityPool : MonoBehaviour, IObjectPool, IFragmentsFactory, IAmmunitionFactory, IPoolSetter
    {
        [SerializeField] private Controller controller;
        [SerializeField] private Metric metric;
        [SerializeField] private LaserView laserView;
        private Dictionary<ObjectType, Queue<GameObjectObserver>> _queue;
        private Dictionary<ObjectType, GameObjectObserver> _proto;
        private IAmmunitionFactory _ammunition;
        private IFragmentsFactory _fragments;
        private IObjectPool _pool;


        private void Awake()
        {
            var objects = Resources.LoadAll<GameObjectObserver>("");

            _queue = objects.ToDictionary(observer => observer.Type, _ => new Queue<GameObjectObserver>());
            _proto = objects.ToDictionary(observer => observer.Type);

            _pool = new DefaultObjectFactory(controller, controller, metric);
            _ammunition = new BulletFactory();
            _fragments = new SmallAsteroidFactory();
        }

        public IObject GetPlayer(PlayerModel model)
        {
            var player = _pool.GetPlayer(model);
            var observer = GetObserver(ObjectType.Player, model.MoveOptions.Position, model.MoveOptions.Angle);
            observer.Init(player as GameObject, this);

            return player;
        }

        public IObject GetAsteroid(AsteroidModel model)
        {
            var asteroid = _pool.GetAsteroid(model);
            var observer = GetObserver(ObjectType.Asteroid, model.MoveOptions.Position, model.MoveOptions.Angle);
            observer.Init(asteroid as GameObject, this);

            return asteroid;
        }

        public IObject GetAlien(AlienModel model)
        {
            var asteroid = _pool.GetAlien(model);
            var observer = GetObserver(ObjectType.Alien,model.MoveOptions.Position, model.MoveOptions.Angle);
            observer.Init(asteroid as GameObject, this);

            return asteroid;
        }

        event Action<IObject> IObjectPool.ObjectCreated
        {
            add => _pool.ObjectCreated += value;
            remove => _pool.ObjectCreated -= value;
        }

        private GameObjectObserver GetObserver(ObjectType type, Vector2 position, float angle)
        {
            if (_queue[type].TryDequeue(out var observer))
            {
                observer.transform.position = new Vector3(position.X, position.Y);
                observer.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                observer.gameObject.SetActive(true);
            }
            else
            {
                observer = Instantiate(_proto[type], new Vector3(position.X, position.Y), Quaternion.Euler(0, 0, angle - 90));
            }

            return observer;
        }

        public IObject GetSmallAsteroid(FragmentAsteroidModel model)
        {
            var asteroid = _fragments.GetSmallAsteroid(model);
            var observer = GetObserver(ObjectType.SmallAsteroid, model.MoveOption.Position, model.MoveOption.Angle);
            observer.Init(asteroid as GameObject, this);

            return asteroid;
        }

        event Action<IObject> IFragmentsFactory.ObjectCreated
        {
            add => _fragments.ObjectCreated += value;
            remove => _fragments.ObjectCreated -= value;
        }

        public IObject GetAmmo(AmmunitionModel model)
        {
            var bullet = _ammunition.GetAmmo(model);
            var observer = GetObserver(ObjectType.Bullet, model.MoveOptions.Position, model.MoveOptions.Angle);
            observer.Init(bullet as GameObject, this);

            return bullet;
        }

        public IObject GetLaser(AmmunitionModel model)
        {
            var laser = _ammunition.GetLaser(model);
            laserView.Init(laser, model.MoveOptions.Position, model.MoveOptions.Angle, model.Size);

            return laser;
        }

        event Action<IObject> IAmmunitionFactory.ObjectCreated
        {
            add => _ammunition.ObjectCreated += value;
            remove => _ammunition.ObjectCreated -= value;
        }

        public void Set(GameObjectObserver observer)
        {
            _queue[observer.Type].Enqueue(observer);
            observer.gameObject.SetActive(false);
        }
    }
}