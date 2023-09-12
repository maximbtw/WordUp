using WordUp.Shared.StaticShared;

namespace WordUp.Views.LearnGroupView
{
    public class LearnGroupSettingsData
    {
        public bool OnlyHard { get; set; }
        
        public bool OnlyUnlearned { get; set; }
        
        public bool Sort { get; set; }
        
        public LanguageHelpers.Language ShowLanguage { get; set; }
    }
}