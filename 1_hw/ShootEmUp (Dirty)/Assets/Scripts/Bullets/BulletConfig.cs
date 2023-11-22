using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletConfig : ScriptableObject
    {
        [SerializeField] public Team team = Team.None;

        [SerializeField] public PhysicsLayer physicsLayer;

        [SerializeField] public Color color;

        [SerializeField] public int damage;

        [SerializeField] public float speed;
    }
}