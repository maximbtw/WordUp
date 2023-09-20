using System;
using UnityEngine;
using WordUp.Service.Contracts.Word;
using WordUp.Shared.StaticShared;
using WordUp.UI.CheckBox;
using WordUp.UI.ValidationMessageBox;
using WordUp.Views.DictionaryEditView.TranslateControl;
using Zenject;

namespace WordUp.Views.DictionaryEditView
{
    public class DictionaryEditViewController : SceneControllerBase
    {
        [Inject] private Canvas _canvas;
        [Inject] private ValidationMessageBox _validationMessageBox;
        
        [SerializeField] private TranslateControlController translateControlController;
        [SerializeField] private CheckBoxSlider checkBoxMarkAsLearned;
        [SerializeField] private CheckBoxSlider checkBoxMarkAsHard;
        [SerializeField] private CheckBoxSlider checkBoxMarkAutoTranslate;

        private WordDto _modifiedWord;
        private WordDto _savedWord;

        protected override void LateStart()
        {
            checkBoxMarkAutoTranslate.selectedChanged.AddListener(x => translateControlController.AutoTranslate = x);
        }
        
        public void OnExitClick()
        {
            _modifiedWord = _savedWord;
            
            UnloadScene((int)SceneNumber.DictionaryEditView);
        }

        public void OnSaveClick()
        {
            BindingModelFromControl();

            var validator = new WordDtoValidator();
            validator.Validate(_modifiedWord);

            if (validator.HasIssues)
            {
                UIHelper.ShowModalValidationMessageBox(_canvas, _validationMessageBox, validator.Issues, () => { });
                
                return;
            }
            
            UnloadScene((int)SceneNumber.DictionaryEditView);
        }

        protected override object GetDataFromScene()
        {
            return _modifiedWord;
        }

        protected override void LoadDataFromScene(object data)
        {
            if (data == null)
            {
                _modifiedWord = new WordDto();
                
                BindingModelToControl();
                
                return;
            }
            
            if (data is WordDto word)
            {
                InitWord(word);
                
                BindingModelToControl();
                
                return;
            }

            throw new ArgumentException(nameof(data));
        }

        private void InitWord(WordDto word)
        {
            _savedWord = word;
            _modifiedWord = _savedWord.DeepClone();
        }
        
        private void BindingModelToControl()
        {
            checkBoxMarkAsLearned.Selected = _modifiedWord.IsLearned;
            checkBoxMarkAsHard.Selected = _modifiedWord.IsHard;

            checkBoxMarkAutoTranslate.Selected = false;

            translateControlController.SourceLanguage = Language.English;
            translateControlController.TargetLanguage = Language.Russian;

            translateControlController.SourceText = _modifiedWord.NameEn;
            translateControlController.TargetText = _modifiedWord.NameRu;
        }
        
        private void BindingModelFromControl()
        {
            string enText = translateControlController.SourceLanguage == Language.English
                ? translateControlController.SourceText
                : translateControlController.TargetText;
            
            string ruText = translateControlController.SourceLanguage == Language.Russian
                ? translateControlController.SourceText
                : translateControlController.TargetText;
            
            _modifiedWord.IsLearned = checkBoxMarkAsLearned.Selected;
            _modifiedWord.IsHard = checkBoxMarkAsHard.Selected;
            _modifiedWord.NameEn = enText;
            _modifiedWord.NameRu = ruText;
        }
    }
}