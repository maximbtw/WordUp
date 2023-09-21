using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WordUp.Service.Contracts.Word;
using Zenject;

namespace WordUp.Service.Word
{
    public class WordService : CachedEntityServiceBase<WordDto>, IWordService
    {
        [Inject]
        public WordService()
        {
            Debug.Log("Word service loaded");
        }
        
        public WordDto GetModel(Guid guid)
        {
            return this.Cache.FirstOrDefault(x => x.Guid == guid);
        }

        public IEnumerable<WordDto> GetModels()
        {
            return this.Cache.OrderBy(x => x.NameEn);
        }

        public bool Delete(Guid guid)
        {
            WordDto model = GetModel(guid);

            if (model == null)
            {
                return false;
            }

            this.Cache.RemoveWhere(x => x.Guid == model.Guid);
            
            Save();

            return true;
        }

        public void DeleteAll()
        {
            this.Cache.Clear();
            
            Save();
        }

        public void CreateOrUpdate(WordDto model)
        {
            WordDto savedModel = GetModel(model.Guid);

            if (savedModel == null)
            {
                model.Guid = Guid.NewGuid();
                
                this.Cache.Add(model);
            }
            else
            {
                savedModel.IsLearned = model.IsLearned;
                savedModel.IsHard = model.IsHard;
                savedModel.NameEn = model.NameEn;
                savedModel.NameRu = model.NameRu;
            }
            
            Save();
        }

        public void CreateMany(ICollection<WordDto> models)
        {
            foreach (WordDto model in models)
            {
                model.Guid = Guid.NewGuid();
                
                this.Cache.Add(model);
            }
            
            Save();
        }

        public IEnumerable<WordDto> GetModelsByGuids(IEnumerable<Guid> guids)
        {
            return GetModels().Where(x => guids.Contains(x.Guid));
        }
    }
}