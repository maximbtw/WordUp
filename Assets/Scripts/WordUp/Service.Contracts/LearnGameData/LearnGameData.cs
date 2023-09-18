using System.Collections.Generic;
using WordUp.Service.Contracts.Word;
using WordUp.Shared.StaticShared;

namespace WordUp.Service.Contracts.LearnGameData
{
    public class LearnGameData
    {
        public bool Shuffle { get; set; }
        
        public Language SourceLanguage { get; set; }
        
        public int? MaxWords { get; set; }

        public List<WordDto> Words { get; set; } = new();
    }
}