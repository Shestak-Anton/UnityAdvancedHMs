using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private BulletConfig bulletConfig;

        public Vector2 Position => firePoint.position;
        private Quaternion Rotation => firePoint.rotation;

        public void Shoot()
        {
            // todo rework
            var bulletSystem = FindObjectOfType<BulletSystem>();
            bulletSystem.FlyBulletByArgs(new BulletData
            {
                IsPlayer = true,
                PhysicsLayer = (int)bulletConfig.physicsLayer,
                Color = bulletConfig.color,
                Damage = bulletConfig.damage,
                Position = Position,
                Direction = Rotation * Vector3.up,
                Speed = bulletConfig.speed
            });
        }
    }
}