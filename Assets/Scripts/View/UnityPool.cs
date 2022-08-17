using System;
using System.Collections.Generic;
using CoreEngine.Core;
using CoreEngine.Player;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace View
{
    public class UnityPool : MonoBehaviour, IObjectPool
    {
        private readonly IObjectPool _pool = new ObjectPool();
        private readonly Queue<Action> _spawns = new();

        public CoreEngine.Core.GameObject GetPlayer()
        {
            var player = _pool.GetPlayer();

            _spawns.Enqueue(() =>
            {
                var proto = Resources.Load<Player>("GameObject");

                var unityPlayer = Instantiate(proto, Vector3.zero, Quaternion.identity);
                unityPlayer.Init(player as PlayerShip);
            });
            
            return player;
        }

        public CoreEngine.Core.GameObject GetAsteroid(Vector2 vector2)
        {
            var asteroid = _pool.GetAsteroid(vector2);

            _spawns.Enqueue(() =>
            {
                var proto = Resources.Load<Asteroid>("Asteroid");

                var unityPlayer = Instantiate(proto, new Vector3(vector2.X, vector2.Y), Quaternion.identity);
                unityPlayer.Init(asteroid as CoreEngine.Core.Asteroid);
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