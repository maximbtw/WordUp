using System.Threading.Tasks;
using WordUp.Api.DeepTranslate.Contracts.Translate;

namespace WordUp.Api.DeepTranslate.Connector
{
    public interface IDeepTranslateConnector
    {
        Task<DeepTranslateTranslateResponse> Translate(DeepTranslateTranslateRequest request);
    }
}