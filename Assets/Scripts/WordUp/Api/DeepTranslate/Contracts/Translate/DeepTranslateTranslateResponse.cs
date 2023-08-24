using Newtonsoft.Json;

namespace WordUp.Api.DeepTranslate.Contracts.Translate
{
    public class DeepTranslateTranslateResponse
    {
        [JsonProperty("data")]
        public DeepTranslateTranslateResponseData Data { get; set; }
    }
}