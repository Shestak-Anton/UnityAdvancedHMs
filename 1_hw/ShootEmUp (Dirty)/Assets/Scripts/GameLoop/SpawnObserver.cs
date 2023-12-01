using ShootEmUp;
using UnityEngine;

namespace GameLoop
{
    public sealed class SpawnObserver : MonoBehaviour
    {
        [SerializeField] private GameLoopManager _gameLoopManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private BulletsShooterSystem _bulletsShooterSystem;

        private void OnEnable()
        {
            _enemyManager.OnNewEnemyAddedListener += _gameLoopManager.OnObjectCreated;
            _enemyManager.OnEnemyRemovedListener += _gameLoopManager.OnObjectRemoved;
            
            _bulletsShooterSystem.OnNewBulletAddedListener += _gameLoopManager.OnObjectCreated;
            _bulletsShooterSystem.OnBulletRemovedListener += _gameLoopManager.OnObjectRemoved;
            
        }

        private void OnDisable()
        {
            _enemyManager.OnNewEnemyAddedListener -= _gameLoopManager.OnObjectCreated;
            _enemyManager.OnEnemyRemovedListener -= _gameLoopManager.OnObjectRemoved;
            
            _bulletsShooterSystem.OnNewBulletAddedListener -= _gameLoopManager.OnObjectCreated;
            _bulletsShooterSystem.OnBulletRemovedListener -= _gameLoopManager.OnObjectRemoved;
        }
    }
}