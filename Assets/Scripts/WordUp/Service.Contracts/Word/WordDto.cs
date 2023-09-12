using System;
using System.ComponentModel;

namespace WordUp.Service.Contracts.Word
{
    [DisplayName("Items")]
    public class WordDto : IModelDto
    {
        public static WordDto DefaultWord = new()
        {
            NameEn = "Example",
            NameRu = "Пример",
            Guid = new Guid()
        };
        
        public Guid Guid { get; set; }
        
        public string NameEn { get; set; }
        
        public string NameRu { get; set; }
        
        public bool IsLearned { get; set; }
        
        public bool IsHard { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.NameEn))
            {
                return this.NameRu;
            }

            if (string.IsNullOrEmpty(this.NameRu))
            {
                return this.NameEn;
            }

            return $"{this.NameEn}  /  {this.NameRu}";
        }
    }
}