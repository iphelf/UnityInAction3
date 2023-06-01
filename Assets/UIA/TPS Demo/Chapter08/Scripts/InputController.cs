using UnityEngine;

namespace UIA.TPS_Demo.Chapter08.Scripts
{
    public class InputController : MonoBehaviour
    {
        private bool _useUIMode;

        public float ViewRotationX()
        {
            return _useUIMode ? 0 : Input.GetAxis("Mouse X");
        }

        public float RightMovement()
        {
            return _useUIMode ? 0 : Input.GetAxis("Horizontal");
        }

        public float ForwardMovement()
        {
            return _useUIMode ? 0 : Input.GetAxis("Vertical");
        }

        public bool JumpButtonDown()
        {
            return !_useUIMode && Input.GetButtonDown("Jump");
        }

        public bool OperateButtonDown()
        {
            return !_useUIMode && Input.GetButtonDown("Fire1");
        }

        public bool ConfigButtonDown()
        {
            return Input.GetButtonDown("Fire2");
        }

        public bool ConfigButtonUp()
        {
            return Input.GetButtonUp("Fire2");
        }

        public void SwitchMode(bool useUIMode)
        {
            _useUIMode = useUIMode;
        }
    }
}