using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveObserver : MonoBehaviour
    {
        [SerializeField] private EnemyMoveAgent enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent enemyAttackAgent;

        private void OnEnable()
        {
            enemyMoveAgent.OnDestinationReachedListener += enemyAttackAgent.EnableAttacking;
        }

        private void OnDisable()
        {
            enemyMoveAgent.OnDestinationReachedListener -= enemyAttackAgent.EnableAttacking;
        }
    }
}