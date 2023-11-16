using UnityEngine;

namespace ShootEmUp
{
    public struct BulletData
    {
        public Vector2 Position;
        public Vector2 Direction;
        public float Speed;
        public Color Color;
        public int PhysicsLayer;
        public int Damage;
        public bool IsPlayer;

        public Vector2 GetVelocity()
        {
            return Direction * Speed;
        }

    }
}