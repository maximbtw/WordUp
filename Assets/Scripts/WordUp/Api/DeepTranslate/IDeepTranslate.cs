using System.Threading.Tasks;
using WordUp.Api.Contracts.Translate;
using WordUp.Api.Contracts.TranslateMany;

namespace WordUp.Api.DeepTranslate
{
    public interface IDeepTranslate
    {
        Task<TranslateResponse> Translate(TranslateRequest request);
        
        Task<TranslateManyResponse> TranslateMany(TranslateManyRequest request);
    }
}