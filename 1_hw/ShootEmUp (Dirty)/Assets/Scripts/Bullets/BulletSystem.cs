using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;

        [SerializeField] private Transform container;
        [SerializeField] private BulletComponent prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<BulletComponent> _bulletPool = new();

        private readonly HashSet<BulletComponent> _activeBullets = new();
        private readonly List<BulletComponent> _cache = new();

        private void Awake()
        {
            FillPool(initialCount);
        }

        private void FixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void ShootBullet(BulletData bulletData)
        {
            var bullet = GetBullet(bulletData);
            if (_activeBullets.Add(bullet))
                bullet.OnCollisionEntered += OnBulletCollision;
        }

        private BulletComponent GetBullet(BulletData bulletData)
        {
            if (!_bulletPool.TryDequeue(out var bullet)) return Instantiate(prefab, worldTransform);
            bullet.transform.SetParent(worldTransform);
            bullet.BulletData = bulletData;
            return bullet;
        }

        private void OnBulletCollision(BulletComponent bulletComponent, Collision2D collision)
        {
            TryDealDamage(collision.gameObject, bulletComponent.BulletData.Damage);
            RemoveBullet(bulletComponent);
        }

        private void TryDealDamage(GameObject collidedGo, int damage)
        {
            if (collidedGo.TryGetComponent(out DamageDealerComponent damageDealer))
            {
                damageDealer.DealDamage(damage);
            }
        }

        private void RemoveBullet(BulletComponent bulletComponent)
        {
            if (!_activeBullets.Remove(bulletComponent)) return;

            bulletComponent.OnCollisionEntered -= OnBulletCollision;
            bulletComponent.transform.SetParent(container);
            _bulletPool.Enqueue(bulletComponent);
        }

        private void FillPool(int poolSize)
        {
            for (var i = 0; i < poolSize; i++)
            {
                var bullet = Instantiate(prefab, container);
                _bulletPool.Enqueue(bullet);
            }
        }
    }
}