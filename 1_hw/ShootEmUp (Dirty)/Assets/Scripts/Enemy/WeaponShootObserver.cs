using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponShootObserver : MonoBehaviour
    {
        [SerializeField] private WeaponComponent weaponComponent;

        private BulletsShooterSystem _bulletsShooterSystem;

        private void Awake()
        {
            _bulletsShooterSystem = FindObjectOfType<BulletsShooterSystem>();
        }

        private void OnEnable()
        {
            weaponComponent.OnBulletShootListener += _bulletsShooterSystem.ShootBullet;
        }

        private void OnDisable()
        {
            weaponComponent.OnBulletShootListener -= _bulletsShooterSystem.ShootBullet;
        }
    }
}