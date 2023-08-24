using TMPro;
using UnityEngine;
using WordUp.Data;

namespace WordUp.UI.ContentView
{
    public class ContentViewWordItem : MonoBehaviour
    {
        private WordDto _word = WordDto.DefaultWord;
        
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Component markIsLearned;
        [SerializeField] private Component markIsHard;

        private void Awake()
        {
            if (text == null)
            {
                text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            }
            
            if (_word == null)
            {
               _word = new WordDto();
            }

            UpdateItem();
        }

        public void UpdateItem(WordDto word = null)
        {
            if (word != null)
            {
                _word = word;
            }

            text.text = _word.ToString();
            SetMark(markIsLearned, _word.IsLearned);
            SetMark(markIsHard, _word.IsHard);
        }

        private void SetMark(Component markComponent, bool setMark)
        {
            if (markComponent != null)
            {
                markComponent.gameObject.SetActive(setMark);
            }
        }
    }
}