using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletComponent : MonoBehaviour
    {
        public event Action<BulletComponent, Collision2D> OnCollisionEntered;

        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

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
            rigidbody2D.velocity = bulletData.Velocity;
            gameObject.layer = bulletData.PhysicsLayer;
            transform.position = bulletData.Position;
            spriteRenderer.color = bulletData.Color;
        }
    }
}