using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using WordUp.Service.Contracts.Word;
using WordUp.Shared.StaticShared;
using WordUp.UI.CheckBox;
using WordUp.UI.WordList;

namespace WordUp.Views.LearnGroupView
{
    public class LearnGroupViewController : SceneControllerBase
    {
        [SerializeField] private WordList wordList;
        [SerializeField] private TextMeshProUGUI textMeshProCount;
        [Space] 
        [SerializeField] private CheckBoxSlider checkBoxOnlyHard;
        [SerializeField] private CheckBoxSlider checkBoxOnlyUnlearned;
        [SerializeField] private CheckBoxSlider checkBoxSort;
        [SerializeField] private CheckBoxSlider checkBoxShowInRussian;

        private LearnGroupSettingsData _learnGroupSettings;
        
        public void OnStartButtonClick()
        {
            _learnGroupSettings = new LearnGroupSettingsData()
            {
                OnlyHard = checkBoxOnlyHard.Selected,
                OnlyUnlearned = checkBoxOnlyUnlearned,
                Sort = checkBoxSort.Selected,
                ShowLanguage = checkBoxShowInRussian.Selected
                    ? LanguageHelpers.Language.Russian
                    : LanguageHelpers.Language.English
            };
            
            LoadSceneAdditive((int)SceneNumber.LearnGameView);
        }
        
        public override object GetDataFromScene()
        {
            return _learnGroupSettings;
        }

        public override void LoadDataFromScene(object data)
        {
            if (data is not List<WordDto> words)
            {
                throw new Exception($"Invalid data: Expected {typeof(List<WordDto>)}");
            }
            
            wordList.LoadItems(words);
            textMeshProCount.text = $"{words.Count(x => x.IsLearned)}/{words.Count}";
        }
    }
}