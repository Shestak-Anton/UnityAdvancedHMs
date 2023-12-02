using ShootEmUp;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameLoop
{
    public sealed class SpawnObserver : MonoBehaviour
    {
        [SerializeField] private GameLifeCycleService _gameLifeCycleService;
        [SerializeField] private GameStateService _gameStateService;

        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private BulletsShooterSystem _bulletsShooterSystem;

        private void OnEnable()
        {
            _enemyManager.OnNewEnemyAddedListener += _gameLifeCycleService.OnObjectCreated;
            _enemyManager.OnEnemyRemovedListener += _gameLifeCycleService.OnObjectRemoved;

            _bulletsShooterSystem.OnNewBulletAddedListener += _gameLifeCycleService.OnObjectCreated;
            _bulletsShooterSystem.OnBulletRemovedListener += _gameLifeCycleService.OnObjectRemoved;


            _enemyManager.OnNewEnemyAddedListener += _gameStateService.OnObjectCreated;
            _enemyManager.OnEnemyRemovedListener += _gameStateService.OnObjectRemoved;

            _bulletsShooterSystem.OnNewBulletAddedListener += _gameStateService.OnObjectCreated;
            _bulletsShooterSystem.OnBulletRemovedListener += _gameStateService.OnObjectRemoved;
        }

        private void OnDisable()
        {
            _enemyManager.OnNewEnemyAddedListener -= _gameLifeCycleService.OnObjectCreated;
            _enemyManager.OnEnemyRemovedListener -= _gameLifeCycleService.OnObjectRemoved;

            _bulletsShooterSystem.OnNewBulletAddedListener -= _gameLifeCycleService.OnObjectCreated;
            _bulletsShooterSystem.OnBulletRemovedListener -= _gameLifeCycleService.OnObjectRemoved;

            _enemyManager.OnNewEnemyAddedListener -= _gameStateService.OnObjectCreated;
            _enemyManager.OnEnemyRemovedListener -= _gameStateService.OnObjectRemoved;

            _bulletsShooterSystem.OnNewBulletAddedListener -= _gameStateService.OnObjectCreated;
            _bulletsShooterSystem.OnBulletRemovedListener -= _gameStateService.OnObjectRemoved;
        }
    }
}