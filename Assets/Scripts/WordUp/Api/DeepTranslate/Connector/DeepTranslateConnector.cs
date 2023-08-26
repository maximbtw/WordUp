using System.Threading.Tasks;
using WordUp.Api.DeepTranslate.Client;
using WordUp.Api.DeepTranslate.Contracts.Translate;
using Zenject;

namespace WordUp.Api.DeepTranslate.Connector
{
    public class DeepTranslateConnector : IDeepTranslateConnector
    {
        private const string TranslateResource = "/language/translate/v2";
        
        private readonly IDeepTranslateRestClient _restClient;

        [Inject]
        public DeepTranslateConnector(IDeepTranslateRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<DeepTranslateTranslateResponse> Translate(DeepTranslateTranslateRequest request)
        {
            var response =
                await _restClient.PostAsync<DeepTranslateTranslateRequest, DeepTranslateTranslateResponse>(
                    TranslateResource, request);

            return response;
        }
    }
}