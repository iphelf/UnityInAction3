using UnityEngine;

namespace UIA.FPS_Demo.Chapter03
{
    public class WanderingAI : MonoBehaviour
    {
        public float speed = 3.0f;
        public float sight = 5.0f;

        private bool _dead = false;

        [SerializeField] private GameObject fireballPrefab;
        private GameObject _fireball;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!_dead)
            {
                transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
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

        public void Kill()
        {
            _dead = true;
        }
    }
}