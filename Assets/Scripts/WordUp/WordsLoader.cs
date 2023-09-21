using System;
using System.Collections.Generic;
using UnityEngine;
using WordUp.Service.Contracts.Word;
using WordUp.Service.Word;
using WordUp.Shared.StaticShared;

namespace WordUp
{
    public class WordsLoader
    {
        private readonly IWordService _wordService;
        
        public WordsLoader(IWordService wordService)
        {
            _wordService = wordService;
        }
        
        public void LoadWords()
        {
            var models = new List<WordDto>();

            string[] initWordsData = Resources.Load("InitialWordsData").ToString().Split("\r\n");

            foreach (string line in initWordsData)
            {
                string[] arg = line.Split(",");

                if (arg.Length != 2)
                {
                    throw new Exception($"Incorrect format: {line}");
                }

                models.Add(CreateWord(arg[0], arg[1]));
            }

            _wordService.CreateMany(models);
        }

        private WordDto CreateWord(string nameEn, string nameRu)
        {
            if (LanguageHelpers.GetLanguageByString(nameEn) != Language.English)
            {
                throw new AggregateException(nameof(nameEn));
            }

            if (LanguageHelpers.GetLanguageByString(nameRu) != Language.Russian)
            {
                throw new AggregateException(nameof(nameRu));
            }

            return new WordDto
            {
                NameEn = nameEn,
                NameRu = nameRu
            };
        }
    }
}