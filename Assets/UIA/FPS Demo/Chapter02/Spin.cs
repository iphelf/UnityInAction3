using UnityEngine;

namespace UIA.FPS_Demo.Chapter02
{
    public class Spin : MonoBehaviour
    {
        public float spinSpeed = 0.1f;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0.0f, spinSpeed, 0.0f);
        }
    }
}