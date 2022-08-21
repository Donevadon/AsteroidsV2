using CoreEngine.Core;
using CoreEngine.Core.Configurations;

namespace View
{
    public class CoreEngineForUnity : CoreEngine.Core.CoreEngine
    {
        private readonly UnityPool _pool;

        public CoreEngineForUnity(UnityPool pool, Options options) : base(options)
        {
            _pool = pool;
        }
        protected override IObjectPool Pool => _pool;
        protected override IFragmentsFactory FragmentsFactory => _pool;
        protected override IAmmunitionFactory AmmunitionFactory => _pool;
    }
}