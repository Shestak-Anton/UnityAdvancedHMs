using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private Transform firePoint;

        public Vector2 Position => firePoint.position;
        private Quaternion Rotation => firePoint.rotation;

        private BulletSystem _bulletSystem;

        private void Awake()
        {
            _bulletSystem = FindObjectOfType<BulletSystem>();
        }

        public void Shoot()
        {
            var args = new BulletSystem.BulletData(
                isPlayer: true,
                physicsLayer: (int)_bulletConfig.physicsLayer,
                color: _bulletConfig.color,
                damage: _bulletConfig.damage,
                position: Position,
                velocity: Rotation * Vector3.up * _bulletConfig.speed
            );
            _bulletSystem.FlyBulletByArgs(args);
        }
    }
}