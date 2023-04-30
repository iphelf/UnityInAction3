using UnityEngine;

namespace UIA.Chapter06.Scripts
{
    public class DeviceTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        private void OnTriggerEnter2D(Collider2D other)
        {
            target.SetActive(false);
        }
    }
}