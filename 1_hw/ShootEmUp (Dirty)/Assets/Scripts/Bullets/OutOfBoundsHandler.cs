using System;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class OutOfBoundsHandler : MonoBehaviour, ILifeCycle.IFixedUpdateListener
    {
        public event Action<BulletComponent> OnBoundsIntersectListener;

        [SerializeField] private BulletComponent _bullet;
        [NonSerialized] public LevelBounds LevelBounds;

        void ILifeCycle.IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            if (!LevelBounds.InBounds(_bullet.transform.position))
            {
                OnBoundsIntersectListener?.Invoke(_bullet);
            }
        }
    }
}