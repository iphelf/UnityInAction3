using TMPro;
using UIA.FPS_Demo.Chapter10.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace UIA.FPS_Demo.Chapter07.Scripts
{
    public class SettingsPopup : MonoBehaviour
    {
        [SerializeField] private Slider speedSlider;
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private AudioClip clickSound;

        // Start is called before the first frame update
        void Start()
        {
            speedSlider.value = PlayerPrefs.GetFloat("speed", 1.0f);
            nameInputField.text = PlayerPrefs.GetString("name", "");
        }

        public void Open()
        {
            Managers.Audio.PlaySound(clickSound);
            gameObject.SetActive(true);
        }

        public void Close()
        {
            Managers.Audio.PlaySound(clickSound);
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

        public void ToggleSound()
        {
            Managers.Audio.SoundMute = !Managers.Audio.SoundMute;
            Managers.Audio.PlaySound(clickSound);
        }

        public void OnSoundVolumeChange(float volume)
        {
            Managers.Audio.SoundVolume = volume;
        }

        public void OnPlayMusic(string selector)
        {
            switch (selector)
            {
                case "Intro":
                    Managers.Audio.PlayIntroMusic();
                    break;
                case "Level":
                    Managers.Audio.PlayLevelMusic();
                    break;
                default:
                    Managers.Audio.StopMusic();
                    break;
            }

            Managers.Audio.PlaySound(clickSound);
        }

        public void ToggleMusic()
        {
            Managers.Audio.MusicMute = !Managers.Audio.MusicMute;
            Managers.Audio.PlaySound(clickSound);
        }

        public void OnMusicVolumeChange(float volume)
        {
            Managers.Audio.MusicVolume = volume;
        }
    }
}