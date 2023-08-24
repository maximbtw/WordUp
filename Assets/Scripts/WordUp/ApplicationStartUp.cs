using Newtonsoft.Json;
using UnityEngine;
using WordUp.Api;
using WordUp.Api.Contracts.Translate;
using WordUp.Api.DeepTranslate.Client;
using WordUp.Api.DeepTranslate.Connector;

namespace WordUp
{
    public class ApplicationStartUp : MonoBehaviour
    {
        private void Start() => Run();

        private async void Run()
        {
            //IUnityContainer container = new UnityContainer();
            
            //container.RegisterType<IDeepTranslateRestClient, DeepTranslateRestClient>();
            //container.RegisterType<IDeepTranslateConnector, DeepTranslateConnector>();
            //container.Configure
            
           // using StreamReader reader = new StreamReader("appsettings.json");
            
            //string jsonString = reader.ReadToEnd();

            //Configuration configuration = JsonSerializer.Deserialize<Configuration>(jsonString);


            IDeepTranslateRestClient deepTranslateRestClient = new DeepTranslateRestClient();
            IDeepTranslateConnector deepTranslateConnector = new DeepTranslateConnector(deepTranslateRestClient);
            IExternalSystemHandler externalSystemHandler = new ExternalSystemHandler(deepTranslateConnector);

            var request = new TranslateRequest
            {
                SourceLanguage = "en",
                TargetLanguage = "ru",
                Text = "Text"
            };

            TranslateResponse response = await externalSystemHandler.Translate(request);
            
            Debug.Log(response.TranslatedText);
        }
    }
}