using System;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter10.Scripts
{
    public class WeatherController : MonoBehaviour
    {
        [SerializeField] private Material sky;
        [SerializeField] private Light sun;

        private float _fullIntensity;
        private static readonly int ShaderParamBlend = Shader.PropertyToID("_Blend");

        private void OnEnable()
        {
            Messenger.AddListener(GameEvents.WeatherUpdated, OnWeatherUpdated);
        }

        private void OnDisable()
        {
            Messenger.RemoveListener(GameEvents.WeatherUpdated, OnWeatherUpdated);
        }

        // Start is called before the first frame update
        void Start()
        {
            _fullIntensity = sun.intensity;
        }

        void OnWeatherUpdated()
        {
            SetOvercast(Managers.Weather.CloudValue);
        }

        private void SetOvercast(float value)
        {
            sky.SetFloat(ShaderParamBlend, value);
            sun.intensity = _fullIntensity - (_fullIntensity * value);
        }
    }
}