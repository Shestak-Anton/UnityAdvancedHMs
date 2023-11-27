using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;

        [SerializeField] private Transform container;
        [SerializeField] private BulletComponent prefab;
        [SerializeField] private Transform worldTransform;

        private readonly Queue<BulletComponent> _bulletPool = new();

        private void Awake()
        {
            FillPool(initialCount);
        }

        public BulletComponent DequeueBullet(BulletData bulletData)
        {
            if (!_bulletPool.TryDequeue(out var bullet)) return Instantiate(prefab, worldTransform);
            bullet.transform.SetParent(worldTransform);
            bullet.BulletData = bulletData;
            return bullet;
        }

        public void EnqueueBullet(BulletComponent bulletComponent)
        {
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