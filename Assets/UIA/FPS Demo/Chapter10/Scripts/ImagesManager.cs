using System;
using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter10.Scripts
{
    public class ImagesManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }
        private NetworkService _network;
        private Texture2D _image;

        public void StartUp(NetworkService service)
        {
            _network = service;
            status = ManagerStatus.On;
        }

        public void GetWebImage(Action<Texture2D> callback)
        {
            if (_image)
                callback(_image);
            else
                StartCoroutine(_network.DownloadImage(image => { callback(_image = image); }));
        }
    }
}