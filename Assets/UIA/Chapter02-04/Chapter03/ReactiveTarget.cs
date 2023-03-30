using System.Collections;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter03
{
    public class ReactiveTarget : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void ReactToHit()
        {
            StartCoroutine(_die());
        }

        private IEnumerator _die()
        {
            transform.Rotate(-75.0f, 0.0f, 0.0f);
            WanderingAI wanderingAI = GetComponent<WanderingAI>();
            if (wanderingAI is not null)
                wanderingAI.Kill();
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}