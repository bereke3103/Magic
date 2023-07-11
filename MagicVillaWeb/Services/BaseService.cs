using MagicVillaWeb.Models;
using MagicVillaWeb.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using static MagicVilla_Utility.SD;

namespace MagicVillaWeb.Services
{
    public class BaseService : IBaseServices
    {
        public APIResponse responseModel { get; set ; }
        public IHttpClientFactory httpClient { get; set; }   

        public BaseService(IHttpClientFactory httpClient)
        {
            responseModel = new APIResponse();
            this.httpClient = httpClient;

        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {

            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Content-Type", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                switch (apiRequest.ApiType)
                {
                    case ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Delete;
                        break;
                }

                HttpResponseMessage apiResponse = null;

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);

                return APIResponse;

            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    isSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIReponse = JsonConvert.DeserializeObject<T>(res);
                return APIReponse;
            }
            
        }
    }
}
