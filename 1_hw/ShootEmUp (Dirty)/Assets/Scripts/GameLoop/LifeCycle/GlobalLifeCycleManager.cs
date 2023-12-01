using UnityEngine;

namespace LifeCycle
{
    public sealed class GlobalLifeCycleManager
    {
        private LifeCycleState _currentState = LifeCycleState.NONE;
        private readonly LifeCycleComponentsRegistry _lifeCycleComponentsRegistry;

        public GlobalLifeCycleManager(LifeCycleState initialState)
        {
            _lifeCycleComponentsRegistry = new LifeCycleComponentsRegistry();
            ApplyState(initialState);
        }

        public void ApplyState(LifeCycleState lifeCycleState)
        {
            ValidateState(lifeCycleState);
            _currentState = lifeCycleState;
            switch (lifeCycleState)
            {
                case LifeCycleState.RESUMED:
                    _lifeCycleComponentsRegistry.PerformResume();
                    break;
                case LifeCycleState.PAUSED:
                    _lifeCycleComponentsRegistry.PerformPause();
                    break;
            }
        }

        public void PerformUpdate()
        {
            if (_currentState != LifeCycleState.RESUMED) return;
            _lifeCycleComponentsRegistry.PerformUpdate();
        }

        public void PerformFixedUpdate(float fixedDeltaTime)
        {
            if (_currentState != LifeCycleState.RESUMED) return;
            _lifeCycleComponentsRegistry.PerformFixedUpdate(fixedDeltaTime);
        }

        public void AddComponent(ILifeCycle listener)
        {
            if (listener is ILifeCycle.IPauseListener pauseListener)
            {
                _lifeCycleComponentsRegistry.Register(pauseListener);
            }

            if (listener is ILifeCycle.IResumeListener resumeListener)
            {
                _lifeCycleComponentsRegistry.Register(resumeListener);
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
            if (listener is ILifeCycle.IPauseListener pauseListener)
            {
                _lifeCycleComponentsRegistry.Remove(pauseListener);
            }

            if (listener is ILifeCycle.IResumeListener resumeListener)
            {
                _lifeCycleComponentsRegistry.Remove(resumeListener);
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