using UnityEngine;

namespace UIA.FPS_Demo.Chapter03
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private int health = 5;

        public void Hurt(int damage)
        {
            health -= damage;
            Debug.Log($"Health: {health}");
        }
    }
}