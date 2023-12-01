using System.Collections.Generic;

namespace LifeCycle
{
    internal sealed class LifeCycleComponentsRegistry
    {
        private List<ILifeCycle.IResumeListener> _resumableComponents = new();
        private List<ILifeCycle.IPauseListener> _pausableComponents = new();
        private List<ILifeCycle.IUpdateListener> _updaterComponents = new();
        private List<ILifeCycle.IFixedUpdateListener> _fixedUpdaterComponents = new();

        public void PerformResume()
        {
            foreach (var resumableComponent in _resumableComponents)
            {
                resumableComponent.OnResume();
            }
        }

        public void PerformPause()
        {
            foreach (var pausableComponent in _pausableComponents)
            {
                pausableComponent.OnPause();
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

        public void Register(ILifeCycle.IResumeListener listener)
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

        public void Register(ILifeCycle.IPauseListener listener)
        {
            _pausableComponents.Add(listener);
        }

        public void Remove(ILifeCycle.IResumeListener listener)
        {
            _resumableComponents.Remove(listener);
        }

        public void Remove(ILifeCycle.IPauseListener listener)
        {
            _pausableComponents.Remove(listener);
        }
    }
}