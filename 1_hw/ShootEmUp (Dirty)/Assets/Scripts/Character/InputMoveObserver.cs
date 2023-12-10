using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMoveObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private InputManager _inputManager;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            _inputManager.OnPositionChanged += _moveComponent.MoveByRigidbodyVelocity;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            _inputManager.OnPositionChanged -= _moveComponent.MoveByRigidbodyVelocity;
        }
    }
}