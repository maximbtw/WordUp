using Newtonsoft.Json;

namespace WordUp.Api.DeepTranslate.Contracts.Translate
{
    public class DeepTranslateTranslateRequest
    {
        [JsonProperty("q")]
        public string Text { get; set; }
        
        [JsonProperty("source")]
        public string SourceLanguage { get; set; }
        
        [JsonProperty("target")]
        public string TargetLanguage { get; set; }
    }
}