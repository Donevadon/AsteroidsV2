using System;
using UnityEngine;

namespace View
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private GameOptions gameOptions;
        [SerializeField] private GameObject endGamePanel;
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
            endGamePanel.SetActive(false);
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