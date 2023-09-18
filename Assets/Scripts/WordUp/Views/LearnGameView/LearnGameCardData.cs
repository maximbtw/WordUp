using UnityEngine.Events;
using WordUp.Service.Contracts.Word;
using WordUp.Service.Word;
using WordUp.Shared.StaticShared;

namespace WordUp.Views.LearnGameView
{
    public class LearnGameCardData
    {
        public WordDto Word { get; set; }
        
        public Language SourceLanguage { get; set; }
        
        public int Number { get; set; }
        
        public int MaxNumber { get; set; }

        public string GetSourceName => this.SourceLanguage == Language.Russian
            ? this.Word.NameRu
            : this.Word.NameEn;
        
        public string GetTargetName => this.SourceLanguage == Language.Russian
            ? this.Word.NameEn
            : this.Word.NameRu;
        
        public UnityAction<WordDto, bool> SwipeAction { get; set; }
        
        public UnityAction EndClickAction { get; set; }
        
        public UnityAction BackClickAction { get; set; }
    }
}