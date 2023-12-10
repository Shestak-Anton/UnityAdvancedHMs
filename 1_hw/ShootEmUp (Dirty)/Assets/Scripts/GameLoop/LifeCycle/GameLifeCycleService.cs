using DefaultNamespace;
using LifeCycle;
using UnityEngine;

namespace GameLoop
{
    public class GameLifeCycleService : MonoBehaviour
    {
        [SerializeField] private GameStateService _gameStateService;

        private LifeCycleComponentsRegistry _componentsRegistry;

        // void IServiceInstaller.Install()
        // {
        //     _componentsRegistry = new LifeCycleComponentsRegistry();
        //     foreach (var rootGameObject in gameObject.scene.GetRootGameObjects())
        //     {
        //         foreach (var lifeCycle in rootGameObject.GetComponentsInChildren<ILifeCycle>())
        //         {
        //             Register(lifeCycle);
        //         }
        //     }
        //
        //     _componentsRegistry.PerformCreation();
        //     _componentsRegistry.PerformEnable();
        // }

        public void Init(LifeCycleComponentsRegistry componentsRegistry)
        {
            _componentsRegistry = componentsRegistry;
            _componentsRegistry.PerformCreation();
            _componentsRegistry.PerformEnable();
        }

        private void Update()
        {
            if (_gameStateService.GameState != GameState.GamePlay) return;
            _componentsRegistry.PerformUpdate();
        }

        private void FixedUpdate()
        {
            if (_gameStateService.GameState != GameState.GamePlay) return;
            _componentsRegistry.PerformFixedUpdate(Time.deltaTime);
        }

        public void AttachToLifecycle(GameObject sceneInstance)
        {
            foreach (var lifeCycle in sceneInstance.GetComponentsInChildren<ILifeCycle>())
            {
                if (lifeCycle is ILifeCycle.ICreateListener createListener)
                {
                    createListener.OnCreate();
                }

                if (lifeCycle is ILifeCycle.IEnableListener enableListener)
                {
                    enableListener.OnEnable();
                }

                Register(lifeCycle);
            }
        }

        public void DetachFromLifeCycle(GameObject sceneInstance)
        {
            foreach (var lifeCycle in sceneInstance.GetComponentsInChildren<ILifeCycle>())
            {
                if (lifeCycle is ILifeCycle.IDisableListener disableListener)
                {
                    disableListener.OnDisable();
                }

                Unregister(lifeCycle);
            }
        }

        public void Register(ILifeCycle lifeCycle)
        {
            _componentsRegistry.AddComponent(lifeCycle);
        }

        private void Unregister(ILifeCycle lifeCycle)
        {
            _componentsRegistry.RemoveComponent(lifeCycle);
        }
    }
}