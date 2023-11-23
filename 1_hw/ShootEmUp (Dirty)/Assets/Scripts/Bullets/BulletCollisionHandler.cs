using System;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletCollisionHandler : MonoBehaviour
    {
        public event Action<BulletComponent> OnBulletCollidedListener;

        [SerializeField] private BulletComponent bullet;

        private void OnEnable()
        {
            bullet.OnCollisionEntered += OnBulletCollision;
        }

        private void OnDisable()
        {
            bullet.OnCollisionEntered -= OnBulletCollision;
        }

        private void OnBulletCollision(BulletComponent bulletComponent, Collision2D collision)
        {
            TryDealDamage(collision.gameObject, bulletComponent.BulletData.Damage);
            OnBulletCollidedListener?.Invoke(bulletComponent);
        }

        private static void TryDealDamage(GameObject collidedGo, int damage)
        {
            if (collidedGo.TryGetComponent(out DamageDealerComponent damageDealer))
            {
                damageDealer.DealDamage(damage);
            }
        }
    }
}