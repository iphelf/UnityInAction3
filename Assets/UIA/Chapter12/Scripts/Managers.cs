using System.Collections;
using System.Collections.Generic;
using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(InventoryManager))]
    [RequireComponent(typeof(MissionManager))]
    [RequireComponent(typeof(SavingManager))]
    public class Managers : MonoBehaviour
    {
        public static PlayerManager Player { get; private set; }
        public static InventoryManager Inventory { get; private set; }
        public static MissionManager Mission { get; private set; }
        public static SavingManager Saving { get; private set; }
        private List<IGameManager> _startSequence;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            Player = GetComponent<PlayerManager>();
            Inventory = GetComponent<InventoryManager>();
            Mission = GetComponent<MissionManager>();
            Saving = GetComponent<SavingManager>();

            _startSequence = new List<IGameManager>
            {
                Player,
                Inventory,
                Mission,
                Saving
            };

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
                {
                    Debug.Log($"Progress: {nReady}/{_startSequence.Count}");
                    Messenger<int, int>.Broadcast(StartupEvent.ManagersProgress, nReady, _startSequence.Count);
                }

                yield return null;
            }

            Debug.Log("All managers started up");
            Messenger.Broadcast(StartupEvent.ManagersStarted);
        }
    }
}