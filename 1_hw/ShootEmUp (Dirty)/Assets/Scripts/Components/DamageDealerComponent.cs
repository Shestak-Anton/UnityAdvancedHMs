using UnityEngine;

namespace ShootEmUp
{
    public sealed class DamageDealerComponent : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;

        public void DealDamage(int damage)
        {
            _hitPointsComponent.TakeDamage(damage);
        }
    }
}