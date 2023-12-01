using System;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class OutOfBoundsHandler : MonoBehaviour, ILifeCycle.IFixedUpdateListener
    {
        public event Action<BulletComponent> OnBoundsIntersectListener;

        [SerializeField] private BulletComponent bullet;
        [NonSerialized] public LevelBounds LevelBounds;

        void ILifeCycle.IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            if (!LevelBounds.InBounds(bullet.transform.position))
            {
                OnBoundsIntersectListener?.Invoke(bullet);
            }
        }
    }
}