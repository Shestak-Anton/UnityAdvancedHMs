using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private InputManager inputManager;

        private bool _fireRequired;

        private void OnEnable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
            inputManager.OnPositionChangedListener += Move;
            inputManager.OnShootListener += ShootListener;
        }

        private void OnDisable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
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
            _fireRequired = true;
        }


        private void FixedUpdate()
        {
            if (this._fireRequired)
            {
                this.OnFlyBullet();
                this._fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int)this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
    }
}