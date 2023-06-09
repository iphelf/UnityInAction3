using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;
using GameState = System.Collections.Generic.Dictionary<string, object>;

namespace UIA.Chapter12.Scripts
{
    public class SavingManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }
        private string _savingPath;

        public void StartUp()
        {
            _savingPath = Path.Combine(Application.persistentDataPath, "saving.dat");

            status = ManagerStatus.On;
        }

        private GameState GetGameState()
        {
            GameState gameState = new();
            Managers.Player.GetData(gameState);
            Managers.Inventory.GetData(gameState);
            Managers.Mission.GetData(gameState);
            return gameState;
        }

        private void SetGameState(GameState gameState)
        {
            Managers.Player.SetData(gameState);
            Managers.Inventory.SetData(gameState);
            Managers.Mission.SetData(gameState);
        }

        private static void SaveGameState(GameState gameState, string path)
        {
            using FileStream stream = File.Create(path);
            BinaryFormatter formatter = new();
            formatter.Serialize(stream, gameState);
        }

        private static GameState LoadGameState(string path)
        {
            if (!File.Exists(path)) return null;
            using FileStream stream = File.OpenRead(path);
            BinaryFormatter formatter = new();
            return formatter.Deserialize(stream) as GameState;
        }

        public void Save()
        {
            SaveGameState(GetGameState(), _savingPath);
        }

        public void Load()
        {
            GameState gameState = LoadGameState(_savingPath);
            if (gameState is null)
                Debug.Log("No saved game");
            else
                SetGameState(gameState);
        }
    }
}