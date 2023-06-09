using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public void Hurt(int damage)
        {
            Managers.Player.ChangeHealth(-damage);
        }
    }
}