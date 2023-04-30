using System;
using UnityEngine;

namespace UIA.TPS_Demo.Chapter09.Scripts
{
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] private string itemName;

        private void OnTriggerEnter(Collider other)
        {
            Managers.Inventory.AddItem(itemName);
            Destroy(gameObject);
        }
    }
}