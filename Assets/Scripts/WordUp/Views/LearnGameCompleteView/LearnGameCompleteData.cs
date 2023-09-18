using System;

namespace WordUp.Views.LearnGameCompleteView
{
    [Serializable]
    public struct LearnGameCompleteData
    {
        public int successCount;
        
        public int maxCount;

        public void Clear()
        {
            successCount = 0;
            maxCount = 0;
        }
    }
}