using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponShootObserver : MonoBehaviour,
        ILifeCycle.ICreateListener,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private WeaponComponent _weaponComponent;

        private BulletsShooterSystem _bulletsShooterSystem;

        void ILifeCycle.ICreateListener.OnCreate()
        {
            _bulletsShooterSystem = FindObjectOfType<BulletsShooterSystem>();
        }

        void ILifeCycle.IEnableListener.OnEnable()
        {
            _weaponComponent.OnBulletShootListener += _bulletsShooterSystem.ShootBullet;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            _weaponComponent.OnBulletShootListener -= _bulletsShooterSystem.ShootBullet;
        }
    }
}