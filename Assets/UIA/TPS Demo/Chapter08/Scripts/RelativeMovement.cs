using UnityEngine;

namespace UIA.TPS_Demo.Chapter08.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class RelativeMovement : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private InputController input;
        public float rotationSpeed = 15.0f;
        public float movementSpeed = 6.0f;
        public float jumpSpeed = 15.0f;
        public float gravity = 9.8f;
        public float minFallSpeed = 1.5f;
        public float maxFallSpeed = 10.0f;
        public float pushForce = 3.0f;

        private CharacterController _controller;
        private float _fallVelocity;
        private float _distancePivotToFeet;
        private ControllerColliderHit _lastContact;
        private Animator _animator;
        private static readonly int AnimParamSpeed = Animator.StringToHash("Speed");
        private static readonly int AnimParamJumping = Animator.StringToHash("Jumping");

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _fallVelocity = minFallSpeed;
            _distancePivotToFeet = _controller.height / 2.0f - _controller.center.y;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var movement = Vector3.zero;

            {
                float horizontalMovement = input.RightMovement();
                float verticalMovement = input.ForwardMovement();
                if (!Mathf.Approximately(horizontalMovement, 0.0f) || !Mathf.Approximately(verticalMovement, 0.0f))
                {
                    var dirHorizontal = target.right;
                    var dirVertical = Vector3.Cross(dirHorizontal, Vector3.up);
                    var dir = dirHorizontal * horizontalMovement + dirVertical * verticalMovement;
                    dir = Vector3.ClampMagnitude(dir, 1.0f);
                    var targetRotation = Quaternion.LookRotation(dir);
                    transform.rotation =
                        Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    movement += movementSpeed * dir;
                }
            }

            _animator.SetFloat(AnimParamSpeed, movement.sqrMagnitude);

            {
                bool hitGround = _fallVelocity < 0 && Physics.Raycast(
                    transform.position, Vector3.down,
                    _distancePivotToFeet * 1.2f
                );
                // 1. Standing on the ground
                if (hitGround)
                {
                    if (input.JumpButtonDown())
                    {
                        _fallVelocity = jumpSpeed;
                        _animator.SetBool(AnimParamJumping, true);
                    }
                    else
                    {
                        _fallVelocity = -minFallSpeed;
                        _animator.SetBool(AnimParamJumping, false);
                    }
                }
                // 2. Falling off edge or sliding down a steep slope
                else if (_controller.isGrounded)
                {
                    Vector3 dirPushedAway = Vector3.Normalize(
                        _lastContact.normal - Vector3.up * Vector3.Dot(_lastContact.normal, Vector3.up)
                    );
                    bool facingCollider = Vector3.Dot(dirPushedAway, movement) < 0;
                    if (facingCollider)
                    {
                        movement = movementSpeed * dirPushedAway;
                    }
                    else
                    {
                        movement += movementSpeed * dirPushedAway;
                    }
                }
                // 3. In the air
                else
                {
                    // _animator.SetBool(AnimParamJumping, true);
                    _fallVelocity -= gravity * 5 * Time.deltaTime;
                    if (_fallVelocity < -maxFallSpeed) _fallVelocity = -maxFallSpeed;
                }

                movement.y += _fallVelocity;
            }

            movement *= Time.deltaTime;
            _controller.Move(movement);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            _lastContact = hit;

            Rigidbody body = hit.rigidbody;
            if (body != null && !body.isKinematic)
            {
                body.velocity = pushForce * hit.moveDirection;
                // body.AddForce(pushForce * hit.moveDirection);
            }
        }
    }
}