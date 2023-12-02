using LifeCycle;
using UnityEngine;

namespace GameLoop
{
    public class GameStateService : MonoBehaviour, ILifeCycle.ICreateListener
    {
        private GameStateManager _gameStateManager;

        void ILifeCycle.ICreateListener.OnCreate()
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

        public void OnObjectCreated(GameObject sceneInstance)
        {
            foreach (var gameEvent in sceneInstance.GetComponentsInChildren<IGameEvent>())
            {
                _gameStateManager.Register(gameEvent);
            }
        }

        public void OnObjectRemoved(GameObject sceneInstance)
        {
            foreach (var gameEvent in sceneInstance.GetComponentsInChildren<IGameEvent>())
            {
                _gameStateManager.Unregister(gameEvent);
            }
        }
    }
}