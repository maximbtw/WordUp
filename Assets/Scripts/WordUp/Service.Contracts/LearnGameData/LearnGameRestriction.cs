using System;
using WordUp.Shared.StaticShared;

namespace WordUp.Service.Contracts.LearnGameData
{
    public class LearnGameRestriction : IModelDto
    {
        public Guid Guid { get; set; }
        
        public bool OnlyHard { get; set; }
        
        public bool OnlyUnlearned { get; set; }
        
        public int? WordsCount { get; set; }
    }
}