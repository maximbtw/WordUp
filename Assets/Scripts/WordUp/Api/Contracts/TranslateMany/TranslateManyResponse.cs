using System.Collections.Generic;

namespace WordUp.Api.Contracts.TranslateMany
{
    public class TranslateManyResponse : IApiOperationResult
    {
        public List<TranslateManyResponseItem> TranslatedItems { get; set; }
        
        public bool IsSuccess { get; set; }
        
        public string Error { get; set; }
    }
}