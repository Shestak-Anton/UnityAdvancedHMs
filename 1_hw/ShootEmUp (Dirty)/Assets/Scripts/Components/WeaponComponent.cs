using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public event Action<BulletData> OnBulletShoot;

        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;

        private Vector2 Position => _firePoint.position;

        public void ShootAtTarget(Vector2 target)
        {
            var vector = target - Position;
            Shoot(vector.normalized);
        }

        public void Shoot(Vector3 direction)
        {
            var bullet = BulletData.FabricateBulletData(_bulletConfig, Position, direction);
            OnBulletShoot?.Invoke(bullet);
        }
    }
}