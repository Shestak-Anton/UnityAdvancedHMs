using GameLoop;
using GameLoop.LifeCycle;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private GameLifeCycleServiceInstaller _gameLifeCycleServiceInstaller;
        [SerializeField] private GameStateServiceInstaller _gameStateServiceInstaller;

        private void Awake()
        {
            Install(_gameLifeCycleServiceInstaller);
            Install(_gameStateServiceInstaller);
        }

        private static void Install(IServiceInstaller serviceInstaller)
        {
            serviceInstaller.Install();
        }
    }
}