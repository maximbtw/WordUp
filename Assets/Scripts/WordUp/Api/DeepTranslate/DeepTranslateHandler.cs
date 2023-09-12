using System.Linq;
using System.Threading.Tasks;
using WordUp.Api.Contracts.Translate;
using WordUp.Api.Contracts.TranslateMany;
using WordUp.Api.DeepTranslate.Connector;
using WordUp.Api.DeepTranslate.Contracts.Translate;
using WordUp.Shared;
using WordUp.Shared.StaticShared;

namespace WordUp.Api.DeepTranslate
{
    public class DeepTranslateHandler : HandlerBase, IDeepTranslate
    {
        private readonly IDeepTranslateConnector _connector;

        public DeepTranslateHandler(IDeepTranslateConnector connector)
        {
            _connector = connector;
        }

        public async Task<TranslateResponse> Translate(TranslateRequest request)
        {
            var apiRequest = new DeepTranslateTranslateRequest()
            {
                Text = request.Text,
                SourceLanguage = request.SourceLanguage,
                TargetLanguage = request.TargetLanguage
            };

            return await OperationInvoke(async () =>
            {
                var apiResponse = await _connector.Translate(apiRequest);

                var response = new TranslateResponse
                {
                    TranslatedText = apiResponse.Data.Translations.TranslatedText
                };

                return response;
            });
        }

        public async Task<TranslateManyResponse> TranslateMany(TranslateManyRequest request)
        {
            var response = new TranslateManyResponse();

            string text = CollectionHelpers.JoinToString(request.SourceText, "/");

            var translateRequest = new TranslateRequest
            {
                SourceLanguage = request.SourceLanguage,
                TargetLanguage = request.TargetLanguage,
                Text = text
            };

            var translateResponse = await Translate(translateRequest);

            if (translateResponse.IsSuccess)
            {
                string[] translatedArray = translateResponse.TranslatedText.Split('/');

                response.TranslatedItems = translatedArray.Select((x, index) =>
                    new TranslateManyResponseItem
                    {
                        TextInSourceLanguage = request.SourceText[index],
                        TextInTargetLanguage = x
                    }).ToList();
            }

            response.IsSuccess = false;
            response.Error = translateResponse.Error;

            return response;
        }
    }
}