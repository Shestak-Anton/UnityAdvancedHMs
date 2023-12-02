
namespace LifeCycle
{
    public sealed class GlobalLifeCycleManager
    {
        private readonly LifeCycleComponentsRegistry _lifeCycleComponentsRegistry = new();

        public void PerformCreation()
        {
            _lifeCycleComponentsRegistry.PerformCreation();
        }

        public void PerformEnable()
        {
            _lifeCycleComponentsRegistry.PerformEnable();
        }

        public void PerformUpdate()
        {
            _lifeCycleComponentsRegistry.PerformUpdate();
        }

        public void PerformFixedUpdate(float fixedDeltaTime)
        {
            _lifeCycleComponentsRegistry.PerformFixedUpdate(fixedDeltaTime);
        }

        public void PerformDisable()
        {
            _lifeCycleComponentsRegistry.PerformDisable();
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
    }
}