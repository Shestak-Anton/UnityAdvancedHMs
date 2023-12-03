using DefaultNamespace;
using UnityEngine;

namespace GameLoop
{
    public class GameStateService : MonoBehaviour, IServiceInstaller
    {
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
        }

        public void ApplyState(GameState gameState)
        {
            _gameStateManager.ApplyState(gameState);
        }

        public void AttachObject(GameObject sceneInstance)
        {
            foreach (var gameEvent in sceneInstance.GetComponentsInChildren<IGameEvent>())
            {
                _gameStateManager.Register(gameEvent);
            }
        }

        public void DetachObject(GameObject sceneInstance)
        {
            foreach (var gameEvent in sceneInstance.GetComponentsInChildren<IGameEvent>())
            {
                _gameStateManager.Unregister(gameEvent);
            }
        }
    }
}