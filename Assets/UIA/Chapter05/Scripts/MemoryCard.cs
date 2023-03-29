using UnityEngine;

namespace UIA.Memory_Demo.Chapter05
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MemoryCard : MonoBehaviour
    {
        [SerializeField] private GameObject cardBack;
        [SerializeField] private SceneController controller;
        private int _id;

        public int Id => _id;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetCard(int id, Sprite sprite)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
            _id = id;
        }

        private void OnMouseDown()
        {
            if (cardBack.activeSelf && controller.CanReveal)
            {
                cardBack.SetActive(false);
                controller.OnReveal(this);
            }
        }

        public void Unreveal()
        {
            cardBack.SetActive(true);
        }
    }
}