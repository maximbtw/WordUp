using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WordUp.Service.Contracts.Word;

namespace WordUp.UI.WordList
{
    public class WordListItem : MonoBehaviour
    {
        private WordDto _word = WordDto.DefaultWord;
        
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button button;
        [SerializeField] private Component markIsLearned;
        [SerializeField] private Component markIsHard;

        public WordDto Word
        {
            get  => _word;
            set
            {
                _word = value;
                UpdateBindings();
            }
        }

        public void UpdateBindings(WordDto word = null)
        {
            if (word != null)
            {
                _word = word;
            }

            text.text = _word.ToString();
            SetMark(markIsLearned, _word.IsLearned);
            SetMark(markIsHard, _word.IsHard);
        }

        public void BindClick(UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        private void Awake()
        {
            text ??= GetComponentInChildren<TextMeshProUGUI>();
            button ??= GetComponent<Button>();

            UpdateBindings();
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