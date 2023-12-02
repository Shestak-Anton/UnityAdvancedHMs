using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterShootObserver : MonoBehaviour,
        ILifeCycle.IEnableListener,
        ILifeCycle.IDisableListener
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private WeaponComponent weaponComponent;

        void ILifeCycle.IEnableListener.OnEnable()
        {
            inputManager.OnShootListener += Shoot;
        }

        void ILifeCycle.IDisableListener.OnDisable()
        {
            inputManager.OnShootListener -= Shoot;
        }

        private void Shoot()
        {
            weaponComponent.Shoot(direction: Vector3.up);
        }
    }
}