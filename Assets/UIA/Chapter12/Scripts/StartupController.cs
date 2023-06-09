using UnityEngine;
using UnityEngine.UI;

namespace UIA.Chapter12.Scripts
{
    public class StartupController : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;

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
            Managers.Mission.GoToNext();
        }
    }
}