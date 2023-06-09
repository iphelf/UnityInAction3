using System.Collections.Generic;
using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIA.Chapter12.Scripts
{
    public class MissionManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }
        public int currLevel { get; private set; }
        public int maxLevel { get; private set; }

        public void StartUp()
        {
            currLevel = 0;
            maxLevel = 3;

            status = ManagerStatus.On;
        }

        public void ReachObjective()
        {
            Messenger.Broadcast(GameEvent.LevelCompleted);
        }

        public void GoToNext()
        {
            if (currLevel < maxLevel)
            {
                ++currLevel;
                string sceneName = $"Level {currLevel}";
                Debug.Log($"Loading {sceneName}");
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.Log("Last level");
                Messenger.Broadcast(GameEvent.GameCompleted);
            }
        }

        public void RestartCurrent()
        {
            string sceneName = $"Level {currLevel}";
            Debug.Log($"Loading {sceneName}");
            SceneManager.LoadScene(sceneName);
        }

        public void GetData(Dictionary<string, object> data)
        {
            data["currLevel"] = currLevel;
            data["maxLevel"] = maxLevel;
        }

        public void SetData(Dictionary<string, object> data)
        {
            currLevel = (int)data["currLevel"];
            maxLevel = (int)data["maxLevel"];
            RestartCurrent();
        }
    }
}