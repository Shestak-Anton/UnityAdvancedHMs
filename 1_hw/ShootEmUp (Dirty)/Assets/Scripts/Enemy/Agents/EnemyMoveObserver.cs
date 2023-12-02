using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private EnemyMoveAgent enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent enemyAttackAgent;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            enemyMoveAgent.OnDestinationReachedListener += enemyAttackAgent.EnableAttacking;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            enemyMoveAgent.OnDestinationReachedListener -= enemyAttackAgent.EnableAttacking;
        }
    }
}