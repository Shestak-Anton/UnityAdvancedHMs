using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private BulletConfig bulletConfig;

        private Vector2 Position => firePoint.position;
        private Quaternion Rotation => firePoint.rotation;

        private BulletSystem _bulletSystem;

        private void Awake()
        {
            _bulletSystem = FindObjectOfType<BulletSystem>();
        }

        public void ShootAtTarget(Vector2 target)
        {
            var vector = target - Position;
            Shoot(vector.normalized);
        }

        public void Shoot(Vector3 direction)
        {
            var bullet = BulletData.FabricateBulletData(bulletConfig, Position, direction);
            _bulletSystem.ShootBullet(bullet);
        }
    }
}