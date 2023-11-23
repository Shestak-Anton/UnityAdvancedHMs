using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private FixedPool fixedPool;
        [SerializeField] private float spawnTimeInterval = 1;
        [SerializeField] private EnemyInstaller enemyInstaller;

        private readonly HashSet<GameObject> _activeEnemies = new();
        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer(spawnTimeInterval, doOnLap: TrySpawnEnemy);
        }

        private void FixedUpdate()
        {
            _timer.InvalidateLeftTime(Time.fixedDeltaTime);
        }

        private void TrySpawnEnemy()
        {
            var enemy = fixedPool.Dequeue();
            if (enemy == null) return;
            enemyInstaller.InstallEnemy(enemy);
            if (_activeEnemies.Add(enemy)) enemy.GetComponent<HitPointsComponent>().OnHpEmpty += OnDestroyed;
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (!_activeEnemies.Remove(enemy)) return;
            enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= OnDestroyed;
            fixedPool.Enqueue(enemy);
        }
    }
}