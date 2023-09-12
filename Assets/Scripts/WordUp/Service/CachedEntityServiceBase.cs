using System.Collections.Generic;
using System.Linq;
using WordUp.Service.Contracts;

namespace WordUp.Service
{
    public abstract class CachedEntityServiceBase<TModel> where TModel : IModelDto
    {
        protected readonly HashSet<TModel> Cache;
        
        protected readonly IDataService<TModel> DataService = new DataService<TModel>();

        protected CachedEntityServiceBase()
        {
            ICollection<TModel> models = DataService.Load();
            
            Cache = models.ToHashSet();
        }

        protected void Save()
        {
            DataService.Save(Cache);
        }
    }
}