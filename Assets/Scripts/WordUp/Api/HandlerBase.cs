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

                return result;
            }
            catch (Exception exception)
            {
                var response = new TResult
                {
                    IsSuccess = true,
                    Error = exception.Message
                };

                return response;
            }
        }
    }
}