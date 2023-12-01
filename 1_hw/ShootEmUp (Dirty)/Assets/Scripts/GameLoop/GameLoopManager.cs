using LifeCycle;
using UnityEngine;

namespace GameLoop
{
    public class GameLoopManager : MonoBehaviour
    {
        private GlobalLifeCycleManager _lifeCycleManager;

        private void Awake()
        {
            _lifeCycleManager = new GlobalLifeCycleManager(initialState: LifeCycleState.RESUMED);

            foreach (var lifeCycle in GetComponentsInChildren<ILifeCycle>())
            {
                Register(lifeCycle);
            }
        }

        private void Update()
        {
            _lifeCycleManager.PerformUpdate();
        }

        private void FixedUpdate()
        {
            _lifeCycleManager.PerformFixedUpdate(Time.deltaTime);
        }

        public void OnObjectCreated(GameObject sceneInstance)
        {
            foreach (var lifeCycle in sceneInstance.GetComponentsInChildren<ILifeCycle>())
            {
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
            _lifeCycleManager?.AddComponent(lifeCycle);
        }

        private void Unregister(ILifeCycle lifeCycle)
        {
            _lifeCycleManager.RemoveComponent(lifeCycle);
        }
        
    }
}