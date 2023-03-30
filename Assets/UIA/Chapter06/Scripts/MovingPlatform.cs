using System;
using UnityEngine;

namespace UIA.Chapter06.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector3 finishPos = Vector3.zero;
        public float speed = 0.5f;

        private Vector3 startPos;
        private int direction = 1;
        private float percentage = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
            startPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if ((direction > 0 && percentage > 1.0f) || (direction < 0 && percentage < 0.0f))
                direction = -direction;
            percentage += direction * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, finishPos, percentage);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, finishPos);
        }
    }
}