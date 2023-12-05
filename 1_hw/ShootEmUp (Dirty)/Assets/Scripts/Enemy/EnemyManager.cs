using System;
using System.Collections.Generic;
using System.Linq;
using GameLoop;
using LifeCycle;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour,
        ILifeCycle.IFixedUpdateListener,
        ILifeCycle.ICreateListener,
        IGameEvent.IStartGameListener,
        IGameEvent.IPauseGameListener,
        IGameEvent.IEndGameListener
    {
        public event Action<GameObject> OnNewEnemyAddedListener;
        public event Action<GameObject> OnEnemyRemovedListener;

        [SerializeField] private FixedPool _fixedPool;
        [SerializeField] private float _spawnTimeInterval = 1;
        [SerializeField] private EnemyInstaller _enemyInstaller;

        private readonly HashSet<GameObject> _activeEnemies = new();
        private Timer _timer;

        void IGameEvent.IStartGameListener.OnGameStarted()
        {
            _timer?.Start();
        }

        void IGameEvent.IPauseGameListener.OnGamePaused()
        {
            _timer?.Stop();
        }

        void ILifeCycle.ICreateListener.OnCreate()
        {
            _timer = new Timer(_spawnTimeInterval, doOnLap: TrySpawnEnemy);
        }

        void IGameEvent.IEndGameListener.OnEndGame()
        {
            _activeEnemies.ToList().ForEach(OnDestroyed);
            _timer?.Stop();
        }

        void ILifeCycle.IFixedUpdateListener.OnFixedUpdate(float deltaTime)
        {
            _timer?.InvalidateLeftTime(Time.fixedDeltaTime);
        }

        private void TrySpawnEnemy()
        {
            if (!_fixedPool.TryDequeue(out var enemy)) return;
            _enemyInstaller.InstallEnemy(enemy);
            OnNewEnemyAddedListener?.Invoke(enemy);
            if (_activeEnemies.Add(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmptyListener += OnDestroyed;
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (!_activeEnemies.Remove(enemy))
            {
                return;
            }

            OnEnemyRemovedListener?.Invoke(enemy);
            enemy.GetComponent<HitPointsComponent>().OnHpEmptyListener -= OnDestroyed;
            _fixedPool.Enqueue(enemy);
        }
    }
}