using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using WordUp.Service.Contracts.Word;
using WordUp.Service.Word;
using WordUp.Shared.StaticShared;
using WordUp.UI;
using WordUp.UI.WordList;
using Zenject;

namespace WordUp.Views.DictionaryListView
{
    public class DictionaryListViewController : SceneControllerBase
    {
        [Inject] private IWordService _wordService;
        [Inject] private Canvas _canvas;
        [Inject] private DictionaryListViewPopupMenu _popupMenu;

        [SerializeField] private TMP_InputField searchInputField;
        [SerializeField] private WordList wordList;

        private WordListSortingSettings _sortingSettings;
        private object _data;

        public void SearchInputFieldValueChanged()
        {
            _sortingSettings.text = searchInputField.text;

            UpdateSortingSettings();
        }

        public void OnSortSettingButtonClick()
        {
            _data = _sortingSettings;
            
            LoadSceneAdditive((int)SceneNumber.DictionarySortView);
        }
        
        public void OnCreateWordButtonClick()
        {
            _data = null;
            
            LoadSceneAdditive((int)SceneNumber.DictionaryEditView);
        }

        protected override void LateStart()
        {
            wordList.onItemClick.AddListener(ShowPopupMenu);

            List<WordDto> words = _wordService.GetModels().ToList();
            wordList.LoadItems(words);
        }

        protected override object GetDataFromScene()
        {
            return _data;
        }

        protected override void LoadDataFromScene(object data)
        {
            if (data == null)
            {
                return;
            }
            
            if (data is WordDto word)
            {
                _wordService.CreateOrUpdate(word);
                wordList.CreateOrUpdateItem(word);
            }
            else if (data is WordListSortingSettings settings)
            {
                _sortingSettings = settings;

                UpdateSortingSettings();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private void UpdateSortingSettings()
        {
            wordList.SortingSettings = _sortingSettings;
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

        private void EditWord(WordDto word)
        {
            _data = word;
            
            LoadSceneAdditive((int)SceneNumber.DictionaryEditView);
        }

        private void DeleteWord(WordDto word)
        {
            _wordService.Delete(word.Guid);
            wordList.Delete(word.Guid);
        }

        private void ShowPopupMenu(WordDto selectedWord)
        {
            var popupMenu = UIHelper.CreateInstantiate(_popupMenu, _canvas.transform);

            popupMenu.ButtonDeleteWord.onClick.AddListener(() => DeleteWord(selectedWord));
            popupMenu.ButtonEditWord.onClick.AddListener(() => EditWord(selectedWord));
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
    }
}