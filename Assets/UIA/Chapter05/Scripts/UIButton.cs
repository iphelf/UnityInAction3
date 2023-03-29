using UnityEngine;

namespace UIA.Memory_Demo.Chapter05
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class UIButton : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private MonoBehaviour target;
        [SerializeField] private string message;
        public Color highlightColor = Color.cyan;

        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnMouseEnter()
        {
            _spriteRenderer.color = highlightColor;
        }

        private void OnMouseExit()
        {
            _spriteRenderer.color = Color.white;
        }

        private void OnMouseDown()
        {
            _spriteRenderer.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }

        private void OnMouseUp()
        {
            _spriteRenderer.transform.localScale = Vector3.one;
            if (target != null)
                target.SendMessage(message);
        }
    }
}