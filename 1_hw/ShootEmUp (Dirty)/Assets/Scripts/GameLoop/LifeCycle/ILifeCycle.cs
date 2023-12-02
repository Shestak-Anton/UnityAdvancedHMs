namespace LifeCycle
{
    public interface ILifeCycle
    {
        public interface ICreateListener : ILifeCycle
        {
            void OnCreate();
        }

        public interface IEnableListener : ILifeCycle
        {
            void OnEnable();
        }

        public interface IUpdateListener : ILifeCycle
        {
            void OnUpdate();
        }

        public interface IFixedUpdateListener : ILifeCycle
        {
            void OnFixedUpdate(float deltaTime);
        }

        public interface IDisableListener : ILifeCycle
        {
            void OnDisable();
        }
    }
}