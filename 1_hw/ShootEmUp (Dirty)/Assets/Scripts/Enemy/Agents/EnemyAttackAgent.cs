using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, ILifeCycle.IFixedUpdateListener
    {
        [SerializeField] private float countdown;
        [SerializeField] private WeaponComponent weaponComponent;

        private GameObject _target;
        private bool _isAttackEnabled;

        private Timer _timer;
        
        private void Awake()
        {
            _timer = new Timer(countdown, doOnLap: Fire);
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void EnableAttacking()
        {
            _isAttackEnabled = true;
        }

        private void Fire()
        {
            weaponComponent.ShootAtTarget(_target.transform.position);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!_isAttackEnabled)
            {
                return;
            }

            if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            _timer.InvalidateLeftTime(deltaTime);
        }
    }
}