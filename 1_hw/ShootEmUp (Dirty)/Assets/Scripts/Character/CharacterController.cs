using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private HitPointsComponent hitPointsComponent;

        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        private bool _fireRequired;

        private void OnEnable()
        {
            hitPointsComponent.hpEmpty += OnCharacterDeath;
            inputManager.OnPositionChangedListener += Move;
            inputManager.OnShootListener += ShootListener;
        }

        private void OnDisable()
        {
            hitPointsComponent.hpEmpty -= OnCharacterDeath;
            inputManager.OnPositionChangedListener -= Move;
            inputManager.OnShootListener -= ShootListener;
        }

        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();

        private void Move(Vector2 direction)
        {
            character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(direction * Time.fixedDeltaTime);
        }

        private void ShootListener()
        {
            var weapon = character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int)_bulletConfig.physicsLayer,
                color = _bulletConfig.color,
                damage = _bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * _bulletConfig.speed
            });
        }
    }
}