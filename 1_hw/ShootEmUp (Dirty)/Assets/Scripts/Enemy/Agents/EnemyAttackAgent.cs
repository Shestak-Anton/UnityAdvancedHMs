using GameLoop;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour,
        ILifeCycle.IFixedUpdateListener,
        ILifeCycle.ICreateListener,
        IGameEvent.IStartGameListener,
        IGameEvent.IPauseGameListener,
        IGameEvent.IEndGameListener
    {
        [SerializeField] private float countdown;
        [SerializeField] private WeaponComponent weaponComponent;

        private GameObject _target;
        private bool _isAttackEnabled;

        private Timer _timer;

        void ILifeCycle.ICreateListener.OnCreate()
        {
            _timer = new Timer(countdown, doOnLap: Fire);
        }

        void IGameEvent.IStartGameListener.OnGameStarted()
        {
            _timer?.Start();
        }

        void IGameEvent.IPauseGameListener.OnGamePaused()
        {
            _timer?.Stop();
        }

        void IGameEvent.IEndGameListener.OnEndGame()
        {
            _timer?.Stop();
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void EnableAttacking()
        {
            _isAttackEnabled = true;
            _timer?.Start();
        }

        private void Fire()
        {
            weaponComponent.ShootAtTarget(_target.transform.position);
        }

        void ILifeCycle.IFixedUpdateListener.OnFixedUpdate(float deltaTime)
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