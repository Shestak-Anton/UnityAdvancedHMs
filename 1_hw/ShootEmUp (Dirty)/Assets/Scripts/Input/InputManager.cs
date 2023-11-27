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
        private bool _isShootPressed;

        private void Awake()
        {
            _inputManager = new KeyboardInputManager();
        }

        private void Update()
        {
            HandleShootInput();
            HandleMoveInput();
        }

        private void HandleMoveInput()
        {
            _direction = _inputManager.HandlePositionChangeInput();
            OnPositionChangedListener?.Invoke(_direction * Time.fixedDeltaTime);
        }

        private void HandleShootInput()
        {
            if (_inputManager.ShouldShoot())
            {
                OnShootListener?.Invoke();
            }
        }
    }
}