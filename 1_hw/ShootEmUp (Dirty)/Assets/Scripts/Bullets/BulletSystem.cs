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

        private readonly Queue<BulletComponent> m_bulletPool = new();
        private readonly HashSet<BulletComponent> m_activeBullets = new();
        private readonly List<BulletComponent> m_cache = new();

        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }

        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(this.m_activeBullets);

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                var bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletData bulletData)
        {
            if (this.m_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }
            bullet.BulletData = bulletData;

            if (this.m_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.OnBulletCollision;
            }
        }

        private void OnBulletCollision(BulletComponent bulletComponent, Collision2D collision)
        {
            BulletUtils.DealDamage(bulletComponent, collision.gameObject);
            this.RemoveBullet(bulletComponent);
        }

        private void RemoveBullet(BulletComponent bulletComponent)
        {
            if (this.m_activeBullets.Remove(bulletComponent))
            {
                bulletComponent.OnCollisionEntered -= this.OnBulletCollision;
                bulletComponent.transform.SetParent(this.container);
                this.m_bulletPool.Enqueue(bulletComponent);
            }
        }
    }
}