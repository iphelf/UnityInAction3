using System;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter02
{
    public class MouseLook : MonoBehaviour
    {
        public enum RotationAxes
        {
            MouseX,
            MouseY,
            MouseXY
        }

        public RotationAxes rotationAxes = RotationAxes.MouseXY;
        public float sensitivityHorizontal = 9.0f;
        public float sensitivityVertical = 9.0f;
        public float verticalMin = -45.0f;
        public float verticalMax = 45.0f;

        private float _rotationHorizontal = 0.0f;
        private float _rotationVertical = 0.0f;

        private bool _ignoreInputs = false;

        // Start is called before the first frame update
        void Start()
        {
            TryGetComponent(out Rigidbody body);
            if (body is not null)
                body.freezeRotation = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_ignoreInputs) return;

            switch (rotationAxes)
            {
                case RotationAxes.MouseX:
                    transform.Rotate(0.0f, Input.GetAxis("Mouse X") * sensitivityHorizontal, 0.0f);
                    break;
                case RotationAxes.MouseY:
                    _rotationVertical -= Input.GetAxis("Mouse Y") * sensitivityVertical;
                    _rotationVertical = Mathf.Clamp(_rotationVertical, verticalMin, verticalMax);
                    transform.localEulerAngles = new Vector3(_rotationVertical, 0.0f, 0.0f);
                    break;
                case RotationAxes.MouseXY:
                    _rotationVertical -= Input.GetAxis("Mouse Y") * sensitivityVertical;
                    _rotationVertical = Mathf.Clamp(_rotationVertical, verticalMin, verticalMax);
                    _rotationHorizontal += Input.GetAxis("Mouse X") * sensitivityHorizontal;
                    if (_rotationHorizontal > 720.0f) _rotationHorizontal -= 720.0f;
                    else if (_rotationHorizontal < -720.0f) _rotationHorizontal += 720.0f;
                    transform.localEulerAngles = new Vector3(_rotationVertical, _rotationHorizontal, 0.0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void IgnoreInputs(bool ignoreInputs)
        {
            _ignoreInputs = ignoreInputs;
        }
    }
}