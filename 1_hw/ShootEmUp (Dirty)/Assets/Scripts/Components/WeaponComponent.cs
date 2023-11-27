using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public event Action<BulletData> OnBulletShootListener;

        [SerializeField] private Transform firePoint;
        [SerializeField] private BulletConfig bulletConfig;

        private Vector2 Position => firePoint.position;

        public void ShootAtTarget(Vector2 target)
        {
            var vector = target - Position;
            Shoot(vector.normalized);
        }

        public void Shoot(Vector3 direction)
        {
            var bullet = BulletData.FabricateBulletData(bulletConfig, Position, direction);
            OnBulletShootListener?.Invoke(bullet);
        }
    }
}