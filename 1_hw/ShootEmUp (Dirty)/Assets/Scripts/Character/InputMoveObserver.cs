using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputMoveObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private InputManager inputManager;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            inputManager.OnPositionChangedListener += moveComponent.MoveByRigidbodyVelocity;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            inputManager.OnPositionChangedListener -= moveComponent.MoveByRigidbodyVelocity;
        }
    }
}