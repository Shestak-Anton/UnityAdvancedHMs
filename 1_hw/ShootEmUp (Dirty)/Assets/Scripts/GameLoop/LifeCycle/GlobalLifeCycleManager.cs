using UnityEngine;

namespace LifeCycle
{
    public sealed class GlobalLifeCycleManager
    {
        private LifeCycleState _currentState = LifeCycleState.NONE;
        private readonly LifeCycleComponentsRegistry _lifeCycleComponentsRegistry = new();

        public void ApplyLifecycleState(LifeCycleState lifeCycleState)
        {
            ValidateState(lifeCycleState);
            _currentState = lifeCycleState;
            switch (lifeCycleState)
            {
                case LifeCycleState.CREATE:
                    _lifeCycleComponentsRegistry.PerformCreation();
                    break;
                case LifeCycleState.ENABLED:
                    _lifeCycleComponentsRegistry.PerformResume();
                    break;
                case LifeCycleState.DISABLED:
                    _lifeCycleComponentsRegistry.PerformPause();
                    break;
            }
        }

        public void PerformUpdate()
        {
            if (_currentState != LifeCycleState.ENABLED) return;
            _lifeCycleComponentsRegistry.PerformUpdate();
        }

        public void PerformFixedUpdate(float fixedDeltaTime)
        {
            if (_currentState != LifeCycleState.ENABLED) return;
            _lifeCycleComponentsRegistry.PerformFixedUpdate(fixedDeltaTime);
        }

        public void AddComponent(ILifeCycle listener)
        {
            if (listener is ILifeCycle.ICreateListener createListener)
            {
                _lifeCycleComponentsRegistry.Register(createListener);
            }

            if (listener is ILifeCycle.IDisableListener disableListener)
            {
                _lifeCycleComponentsRegistry.Register(disableListener);
            }

            if (listener is ILifeCycle.IEnableListener enableListener)
            {
                _lifeCycleComponentsRegistry.Register(enableListener);
            }

            if (listener is ILifeCycle.IFixedUpdateListener fixedUpdateListener)
            {
                _lifeCycleComponentsRegistry.Register(fixedUpdateListener);
            }

            if (listener is ILifeCycle.IUpdateListener updateListener)
            {
                _lifeCycleComponentsRegistry.Register(updateListener);
            }
        }

        public void RemoveComponent(ILifeCycle listener)
        {
            if (listener is ILifeCycle.ICreateListener createListener)
            {
                _lifeCycleComponentsRegistry.Remove(createListener);
            }

            if (listener is ILifeCycle.IDisableListener disableListener)
            {
                _lifeCycleComponentsRegistry.Remove(disableListener);
            }

            if (listener is ILifeCycle.IEnableListener enableListener)
            {
                _lifeCycleComponentsRegistry.Remove(enableListener);
            }

            if (listener is ILifeCycle.IFixedUpdateListener fixedUpdateListener)
            {
                _lifeCycleComponentsRegistry.Remove(fixedUpdateListener);
            }

            if (listener is ILifeCycle.IUpdateListener updateListener)
            {
                _lifeCycleComponentsRegistry.Remove(updateListener);
            }
        }

        private void ValidateState(LifeCycleState newState)
        {
            if (IsStateValid(newState))
            {
                LogErrorState(_currentState, newState);
            }
        }

        private static bool IsStateValid(LifeCycleState lifeCycleState)
        {
            return lifeCycleState is LifeCycleState.NONE;
        }

        private static void LogErrorState(LifeCycleState currentState, LifeCycleState newState)
        {
            Debug.LogError($"Invalid state. Current: {currentState}, New (Wrong): {newState}");
        }
    }
}