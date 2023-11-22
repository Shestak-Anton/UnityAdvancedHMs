using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;

        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<Bullet> m_bulletPool = new();
        private readonly HashSet<Bullet> m_activeBullets = new();
        private readonly List<Bullet> m_cache = new();

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

            bullet.SetPosition(bulletData.Position);
            bullet.SetColor(bulletData.Color);
            bullet.SetPhysicsLayer(bulletData.PhysicsLayer);
            bullet.damage = bulletData.Damage;
            bullet.isPlayer = bulletData.IsPlayer;
            bullet.SetVelocity(bulletData.Velocity);

            if (this.m_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (this.m_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= this.OnBulletCollision;
                bullet.transform.SetParent(this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }

        public struct BulletData
        {
            public readonly Vector2 Position;
            public readonly Vector2 Velocity;
            public readonly Color Color;
            public readonly int PhysicsLayer;
            public readonly int Damage;
            public readonly bool IsPlayer;

            public BulletData(
                Vector2 position,
                Vector2 velocity,
                Color color,
                int physicsLayer,
                int damage,
                bool isPlayer)
            {
                Position = position;
                Velocity = velocity;
                Color = color;
                PhysicsLayer = physicsLayer;
                Damage = damage;
                IsPlayer = isPlayer;
            }
            
        }
    }
}