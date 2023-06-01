using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

namespace UIA.FPS_Demo.Chapter10.Scripts
{
    public class NetworkService
    {
        private const string AppId = "b192d0a892852797b838c08b5738377b";
        private const float Lat = 30.181500800643175f;
        private const float Lon = 120.19649772474597f;

        private static readonly string XmlApiUrl =
            $"https://api.openweathermap.org/data/2.5/weather?mode=xml&lat={Lat}&lon={Lon}&appid={AppId}";

        private static readonly string JsonApiUrl =
            $"https://api.openweathermap.org/data/2.5/weather?mode=json&lat={Lat}&lon={Lon}&appid={AppId}";

        private const string WebImage =
            "https://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";

        private const string LogApiUrl = "http://127.0.0.1:5000";

        private IEnumerator CallAPI(string url, WWWForm form, Action<string> callback)
        {
            Debug.Log($"CallAPI on url: {url}");
            using var request = form is null ? UnityWebRequest.Get(url) : UnityWebRequest.Post(url, form);
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError($"network problem: {request.error}");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"response error: {request.responseCode}");
                    break;
                default:
                    callback(request.downloadHandler.text);
                    break;
            }
        }

        public IEnumerator GetWeatherXmlData(Action<string> callback)
        {
            return CallAPI(XmlApiUrl, null, callback);
        }

        public IEnumerator GetWeatherJsonData(Action<string> callback)
        {
            return CallAPI(JsonApiUrl, null, callback);
        }

        public IEnumerator DownloadImage(Action<Texture2D> callback)
        {
            using var request = UnityWebRequestTexture.GetTexture(WebImage);
            yield return request.SendWebRequest();
            callback(DownloadHandlerTexture.GetContent(request));
        }

        public IEnumerator LogWeather(string message, float cloudValue, Action<string> callback)
        {
            WWWForm form = new();
            form.AddField("message", message);
            form.AddField("cloud_value", cloudValue.ToString(CultureInfo.CurrentCulture));
            form.AddField("timestamp", DateTime.UtcNow.Ticks.ToString());
            return CallAPI(LogApiUrl, form, callback);
        }
    }
}