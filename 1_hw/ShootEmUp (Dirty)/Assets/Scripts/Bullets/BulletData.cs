using UnityEngine;

namespace ShootEmUp
{
    public readonly struct BulletData
    {
        public readonly Vector2 Position;
        public readonly Vector2 Velocity;
        public readonly Color Color;
        public readonly int PhysicsLayer;
        public readonly int Damage;
        public readonly Team Emitter;

        private BulletData(Vector2 position, Vector2 velocity, Color color, int physicsLayer, int damage, Team emitter)
        {
            Position = position;
            Velocity = velocity;
            Color = color;
            PhysicsLayer = physicsLayer;
            Damage = damage;
            Emitter = emitter;
        }

        public static BulletData FabricateBulletData(
            BulletConfig bulletConfig,
            Vector2 position,
            Vector2 direction
        )
        {
            return MapConfigToData(bulletConfig, position, direction);
        }

        private static Vector2 GetDirectionByEmitter(Team emitter)
        {
            return emitter == Team.Player ? Vector2.up : Vector2.one;
        }

        private static BulletData MapConfigToData(BulletConfig bulletConfig, Vector2 position, Vector3 rotation)
        {
            return new BulletData(
                position: position,
                velocity: rotation * bulletConfig.speed,
                color: bulletConfig.color,
                physicsLayer: (int)bulletConfig.physicsLayer,
                damage: bulletConfig.damage,
                emitter: bulletConfig.team
            );
        }
    }
}