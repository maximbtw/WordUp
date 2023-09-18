using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WordUp.Service.Contracts.LearnGameData;
using WordUp.Service.Contracts.Word;
using WordUp.Shared.StaticShared;
using WordUp.UI.CheckBox;
using WordUp.UI.ValidationMessageBox;
using WordUp.UI.WordList;
using Zenject;

namespace WordUp.Views.LearnGroupView
{
    public class LearnGroupViewController : SceneControllerBase
    {
        [SerializeField] private WordList wordList;
        [SerializeField] private TextMeshProUGUI textMeshProCount;
        [Space] 
        [SerializeField] private CheckBoxSlider checkBoxOnlyHard;
        [SerializeField] private CheckBoxSlider checkBoxOnlyUnlearned;
        [FormerlySerializedAs("checkBoxSort")] [SerializeField] private CheckBoxSlider checkBoxShuffle;
        [SerializeField] private CheckBoxSlider checkBoxShowInRussian;

        [Inject] private Canvas _canvas;
        [Inject] private ValidationMessageBox _validationMessageBox;

        private LearnGameData _gameData;
        
        public void OnStartButtonClick()
        {
            var restriction = new LearnGameRestriction
            {
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
                    onApplyAction: () => LoadSceneAdditive((int)SceneNumber.LearnGameView));
                
                return;
            }

            SetGameData(matchedWords);
            
            LoadSceneAdditive((int)SceneNumber.LearnGameView);
        }

        protected override object GetDataFromScene()
        {
            return _gameData;
        }

        protected override void LoadDataFromScene(object data)
        {
            if (data is not List<WordDto> words)
            {
                return;
            }
            
            wordList.LoadItems(words);
            textMeshProCount.text = $"{words.Count(x => x.IsLearned)}/{words.Count}";
        }


        private void SetGameData(List<WordDto> matchedWords)
        {
            _gameData = new LearnGameData
            {
                Words = matchedWords,
                Shuffle = checkBoxShuffle.Selected,
                SourceLanguage = checkBoxShowInRussian.Selected
                    ? Language.Russian
                    : Language.English
            };
        }
        
        private List<WordDto> SetMatchedWords(LearnGameRestriction restriction)
        {
            return wordList.GetDataSource().Where(x => WordMatched(x, restriction)).ToList();
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