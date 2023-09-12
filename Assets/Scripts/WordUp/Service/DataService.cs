using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using WordUp.Service.Contracts;

namespace WordUp.Service
{
    public class DataService<TModel> : IDataService<TModel> where TModel : IModelDto
    {
        private readonly string _modelTableName;

        public DataService()
        {
            var nameAttribute = typeof(TModel).GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Cast<DisplayNameAttribute>().Single();
            
            _modelTableName = nameAttribute.DisplayName;
        }
        
        public void Save(ICollection<TModel> models)
        {
            string jsonValue = JsonConvert.SerializeObject(models);

            PlayerPrefs.SetString(_modelTableName, jsonValue);
            PlayerPrefs.Save();
        }

        public ICollection<TModel> Load()
        {
            string jsonValue = PlayerPrefs.GetString(_modelTableName);

            var models = JsonConvert.DeserializeObject<ICollection<TModel>>(jsonValue);

            return models ?? new HashSet<TModel>();
        }
    }
}