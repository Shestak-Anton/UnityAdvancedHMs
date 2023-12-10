using System.Collections.Generic;

namespace LifeCycle
{
    public sealed class LifeCycleComponentsRegistry
    {
        private readonly List<ILifeCycle.ICreateListener> _creates = new();
        private readonly List<ILifeCycle.IEnableListener> _enables = new();
        private readonly List<ILifeCycle.IUpdateListener> _updates = new();
        private readonly List<ILifeCycle.IFixedUpdateListener> _fixedUpdates = new();
        private readonly List<ILifeCycle.IDisableListener> _disables = new();

        public void PerformCreation()
        {
            foreach (var creatableComponent in _creates)
            {
                creatableComponent.OnCreate();
            }
        }

        public void PerformEnable()
        {
            foreach (var component in _enables)
            {
                component.OnEnable();
            }
        }

        public void PerformFixedUpdate(float fixedDeltaTime)
        {
            for (int i = 0; i < _fixedUpdates.Count; i++)
            {
                _fixedUpdates[i].OnFixedUpdate(fixedDeltaTime);
            }
        }

        public void PerformUpdate()
        {
            for (int i = 0; i < _updates.Count; i++)
            {
                _updates[i].OnUpdate();
            }
        }

        public void PerformDisable()
        {
            foreach (var disable in _disables)
            {
                disable.OnDisable();
            }
        }

        public void AddComponent(ILifeCycle listener)
        {
            if (listener is ILifeCycle.ICreateListener createListener)
            {
                _creates.Add(createListener);
            }

            if (listener is ILifeCycle.IDisableListener disableListener)
            {
                _disables.Add(disableListener);
            }

            if (listener is ILifeCycle.IEnableListener enableListener)
            {
                _enables.Add(enableListener);
            }

            if (listener is ILifeCycle.IFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdates.Add(fixedUpdateListener);
            }

            if (listener is ILifeCycle.IUpdateListener updateListener)
            {
                _updates.Add(updateListener);
            }
        }

        public void RemoveComponent(ILifeCycle listener)
        {
            if (listener is ILifeCycle.ICreateListener createListener)
            {
                _creates.Remove(createListener);
            }

            if (listener is ILifeCycle.IDisableListener disableListener)
            {
                _disables.Remove(disableListener);
            }

            if (listener is ILifeCycle.IEnableListener enableListener)
            {
                _enables.Remove(enableListener);
            }

            if (listener is ILifeCycle.IFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdates.Remove(fixedUpdateListener);
            }

            if (listener is ILifeCycle.IUpdateListener updateListener)
            {
                _updates.Remove(updateListener);
            }
        }
    }
}