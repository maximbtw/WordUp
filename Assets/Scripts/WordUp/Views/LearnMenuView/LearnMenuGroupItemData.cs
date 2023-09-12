using System;
using System.Collections.Generic;
using WordUp.Service.Contracts.Word;

namespace WordUp.Views.LearnMenuView
{
    [Serializable]
    public struct LearnMenuGroupItemData
    {
        public static LearnMenuGroupItemData DefaultLearnMenuGroupItem;
        
        public int number;
        public int count;
        public int learnedCount;
        
        public List<WordDto> Words { get; set; }
        
        static LearnMenuGroupItemData()
        {
            DefaultLearnMenuGroupItem = new LearnMenuGroupItemData
            {
                number = 1,
                count = 25,
                learnedCount = 0
            };
        }
    }
}