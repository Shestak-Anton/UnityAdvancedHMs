using System;
using System.Collections.Generic;
using System.Linq;
using GameLoop;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletsShooterSystem : MonoBehaviour, IGameEvent.IEndGameListener
    {
        public event Action<GameObject> OnNewBulletAdded;
        public event Action<GameObject> OnBulletRemoved;

        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly HashSet<BulletComponent> _activeBullets = new();

        public void ShootBullet(BulletData bulletData)
        {
            var bullet = _bulletPool.DequeueBullet(bulletData);
            if (_activeBullets.Add(bullet))
            {
                OnNewBulletAdded?.Invoke(bullet.gameObject);
                bullet.GetComponent<BulletCollisionHandler>().OnBulletCollided += RemoveBullet;

                var outOfBoundsObserver = bullet.GetComponent<OutOfBoundsHandler>();
                outOfBoundsObserver.LevelBounds = _levelBounds;
                outOfBoundsObserver.OnBoundsIntersect += RemoveBullet;
            }
        }

        private void RemoveBullet(BulletComponent bulletComponent)
        {
            if (!_activeBullets.Remove(bulletComponent)) return;
            OnBulletRemoved?.Invoke(bulletComponent.gameObject);
            bulletComponent.GetComponent<BulletCollisionHandler>().OnBulletCollided -= RemoveBullet;
            bulletComponent.GetComponent<OutOfBoundsHandler>().OnBoundsIntersect -= RemoveBullet;
            _bulletPool.EnqueueBullet(bulletComponent);
        }

        void IGameEvent.IEndGameListener.OnEndGame()
        {
            _activeBullets.ToList().ForEach(RemoveBullet);
        }
    }
}