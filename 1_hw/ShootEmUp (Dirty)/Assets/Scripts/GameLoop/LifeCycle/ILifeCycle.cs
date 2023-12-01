namespace LifeCycle
{
    public interface ILifeCycle
    {
        public interface IResumeListener : ILifeCycle
        {
            void OnResume();
        }
        
        public interface IUpdateListener : ILifeCycle
        {
            void OnUpdate();
        }

        public interface IFixedUpdateListener : ILifeCycle
        {
            void OnFixedUpdate(float deltaTime);
        }
        
        public interface IPauseListener : ILifeCycle
        {
            void OnPause();
        }
    }
}