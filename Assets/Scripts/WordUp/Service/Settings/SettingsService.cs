using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WordUp.Service.Contracts.Settings;
using Zenject;

namespace WordUp.Service.Settings
{
    public class SettingsService : CachedEntityServiceBase<SettingsDto>, ISettingsService
    {
        [Inject]
        public SettingsService()
        {
            bool settingsExist = this.DataService.Load().Any();
            if (!settingsExist)
            {
                DataService.Save(new List<SettingsDto>(new[]
                {
                    new SettingsDto
                    {
                        Guid = Guid.NewGuid(),
                        SoundActive = false,
                        CountWordInGroup = 25,
                    }
                }));
            }
            
            Debug.Log("Settings service loaded");
        }
        
        public SettingsDto GetModel()
        {
            return this.Cache.First();
        }

        public void Update(SettingsDto model)
        {
            SettingsDto savedModel = GetModel();

            savedModel.SoundActive = model.SoundActive;
            savedModel.CountWordInGroup = model.CountWordInGroup;
            
            Save();
        }
    }
}