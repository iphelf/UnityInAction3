using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    public class Fireball : MonoBehaviour
    {
        public float speed = 6.0f;
        public int damage = 1;
        public bool bounce = false;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var playerController = other.GetComponent<PlayerController>();
            if (playerController is not null)
            {
                Debug.Log("Hit at player");
                playerController.Hurt(damage);
                Destroy(gameObject);
            }
            else if (!bounce)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
    }
}