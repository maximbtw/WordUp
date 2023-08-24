using System.Collections.Generic;

namespace WordUp.Api.Contracts.TranslateMany
{
    public class TranslateManyRequest
    {
        public List<string> SourceText { get; set; }
        
        public string SourceLanguage { get; set; }
        
        public string TargetLanguage { get; set; }
    }
}