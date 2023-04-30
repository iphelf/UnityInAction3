using UnityEngine;

namespace UIA.TPS_Demo.Chapter08.Scripts
{
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private InputController input;
        public float rotationSpeed = 1.5f;

        private Vector3 _offset;
        private float _angleY = 0.0f;

        void Start()
        {
            _offset = target.position - transform.position;
        }

        private void LateUpdate()
        {
            // float deltaY = input.HorizontalMovement() * 0.4f;
            // if (Mathf.Approximately(deltaY, 0.0f))
            //     deltaY = input.HorizontalViewRotation() * 3.0f;

            float deltaY = input.ViewRotationX() * 3.0f;

            _angleY += deltaY * rotationSpeed;
            if (_angleY > 720.0f) _angleY -= 720.0f;
            else if (_angleY < -720.0f) _angleY += 720.0f;
            Quaternion rotationYaw = Quaternion.Euler(0.0f, _angleY, 0.0f);
            transform.position = target.position - rotationYaw * _offset;
            transform.LookAt(target);
        }
    }
}