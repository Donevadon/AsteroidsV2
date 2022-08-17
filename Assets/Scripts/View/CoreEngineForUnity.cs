using CoreEngine.Core;

namespace View
{
    public class CoreEngineForUnity : CoreEngine.Core.CoreEngine
    {
        private readonly UnityPool _pool;

        public CoreEngineForUnity(UnityPool pool)
        {
            _pool = pool;
        }
        protected override IObjectPool Pool => _pool;
    }
}