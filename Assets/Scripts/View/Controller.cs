﻿using System;
using CoreEngine.Entities.Objects;
using UnityEngine;

namespace View
{
    public class Controller : MonoBehaviour, IController
    {
        public event Action Move;
        public event Action<float> Rotate;

        private void Start()
        {
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Rotate?.Invoke(1);
            }else if (Input.GetKey(KeyCode.D))
            {
                Rotate?.Invoke(-1);
            }
            else
            {
                Rotate?.Invoke(0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                Move?.Invoke();
            }
        }
    }
}