using System.Collections.Generic;

namespace GameLoop
{
    public class GameStateManager
    {
        private readonly List<IGameEvent> _gameEventComponents = new();

        private GameState _gameState = GameState.None;
        public GameState GameState => _gameState;

        public void ApplyState(GameState gameState)
        {
            if (_gameState == gameState) return;
            _gameState = gameState;

            foreach (var gameEventComponent in _gameEventComponents)
            {
                TryTriggerEventByState(gameEventComponent, gameState);
            }
        }

        public void Register(IGameEvent gameEvent)
        {
            _gameEventComponents.Add(gameEvent);
        }

        private void TryTriggerEventByState(IGameEvent gameEventComponent, GameState gameState)
        {
            switch (gameState)
            {
                case GameState.GamePlay:
                    if (gameEventComponent is IGameEvent.IStartGameListener startGameListener)
                    {
                        startGameListener.OnGameStarted();
                    }

                    break;
                case GameState.Pause:
                    if (gameEventComponent is IGameEvent.IPauseGameListener pauseGameListener)
                    {
                        pauseGameListener.OnGamePaused();
                    }

                    break;
                case GameState.EndGame:
                    if (gameEventComponent is IGameEvent.IEndGameListener endGameListener)
                    {
                        endGameListener.OnEndGame();
                    }

                    break;
            }
        }
    }
}