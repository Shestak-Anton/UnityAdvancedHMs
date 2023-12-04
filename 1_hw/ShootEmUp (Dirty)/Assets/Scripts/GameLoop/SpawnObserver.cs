using LifeCycle;
using ShootEmUp;
using UnityEngine;

namespace GameLoop
{
    public sealed class SpawnObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private GameLifeCycleService _gameLifeCycleService;
        [SerializeField] private GameStateService _gameStateService;

        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private BulletsShooterSystem _bulletsShooterSystem;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            _enemyManager.OnNewEnemyAddedListener += _gameLifeCycleService.AttachToLifecycle;
            _enemyManager.OnEnemyRemovedListener += _gameLifeCycleService.DetachFromLifeCycle;

            _bulletsShooterSystem.OnNewBulletAddedListener += _gameLifeCycleService.AttachToLifecycle;
            _bulletsShooterSystem.OnBulletRemovedListener += _gameLifeCycleService.DetachFromLifeCycle;


            _enemyManager.OnNewEnemyAddedListener += _gameStateService.AttachObject;

            _bulletsShooterSystem.OnNewBulletAddedListener += _gameStateService.AttachObject;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            _enemyManager.OnNewEnemyAddedListener -= _gameLifeCycleService.AttachToLifecycle;
            _enemyManager.OnEnemyRemovedListener -= _gameLifeCycleService.DetachFromLifeCycle;

            _bulletsShooterSystem.OnNewBulletAddedListener -= _gameLifeCycleService.AttachToLifecycle;
            _bulletsShooterSystem.OnBulletRemovedListener -= _gameLifeCycleService.DetachFromLifeCycle;

            _enemyManager.OnNewEnemyAddedListener -= _gameStateService.AttachObject;

            _bulletsShooterSystem.OnNewBulletAddedListener -= _gameStateService.AttachObject;
        }
    }
}