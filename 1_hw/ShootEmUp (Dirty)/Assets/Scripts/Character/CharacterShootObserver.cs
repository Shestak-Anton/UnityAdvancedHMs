using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterShootObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private WeaponComponent _weaponComponent;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            _inputManager.OnShoot += Shoot;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            _inputManager.OnShoot -= Shoot;
        }

        private void Shoot()
        {
            _weaponComponent.Shoot(direction: Vector3.up);
        }
    }
}