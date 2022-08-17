using NewScript.Core;
using NewScript.Player;
using UnityEngine;
using GameObject = NewScript.Core.GameObject;
using Vector2 = System.Numerics.Vector2;

namespace View
{
    public class CoreEngineForUnity : CoreEngine
    {
        protected override IObjectPool Pool { get; } = new UnityPool(new ObjectPool());
    }

    public class UnityPool : IObjectPool
    {
        private readonly IObjectPool _pool;

        public UnityPool(IObjectPool pool)
        {
            _pool = pool;
        }
        public GameObject GetPlayer()
        {
            var player = _pool.GetPlayer();
            var proto = Resources.Load<Player>("GameObject");

            var unityPlayer = Object.Instantiate(proto, Vector3.zero, Quaternion.identity);
            unityPlayer.Init(player as PlayerShip);

            return player;
        }

        public GameObject GetAsteroid(Vector2 vector2)
        {
            var asteroid = _pool.GetAsteroid(vector2);
            var proto = Resources.Load<Asteroid>("GameObject");

            var unityPlayer = Object.Instantiate(proto, Vector3.zero, Quaternion.identity);
            unityPlayer.Init(asteroid as NewScript.Core.Asteroid);

            return asteroid;

        }
    }
}