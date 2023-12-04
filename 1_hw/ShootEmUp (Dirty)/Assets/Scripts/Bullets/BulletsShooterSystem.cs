using System;
using System.Collections.Generic;
using System.Linq;
using GameLoop;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletsShooterSystem : MonoBehaviour, IGameEvent.IEndGameListener
    {
        public event Action<GameObject> OnNewBulletAddedListener;
        public event Action<GameObject> OnBulletRemovedListener;

        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly HashSet<BulletComponent> _activeBullets = new();

        public void ShootBullet(BulletData bulletData)
        {
            var bullet = _bulletPool.DequeueBullet(bulletData);
            if (_activeBullets.Add(bullet))
            {
                OnNewBulletAddedListener?.Invoke(bullet.gameObject);
                bullet.GetComponent<BulletCollisionHandler>().OnBulletCollidedListener += RemoveBullet;

                var outOfBoundsObserver = bullet.GetComponent<OutOfBoundsHandler>();
                outOfBoundsObserver.LevelBounds = _levelBounds;
                outOfBoundsObserver.OnBoundsIntersectListener += RemoveBullet;
            }
        }

        private void RemoveBullet(BulletComponent bulletComponent)
        {
            if (!_activeBullets.Remove(bulletComponent)) return;
            OnBulletRemovedListener?.Invoke(bulletComponent.gameObject);
            bulletComponent.GetComponent<BulletCollisionHandler>().OnBulletCollidedListener -= RemoveBullet;
            bulletComponent.GetComponent<OutOfBoundsHandler>().OnBoundsIntersectListener -= RemoveBullet;
            _bulletPool.EnqueueBullet(bulletComponent);
        }

        void IGameEvent.IEndGameListener.OnEndGame()
        {
            _activeBullets.ToList().ForEach(RemoveBullet);
        }
    }
}