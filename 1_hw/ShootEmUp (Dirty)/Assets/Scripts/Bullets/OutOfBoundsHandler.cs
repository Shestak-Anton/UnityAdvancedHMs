using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class OutOfBoundsHandler : MonoBehaviour
    {
        public event Action<BulletComponent> OnBoundsIntersectListener;

        [SerializeField] private BulletComponent bullet;
        [NonSerialized] public LevelBounds LevelBounds;

        private void FixedUpdate()
        {
            if (!LevelBounds.InBounds(bullet.transform.position))
            {
                OnBoundsIntersectListener?.Invoke(bullet);
            }
        }
    }
}