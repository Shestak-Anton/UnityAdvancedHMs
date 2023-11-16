using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static void DealDamage(Bullet bullet, GameObject target)
        {
            if (!target.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (bullet.IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (target.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bullet.Damage);
            }
        }
    }
}