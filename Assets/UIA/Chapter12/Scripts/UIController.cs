using System.Collections;
using TMPro;
using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    [RequireComponent(typeof(InputController))]
    public class UIController : MonoBehaviour
    {
        private InputController _input;

        [SerializeField] private TMP_Text healthLabel;
        [SerializeField] private InventoryPopup inventoryPopup;
        [SerializeField] private TMP_Text levelEnding;

        private void Start()
        {
            _input = GetComponent<InputController>();
            OnHealthUpdated();
            inventoryPopup.gameObject.SetActive(false);
            levelEnding.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_input.MenuButtonDown())
            {
                bool active = !inventoryPopup.gameObject.activeSelf;
                inventoryPopup.gameObject.SetActive(active);
                if (active) inventoryPopup.Refresh();
            }
        }

        private void OnEnable()
        {
            Messenger.AddListener(GameEvent.HealthUpdated, OnHealthUpdated);
            Messenger.AddListener(GameEvent.LevelCompleted, OnLevelCompleted);
            Messenger.AddListener(GameEvent.LevelFailed, OnLevelFailed);
            Messenger.AddListener(GameEvent.GameCompleted, OnGameCompleted);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener(GameEvent.HealthUpdated, OnHealthUpdated);
            Messenger.RemoveListener(GameEvent.LevelCompleted, OnLevelCompleted);
            Messenger.RemoveListener(GameEvent.LevelFailed, OnLevelFailed);
            Messenger.RemoveListener(GameEvent.GameCompleted, OnGameCompleted);
        }

        private void OnHealthUpdated()
        {
            string healthLabelText = $"Health: {Managers.Player.Health}/{Managers.Player.MaxHealth}";
            healthLabel.text = healthLabelText;
        }

        private void OnLevelCompleted()
        {
            if (levelEnding.gameObject.activeSelf) return;
            StartCoroutine(EndLevelInSuccess());
        }

        private void OnLevelFailed()
        {
            if (levelEnding.gameObject.activeSelf) return;
            StartCoroutine(EndLevelInFailure());
        }

        private void OnGameCompleted()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "You Finished the Game!";
        }

        private IEnumerator EndLevelInSuccess()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "Level Complete!";
            yield return new WaitForSeconds(2);
            Managers.Mission.GoToNext();
        }

        private IEnumerator EndLevelInFailure()
        {
            levelEnding.gameObject.SetActive(true);
            levelEnding.text = "Level Failed!";
            yield return new WaitForSeconds(2);
            Managers.Inventory.Clear();
            Managers.Player.Respawn();
            Managers.Mission.RestartCurrent();
        }

        public void OnSaveGame()
        {
            Managers.Saving.Save();
        }

        public void OnLoadGame()
        {
            Managers.Saving.Load();
        }
    }
}