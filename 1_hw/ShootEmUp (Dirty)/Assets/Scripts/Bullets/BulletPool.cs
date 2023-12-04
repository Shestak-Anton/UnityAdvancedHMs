using System.Collections.Generic;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : MonoBehaviour, ILifeCycle.ICreateListener
    {
        [SerializeField] private int _initialCount = 50;

        [SerializeField] private Transform _container;
        [SerializeField] private BulletComponent _prefab;
        [SerializeField] private Transform _worldTransform;

        private readonly Queue<BulletComponent> _bulletPool = new();

        void ILifeCycle.ICreateListener.OnCreate()
        {
            FillPool(_initialCount);
        }

        public BulletComponent DequeueBullet(BulletData bulletData)
        {
            if (!_bulletPool.TryDequeue(out var bullet)) return Instantiate(_prefab, _worldTransform);
            bullet.transform.SetParent(_worldTransform);
            bullet.BulletData = bulletData;
            return bullet;
        }

        public void EnqueueBullet(BulletComponent bulletComponent)
        {
            bulletComponent.transform.SetParent(_container);
            _bulletPool.Enqueue(bulletComponent);
        }

        private void FillPool(int poolSize)
        {
            for (var i = 0; i < poolSize; i++)
            {
                var bullet = Instantiate(_prefab, _container);
                _bulletPool.Enqueue(bullet);
            }
        }
    }
}