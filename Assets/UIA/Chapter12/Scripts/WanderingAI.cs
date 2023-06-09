using UIA.FPS_Demo.Chapter07.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UIA.Chapter12.Scripts
{
    public class WanderingAI : MonoBehaviour
    {
        public const float baseSpeed = 3.0f;
        private float _speed = baseSpeed;
        public float sight = 5.0f;

        private bool _dead = false;

        [SerializeField] private GameObject fireballPrefab;
        private GameObject _fireball;

        // Update is called once per frame
        void Update()
        {
            if (!_dead)
            {
                transform.Translate(0.0f, 0.0f, _speed * Time.deltaTime);
                Transform currTransform = transform;
                Ray ray = new(currTransform.position, currTransform.forward);
                if (Physics.SphereCast(ray, 0.75f, out var hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.GetComponent<PlayerController>())
                    {
                        if (_fireball == null)
                        {
                            _fireball = Instantiate(fireballPrefab);
                            _fireball.transform.position =
                                currTransform.TransformPoint(Vector3.forward * 1.5f);
                            _fireball.transform.rotation = currTransform.rotation;
                        }
                    }
                    else if (hit.distance < sight)
                    {
                        float angle = Random.Range(-110.0f, 110.0f);
                        transform.Rotate(0.0f, angle, 0.0f);
                    }
                }
            }
        }

        private void OnEnable()
        {
            Messenger<float>.AddListener(GameEvents.SpeedChanged, OnSpeedChanged);
        }

        private void OnDisable()
        {
            Messenger<float>.RemoveListener(GameEvents.SpeedChanged, OnSpeedChanged);
        }

        public void OnSpeedChanged(float speedScale)
        {
            _speed = baseSpeed * speedScale;
        }

        public void Kill()
        {
            _dead = true;
        }
    }
}