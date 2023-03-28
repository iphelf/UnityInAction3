using UnityEngine;

namespace UIA.Chapter02
{
    public class Spin : MonoBehaviour
    {
        public float spinSpeed = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0.0f, spinSpeed, 0.0f);
        }
    }
}