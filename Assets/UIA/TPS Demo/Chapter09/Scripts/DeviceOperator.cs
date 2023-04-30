using UIA.TPS_Demo.Chapter08.Scripts;
using UnityEngine;

namespace UIA.TPS_Demo.Chapter09.Scripts
{
    public class DeviceOperator : MonoBehaviour
    {
        [SerializeField] private InputController input;
        public float radius = 1.5f;

        // Update is called once per frame
        void Update()
        {
            if (input.OperateButtonDown())
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider hitCollider in colliders)
                {
                    Vector3 direction = hitCollider.transform.position - transform.position;
                    direction.y = 0.0f;
                    if (Vector3.Dot(direction.normalized, transform.forward) > 0.5f)
                    {
                        hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }
}