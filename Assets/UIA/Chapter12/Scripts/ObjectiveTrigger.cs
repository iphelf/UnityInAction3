using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    public class ObjectiveTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>() is null) return;
            gameObject.SetActive(false);
            Managers.Mission.ReachObjective();
        }
    }
}