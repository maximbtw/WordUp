using System.Collections.Generic;
using WordUp.Shared.StaticShared;

namespace WordUp.Service.Contracts.Word
{
    public class WordDtoValidator : ValidatorModelBase<WordDto>
    {
        protected override void CustomValidate(WordDto model, ref List<Issue> issues)
        {
            if (string.IsNullOrEmpty(model.NameRu))
            {
                AddError(ref issues, "Russian name must be provided");
            }
            else if (LanguageHelpers.GetLanguageByString(model.NameRu) != Language.Russian)
            {
                AddError(ref issues, "The word in Russian should be in Russian");
            }
            
            if (string.IsNullOrEmpty(model.NameEn))
            {
                AddError(ref issues, "English name must be provided");
            }
            else if (LanguageHelpers.GetLanguageByString(model.NameEn) != Language.English)
            {
                AddError(ref issues, "The word in English should be in English");
            }
        }
    }
}