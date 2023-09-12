using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WordUp.Service.Contracts.Settings;
using WordUp.Service.Contracts.Word;
using WordUp.Service.Settings;
using WordUp.Service.Word;
using WordUp.Shared.StaticShared;
using Zenject;

namespace WordUp.Views.LearnMenuView
{
    public class LearnMenuViewController : SceneControllerBase
    {
        [SerializeField] private LearnMenuGroupContentView contentView;

        [Inject] private IWordService _wordService;
        [Inject] private ISettingsService _settingsService;

        private LearnMenuGroupItemData _selectedData;

        protected override void LateStart()
        {
            contentView ??= GetComponentInChildren<LearnMenuGroupContentView>();

            List<LearnMenuGroupItemData> data = CreateItemsData().ToList();
            contentView.CreateItems(data, OnItemClick);
            
        }

        private IEnumerable<LearnMenuGroupItemData> CreateItemsData()
        {
           SettingsDto settings = _settingsService.GetModel();
           IEnumerable<WordDto> words = _wordService.GetModels();

           int number = 1;
           foreach (IEnumerable<WordDto> wordsGroup in CollectionHelpers.GetBatches(words, settings.CountWordInGroup))
           {
               List<WordDto> wordGroupList = wordsGroup.ToList();
               
               yield return new LearnMenuGroupItemData
               {
                   number = number,
                   count = wordGroupList.Count,
                   learnedCount = wordGroupList.Count(x => x.IsLearned),
                   Words = wordGroupList
               };

               number++;
           }
        }


        private void OnItemClick(LearnMenuGroupItem item)
        {
            _selectedData = item.Data;
            LoadSceneAdditive((int)SceneNumber.LearnGroupView);
        }

        protected override object GetDataFromScene()
        {
            return _selectedData.Words;
        }
    }
}