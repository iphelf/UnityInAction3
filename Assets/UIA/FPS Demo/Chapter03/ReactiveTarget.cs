using System.Collections;
using UIA.FPS_Demo.Chapter07.Scripts;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter03
{
    public class ReactiveTarget : MonoBehaviour
    {
        private bool _killed = false;

        public void ReactToHit()
        {
            if (!_killed)
                StartCoroutine(_die());
        }

        private IEnumerator _die()
        {
            transform.Rotate(-75.0f, 0.0f, 0.0f);
            _killed = true;
            Messenger.Broadcast(GameEvents.EnemyHit);
            WanderingAI wanderingAI = GetComponent<WanderingAI>();
            if (wanderingAI is not null)
                wanderingAI.Kill();
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}