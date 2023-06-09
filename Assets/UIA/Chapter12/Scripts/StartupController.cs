using System;
using TMPro;
using UIA.Chapter13;
using UnityEngine;
using UnityEngine.UI;

namespace UIA.Chapter12.Scripts
{
    public class StartupController : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;
        [SerializeField] private Button playButton;
        [SerializeField] private TMP_Text platformLabel;
        [SerializeField] private WebTestObject webTestObject;

        private void Start()
        {
            // platformLabel.text = "Current Platform: "
            const string currentPlatform =
#if UNITY_EDITOR
                "Unity Editor";
#elif UNITY_STANDALONE
                "Desktop";
#else
                "Some Unknown Platform";
#endif
            platformLabel.text = $"Current Platform: {currentPlatform}";
        }

        private void OnEnable()
        {
            Messenger<int, int>.AddListener(StartupEvent.ManagersProgress, OnManagersProgress);
            Messenger.AddListener(StartupEvent.ManagersStarted, OnManagersStarted);
        }

        private void OnDisable()
        {
            Messenger<int, int>.RemoveListener(StartupEvent.ManagersProgress, OnManagersProgress);
            Messenger.RemoveListener(StartupEvent.ManagersStarted, OnManagersStarted);
        }

        private void OnManagersProgress(int nReady, int nTotal)
        {
            float progress = (float)nReady / nTotal;
            progressBar.value = progress;
        }

        private void OnManagersStarted()
        {
            playButton.interactable = true;
        }

        public void OnPlay()
        {
            Managers.Mission.GoToNext();
        }

        public void OnTest()
        {
            WebTestObject.JsAlert("Hello out there!");
        }
    }
}