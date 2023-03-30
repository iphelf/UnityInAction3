using System;
using UnityEngine;

namespace UIA.Chapter06.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class PlatformerPlayer : MonoBehaviour
    {
        public float speed = 4.5f;
        public float jumpForce = 12.0f;
        public float gravityScale = 4.0f;
        private Rigidbody2D _body;
        private BoxCollider2D _box;
        private Animator _animator;
        private static readonly int ParamSpeed = Animator.StringToHash("speed");

        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
            _box = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            float deltaX = Input.GetAxis("Horizontal2D") * speed;
            var movement = new Vector2(deltaX, _body.velocity.y);
            _body.velocity = movement;
            _animator.SetFloat(ParamSpeed, Mathf.Abs(deltaX));

            // Check grounding
            Collider2D overlapped;
            {
                Bounds bounds = _box.bounds;
                Vector2 baseBL = new(bounds.min.x, bounds.min.y - 0.2f);
                Vector2 baseTR = new(bounds.max.x, bounds.min.y - 0.1f);
                overlapped = Physics2D.OverlapArea(baseBL, baseTR);
            }
            var grounded = overlapped is not null;

            // Stay idle on slope
            _body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0.0f)) ? 0.0f : gravityScale;

            // Jump only from ground
            if (grounded && Input.GetButtonDown("Jump"))
                _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Move along platform
            var platform = grounded ? overlapped.GetComponent<MovingPlatform>() : null;
            Vector3 parentScale;
            if (platform is not null)
            {
                var parentTransform = overlapped.transform;
                transform.parent = parentTransform;
                parentScale = parentTransform.localScale;
            }
            else
            {
                transform.parent = null;
                parentScale = Vector3.one;
            }

            // Scale/reflect properly
            if (!Mathf.Approximately(deltaX, 0.0f))
            {
                _body.transform.localScale =
                    new Vector3(Mathf.Sign(deltaX) / parentScale.x, 1.0f / parentScale.y, 1.0f);
            }
        }
    }
}