using UnityEngine;
using WordUp.Api;
using WordUp.Api.Contracts.Translate;
using Zenject;

namespace WordUp
{
    public class TestZenject : MonoBehaviour
    {
        [Inject] private IExternalSystemHandler _externalSystemHandler;

        public async void Translate()
        {
            var request = new TranslateRequest
            {
                SourceLanguage = "en",
                TargetLanguage = "ru",
                Text = "Text"
            };

            TranslateResponse response = await _externalSystemHandler.Translate(request);
            
            Debug.Log(response.TranslatedText);
        }
    }
}