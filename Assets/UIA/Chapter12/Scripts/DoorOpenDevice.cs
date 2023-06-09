using UIA.TPS_Demo.Chapter09.Scripts;
using UnityEngine;

namespace UIA.Chapter12.Scripts
{
    public class DoorOpenDevice : BaseDevice
    {
        [SerializeField] private Vector3 delta;
        private bool _open = false;

        public override void Operate()
        {
            if (_open)
            {
                transform.position -= delta;
            }
            else
            {
                transform.position += delta;
            }

            _open = !_open;
        }

        public void Activate()
        {
            if (!_open)
            {
                transform.position += delta;
                _open = true;
            }
        }

        public void Deactivate()
        {
            if (_open)
            {
                transform.position -= delta;
                _open = false;
            }
        }
    }
}