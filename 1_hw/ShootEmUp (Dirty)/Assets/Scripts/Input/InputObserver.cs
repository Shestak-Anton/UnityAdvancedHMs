using UnityEngine;
using UnityEngine.Events;

namespace ShootEmUp
{
    public sealed class InputObserver : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2> onHorizontalPositionChanged;
        [SerializeField] private UnityEvent onShoot;

        private readonly IInputHandler _inputHandler = new KeyboardInputHandler();

        private Vector2 _moveDirection = Vector2.zero;

        private void Update()
        {
            EmitShootingEventIfNeeded();
            _moveDirection = _inputHandler.GetMoveDirection();
        }

        private void FixedUpdate()
        {
            EmitMovingEvent(_moveDirection);
        }

        private void EmitMovingEvent(Vector2 direction)
        {
            onHorizontalPositionChanged.Invoke(direction * Time.fixedDeltaTime);
        }

        private void EmitShootingEventIfNeeded()
        {
            if (_inputHandler.ShouldShoot()) onShoot?.Invoke();
        }
    }
}