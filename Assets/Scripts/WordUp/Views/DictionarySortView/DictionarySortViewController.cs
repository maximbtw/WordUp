using System;
using UnityEngine;
using WordUp.UI.CheckBox;
using WordUp.UI.WordList;

namespace WordUp.Views.DictionarySortView
{
    public class DictionarySortViewController : SceneControllerBase
    {
        [SerializeField] private CheckBoxSlider checkBoxShowOnlyLearned;
        [SerializeField] private CheckBoxSlider checkBoxShowOnlyHard;

        private WordListSortingSettings _savedSortingSettings;
        private WordListSortingSettings _modifiedSortingSettings;

        public void OnApplyButtonClick()
        {
            _modifiedSortingSettings.showOnlyHard = checkBoxShowOnlyHard.Selected;
            _modifiedSortingSettings.showOnlyLearned = checkBoxShowOnlyLearned.Selected;
            
            UnloadScene((int)SceneNumber.DictionarySortView);
        }

        public void OnExitButtonClick()
        {
            UnloadScene((int)SceneNumber.DictionarySortView);
        }

        public override object GetDataFromScene()
        {
            return _modifiedSortingSettings;
        }

        public override void LoadDataFromScene(object data)
        {
            if (data is not WordListSortingSettings settings)
            {
                throw new ArgumentException();
            }
            
            _savedSortingSettings = settings;
            _modifiedSortingSettings = _savedSortingSettings;

            BindingModel();
        }
        
        private void BindingModel()
        {
            checkBoxShowOnlyLearned.Selected = _modifiedSortingSettings.showOnlyLearned;
            checkBoxShowOnlyHard.Selected = _modifiedSortingSettings.showOnlyHard;
        }
    }
}