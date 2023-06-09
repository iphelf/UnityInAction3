using UnityEngine;
using Random = UnityEngine.Random;

namespace UIA.Chapter12.Scripts
{
    [RequireComponent(typeof(MeshRenderer))]
    public class ColorChangingDevice : BaseDevice
    {
        private MeshRenderer _renderer;

        // Start is called before the first frame update
        void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public override void Operate()
        {
            _renderer.material.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
        }
    }
}