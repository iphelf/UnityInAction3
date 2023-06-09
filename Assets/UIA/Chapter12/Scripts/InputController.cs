using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    public class InputController : MonoBehaviour
    {
        public float HorizontalMovement()
        {
            return Input.GetAxis("Horizontal");
        }

        public bool OperateButtonDown()
        {
            return Input.GetButtonDown("Fire1");
        }

        public bool MenuButtonDown()
        {
            return Input.GetKeyDown(KeyCode.M);
        }
    }
}