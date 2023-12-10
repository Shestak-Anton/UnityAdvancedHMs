using DefaultNamespace;
using UnityEngine;

namespace GameLoop
{
    public class GameStateServiceInstaller : MonoBehaviour, IServiceInstaller
    {
        [SerializeField] private GameStateService _gameStateService;

        void IServiceInstaller.Install()
        {
            var gameStateManager = new GameStateManager();

            foreach (var rootGameObject in gameObject.scene.GetRootGameObjects())
            {
                foreach (var gameEvent in rootGameObject.GetComponentsInChildren<IGameEvent>())
                {
                    gameStateManager.Register(gameEvent);
                }
            }

            _gameStateService.Init(gameStateManager);
        }
    }
}