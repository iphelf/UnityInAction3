using UnityEngine;
using UnityEngine.EventSystems;

namespace UIA.Chapter12.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PointClickMovement : MonoBehaviour
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

        public float targetBuffer = 1.5f;
        public float deceleration = 25.0f;
        private float currentSpeed = 0.0f;
        private Vector3? targetPos;

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

            #region set targetPos

            if (input.OperateButtonDown() && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main!.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var mouseHit)
                    && mouseHit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    targetPos = mouseHit.point;
                    currentSpeed = movementSpeed;
                }
            }

            #endregion

            #region move towards targetPos

            if (targetPos.HasValue)
            {
                if (currentSpeed > movementSpeed * 0.5f)
                {
                    Vector3 adjustedPos = new(targetPos.Value.x, transform.position.y, targetPos.Value.z);
                    Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
                    transform.rotation =
                        Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
                }

                movement = currentSpeed * Vector3.forward;
                movement = transform.TransformDirection(movement);

                if (Vector3.Distance(targetPos.Value, transform.position) < targetBuffer)
                {
                    currentSpeed -= deceleration * Time.deltaTime;
                    if (currentSpeed <= 0)
                        targetPos = null;
                }
            }

            #endregion

            _animator.SetFloat(AnimParamSpeed, movement.sqrMagnitude);

            #region falling animation

            {
                bool hitGround = _fallVelocity < 0 && Physics.Raycast(
                    transform.position, Vector3.down,
                    _distancePivotToFeet * 1.2f
                );
                // 1. Standing on the ground
                if (hitGround)
                {
                    // if (input.JumpButtonDown())
                    // {
                    //     _fallVelocity = jumpSpeed;
                    //     _animator.SetBool(AnimParamJumping, true);
                    // }
                    // else
                    // {
                    _fallVelocity = -minFallSpeed;
                    _animator.SetBool(AnimParamJumping, false);
                    // }
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

            #endregion

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