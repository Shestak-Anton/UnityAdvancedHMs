using LifeCycle;
using ShootEmUp;
using UnityEngine;

namespace GameLoop
{
    public sealed class EnemySpawnObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private GameLifeCycleService _gameLifeCycleService;
        [SerializeField] private GameStateService _gameStateService;

        [SerializeField] private EnemyManager _enemyManager;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            _enemyManager.OnNewEnemyAdded += _gameLifeCycleService.AttachToLifecycle;
            _enemyManager.OnEnemyRemoved += _gameLifeCycleService.DetachFromLifeCycle;
            _enemyManager.OnNewEnemyAdded += _gameStateService.AttachObject;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            _enemyManager.OnNewEnemyAdded -= _gameLifeCycleService.AttachToLifecycle;
            _enemyManager.OnEnemyRemoved -= _gameLifeCycleService.DetachFromLifeCycle;
            _enemyManager.OnNewEnemyAdded -= _gameStateService.AttachObject;
        }
    }
}