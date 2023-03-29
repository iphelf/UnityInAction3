using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIA.Memory_Demo.Chapter05
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private GameObject firstMemoryCard;
        public int gridRows = 2;
        public int gridColumns = 4;
        public float offsetX = 2.0f;
        public float offsetY = 2.5f;
        private MemoryCard _revealedCard1;
        private MemoryCard _revealedCard2;
        private int score = 0;
        [SerializeField] private TMPro.TMP_Text scoreLabel;

        // Start is called before the first frame update
        void Start()
        {
            int[] ids = new int[sprites.Length * 2];
            for (var i = 0; i < ids.Length; ++i)
                ids[i] = i / 2;
            for (var i = ids.Length - 1; i >= 0; --i)
            {
                int j = Random.Range(0, i + 1);
                (ids[i], ids[j]) = (ids[j], ids[i]);
            }

            Vector3 startPos = firstMemoryCard.transform.position;
            startPos.x -= ((gridColumns - 1) * offsetX) / 2.0f;
            startPos.y += ((gridRows - 1) * offsetY) / 2.0f;
            for (var i = 0; i < gridRows; ++i)
            for (var j = 0; j < gridColumns; ++j)
            {
                GameObject card = (i == 0 && j == 0) ? firstMemoryCard : Instantiate(firstMemoryCard);
                card.transform.position = new Vector3(startPos.x + offsetX * j,
                    startPos.y - offsetY * i, startPos.z);
                int id = i * gridColumns + j;
                card.GetComponent<MemoryCard>().SetCard(ids[id], sprites[ids[id]]);
            }
        }

        // Update is called once per frame
        void Update()
        {
        }

        public bool CanReveal => _revealedCard2 is null;

        public void OnReveal(MemoryCard card)
        {
            if (_revealedCard1 is null)
            {
                _revealedCard1 = card;
            }
            else
            {
                _revealedCard2 = card;
                StartCoroutine(CheckMatch());
            }
        }

        private IEnumerator CheckMatch()
        {
            if (_revealedCard1.Id == _revealedCard2.Id)
            {
                Debug.Log("A match!");
                score += 1;
                scoreLabel.text = $"Score: {score}";
                if (score == sprites.Length)
                    Debug.Log("Game over.");
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                Debug.Log("A mismatch...");
                _revealedCard1.Unreveal();
                _revealedCard2.Unreveal();
            }

            _revealedCard1 = _revealedCard2 = null;
        }

        public void Restart()
        {
            SceneManager.LoadScene("Memory Demo");
        }
    }
}