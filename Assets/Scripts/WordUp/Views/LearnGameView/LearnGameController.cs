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
        private int _indexCard;

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
                    RefreshTest();
                }
                else
                {
                    UnloadScene((int)SceneNumber.LearnGameView);
                }
            }
            else if (data is LearnGameData gameData)
            {
                InitTestFromGameData(gameData);
            }
            else
            {
                throw new ArgumentException(message: nameof(data));    
            }
        }

        private void RefreshTest()
        {
            _completeData.Clear();
            _completeData.maxCount = _gameData.MaxWords;
            _indexCard = 0;
            
            UpdateWordsByService();
            CreateTest();
        }

        private void InitTestFromGameData(LearnGameData gameData)
        {
            _gameData = gameData;
            _words = gameData.Words;
            _completeData.maxCount = gameData.MaxWords;

            UpdateWordsByGameData();
            CreateTest();
        }

        private void CreateTest()
        {
            AddCard();
            AddCard();
        }

        private void AddCard()
        {
            if (_indexCard == _words.Count)
            {
                return;
            }

            var currentWord = _words[_indexCard];
            CreateCard(currentWord, _indexCard + 1);

            _indexCard++;
        }

        private void CreateCard(WordDto word, int number)
        {
            LearnGameCard card = UIHelper.CreateInstantiate(cardPrefab, transform);

            card.gameObject.name = $"Card_{number}";

            card.Data = new LearnGameCardData
            {
                Word = word,
                MaxNumber = _words.Count,
                Number = number,
                SourceLanguage = _gameData.SourceLanguage,
                EndClickAction = OnCompleteTest,
                BackClickAction = () => UnloadScene((int)SceneNumber.LearnGameView),
                SwipeAction = OnSwipe
            };
            
            card.transform.SetSiblingIndex(0);
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

            _words = _words.Take(_gameData.MaxWords).ToList();
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

            AddCard();
        }

        private void OnCompleteTest()
        {
            _data = _completeData;
            
            LoadSceneAdditive((int)SceneNumber.LearnGameCompleteView);
        }
    }
}