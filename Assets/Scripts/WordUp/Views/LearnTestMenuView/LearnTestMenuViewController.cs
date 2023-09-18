using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using WordUp.Service.Contracts.LearnGameData;
using WordUp.Service.Contracts.Word;
using WordUp.Service.Word;
using WordUp.Shared.StaticShared;
using WordUp.UI.CheckBox;
using WordUp.UI.ValidationMessageBox;
using Zenject;

namespace WordUp.Views.LearnTestMenuView
{
    public class LearnTestMenuViewController : SceneControllerBase
    {
        [SerializeField] private TMP_InputField inputFieldCountWord;
        [SerializeField] private CheckBoxSlider checkBoxOnlyHard;
        [SerializeField] private CheckBoxSlider checkBoxOnlyUnlearned;
        [SerializeField] private CheckBoxSlider checkBoxShuffle;
        [SerializeField] private CheckBoxSlider checkBoxShowInRussian;

        [Inject] private IWordService _wordService;
        [Inject] private Canvas _canvas;
        [Inject] private ValidationMessageBox _validationMessageBox;
        
        private LearnGameData _gameData;

        public void OnStartButtonClick()
        {
            var restriction = new LearnGameRestriction
            {
                WordsCount = GetWordCount(),
                OnlyHard = checkBoxOnlyHard.Selected,
                OnlyUnlearned = checkBoxOnlyUnlearned.Selected
            };

            List<WordDto> matchedWords = SetMatchedWords(restriction);

            var validator = new LearnGameRestrictionValidator(getWords: () => matchedWords);
            validator.Validate(restriction);

            if (validator.HasIssues)
            {
                UIHelper.ShowModalValidationMessageBox(
                    _canvas, 
                    _validationMessageBox, 
                    validator.Issues,
                    onApplyAction: () => LoadGameScene(matchedWords));
                
                return;
            }

            LoadGameScene(matchedWords);
        }
        
        protected override object GetDataFromScene()
        {
            return _gameData;
        }

        protected override void LoadDataFromScene(object data)
        {
            inputFieldCountWord.text = _wordService.GetModels().Count().ToString();
        }

        private void LoadGameScene(List<WordDto> matchedWords)
        {
            SetGameData(matchedWords);
            
            LoadSceneAdditive((int)SceneNumber.LearnGameView);
        }

        private int GetWordCount()
        {
            if (string.IsNullOrEmpty(inputFieldCountWord.text))
            {
                return 0;
            }

            return int.Parse(inputFieldCountWord.text);
        }

        private void SetGameData(List<WordDto> matchedWords)
        {
            _gameData = new LearnGameData
            {
                Words = matchedWords,
                MaxWords = GetWordCount(),
                Shuffle = checkBoxShuffle.Selected,
                SourceLanguage = checkBoxShowInRussian.Selected
                    ? Language.Russian
                    : Language.English
            };
        }
        
        private List<WordDto> SetMatchedWords(LearnGameRestriction restriction)
        {
            return _wordService.GetModels().Where(x => WordMatched(x, restriction)).ToList();
        }

        private bool WordMatched(WordDto word, LearnGameRestriction restriction)
        {
            if (restriction.OnlyHard && !word.IsHard)
            {
                return false;
            }

            if (restriction.OnlyUnlearned && word.IsLearned)
            {
                return false;
            }

            return true;
        }
    }
}