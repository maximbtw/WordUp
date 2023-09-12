using System.Collections.Generic;

namespace WordUp.Service
{
    public interface IDataService<TModel> 
    {
        void Save(ICollection<TModel> models);
        
        ICollection<TModel> Load();
    }
}