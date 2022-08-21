using CoreEngine.Core.Configurations;
using UnityEngine;

namespace View
{
    [CreateAssetMenu(fileName = "Options", menuName = "Options", order = 0)]
    public class GameOptions : ScriptableObject
    {
        public Options options;
    }
}