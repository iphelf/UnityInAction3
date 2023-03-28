using Unity.VisualScripting;
using UnityEngine;

namespace UIA.Chapter02
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

        private float rotationHorizontal = 0.0f;
        private float rotationVertical = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            Rigidbody body;
            TryGetComponent(out body);
            if (body is not null)
                body.freezeRotation = true;
        }

        // Update is called once per frame
        void Update()
        {
            switch (rotationAxes)
            {
                case RotationAxes.MouseX:
                    transform.Rotate(0.0f, Input.GetAxis("Mouse X") * sensitivityHorizontal, 0.0f);
                    break;
                case RotationAxes.MouseY:
                    rotationVertical -= Input.GetAxis("Mouse Y") * sensitivityVertical;
                    rotationVertical = Mathf.Clamp(rotationVertical, verticalMin, verticalMax);
                    transform.localEulerAngles = new Vector3(rotationVertical, 0.0f, 0.0f);
                    break;
                case RotationAxes.MouseXY:
                    rotationVertical -= Input.GetAxis("Mouse Y") * sensitivityVertical;
                    rotationVertical = Mathf.Clamp(rotationVertical, verticalMin, verticalMax);
                    rotationHorizontal += Input.GetAxis("Mouse X") * sensitivityHorizontal;
                    if (rotationHorizontal > 720.0f) rotationHorizontal -= 720.0f;
                    else if (rotationHorizontal < -720.0f) rotationHorizontal += 720.0f;
                    transform.localEulerAngles = new Vector3(rotationVertical, rotationHorizontal, 0.0f);
                    break;
            }
        }
    }
}