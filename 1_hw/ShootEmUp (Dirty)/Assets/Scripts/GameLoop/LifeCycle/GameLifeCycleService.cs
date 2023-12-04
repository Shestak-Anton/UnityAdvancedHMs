using DefaultNamespace;
using LifeCycle;
using UnityEngine;

namespace GameLoop
{
    public class GameLifeCycleService : MonoBehaviour, IServiceInstaller
    {
        private GlobalLifeCycleManager _lifeCycleManager;
        private bool _isInitialized;

        void IServiceInstaller.Install()
        {
            _lifeCycleManager = new GlobalLifeCycleManager();
            _isInitialized = true;
            foreach (var rootGameObject in gameObject.scene.GetRootGameObjects())
            {
                foreach (var lifeCycle in rootGameObject.GetComponentsInChildren<ILifeCycle>())
                {
                    Register(lifeCycle);
                }
            }

            _lifeCycleManager.PerformCreation();
            _lifeCycleManager.PerformEnable();
        }

        private void Update()
        {
            if (!_isInitialized) return;
            _lifeCycleManager.PerformUpdate();
        }

        private void FixedUpdate()
        {
            if (!_isInitialized) return;
            _lifeCycleManager.PerformFixedUpdate(Time.deltaTime);
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

        private void Register(ILifeCycle lifeCycle)
        {
            _lifeCycleManager.AddComponent(lifeCycle);
        }

        private void Unregister(ILifeCycle lifeCycle)
        {
            _lifeCycleManager.RemoveComponent(lifeCycle);
        }
    }
}