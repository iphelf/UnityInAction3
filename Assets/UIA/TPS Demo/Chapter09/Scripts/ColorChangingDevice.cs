using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UIA.TPS_Demo.Chapter09.Scripts
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ColorChangingDevice : MonoBehaviour
    {
        private MeshRenderer _renderer;

        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Operate()
        {
            _renderer.material.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
        }
    }
}