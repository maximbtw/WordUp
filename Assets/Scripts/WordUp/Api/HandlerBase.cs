using System;
using System.Threading.Tasks;

namespace WordUp.Api
{
    public abstract class HandlerBase
    {
        protected async Task<TResult> OperationInvoke<TResult>(Func<Task<TResult>> operation)
            where TResult : IApiOperationResult, new()
        {
            try
            {
                TResult result = await operation.Invoke();
                
                result.IsSuccess = true;

                return result;
            }
            catch (Exception exception)
            {
                var response = new TResult
                {
                    IsSuccess = false,
                    Error = exception.Message
                };

                return response;
            }
        }
    }
}