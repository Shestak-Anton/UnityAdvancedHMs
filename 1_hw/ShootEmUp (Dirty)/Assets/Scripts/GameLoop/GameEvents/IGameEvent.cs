namespace GameLoop
{
    public interface IGameEvent
    {
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
            void OnEndGame();
        }
    }
}