using System;
using CoreEngine.Player;
using UnityEngine;

namespace View
{
    public class Controller : MonoBehaviour, IPlayerController
    {
        public event Action<float> Move;
        public event Action<float> Rotate;

        private void Start()
        {
            new CoreEngineForUnity().Start();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Rotate?.Invoke(1);
            }else if (Input.GetKey(KeyCode.D))
            {
                Rotate?.Invoke(-1);
            }else if (Input.GetKey(KeyCode.W))
            {
                Move?.Invoke(1);
            }
        }
    }
}