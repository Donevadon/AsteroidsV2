using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Timers;
using NewScript.Player;

namespace NewScript.Core
{
    public abstract class CoreEngine
    {
        private readonly List<GameObject> _objects = new();
        private readonly Thread _updateObjectsThread;
        private readonly Thread _spawnAsteroidsThread;
        private readonly CancellationTokenSource _tokenSource = new();

        protected abstract IObjectPool Pool { get; }

        protected CoreEngine()
        {
            _updateObjectsThread = new Thread(() => UpdateObjects(_tokenSource.Token));
            _spawnAsteroidsThread = new Thread(() => SpawnAsteroids(_tokenSource.Token));
        }

        public void Start()
        {
            CreatePlayer();
            _updateObjectsThread.Start();
            _spawnAsteroidsThread.Start();
        }

        private void CreatePlayer()
        {
            var player = Pool.GetPlayer();
            Console.WriteLine("Player Created");
            Add(player);
        }

        private void UpdateObjects(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                for (var i = 0; i < _objects.Count; i++)
                {
                    var gameObject = Get(i);
                    gameObject.Update();
                }
            }
        }

        private void Add(GameObject obj)
        {
            lock (this)
            {
                _objects.Add(obj);
            }
        }

        private GameObject Get(int index)
        {
            lock (this)
            {
                return _objects[index];
            }
        }

        private void SpawnAsteroids(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Pool.GetAsteroid(new Vector2(1,1));
            }
        }
    }

    public class ObjectPool : IObjectPool
    {
        public GameObject GetPlayer()
        {
            return new PlayerShip(null);
        }

        public GameObject GetAsteroid(Vector2 vector2)
        {
            return new Asteroid(vector2);
        }
    }

    public class Asteroid : GameObject
    {
        private readonly IMovement _movement = new PlayerMovement(Vector2.Zero, Vector2.UnitY, 0.1f);
        private readonly IRotate _rotation = new PlayerRotation(Vector3.Zero, Vector3.UnitZ, 5);
        public event Action<Vector2> PositionChanged;
        public event Action<Vector3> RotationChanged;

        public Asteroid(Vector2 vector2)
        {
            
        }

        public override void Update()
        {
            
        }
    }


    public interface IObjectPool
    {
        GameObject GetPlayer();
        GameObject GetAsteroid(Vector2 vector2);
    }

    public abstract class GameObject
    {
        public abstract void Update();
    }
}
