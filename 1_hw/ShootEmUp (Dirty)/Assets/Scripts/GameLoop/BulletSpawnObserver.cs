using LifeCycle;
using ShootEmUp;
using UnityEngine;

namespace GameLoop
{
    public class BulletSpawnObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private GameLifeCycleService _gameLifeCycleService;
        [SerializeField] private GameStateService _gameStateService;

        [SerializeField] private BulletsShooterSystem _bulletsShooterSystem;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            _bulletsShooterSystem.OnNewBulletAdded += _gameLifeCycleService.AttachToLifecycle;
            _bulletsShooterSystem.OnBulletRemoved += _gameLifeCycleService.DetachFromLifeCycle;
            _bulletsShooterSystem.OnNewBulletAdded += _gameStateService.AttachObject;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            _bulletsShooterSystem.OnNewBulletAdded -= _gameLifeCycleService.AttachToLifecycle;
            _bulletsShooterSystem.OnBulletRemoved -= _gameLifeCycleService.DetachFromLifeCycle;
            _bulletsShooterSystem.OnNewBulletAdded -= _gameStateService.AttachObject;
        }
    }
}