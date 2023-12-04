using System;
using GameLoop;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour,
        ILifeCycle.IUpdateListener,
        ILifeCycle.ICreateListener,
        IGameEvent.IEndGameListener,
        IGameEvent.IPauseGameListener,
        IGameEvent.IStartGameListener
    {
        public event Action<Vector2> OnPositionChangedListener;
        public event Action OnShootListener;

        private IInputManager _inputManager;
        private Vector2 _direction = Vector2.zero;
        private bool _isShootPressed;
        private bool _isInputActive;

        void ILifeCycle.ICreateListener.OnCreate()
        {
            _inputManager = new KeyboardInputManager();
        }

        void ILifeCycle.IUpdateListener.OnUpdate()
        {
            if (!_isInputActive) return;
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

        public void OnEndGame()
        {
            _isInputActive = false;
        }

        public void OnGamePaused()
        {
            _isInputActive = false;
        }

        public void OnGameStarted()
        {
            _isInputActive = true;
        }
    }
}