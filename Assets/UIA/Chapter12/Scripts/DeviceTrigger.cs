using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    public class DeviceTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject[] targets;
        public bool requireKey = true;

        private void OnTriggerEnter(Collider other)
        {
            if (requireKey && Managers.Inventory.equippedItem != "Key")
            {
                Debug.Log("This trigger requires a key");
                Debug.Log($"Please equip a key instead of {Managers.Inventory.equippedItem}");
                return;
            }

            foreach (GameObject target in targets)
            {
                target.SendMessage("Activate");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            foreach (GameObject target in targets)
            {
                target.SendMessage("Deactivate");
            }
        }
    }
}