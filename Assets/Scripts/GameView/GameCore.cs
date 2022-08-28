using GameView.ObjectsPool;
using UnityEngine;

namespace GameView
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private GameOptions gameOptions;
        [SerializeField] private UnityPool pool;

        private CoreEngine.Core.CoreEngine _coreEngine;


        private void Awake()
        {
            _coreEngine = CreateCoreEngine();
        }
        
        public void Restart()
        {
            _coreEngine.Dispose();
            _coreEngine = CreateCoreEngine();
            _coreEngine.Start();
        }

        private CoreEngine.Core.CoreEngine CreateCoreEngine() => new(gameOptions.options, pool, pool, pool);

        private void Start()
        {
            _coreEngine.Start();
        }

        private void Update()
        {
            _coreEngine?.UpdateFrame(Time.deltaTime);
        }
    }
}