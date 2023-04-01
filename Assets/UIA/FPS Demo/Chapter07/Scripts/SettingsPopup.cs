using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIA.FPS_Demo.Chapter07.Scripts
{
    public class SettingsPopup : MonoBehaviour
    {
        [SerializeField] private Slider speedSlider;
        [SerializeField] private TMP_InputField nameInputField;

        // Start is called before the first frame update
        void Start()
        {
            speedSlider.value = PlayerPrefs.GetFloat("speed", 1.0f);
            nameInputField.text = PlayerPrefs.GetString("name", "");
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void OnSubmitName(string name)
        {
            Debug.Log($"Name \"{name}\" submitted");
            PlayerPrefs.SetString("name", name);
        }

        public void OnSpeedChange(float speed)
        {
            Debug.Log($"Speed changed to {speed}");
            PlayerPrefs.SetFloat("speed", speed);
            Messenger<float>.Broadcast(GameEvents.SpeedChanged, speed);
        }
    }
}