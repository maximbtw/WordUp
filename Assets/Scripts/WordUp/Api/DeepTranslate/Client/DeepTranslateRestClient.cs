using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Unity.Services.RemoteConfig;

namespace WordUp.Api.DeepTranslate.Client
{
    public class DeepTranslateRestClient : IDeepTranslateRestClient
    {
        private static readonly RestClient RestClient = new(baseUrl: "https://deep-translate1.p.rapidapi.com");

        private const string ApplicationJsonMediaType = "application/json";

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string resource, TRequest request)
        {
            try
            {
                IRestRequest restRequest = new RestRequest(resource, Method.POST);

                string apiKey = RemoteConfigService.Instance.appConfig.GetString("DeepTranslateApiKey");
                string apiHost = RemoteConfigService.Instance.appConfig.GetString("DeepTranslateHost");
                
                restRequest.AddHeader("content-type", value: ApplicationJsonMediaType);
                restRequest.AddHeader("X-RapidAPI-Key", value: apiKey);
                restRequest.AddHeader("X-RapidAPI-Host", value: apiHost);

                string jsonRequest = JsonConvert.SerializeObject(request);

                restRequest.AddJsonBody(jsonRequest);

                IRestResponse response = await RestClient.ExecuteAsync(restRequest);

                string error;

                if (!string.IsNullOrEmpty(response.ErrorMessage))
                {
                    error = $"Operation completed with error: {response.ErrorMessage}";
                }
                else if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
                {
                    error = $"Invalid response status: {response.StatusCode}";
                }
                else
                {
                    return JsonConvert.DeserializeObject<TResponse>(response.Content);
                }

                throw new Exception(error);
            }
            catch (Exception exception)
            {
                string error = $"Exception ({exception.GetType().Name}): {exception.Message}";

                throw new Exception(error);
            }
        }
    }
}