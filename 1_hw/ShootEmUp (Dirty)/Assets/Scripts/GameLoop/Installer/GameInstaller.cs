using GameLoop;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private GameStateService _gameStateService;
        [SerializeField] private GameLifeCycleService _gameLifeCycleService;

        private void Awake()
        {
            Install(_gameLifeCycleService);
            Install(_gameStateService);
        }

        private static void Install(IServiceInstaller serviceInstaller)
        {
            serviceInstaller.Install();
        }
    }
}