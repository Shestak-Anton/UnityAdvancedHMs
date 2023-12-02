namespace GameLoop
{
    public interface IGameEvent
    {
        public interface IShowHomeScreenListener : IGameEvent
        {
            void OnHomeShowed();
        }

        public interface IPauseGameListener : IGameEvent
        {
            void OnGamePaused();
        }

        public interface IStartGameListener : IGameEvent
        {
            void OnGameStarted();
        }

        public interface IEndGameListener : IGameEvent
        {
            void OnGameLoosed();
        }
    }
}