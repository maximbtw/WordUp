using System.Threading.Tasks;
using WordUp.Api.Contracts.Translate;
using WordUp.Api.Contracts.TranslateMany;
using WordUp.Api.DeepTranslate;
using WordUp.Api.DeepTranslate.Connector;
using Zenject;

namespace WordUp.Api
{
    public class ExternalSystemHandler : IExternalSystemHandler
    {
        private readonly IDeepTranslate _deepTranslateHandler;

        [Inject]
        public ExternalSystemHandler(IDeepTranslateConnector deepTranslateRestConnector)
        {
            _deepTranslateHandler = new DeepTranslateHandler(deepTranslateRestConnector);
        }

        public async Task<TranslateResponse> Translate(TranslateRequest request) =>
            await _deepTranslateHandler.Translate(request);

        public async Task<TranslateManyResponse> TranslateMany(TranslateManyRequest request) =>
            await _deepTranslateHandler.TranslateMany(request);
    }
}