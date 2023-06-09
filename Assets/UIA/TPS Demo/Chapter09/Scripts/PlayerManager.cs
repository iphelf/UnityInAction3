using System;
using System.Collections.Generic;
using UIA.Chapter12.Scripts;
using UnityEngine;

namespace UIA.TPS_Demo.Chapter09.Scripts
{
    public class PlayerManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public void StartUp()
        {
            Debug.Log("Player manager starting...");

            ResetHealth();

            status = ManagerStatus.On;
        }

        public void GetData(Dictionary<string, object> data)
        {
            data["health"] = Health;
            data["maxHealth"] = MaxHealth;
        }

        public void SetData(Dictionary<string, object> data)
        {
            SetData((int)data["health"], (int)data["maxHealth"]);
        }

        public void SetData(int health, int maxHealth)
        {
            Health = health;
            MaxHealth = maxHealth;
        }

        private void ResetHealth()
        {
            SetData(50, 100);
        }

        public void ChangeHealth(int delta)
        {
            Health = Math.Clamp(Health + delta, 0, MaxHealth);
            Messenger.Broadcast(GameEvent.HealthUpdated);
            Debug.Log($"Health: {Health}/{MaxHealth}");
            if (Health == 0)
                Messenger.Broadcast(GameEvent.LevelFailed);
        }

        public void Respawn()
        {
            ResetHealth();
        }
    }
}