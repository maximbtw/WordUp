using Newtonsoft.Json;

namespace WordUp.Api.DeepTranslate.Contracts.Translate
{
    public class DeepTranslateTranslateResponseDataTranslations
    {
        [JsonProperty("translatedText")]
        public string TranslatedText { get; set; }
    }
}