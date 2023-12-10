using System;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class OutOfBoundsHandler : MonoBehaviour, ILifeCycle.IFixedUpdateListener
    {
        public event Action<BulletComponent> OnBoundsIntersect;

        [SerializeField] private BulletComponent _bullet;
        [NonSerialized] public LevelBounds LevelBounds;

        void ILifeCycle.IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            if (!LevelBounds.InBounds(_bullet.transform.position))
            {
                OnBoundsIntersect?.Invoke(_bullet);
            }
        }
    }
}