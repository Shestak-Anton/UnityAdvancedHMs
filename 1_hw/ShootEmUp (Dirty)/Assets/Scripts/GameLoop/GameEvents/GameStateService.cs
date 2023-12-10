using DefaultNamespace;
using UnityEngine;

namespace GameLoop
{
    public class GameStateService : MonoBehaviour
    {
        [SerializeField] private GameState _gameState = GameState.Pause;
        public GameState GameState => _gameState;

        private GameStateManager _gameStateManager;

        public void Init(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
            _gameStateManager.ApplyState(_gameState);
        }

        public void ApplyState(GameState gameState)
        {
            _gameState = gameState;
            _gameStateManager.ApplyState(gameState);
        }

        public void AttachObject(GameObject sceneInstance)
        {
            foreach (var gameEvent in sceneInstance.GetComponentsInChildren<IGameEvent>())
            {
                _gameStateManager.Register(gameEvent);
            }
        }
        
    }
}