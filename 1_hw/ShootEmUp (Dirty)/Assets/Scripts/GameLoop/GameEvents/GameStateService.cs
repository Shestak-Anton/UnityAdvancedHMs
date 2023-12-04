using DefaultNamespace;
using UnityEngine;

namespace GameLoop
{
    public class GameStateService : MonoBehaviour, IServiceInstaller
    {
        [SerializeField] private GameState _gameState = GameState.Pause;

        private GameStateManager _gameStateManager;

        void IServiceInstaller.Install()
        {
            _gameStateManager = new GameStateManager();

            foreach (var rootGameObject in gameObject.scene.GetRootGameObjects())
            {
                foreach (var gameEvent in rootGameObject.GetComponentsInChildren<IGameEvent>())
                {
                    _gameStateManager.Register(gameEvent);
                }
            }

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