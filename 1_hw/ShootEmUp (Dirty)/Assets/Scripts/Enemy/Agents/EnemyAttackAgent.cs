using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);

        public event FireHandler OnFire;

        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private BulletConfig bulletConfig;

        private GameObject target;
        private float currentTime;

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void Reset()
        {
            this.currentTime = this.countdown;
        }

        private void FixedUpdate()
        {
            if (!this.moveAgent.IsReached)
            {
                return;
            }
            
            if (!this.target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                this.Fire();
                this.currentTime += this.countdown;
            }
        }

        private void Fire()
        {
            weaponComponent.ShootAtTarget(target.transform.position);
        }
    }
}