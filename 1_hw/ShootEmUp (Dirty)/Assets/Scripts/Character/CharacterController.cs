using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private WeaponComponent weaponComponent;

        private void OnEnable()
        {
            inputManager.OnPositionChangedListener += Move;
            inputManager.OnShootListener += ShootListener;
        }

        private void OnDisable()
        {
            inputManager.OnPositionChangedListener -= Move;
            inputManager.OnShootListener -= ShootListener;
        }

        private void Move(Vector2 velocity)
        {
            character.GetComponent<MoveComponent>().MoveByRigidbodyVelocity(velocity);
        }

        private void ShootListener()
        {
            weaponComponent.Shoot();
        }
    }
}