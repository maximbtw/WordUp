using System;
using System.Collections.Generic;
using System.Linq;
using WordUp.Service.Contracts.Word;

namespace WordUp.Service.Contracts.LearnGameData
{
    public class LearnGameRestrictionValidator : ValidatorModelBase<LearnGameRestriction>
    {
        private readonly Func<List<WordDto>> _getWords;

        public LearnGameRestrictionValidator(Func<List<WordDto>> getWords)
        {
            _getWords = getWords;
        }
        
        protected override void CustomValidate(LearnGameRestriction model, ref List<Issue> issues)
        {
            List<WordDto> words = _getWords();
            
            if (model.WordsCount != null)
            {
                bool wordsCountNegativeOrZero = model.WordsCount <= 1;

                if (wordsCountNegativeOrZero)
                {
                    AddError(ref issues, "The word count must be greater than zero.");
                    
                    return;
                }

                if (words.Count < model.WordsCount)
                {
                    AddWarning(ref issues,
                        $"Not find the right number of words to fit the given constraints. " +
                        $"There are words: {words.Count}. Continue?");
                }
            }
            
            if (model.OnlyHard)
            {
                bool hasAnyHardWord = words.Any(x => x.IsHard);

                if (!hasAnyHardWord)
                {
                    AddError(ref issues, "There's not a single hard word.");
                }
            }

            if (model.OnlyUnlearned)
            {
                bool hasAnyUnlearnedWord = words.Any(x => !x.IsLearned);

                if (!hasAnyUnlearnedWord)
                {
                    AddError(ref issues, "All words marked as learned.");
                }
            }

            if (model.OnlyHard && model.OnlyUnlearned)
            {
                bool matchedWords = words.Any(x => !x.IsLearned && x.IsHard);
                
                if (!matchedWords)
                {
                    AddError(ref issues, "There were no words to match the settings.");
                }
            }
        }
    }
}