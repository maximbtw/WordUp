using System;
using System.ComponentModel;

namespace WordUp.Service.Contracts.Settings
{
    [DisplayName("Settings")]
    [Serializable]
    public class SettingsDto : IModelDto
    {
        public Guid Guid { get; set; }
        
        public int CountWordInGroup { get; set; }
        
        public bool SoundActive { get; set; }
    }
}