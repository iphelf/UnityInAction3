using System.Collections;
using System.Collections.Generic;
using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter10.Scripts
{
    [RequireComponent(typeof(WeatherManager))]
    [RequireComponent(typeof(ImagesManager))]
    public class Managers : MonoBehaviour
    {
        public static WeatherManager Weather { get; private set; }
        public static ImagesManager Images { get; private set; }
        private List<IGameManager> _startSequence;

        private void Awake()
        {
            Weather = GetComponent<WeatherManager>();
            Images = GetComponent<ImagesManager>();

            _startSequence = new List<IGameManager>
            {
                Weather,
                Images
            };

            StartCoroutine(StartUpManagers());
        }

        private IEnumerator StartUpManagers()
        {
            NetworkService networkService = new();
            foreach (IGameManager manager in _startSequence)
                manager.StartUp(networkService);
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