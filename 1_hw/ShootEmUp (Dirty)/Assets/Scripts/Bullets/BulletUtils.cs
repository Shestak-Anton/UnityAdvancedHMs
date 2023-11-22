using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static void DealDamage(BulletComponent bulletComponent, GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (bulletComponent.BulletData.IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bulletComponent.BulletData.Damage);
            }
        }
    }
}