using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyInstaller : MonoBehaviour
    {
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private GameObject _attackTarget;
        [SerializeField] private Transform _worldTransform;

        public void InstallEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_attackTarget);
        }
    }
}