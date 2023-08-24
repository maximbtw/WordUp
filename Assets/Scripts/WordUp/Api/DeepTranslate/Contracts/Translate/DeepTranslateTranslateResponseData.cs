using Newtonsoft.Json;

namespace WordUp.Api.DeepTranslate.Contracts.Translate
{
    public class DeepTranslateTranslateResponseData
    {
        [JsonProperty("translations")]
        public DeepTranslateTranslateResponseDataTranslations Translations { get; set; }
    }
}