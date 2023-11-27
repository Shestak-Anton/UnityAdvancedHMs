using UnityEngine;

namespace ShootEmUp
{
    public interface IInputManager
    {
        Vector2 HandlePositionChangeInput();
        bool ShouldShoot();
    }

    public sealed class KeyboardInputManager : IInputManager
    {
        public Vector2 HandlePositionChangeInput()
        {
            var result = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow)) result.x -= 1;
            if (Input.GetKey(KeyCode.RightArrow)) result.x += 1;
            return result;
        }

        public bool ShouldShoot()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}