using System;
using System.Xml;
using ManagerStatus = UIA.TPS_Demo.Chapter09.Scripts.ManagerStatus;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace UIA.FPS_Demo.Chapter10.Scripts
{
    public class WeatherManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }

        private NetworkService _network;

        public float CloudValue { get; private set; }

        public enum ApiProtocol
        {
            Json,
            Xml
        }

        public ApiProtocol protocol = ApiProtocol.Json;

        public void StartUp(NetworkService service)
        {
            Debug.Log("Weather manager starting...");
            _network = service;
            switch (protocol)
            {
                case ApiProtocol.Json:
                    StartCoroutine(_network.GetWeatherJsonData(OnJsonDataLoaded));
                    break;
                case ApiProtocol.Xml:
                    StartCoroutine(_network.GetWeatherXmlData(OnXmlDataLoaded));
                    break;
            }

            status = ManagerStatus.Initializing;
        }

        private void OnXmlDataLoaded(string data)
        {
            Debug.Log(data);
            XmlDocument xmlDoc = new();
            xmlDoc.LoadXml(data);
            XmlNode root = xmlDoc.DocumentElement;
            if (root is null) return;
            XmlNode node = root.SelectSingleNode("clouds");
            if (node is null) return;
            string value = node.Attributes["value"].Value;
            CloudValue = Convert.ToInt32(value) / 100.0f;
            Debug.Log($"cloud value = {CloudValue}");
            status = ManagerStatus.On;
        }

        private void OnJsonDataLoaded(string data)
        {
            Debug.Log(data);
            JObject root = JObject.Parse(data);
            JToken clouds = root["clouds"];
            if (clouds is null) return;
            CloudValue = clouds.Value<Int32>("all") / 100.0f;
            Debug.Log($"cloud value = {CloudValue}");
            status = ManagerStatus.On;
        }

        public void LogWeather(string message)
        {
            StartCoroutine(_network.LogWeather(message, CloudValue, OnLogged));
        }

        private void OnLogged(string response)
        {
            Debug.Log(response);
        }
    }
}