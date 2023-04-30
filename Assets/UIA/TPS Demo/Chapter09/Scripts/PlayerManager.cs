using System;
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

            MaxHealth = 100;
            Health = 50;

            status = ManagerStatus.On;
        }

        public void ChangeHealth(int delta)
        {
            Health = Math.Clamp(Health + delta, 0, MaxHealth);
            Debug.Log($"Health: {Health}/{MaxHealth}");
        }
    }
}