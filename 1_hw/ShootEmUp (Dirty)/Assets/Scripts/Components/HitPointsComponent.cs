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

        [SerializeField] private int hitPoints;

        private int _initialHitPoints;
        
        public void OnCreate()
        {
            _initialHitPoints = hitPoints;
        }
        
        public bool IsHitPointsExists()
        {
            return hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                OnHpEmptyListener?.Invoke(gameObject);
            }
        }

         void IGameEvent.IStartGameListener.OnGameStarted()
        {
            if (!IsHitPointsExists())
            {
                hitPoints = _initialHitPoints;
            }   
        }

 
    }
}