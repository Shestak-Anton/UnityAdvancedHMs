using System;
using GameLoop;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour, 
        IGameEvent.IStartGameListener,
        ILifeCycle.ICreateListener
    {
        public event Action<GameObject> OnHpEmptyListener;

        [SerializeField] private int _hitPoints;

        private int _initialHitPoints;
        
        public void OnCreate()
        {
            _initialHitPoints = _hitPoints;
        }
        
        public bool IsHitPointsExists()
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if (_hitPoints <= 0)
            {
                OnHpEmptyListener?.Invoke(gameObject);
            }
        }

         void IGameEvent.IStartGameListener.OnGameStarted()
        {
            if (!IsHitPointsExists())
            {
                _hitPoints = _initialHitPoints;
            }   
        }

 
    }
}