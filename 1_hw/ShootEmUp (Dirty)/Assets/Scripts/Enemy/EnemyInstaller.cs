using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyInstaller : MonoBehaviour
    {
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObject attackTarget;
        [SerializeField] private Transform worldTransform;

        public void InstallEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(worldTransform);

            var spawnPosition = enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(attackTarget);
        }
    }
}