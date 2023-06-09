using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    public class BaseDevice : MonoBehaviour
    {
        public float radius = 3.5f;

        private void OnMouseUp()
        {
            if (PlayerNearbyFacing())
                Operate();
        }

        public virtual void Operate()
        {
        }

        private bool PlayerNearbyFacing()
        {
            Transform player = GameObject.FindWithTag("Player").transform;
            Vector3 playerPosition = player.position;
            playerPosition.y = transform.position.y;
            if (Vector3.Distance(transform.position, playerPosition) < radius)
            {
                Vector3 direction = transform.position - playerPosition;
                if (Vector3.Dot(player.forward, direction) > 0.5f)
                    return true;
            }

            return false;
        }
    }
}