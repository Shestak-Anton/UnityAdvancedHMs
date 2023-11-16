using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnHitPointsDrained;

        [SerializeField] private int hitPoints;

        public bool IsHitPointsExists()
        {
            return hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            InvalidateStatus();
        }

        private void InvalidateStatus()
        {
            var isDead = GetHpStatus() == Status.Dead;
            if (isDead)
                OnHitPointsDrained?.Invoke(gameObject);
        }

        private Status GetHpStatus() => hitPoints > 0 ? Status.Alive : Status.Dead;

        private enum Status
        {
            Alive,
            Dead
        }
    }
}