using System;
using GameLoop;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletComponent : MonoBehaviour,
        IGameEvent.IPauseGameListener,
        IGameEvent.IStartGameListener,
        IGameEvent.IEndGameListener
    {
        public event Action<BulletComponent, Collision2D> OnCollisionEntered;

        [SerializeField] private new Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public BulletData BulletData
        {
            set => SetBulletData(value);
            get => _bulletData;
        }

        private BulletData _bulletData;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        private void SetBulletData(BulletData bulletData)
        {
            _bulletData = bulletData;
            gameObject.layer = bulletData.PhysicsLayer;
            transform.position = bulletData.Position;
            _spriteRenderer.color = bulletData.Color;
            SetVelocity(bulletData.Velocity);
        }

        private void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        void IGameEvent.IPauseGameListener.OnGamePaused()
        {
            SetVelocity(Vector2.zero);
        }

        void IGameEvent.IStartGameListener.OnGameStarted()
        {
            SetVelocity(_bulletData.Velocity);
        }

        void IGameEvent.IEndGameListener.OnEndGame()
        {
            SetVelocity(Vector2.zero);
        }
    }
}