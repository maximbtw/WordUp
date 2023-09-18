using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WordUp.Service.Contracts.LearnGameData;
using WordUp.Service.Contracts.Word;
using WordUp.Service.Word;
using WordUp.Shared.StaticShared;
using WordUp.Views.LearnGameCompleteView;
using Zenject;

namespace WordUp.Views.LearnGameView
{
    public class LearnGameController : SceneControllerBase
    {
        [SerializeField] private LearnGameCard cardPrefab;

        [Inject] private IWordService _wordService;

        private object _data;
        
        private LearnGameCompleteData _completeData;
        private List<WordDto> _words;
        private LearnGameData _gameData;

        protected override object GetDataFromScene()
        {
            return _data;
        }

        protected override void LoadDataFromScene(object data)
        {
            UIHelper.DestroyAllChildren(transform);
            
            if (data is bool again)
            {
                if (again)
                {
                    _completeData.Clear();
                    UpdateWordsByService();
                    CreateTest();
                }
                else
                {
                    UnloadScene();
                }
            }
            else if (data is LearnGameData gameData)
            {
                _gameData = gameData;
                _words = gameData.Words;

                UpdateWordsByGameData();
                CreateTest();
            }
            else
            {
                throw new ArgumentException(message: nameof(data));    
            }
        }

        private void CreateTest()
        {
            _completeData.maxCount = _words.Count;

            for (int i = _words.Count - 1; i >= 0; i--)
            {
                LearnGameCard card = UIHelper.CreateInstantiate(cardPrefab, transform);

                card.gameObject.name = $"Card_{i + 1}";

                card.Data = new LearnGameCardData
                {
                    Word = _words[i],
                    MaxNumber = _words.Count,
                    Number = i + 1,
                    SourceLanguage = _gameData.SourceLanguage,
                    EndClickAction = OnCompleteTest,
                    BackClickAction = UnloadScene,
                    SwipeAction = OnSwipe
                };
            }
        }

        private void UpdateWordsByService()
        {
            List<Guid> wordsGuids = _words.Select(x => x.Guid).ToList();

            _words.Clear();
            _words = _wordService.GetModelsByGuids(wordsGuids).ToList();
            
            UpdateWordsByGameData();
        }

        private void UpdateWordsByGameData()
        {
            if (_gameData.Shuffle)
            {
                CollectionHelpers.ShuffleCollection(_words);
            }

            if (_gameData.MaxWords != null)
            {
                _words = _words.Take((int)_gameData.MaxWords).ToList();
            }
        }

        private void OnSwipe(WordDto modifiedWord, bool successSwipe)
        {
            var savedWord = _wordService.GetModel(modifiedWord.Guid);
            
            bool wordChanged =
                savedWord.IsHard != modifiedWord.IsHard ||
                savedWord.IsLearned != modifiedWord.IsLearned;

            if (wordChanged)
            {
                _wordService.CreateOrUpdate(modifiedWord);   
            }
            
            if (successSwipe)
            {
                _completeData.successCount++;
            }
        }

        private void OnCompleteTest()
        {
            _data = _completeData;
            
            LoadSceneAdditive((int)SceneNumber.LearnGameCompleteView);
        }
        
        private void UnloadScene()
        {
            _data = _words;
            
            UnloadScene((int)SceneNumber.LearnGameView);
        }
    }
}