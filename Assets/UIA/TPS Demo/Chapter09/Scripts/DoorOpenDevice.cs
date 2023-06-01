using UnityEngine;

namespace UIA.TPS_Demo.Chapter09.Scripts
{
    public class DoorOpenDevice : MonoBehaviour, IOperatee
    {
        [SerializeField] private Vector3 delta;
        private bool _open = false;

        public void Operate()
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