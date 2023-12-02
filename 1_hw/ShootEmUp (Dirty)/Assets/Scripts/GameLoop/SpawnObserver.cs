using ShootEmUp;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameLoop
{
    public sealed class SpawnObserver : MonoBehaviour
    {
        [SerializeField] private GameLifeCycleManager _gameLifeCycleManager;
        
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private BulletsShooterSystem _bulletsShooterSystem;

        private void OnEnable()
        {
            _enemyManager.OnNewEnemyAddedListener += _gameLifeCycleManager.OnObjectCreated;
            _enemyManager.OnEnemyRemovedListener += _gameLifeCycleManager.OnObjectRemoved;

            _bulletsShooterSystem.OnNewBulletAddedListener += _gameLifeCycleManager.OnObjectCreated;
            _bulletsShooterSystem.OnBulletRemovedListener += _gameLifeCycleManager.OnObjectRemoved;
        }

        private void OnDisable()
        {
            _enemyManager.OnNewEnemyAddedListener -= _gameLifeCycleManager.OnObjectCreated;
            _enemyManager.OnEnemyRemovedListener -= _gameLifeCycleManager.OnObjectRemoved;

            _bulletsShooterSystem.OnNewBulletAddedListener -= _gameLifeCycleManager.OnObjectCreated;
            _bulletsShooterSystem.OnBulletRemovedListener -= _gameLifeCycleManager.OnObjectRemoved;
        }
    }
}