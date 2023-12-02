using System.Collections.Generic;

namespace LifeCycle
{
    internal sealed class LifeCycleComponentsRegistry
    {
        private List<ILifeCycle.ICreateListener> _creatableComponents = new();
        private List<ILifeCycle.IEnableListener> _resumableComponents = new();
        private List<ILifeCycle.IDisableListener> _pausableComponents = new();
        private List<ILifeCycle.IUpdateListener> _updaterComponents = new();
        private List<ILifeCycle.IFixedUpdateListener> _fixedUpdaterComponents = new();

        public void PerformCreation()
        {
            foreach (var creatableComponent in _creatableComponents)
            {
                creatableComponent.OnCreate();
            }
        }

        public void PerformEnable()
        {
            foreach (var resumableComponent in _resumableComponents)
            {
                resumableComponent.OnEnable();
            }
        }

        public void PerformDisable()
        {
            foreach (var pausableComponent in _pausableComponents)
            {
                pausableComponent.OnDisable();
            }
        }

        public void PerformFixedUpdate(float fixedDeltaTime)
        {
            for (int i = 0; i < _fixedUpdaterComponents.Count; i++)
            {
                _fixedUpdaterComponents[i].OnFixedUpdate(fixedDeltaTime);
            }
        }

        public void PerformUpdate()
        {
            for (int i = 0; i < _updaterComponents.Count; i++)
            {
                _updaterComponents[i].OnUpdate();
            }
        }

        public void Register(ILifeCycle.IEnableListener listener)
        {
            _resumableComponents.Add(listener);
        }

        public void Register(ILifeCycle.IFixedUpdateListener listener)
        {
            _fixedUpdaterComponents.Add(listener);
        }

        public void Register(ILifeCycle.IUpdateListener listener)
        {
            _updaterComponents.Add(listener);
        }

        public void Register(ILifeCycle.IDisableListener listener)
        {
            _pausableComponents.Add(listener);
        }

        public void Register(ILifeCycle.ICreateListener listener)
        {
            _creatableComponents.Add(listener);
        }

        public void Remove(ILifeCycle.IEnableListener listener)
        {
            _resumableComponents.Remove(listener);
        }

        public void Remove(ILifeCycle.IFixedUpdateListener listener)
        {
            _fixedUpdaterComponents.Remove(listener);
        }

        public void Remove(ILifeCycle.IUpdateListener listener)
        {
            _updaterComponents.Remove(listener);
        }

        public void Remove(ILifeCycle.IDisableListener listener)
        {
            _pausableComponents.Remove(listener);
        }

        public void Remove(ILifeCycle.ICreateListener listener)
        {
            _creatableComponents.Remove(listener);
        }
        
    }
}