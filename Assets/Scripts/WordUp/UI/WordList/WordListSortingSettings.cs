using System;

namespace WordUp.UI.WordList
{
    [Serializable]
    public struct WordListSortingSettings
    {
        public bool showOnlyLearned;
        public bool showOnlyHard;
        public string text;
    }
}