using System;
using GameLoop;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour,
        ILifeCycle.IFixedUpdateListener,
        IGameEvent.IPauseGameListener,
        IGameEvent.IStartGameListener,
        IGameEvent.IEndGameListener
    {
        public event Action OnDestinationReachedListener;

        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private float _targetPositionRadius = 0.25f;

        private Vector2 _destination;
        private bool _isReached;
        private bool _isGamePaused;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        void ILifeCycle.IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            if (_isReached)
            {
                return;
            }

            if (_isGamePaused)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= _targetPositionRadius)
            {
                _isReached = true;
                OnDestinationReachedListener?.Invoke();
                return;
            }

            var direction = vector.normalized * deltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }

        void IGameEvent.IPauseGameListener.OnGamePaused()
        {
            _isGamePaused = true;
        }

        void IGameEvent.IStartGameListener.OnGameStarted()
        {
            _isGamePaused = false;
        }

        void IGameEvent.IEndGameListener.OnEndGame()
        {
            _isGamePaused = true;
        }
    }
}