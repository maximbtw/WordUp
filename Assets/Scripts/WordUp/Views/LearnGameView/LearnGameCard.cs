using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WordUp.Service.Contracts.Word;
using WordUp.Shared;
using WordUp.Shared.StaticShared;
using WordUp.UI.CheckBox;

namespace WordUp.Views.LearnGameView
{
    public class LearnGameCard : MonoBehaviour
    {
        [SerializeField] private CheckBoxSlider checkBoxSliderMarkAsHard;
        [SerializeField] private CheckBoxSlider checkBoxSliderMarkAsLearned;
        [SerializeField] private Button buttonEnd;
        [SerializeField] private Button buttonBack;
        [SerializeField] private TextMeshProUGUI textNumber;
        [SerializeField] private TextMeshProUGUI textSourceWord;

        private LearnGameCardData _data;

        public LearnGameCardData Data
        {
            get => _data;
            set
            {
                _data = value;
                onDataSet?.Invoke(_data);
                BindingFromData();
            }
        }

        public UnityEvent<LearnGameCardData> onDataSet;

        private void BindingFromData()
        {
            textNumber.text = $"{_data.Number}/{_data.MaxNumber}";
            textSourceWord.text = _data.GetSourceName;
            checkBoxSliderMarkAsHard.Selected = _data.Word.IsHard;
            checkBoxSliderMarkAsLearned.Selected = _data.Word.IsLearned;

            buttonEnd.onClick.AddListener(_data.EndClickAction);
            buttonBack.onClick.AddListener(_data.BackClickAction);
        }

        private void SwipeLeft()
        {
            Swipe(false);
        }

        private void SwipeRight()
        {
            Swipe(true);
        }

        private void Swipe(bool swipeSuccess)
        {
            var modifiedWord = _data.Word.DeepClone();
            
            modifiedWord.IsHard = checkBoxSliderMarkAsHard.Selected;
            modifiedWord.IsLearned = checkBoxSliderMarkAsLearned.Selected;

            _data.SwipeAction.Invoke(modifiedWord, swipeSuccess);

            if (_data.Number == _data.MaxNumber)
            {
                _data.EndClickAction.Invoke();
            }
        }

        private void Start()
        {
            var swipe = GetComponent<SwipeEffect>();
            
            swipe.swipeLeft.AddListener(SwipeLeft);
            swipe.swipeRight.AddListener(SwipeRight);
        }
    }
}