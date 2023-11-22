using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public event Action<Vector2> OnPositionChangedListener;
        public event Action OnShootListener;

        private IInputManager _inputManager;

        private Vector2 _direction = Vector2.zero;

        private void Awake()
        {
            _inputManager = new KeyboardInputManager();
        }

        private void Update()
        {
            HandleShootingInput();
            HandleMoveInput();
        }

        private void HandleMoveInput()
        {
            _direction = _inputManager.HandlePositionChangeInput();
        }

        private void HandleShootingInput()
        {
            var shouldShoot = _inputManager.ShouldShoot();
            if (shouldShoot) OnShootListener?.Invoke();
        }

        private void FixedUpdate()
        {
            OnPositionChangedListener?.Invoke(_direction);
        }
    }
}