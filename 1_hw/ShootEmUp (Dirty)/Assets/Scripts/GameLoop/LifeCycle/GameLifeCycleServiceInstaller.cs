using DefaultNamespace;
using LifeCycle;
using UnityEngine;

namespace GameLoop.LifeCycle
{
    public class GameLifeCycleServiceInstaller : MonoBehaviour, IServiceInstaller
    {
        [SerializeField] private GameLifeCycleService _gameLifeCycleService;
        
        void IServiceInstaller.Install()
        {
            var componentsRegistry = new LifeCycleComponentsRegistry();
            foreach (var rootGameObject in gameObject.scene.GetRootGameObjects())
            {
                foreach (var lifeCycle in rootGameObject.GetComponentsInChildren<ILifeCycle>())
                {
                    componentsRegistry.AddComponent(lifeCycle);
                }
            }
            _gameLifeCycleService.Init(componentsRegistry);
        }
    }
}