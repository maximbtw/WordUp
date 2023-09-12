using System;
using System.Collections.Generic;
using WordUp.Service.Contracts.Word;

namespace WordUp.Service.Word
{
    public interface IWordService
    {
        WordDto GetModel(Guid guid);
        
        IEnumerable<WordDto> GetModels();
        
        bool Delete(Guid guid);
        
        void CreateOrUpdate(WordDto model);
    }
}