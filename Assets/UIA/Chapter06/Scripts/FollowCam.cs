using UnityEngine;

namespace UIA.Chapter06.Scripts
{
    public class FollowCam : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public float smoothTime = 0.2f;
        private Vector3 _velocity = Vector3.zero;

        // Update is called once per frame
        void FixedUpdate()
        {
            var camTransform = transform;
            var cameraPos = camTransform.position;
            var targetPos = target.position;
            targetPos.z = cameraPos.z;
            camTransform.position = Vector3.SmoothDamp(cameraPos, targetPos, ref _velocity, smoothTime);
        }
    }
}