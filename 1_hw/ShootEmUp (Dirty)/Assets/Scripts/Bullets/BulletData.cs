using UnityEngine;

namespace ShootEmUp
{
    public struct BulletData
    {
        public readonly Vector2 Position;
        public readonly Vector2 Velocity;
        public readonly Color Color;
        public readonly int PhysicsLayer;
        public readonly int Damage;
        public bool IsPlayer { get; private set; }

        private BulletData(Vector2 position, Vector2 velocity, Color color, int physicsLayer, int damage)
        {
            Position = position;
            Velocity = velocity;
            Color = color;
            PhysicsLayer = physicsLayer;
            Damage = damage;
            IsPlayer = true;
        }

        public static BulletData GetEnemyBulletData(BulletConfig bulletConfig, Vector2 position, Vector2 direction)
        {
            var bd = MapConfigToData(bulletConfig, position, direction);
            bd.IsPlayer = false;
            return bd;
        }

        public static BulletData GetCharacterBulletData(BulletConfig bulletConfig, Vector2 position, Vector2 direction)
        {
            var bd = MapConfigToData(bulletConfig, position, direction);
            bd.IsPlayer = true;
            return bd;
        }

        private static BulletData MapConfigToData(BulletConfig bulletConfig, Vector2 position, Vector2 direction)
        {
            return new BulletData(
                position: position,
                velocity: direction * bulletConfig.speed,
                color: bulletConfig.color,
                physicsLayer: (int)bulletConfig.physicsLayer,
                damage: bulletConfig.damage
            );
        }
    }
}