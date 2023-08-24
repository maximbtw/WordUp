using System.Threading.Tasks;

namespace WordUp.Api.DeepTranslate.Client
{
    public interface IDeepTranslateRestClient
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(string resource, TRequest request);
    }
}