using System;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter03
{
    public class Fireball : MonoBehaviour
    {
        public float speed = 10.0f;
        public int damage = 1;

        // Start is called before the first frame update
        void Start()
        {
        }

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
            }

            Destroy(gameObject);
        }
    }
}