using UIA.FPS_Demo.Chapter07.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UIA.FPS_Demo.Chapter03
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        private GameObject _enemy;
        private float _speedScale = 1.0f;

        // Update is called once per frame
        void Update()
        {
            if (_enemy == null)
            {
                _enemy = Instantiate(enemyPrefab);
                _enemy.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
                _enemy.GetComponent<WanderingAI>().OnSpeedChanged(_speedScale);
                float angle = Random.Range(0.0f, 360.0f);
                _enemy.transform.Rotate(0.0f, angle, 0.0f);
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

        void OnSpeedChanged(float speedScale)
        {
            _speedScale = speedScale;
        }
    }
}