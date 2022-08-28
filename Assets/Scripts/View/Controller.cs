using System;
using CoreEngine.Behaviors.ControlledBehaviors;
using CoreEngine.Entities.Objects.ControlledObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace View
{
    public class Controller : MonoBehaviour, IMotion, IShoot
    {
        private PlayerInput _input;
        public event Action Move;
        public event Action<float> Rotate;
        public event Action Fire;
        public event Action LaunchLaser;

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Start()
        {
            _input.Player.Fire.started += FireOnStarted;
            _input.Player.Laser.started += LaserOnStarted;
        }

        private void LaserOnStarted(InputAction.CallbackContext obj)
        {
            LaunchLaser?.Invoke();
        }

        private void FireOnStarted(InputAction.CallbackContext obj)
        {
            Fire?.Invoke();
        }

        private void Update()
        {
            if (_input.Player.Move.IsPressed())
            {
                Move?.Invoke();
            }

            if (_input.Player.Fire.IsPressed())
            {
                Fire?.Invoke();
            }
            
            Rotate?.Invoke(_input.Player.Rotate.ReadValue<float>());
        }

        private void OnDestroy()
        {
            _input.Player.Fire.started -= FireOnStarted;
            _input.Player.Laser.started -= LaserOnStarted;
        }
    }
}