using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletsShooterSystem : MonoBehaviour
    {

        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private LevelBounds levelBounds;
        
        private readonly HashSet<BulletComponent> _activeBullets = new();
        
        public void ShootBullet(BulletData bulletData)
        {
            var bullet = bulletPool.DequeueBullet(bulletData);
            if (_activeBullets.Add(bullet))
            {
                bullet.GetComponent<BulletCollisionHandler>().OnBulletCollidedListener += RemoveBullet;
        
                var outOfBoundsObserver = bullet.GetComponent<OutOfBoundsHandler>();
                outOfBoundsObserver.LevelBounds = levelBounds;
                outOfBoundsObserver.OnBoundsIntersectListener += RemoveBullet;
            }
        }
        
        private void RemoveBullet(BulletComponent bulletComponent)
        {
            if (!_activeBullets.Remove(bulletComponent)) return;

            bulletComponent.GetComponent<BulletCollisionHandler>().OnBulletCollidedListener -= RemoveBullet;
            bulletComponent.GetComponent<OutOfBoundsHandler>().OnBoundsIntersectListener -= RemoveBullet;
            bulletPool.EnqueueBullet(bulletComponent);
        }

    }
}