using TMPro;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter07.Scripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreLabel;
        [SerializeField] private SettingsPopup settingsPopup;

        private int _score;

        // Start is called before the first frame update
        void Start()
        {
            _score = 0;
            scoreLabel.text = _score.ToString();

            settingsPopup.Close();
        }

        private void OnEnable()
        {
            Messenger.AddListener(GameEvents.EnemyHit, OnEnemyHit);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener(GameEvents.EnemyHit, OnEnemyHit);
        }

        public void OnOpenSettings()
        {
            settingsPopup.Open();
        }

        private void OnEnemyHit()
        {
            ++_score;
            scoreLabel.text = _score.ToString();
        }
    }
}