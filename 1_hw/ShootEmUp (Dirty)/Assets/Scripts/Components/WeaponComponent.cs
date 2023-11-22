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
            var bullet = BulletData.GetCharacterBulletData(_bulletConfig, Position, Rotation * Vector3.up);
            _bulletSystem.FlyBulletByArgs(bullet);
        }
    }
}