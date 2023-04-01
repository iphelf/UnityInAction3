using UIA.FPS_Demo.Chapter07.Scripts;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter02
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Control Script/FPS Input")]
    public class FPSInput : MonoBehaviour
    {
        public const float BaseSpeed = 6.0f;
        private float _speed = BaseSpeed;
        public float gravity = -9.8f;
        private CharacterController _controller;
        private bool _ignoreInputs = false;

        // Start is called before the first frame update
        void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_ignoreInputs) return;

            Vector3 movement = _speed * new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            movement = Vector3.ClampMagnitude(movement, _speed);

            movement.y = gravity;

            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _controller.Move(movement);
        }

        public void IgnoreInputs(bool ignoreInputs)
        {
            _ignoreInputs = ignoreInputs;
        }

        private void OnEnable()
        {
            Messenger<float>.AddListener(GameEvents.SpeedChanged, OnSpeedChanged);
        }

        private void OnDisable()
        {
            Messenger<float>.RemoveListener(GameEvents.SpeedChanged, OnSpeedChanged);
        }

        private void OnSpeedChanged(float speedScale)
        {
            _speed = BaseSpeed * speedScale;
        }
    }
}