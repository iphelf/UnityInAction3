using System;
using UIA.Chapter12.Scripts;
using UnityEngine;

namespace UIA.TPS_Demo.Chapter09.Scripts
{
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField] private string itemName;

        private void OnTriggerEnter(Collider other)
        {
            Messenger<string>.Broadcast(GameEvent.ItemCollected, itemName);
            Destroy(gameObject);
        }
    }
}