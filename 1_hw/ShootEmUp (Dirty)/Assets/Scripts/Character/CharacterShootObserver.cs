using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterShootObserver : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private WeaponComponent weaponComponent;

        private void OnEnable()
        {
            inputManager.OnShootListener += Shoot;
        }

        private void OnDisable()
        {
            inputManager.OnShootListener -= Shoot;
        }

        private void Shoot()
        {
            weaponComponent.Shoot(direction: Vector3.up);
        }
    }
}