using Unity.VisualScripting;
using UnityEngine;

namespace UIA.FPS_Demo.Chapter03
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        private GameObject enemy;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (enemy == null)
            {
                enemy = Instantiate(enemyPrefab);
                enemy.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
                float angle = Random.Range(0.0f, 360.0f);
                enemy.transform.Rotate(0.0f, angle, 0.0f);
            }
        }
    }
}