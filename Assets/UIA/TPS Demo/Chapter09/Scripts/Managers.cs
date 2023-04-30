using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIA.TPS_Demo.Chapter09.Scripts
{
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(InventoryManager))]
    public class Managers : MonoBehaviour
    {
        public static PlayerManager Player { get; private set; }
        public static InventoryManager Inventory { get; private set; }
        private List<IGameManager> _startSequence;

        private void Awake()
        {
            _startSequence = new List<IGameManager>();

            Player = GetComponent<PlayerManager>();
            _startSequence.Add(Player);

            Inventory = GetComponent<InventoryManager>();
            _startSequence.Add(Inventory);

            StartCoroutine(StartUpManagers());
        }

        private IEnumerator StartUpManagers()
        {
            foreach (IGameManager manager in _startSequence)
                manager.StartUp();
            yield return null;

            int nReady = 0;
            while (nReady < _startSequence.Count)
            {
                int lastNReady = nReady;
                nReady = 0;
                foreach (IGameManager manager in _startSequence)
                    if (manager.status == ManagerStatus.On)
                        ++nReady;
                if (nReady > lastNReady)
                    Debug.Log($"Progress: {nReady}/{_startSequence.Count}");
                yield return null;
            }

            Debug.Log("All managers started up");
        }
    }
}