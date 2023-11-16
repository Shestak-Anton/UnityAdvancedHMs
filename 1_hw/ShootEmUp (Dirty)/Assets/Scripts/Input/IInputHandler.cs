using UnityEngine;

namespace ShootEmUp
{
    public interface IInputHandler
    {
        Vector2 GetMoveDirection();
        bool ShouldShoot();
    }
}