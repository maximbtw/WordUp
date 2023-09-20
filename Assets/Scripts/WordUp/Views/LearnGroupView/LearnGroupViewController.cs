using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WordUp.Service.Contracts.LearnGameData;
using WordUp.Service.Contracts.Word;
using WordUp.Service.Word;
using WordUp.Shared.StaticShared;
using WordUp.UI;
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
        [SerializeField] private CheckBoxSlider checkBoxShuffle;
        [SerializeField] private CheckBoxSlider checkBoxShowInRussian;

        [Inject] private IWordService _wordService;
        [Inject] private Canvas _canvas;
        [Inject] private ValidationMessageBox _validationMessageBox;
        [Inject] private LearnGroupViewPopupMenu _popupMenu;

        private LearnGameData _gameData;
        private List<WordDto> _words;
        
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
            if (data is List<WordDto> words)
            {
                _words = words;
            }

            if (_words == null)
            {
                throw new ArgumentException(nameof(data));
            }
            
            wordList.LoadItems(_words);
            textMeshProCount.text = $"{_words.Count(x => x.IsLearned)}/{_words.Count}";
        }

        protected override void LateStart()
        {
            wordList.onItemClick.AddListener(ShowPopupMenu);
        }

        private void SetGameData(List<WordDto> matchedWords)
        {
            _gameData = new LearnGameData
            {
                Words = matchedWords,
                MaxWords = matchedWords.Count,
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
        
        private void ShowPopupMenu(WordDto selectedWord)
        {
            var popupMenu = UIHelper.CreateInstantiate(_popupMenu, _canvas.transform);
            
            popupMenu.ButtonChangeLearnedMarkWord.onClick.AddListener(() => MarkAsLearned(selectedWord));
            popupMenu.ButtonChangeHardMarkWord.onClick.AddListener(() => MarkAsHard(selectedWord));
            
            //TODO: ChangePosition
            popupMenu.transform.localPosition = new Vector2(0, 0);

            ModalWindow.Construct(
                _canvas,
                popupMenu,
                readOnly: false,
                new Color(0, 0, 0, 0.5f));
        }
        
        private void MarkAsLearned(WordDto word)
        {
            word.IsLearned = !word.IsLearned;
            
            _wordService.CreateOrUpdate(word);
            wordList.CreateOrUpdateItem(word);
        }

        private void MarkAsHard(WordDto word)
        {
            word.IsHard = !word.IsHard;
            
            _wordService.CreateOrUpdate(word);
            wordList.CreateOrUpdateItem(word);
        }
    }
}