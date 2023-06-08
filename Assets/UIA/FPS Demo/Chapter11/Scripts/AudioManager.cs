using System.Collections;
using UIA.FPS_Demo.Chapter10.Scripts;
using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;
using IGameManager = UIA.FPS_Demo.Chapter10.Scripts.IGameManager;

namespace UIA.FPS_Demo.Chapter11.Scripts
{
    public class AudioManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }
        [SerializeField] private AudioSource audioSource2D;

        [SerializeField] private AudioSource musicSource2D;

        // [SerializeField] private AudioSource music2Source2D;
        [SerializeField] private string introBGMusic;
        [SerializeField] private string levelBGMusic;

        public void StartUp(NetworkService _)
        {
            musicSource2D.ignoreListenerPause = true;
            musicSource2D.ignoreListenerVolume = true;
            nextMusicSource2D.ignoreListenerPause = true;
            nextMusicSource2D.ignoreListenerVolume = true;

            SoundVolume = 1.0f;
            MusicVolume = 1.0f;

            status = ManagerStatus.On;
        }

        public float SoundVolume
        {
            get => AudioListener.volume;
            set => AudioListener.volume = value;
        }

        public bool SoundMute
        {
            get => AudioListener.pause;
            set => AudioListener.pause = value;
        }

        public void PlaySound(AudioClip clip)
        {
            audioSource2D.PlayOneShot(clip);
        }

        private float _musicVolume;

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                if (!_crossFading) musicSource2D.volume = value;
            }
        }

        public bool MusicMute
        {
            get => musicSource2D.mute;
            set => nextMusicSource2D.mute = musicSource2D.mute = value;
        }

        public void PlayIntroMusic()
        {
            PlayMusic(Resources.Load<AudioClip>($"Music/{introBGMusic}"));
        }

        public void PlayLevelMusic()
        {
            PlayMusic(Resources.Load<AudioClip>($"Music/{levelBGMusic}"));
        }

        [SerializeField] private AudioSource nextMusicSource2D;
        private bool _crossFading;
        public float crossFadingRate = 1.5f;
        private float _crossFadingProgress;

        private void PlayMusic(AudioClip clip)
        {
            if (musicSource2D.clip is null)
            {
                musicSource2D.clip = clip;
                musicSource2D.Play();
            }
            else if (!_crossFading)
            {
                StartCoroutine(CrossFadeMusic(clip));
            }
        }

        private IEnumerator CrossFadeMusic(AudioClip clip)
        {
            _crossFading = true;

            nextMusicSource2D.volume = 0;
            nextMusicSource2D.clip = clip;
            nextMusicSource2D.Play();
            _crossFadingProgress = 0.0f;

            while (_crossFadingProgress <= 1.0f)
            {
                nextMusicSource2D.volume = _musicVolume * _crossFadingProgress;
                musicSource2D.volume = _musicVolume * (1.0f - _crossFadingProgress);
                yield return null;
                _crossFadingProgress += crossFadingRate * Time.deltaTime;
            }

            musicSource2D.volume = 0.0f;
            musicSource2D.clip = null;
            nextMusicSource2D.volume = _musicVolume;
            (musicSource2D, nextMusicSource2D) = (nextMusicSource2D, musicSource2D);

            _crossFading = false;
        }

        public void StopMusic()
        {
            musicSource2D.Stop();
            nextMusicSource2D.Stop();
        }
    }
}