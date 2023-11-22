using UnityEngine;

namespace ShootEmUp
{
    public class DamageDealerComponent : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent hitPointsComponent;

        public void DealDamage(int damage)
        {
            hitPointsComponent.TakeDamage(damage);
        }
    }
}