using System;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, ILifeCycle.IFixedUpdateListener
    {
        public event Action OnDestinationReachedListener;

        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private float targetPositionRadius = 0.25f;

        private Vector2 _destination;
        private bool _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (_isReached)
            {
                return;
            }

            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= targetPositionRadius)
            {
                _isReached = true;
                OnDestinationReachedListener?.Invoke();
                return;
            }

            var direction = vector.normalized * deltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}