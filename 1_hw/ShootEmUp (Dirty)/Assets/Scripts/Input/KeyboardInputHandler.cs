using UnityEngine;

namespace ShootEmUp
{
    public class KeyboardInputHandler : IInputHandler
    {
        public Vector2 GetMoveDirection()
        {
            var result = Vector2.zero;

            if (Input.GetKey(KeyCode.LeftArrow))
                result.x -= 1;

            if (Input.GetKey(KeyCode.RightArrow))
                result.x += 1;

            return result;
        }

        public bool ShouldShoot()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }
}