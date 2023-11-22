using UnityEngine;

namespace ShootEmUp
{
    public class EnemyMoveObserver : MonoBehaviour
    {
        [SerializeField] private EnemyMoveAgent enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent enemyAttackAgent;

        private void OnEnable()
        {
            enemyMoveAgent.DestinationReachedListener += enemyAttackAgent.EnableAttacking;
        }

        private void OnDisable()
        {
            enemyMoveAgent.DestinationReachedListener -= enemyAttackAgent.EnableAttacking;
        }
    }
}