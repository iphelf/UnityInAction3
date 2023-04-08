using UnityEngine;

namespace UIA.Chapter08.Scripts
{
    public class InputController : MonoBehaviour
    {
        public float ViewRotationX()
        {
            return Input.GetAxis("Mouse X");
        }

        public float RightMovement()
        {
            return Input.GetAxis("Horizontal");
        }

        public float ForwardMovement()
        {
            return Input.GetAxis("Vertical");
        }

        public bool JumpButtonDown()
        {
            return Input.GetButtonDown("Jump");
        }
    }
}