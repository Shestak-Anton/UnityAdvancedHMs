using LifeCycle;
using UnityEngine;

namespace GameLoop
{
    public class GameLifeCycleService : MonoBehaviour
    {
        private GlobalLifeCycleManager _lifeCycleManager;

        private void Awake()
        {
            _lifeCycleManager = new GlobalLifeCycleManager();

            foreach (var rootGameObject in gameObject.scene.GetRootGameObjects())
            {
                foreach (var lifeCycle in rootGameObject.GetComponentsInChildren<ILifeCycle>())
                {
                    Register(lifeCycle);
                }
            }


            _lifeCycleManager.PerformCreation();
        }

        private void OnEnable()
        {
            _lifeCycleManager.PerformEnable();
        }

        private void Update()
        {
            _lifeCycleManager.PerformUpdate();
        }

        private void FixedUpdate()
        {
            _lifeCycleManager.PerformFixedUpdate(Time.deltaTime);
        }

        private void OnDisable()
        {
            _lifeCycleManager.PerformDisable();
        }

        public void OnObjectCreated(GameObject sceneInstance)
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

        public void OnObjectRemoved(GameObject sceneInstance)
        {
            foreach (var lifeCycle in sceneInstance.GetComponentsInChildren<ILifeCycle>())
            {
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