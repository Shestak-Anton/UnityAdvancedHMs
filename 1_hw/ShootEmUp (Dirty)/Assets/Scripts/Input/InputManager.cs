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
        public event Action<Vector2> OnPositionChanged;
        public event Action OnShoot;

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
            OnPositionChanged?.Invoke(_direction * Time.fixedDeltaTime);
        }

        private void HandleShootInput()
        {
            if (_inputManager.ShouldShoot())
            {
                OnShoot?.Invoke();
            }
        }

        void IGameEvent.IEndGameListener.OnEndGame()
        {
            _isInputActive = false;
        }

        void IGameEvent.IPauseGameListener.OnGamePaused()
        {
            _isInputActive = false;
        }

        void IGameEvent.IStartGameListener.OnGameStarted()
        {
            _isInputActive = true;
        }
    }
}